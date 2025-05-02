using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class SeatingChartForm : Form
    {
        private MainForm mainForm;
        private string movieTitle;
        private DateTime showtime;
        private HashSet<string> reservedSeats;
        private Dictionary<string, string> selectedSeats = new Dictionary<string, string>();
        private HashSet<string> premiumSeatsSet;
        private DateTime reservationDate; // *** NEW Field ***
       

        public SeatingChartForm(MainForm mainForm, string movieTitle, DateTime showtime, DateTime reservationDate)
        {
            this.mainForm = mainForm;
            this.movieTitle = movieTitle;
            this.showtime = showtime;
            this.reservationDate = reservationDate;

            InitializeComponent();
            SetDynamicValues();
            GenerateSeatLayout(showtime, GlobalVariable.getCurrentMovie(), GlobalVariable.getCurrentHallId());
            CreateLegend(); // Add the legend
        }

        private void SetDynamicValues()
        {
            lblTitle.Text = $"{movieTitle} - {showtime} - {"Hall " + GlobalVariable.getCurrentHallId()}";
        }


        private async Task GenerateSeatLayout(DateTime showtime, int movieID, int hallID)
        {
            seatLayout.Controls.Clear();

            var dbManager = new DatabaseManager();
            var allSeats = dbManager.GetSeatsByHallCombined(hallID);
            var reservedSeats = dbManager.GetReservedSeatsCombined(showtime, hallID, movieID);

            var premiumSeatsSet = allSeats
                .Where(s => s.Value.Equals("Premium", StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Key)
                .ToHashSet();

            // Get max row and column from seat IDs
            var seatIds = allSeats.Keys.ToList();
            int maxRow = seatIds.Max(id => id[0] - 'A'); // e.g. 'H' - 'A' = 7
            int maxCol = seatIds.Max(id => int.Parse(id.Substring(1))) - 1;

            seatLayout.RowCount = maxRow + 1;
            seatLayout.ColumnCount = maxCol + 1;

            for (int row = 0; row <= maxRow; row++)
            {
                for (int col = 0; col <= maxCol; col++)
                {
                    string seatId = $"{(char)('A' + row)}{col + 1}";

                    if (!allSeats.ContainsKey(seatId))
                        continue; // Skip if seat doesn't exist in DB

                    var btn = new Button { Text = seatId, Tag = seatId };

                    if (reservedSeats.Contains(seatId))
                    {
                        StyleAsReserved(btn);
                    }
                    else if (premiumSeatsSet.Contains(seatId))
                    {
                        StyleAsPremium(btn);
                        btn.Click += (s, e) => PremiumSeatButton_Click(s, e, seatId, btn);
                    }
                    else
                    {
                        StyleAsAvailable(btn);
                        btn.Click += (s, e) => SeatButton_Click(s, e);
                    }

                    seatLayout.Controls.Add(btn, col, row);
                }
            }
        }



        private void StyleAsPremium(Button btn)
        {
            btn.BackColor = Color.Gold;
            btn.ForeColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.DarkGoldenrod;
            btn.FlatAppearance.BorderSize = 2;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(2);
        }

        private void StyleAsReserved(Button btn)
        {
            btn.BackColor = Color.DarkRed;
            btn.ForeColor = Color.WhiteSmoke;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Red;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Strikeout);
            btn.Enabled = false;
            btn.Cursor = Cursors.No;
            btn.Margin = new Padding(2);
        }

        private void StyleAsAvailable(Button btn)
        {
            btn.BackColor = Color.FromArgb(60, 60, 75); // Dark slate gray
            btn.ForeColor = Color.WhiteSmoke;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(2);
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
                selectedSeats.Add(seatId, "Premium");
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
                    selectedSeats.Add(seatId, "Standard");
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

            TicketConfirmationForm ticketConfirmationForm = new TicketConfirmationForm(mainForm, movieTitle, showtime, reservationDate, selectedSeats);
            mainForm.OpenChildForm(ticketConfirmationForm);
            // === BACKEND PLACEHOLDER ===
            MessageBox.Show($"Seats selected: {string.Join(", ", selectedSeats)}", "Continue");
        }
    }
}