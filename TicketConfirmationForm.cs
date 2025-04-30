using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class TicketConfirmationForm : Form
    {
        private string movieTitle;
        private string showtime;
        private List<string> selectedSeats;
        private MainForm mainForm;
        private const decimal UnitPrice = 80.00m;

        public TicketConfirmationForm(MainForm mainForm, string movieTitle, string showtime, List<string> selectedSeats)
        {
            this.mainForm = mainForm;
            this.movieTitle = movieTitle;
            this.showtime = showtime;
            this.selectedSeats = selectedSeats;

            InitializeComponent();

            contentPanel.Location = new Point(
             (this.ClientSize.Width - contentPanel.Width) / 2,
             (this.ClientSize.Height - contentPanel.Height) / 2
            );

            RenderReceipt();
            AttachEvents();
        }

        private void RenderReceipt()
        {

            string ticketId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            lblMovie.Text = $"🎬 Movie: {movieTitle}";
            lblShowtime.Text = $"🕒 Showtime: {showtime}";
            lblTicketId.Text = $"🧾 Ticket ID: {ticketId}";

            lstSeats.Items.Clear();
            foreach (string seat in selectedSeats)
                lstSeats.Items.Add(seat);

            lblUnitPrice.Text = $"🎟️ Price per seat: {UnitPrice:C2}";
            lblTotalPrice.Text = $"💰 Total: {(selectedSeats.Count * UnitPrice):C2}";
        }

        private void AttachEvents()
        {
            btnCancel.Click += (s, e) => { 
            
            SeatingChartForm seatingChartForm = new SeatingChartForm(mainForm, movieTitle, showtime);
            mainForm.OpenChildForm(seatingChartForm);
            
            
            };

            btnConfirm.Click += (s, e) =>
            {
                // === BACKEND PLACEHOLDER SEND RESERVATION TO DB ===
                MessageBox.Show("✅ Booking confirmed! Your ticket has been saved.\n(This is a placeholder)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CustomerMovieListForm movieListForm = new CustomerMovieListForm(mainForm);
                mainForm.OpenChildForm(movieListForm);
            };
        }
    }
}
