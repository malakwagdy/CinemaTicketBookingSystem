using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Needed for Any()
using System.Windows.Forms;
using static GUI_DB.DatabaseManager;

namespace GUI_DB
{
    public partial class TicketConfirmationForm : Form
    {
        private MainForm mainForm;
        private string movieTitle;
        private DateTime showtime;
        private DateTime reservationDate; // *** NEW Field ***
        private Dictionary<string, string> selectedSeats;
        private float totalprice = 0;// *** NEW Field ***
        private List<Ticket> usertickets = new List<Ticket>(); // *** NEW Field ***

        // *** MODIFIED Constructor Signature ***
        public TicketConfirmationForm(MainForm mainForm, string movieTitle, DateTime showtime, DateTime reservationDate, Dictionary<string, string> selectedSeats)
        {
            InitializeComponent();

            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.movieTitle = movieTitle ?? throw new ArgumentNullException(nameof(movieTitle));
            this.showtime = showtime;
            this.reservationDate = reservationDate; // *** Store the date ***
            this.selectedSeats = selectedSeats ?? new Dictionary<string, string>(); // Ensure list exists

            RenderReceipt();
            AttachEvents();
        }

        private void RenderReceipt()
        {
            // Populate Labels
            lblMovie.Text = $"🎬 Movie: {movieTitle}";
            lblShowtime.Text = $"🕒 Showtime: {showtime}";
            lblReservationDate.Text = $"📅 Date: {reservationDate:D}";
            lblBookingTime.Text = $"🛒 Booked On: {DateTime.Now:g}"; // Short date/time
            lblHallID.Text = $"📺 Hall ID: {GlobalVariable.getCurrentHallId()}"; 


            // Listbox
            lstSeats.Items.Clear();
            if (selectedSeats != null && selectedSeats.Any())
            {
                foreach (var seatEntry in selectedSeats)
                {
                    string seatId = seatEntry.Key;     // e.g., "A5"
                    string seatType = seatEntry.Value; // e.g., "VIP"
                    lstSeats.Items.Add($"   • {seatId} ({seatType})");

                    // Extract row and number from seatId
                    char row = seatId[0]; // First character is row letter
                    int seatNumber = int.Parse(seatId.Substring(1)); // Rest is the seat number

                    // Create ticket for each seat (example values used for IDs)
                    Ticket ticket = new Ticket(
                        ticketID: 0,                  // You can generate or assign later
                        bookingID: "0",               // Replace with actual booking ID
                        startTime: showtime,
                        seatNumber: seatNumber,
                        rowNumber: row,
                        movieID: GlobalVariable.getCurrentMovie(),             // Make sure movieId is in scope
                        hallID: GlobalVariable.getCurrentHallId(),               // Make sure hallId is in scope
                        price: 0
                    );
                    DatabaseManager db = new DatabaseManager();
                    totalprice = db.calculatePrice(ticket) + totalprice;
                    usertickets.Add(ticket);
                    // Update total price
                    // Optionally store or display the ticket
                    // e.g., TicketRepository.AddTicket(ticket);
                }
            }
            else
            {
                lstSeats.Items.Add("   No seats selected.");
            }


            lblTotalPrice.Text = $"💰 Total: {totalprice:C2}";
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

                    decimal totalPrice = selectedSeats.Count;
                    // *** Pass date to Booking constructor ***
                    Booking newBooking = new Booking("0", this.movieTitle, this.showtime, this.reservationDate, this.selectedSeats, totalPrice);
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
            DatabaseManager db = new DatabaseManager();
            db.confirmBooking(usertickets.ToArray(), GlobalVariable.getCurrentlyLoggedIN());
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