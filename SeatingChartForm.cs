using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class SeatingChartForm : Form
    {
        private MainForm mainForm;
        private string movieTitle;
        private string showtime;
        private HashSet<string> reservedSeats;
        private List<string> selectedSeats = new List<string>();
        private HashSet<string> premiumSeatsSet;

        public SeatingChartForm(MainForm mainForm, string movieTitle, string showtime)
        {
            this.mainForm = mainForm;
            this.movieTitle = movieTitle;
            this.showtime = showtime;

            InitializeComponent();
            SetDynamicValues();
            FetchReservedSeats();
            FetchPremiumSeats();
            GenerateSeatLayout();
            CreateLegend(); // Add the legend
        }

        private void SetDynamicValues()
        {
            lblTitle.Text = $"{movieTitle} - {showtime}";
        }

        private void FetchReservedSeats()
        {
            // === BACKEND PLACEHOLDER ===
            reservedSeats = new HashSet<string> { "B2", "B9", "C5", "D7" };
        }

        private void FetchPremiumSeats()
        {
            // === BACKEND PLACEHOLDER ===
            premiumSeatsSet = new HashSet<string>
            {
                "F1", "F2", "F3", "F4", "F5",
                "G1", "G2", "G3", "G4", "G5",
                "H1", "H2", "H3", "H4", "H5"
            };
        }

        private void GenerateSeatLayout()
        {
            seatLayout.Controls.Clear();
            seatLayout.RowCount = 8;
            seatLayout.ColumnCount = 10;
            seatLayout.ColumnStyles.Clear();
            seatLayout.RowStyles.Clear();

            for (int i = 0; i < seatLayout.ColumnCount; i++)
                seatLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            for (int i = 0; i < seatLayout.RowCount; i++)
                seatLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    string seatId = $"{(char)('A' + row)}{col + 1}";
                    Button btn = new Button
                    {
                        Text = seatId,
                        Width = 55,
                        Height = 55,
                        Margin = new Padding(2),
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderColor = Color.Gray }
                    };

                    // Highlight premium rows H, G, F in gold
                    if (premiumSeatsSet.Contains(seatId))
                    {
                        btn.BackColor = Color.Gold; // Gold for premium seats
                        btn.ForeColor = Color.Black; // Black text for better contrast
                        btn.Click += (s, e) => PremiumSeatButton_Click(s, e, seatId, btn);
                    }
                    else if (reservedSeats.Contains(seatId))
                    {
                        btn.BackColor = Color.DarkRed; // Dark red for reserved seats
                        btn.ForeColor = Color.White;
                        btn.Enabled = false;
                        btn.Cursor = Cursors.No;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(60, 60, 75); // Default gray for standard seats
                        btn.ForeColor = Color.White;
                        btn.Click += SeatButton_Click;
                    }

                    seatLayout.Controls.Add(btn, col, row);
                }
            }
        }

        private void CreateLegend()
        {
            // Create a panel for the legend
            FlowLayoutPanel legendPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(25, 25, 35), // Match the theme
                Padding = new Padding(10),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            // Add legend items
            legendPanel.Controls.Add(CreateLegendItem(Color.FromArgb(60, 60, 75), "Standard Seat"));
            legendPanel.Controls.Add(CreateLegendItem(Color.Gold, "Premium Seat"));
            legendPanel.Controls.Add(CreateLegendItem(Color.DarkRed, "Reserved Seat"));
            legendPanel.Controls.Add(CreateLegendItem(Color.SkyBlue, "Selected Seat"));

            // Add the legend panel to the form
            this.Controls.Add(legendPanel);
        }

        private Panel CreateLegendItem(Color color, string text)
        {
            Panel legendItem = new Panel
            {
                Width = 200, // Increased width for better spacing
                Height = 30,
                Margin = new Padding(5),
                BackColor = Color.Transparent // Ensure background is clear
            };

            // Color box
            Panel colorBox = new Panel
            {
                Width = 20,
                Height = 20,
                BackColor = color,
                Margin = new Padding(5, 5, 10, 5), // Space between color box and label
            };

            // Label
            Label label = new Label
            {
                Text = text,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 5, 0, 0) // Adjust for proper alignment
            };

            // Use a FlowLayoutPanel to avoid overlap
            FlowLayoutPanel legendContent = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false
            };
            legendContent.Controls.Add(colorBox);
            legendContent.Controls.Add(label);

            legendItem.Controls.Add(legendContent);

            return legendItem;
        }

        private void PremiumSeatButton_Click(object sender, EventArgs e, string seatId, Button btn)
        {
            if (btn.BackColor == Color.SkyBlue) // If the premium seat is selected
            {
                btn.BackColor = Color.Gold; // Revert to gold when unselected
                selectedSeats.Remove(seatId);
            }
            else
            {
                btn.BackColor = Color.SkyBlue; // Highlight in blue when selected
                selectedSeats.Add(seatId);
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string seatId = btn.Text;

                if (btn.BackColor == Color.SkyBlue)
                {
                    btn.BackColor = Color.FromArgb(60, 60, 75); // Default gray for standard seats
                    selectedSeats.Remove(seatId);
                }
                else
                {
                    btn.BackColor = Color.SkyBlue; // Highlight in blue when selected
                    selectedSeats.Add(seatId);
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            CustomerMovieListForm movieListForm = new CustomerMovieListForm(mainForm);
            mainForm.OpenChildForm(movieListForm);
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Please select at least one seat.", "No Seats Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // === BACKEND PLACEHOLDER ===
            MessageBox.Show($"Seats selected: {string.Join(", ", selectedSeats)}\n(Ticket screen not yet implemented)", "Continue");
        }
    }
}