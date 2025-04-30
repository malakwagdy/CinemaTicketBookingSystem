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

        public SeatingChartForm(MainForm mainForm, string movieTitle, string showtime)
        {
            this.mainForm = mainForm;
            this.movieTitle = movieTitle;
            this.showtime = showtime;

            InitializeComponent();
            SetDynamicValues();
            FetchReservedSeats();
            GenerateSeatLayout();
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
                        ForeColor = Color.White,
                        FlatAppearance = { BorderColor = Color.Gray }
                    };

                    if (reservedSeats.Contains(seatId))
                    {
                        btn.BackColor = Color.DarkRed;
                        btn.Enabled = false;
                        btn.Cursor = Cursors.No;
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(60, 60, 75);
                        btn.Click += SeatButton_Click;
                    }

                    seatLayout.Controls.Add(btn, col, row);
                }
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string seatId = btn.Text;

                if (btn.BackColor == Color.SkyBlue)
                {
                    btn.BackColor = Color.FromArgb(60, 60, 75);
                    selectedSeats.Remove(seatId);
                }
                else
                {
                    btn.BackColor = Color.SkyBlue;
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

            TicketConfirmationForm ticketConfirmationForm = new TicketConfirmationForm(mainForm, movieTitle, showtime, selectedSeats);
            mainForm.OpenChildForm(ticketConfirmationForm); 
            // === BACKEND PLACEHOLDER ===
            MessageBox.Show($"Seats selected: {string.Join(", ", selectedSeats)}\n(Ticket screen not yet implemented)", "Continue");
        }
    }
}
