using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Needed for Any()
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class TicketConfirmationForm : Form
    {
        private MainForm mainForm;
        private string movieTitle;
        private string showtime;
        private DateTime reservationDate; // *** NEW Field ***
        private List<string> selectedSeats;
        private const decimal UnitPrice = 80.00m;
        private string ticketId;

        // *** MODIFIED Constructor Signature ***
        public TicketConfirmationForm(MainForm mainForm, string movieTitle, string showtime, DateTime reservationDate, List<string> selectedSeats)
        {
            InitializeComponent();

            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.movieTitle = movieTitle ?? throw new ArgumentNullException(nameof(movieTitle));
            this.showtime = showtime ?? throw new ArgumentNullException(nameof(showtime));
            this.reservationDate = reservationDate; // *** Store the date ***
            this.selectedSeats = selectedSeats ?? new List<string>(); // Ensure list exists

            RenderReceipt();
            AttachEvents();
        }

        private void RenderReceipt()
        {
            // Generate and store the ticket ID
            this.ticketId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // Populate Labels
            lblMovie.Text = $"🎬 Movie: {movieTitle}";
            lblShowtime.Text = $"🕒 Showtime: {showtime}";
            lblReservationDate.Text = $"📅 Date: {reservationDate:D}"; // *** Set Date Label text (Long Date format) ***
            lblTicketId.Text = $"🧾 Ticket ID: {ticketId}";

            // Populate Seats ListBox
            lstSeats.Items.Clear();
            if (selectedSeats != null && selectedSeats.Any())
            {
                foreach (string seat in selectedSeats)
                    lstSeats.Items.Add($"   • {seat}");
            }
            else
            {
                lstSeats.Items.Add("   No seats selected.");
            }


            // Calculate and Display Price
            decimal totalPrice = (selectedSeats?.Count ?? 0) * UnitPrice;
            lblUnitPrice.Text = $"🎟️ Price per seat: {UnitPrice:C2}";
            lblTotalPrice.Text = $"💰 Total: {totalPrice:C2}";
        }

        private void AttachEvents()
        {
            btnCancel.Click += (s, e) => {
                // Go back to seating chart, passing necessary info INCLUDING DATE
                if (mainForm != null)
                {
                    // Make sure SeatingChartForm constructor accepts the date
                    SeatingChartForm seatingChartForm = new SeatingChartForm(mainForm, movieTitle, showtime, reservationDate); // Pass date back
                    mainForm.OpenChildForm(seatingChartForm);
                }
                else
                {
                    MessageBox.Show("Navigation unavailable. MainForm reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            };

            btnConfirm.Click += (s, e) =>
            {
                // *** SAVE THE BOOKING (with Date) ***
                try
                {
                    // Ensure seats were actually selected before booking
                    if (selectedSeats == null || !selectedSeats.Any())
                    {
                        MessageBox.Show("Cannot confirm booking with no seats selected.", "Booking Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop booking process
                    }

                    decimal totalPrice = selectedSeats.Count * UnitPrice;
                    // *** Pass date to Booking constructor ***
                    Booking newBooking = new Booking(this.ticketId, this.movieTitle, this.showtime, this.reservationDate, this.selectedSeats, totalPrice);
                    BookingRepository.AddBooking(newBooking);

                    MessageBox.Show("✅ Booking confirmed! Your ticket has been saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Navigate back to the movie list form
                    CustomerMovieListForm movieListForm = new CustomerMovieListForm(mainForm);
                    mainForm.OpenChildForm(movieListForm);
                }
                catch (ArgumentException argEx) // Catch specific validation errors from Booking constructor
                {
                    MessageBox.Show($"Booking Error: {argEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred while saving the booking: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Optionally, stay on this form or navigate elsewhere on error
                }
            };
        }

        private void TicketConfirmationForm_Load(object sender, EventArgs e)
        {
            CenterContentPanel();
            // No style changes needed here - handled by OpenChildForm
        }

        private void CenterContentPanel()
        {
            if (contentPanel == null) return;
            int x = (this.ClientSize.Width - contentPanel.Width) / 2;
            int y = (this.ClientSize.Height - contentPanel.Height) / 2;
            contentPanel.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            contentPanel.Anchor = AnchorStyles.None;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterContentPanel();
        }
    }
}