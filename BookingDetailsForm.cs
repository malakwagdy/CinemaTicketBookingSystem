using System;
using System.Drawing;
using System.Linq; // Needed for Any()
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class BookingDetailsForm : Form
    {
        private MainForm mainForm;
        private Booking currentBooking;
        private const decimal UnitPrice = 80.00m; // Keep consistent

        public BookingDetailsForm(MainForm mainForm, Booking bookingToShow)
        {
            InitializeComponent();

            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.currentBooking = bookingToShow ?? throw new ArgumentNullException(nameof(bookingToShow));

            this.btnBack.Click += BtnBack_Click;

            RenderBookingDetails();
        }

        private void RenderBookingDetails()
        {
            if (currentBooking == null) return;

            // Populate Labels
            lblMovie.Text = $"🎬 Movie: {currentBooking.MovieTitle}";
            lblShowtime.Text = $"🕒 Showtime: {currentBooking.Showtime}";
            lblReservationDate.Text = $"📅 Reservation Date: {currentBooking.ReservationDate:D}"; // *** Set Date Label text (Long Date) ***
            lblTicketId.Text = $"🧾 Ticket ID: {currentBooking.TicketId}";
            lblBookingTime.Text = $"🛒 Booked On: {currentBooking.BookingTime:g}"; // Short date/time

            // Populate Seats
            lstSeats.Items.Clear();
            if (currentBooking.SelectedSeats != null && currentBooking.SelectedSeats.Any())
            {

            }
            else
            {
                lstSeats.Items.Add("   No seats listed.");
            }

            // Display Price
            lblUnitPrice.Text = $"🎟️ Price per seat: {UnitPrice:C2}";
            lblTotalPrice.Text = $"💰 Total: {currentBooking.TotalPrice:C2}";
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
            {
                // Navigate back to the movie list form
                CustomerMovieListForm movieListForm = new CustomerMovieListForm(mainForm);
                mainForm.OpenChildForm(movieListForm);
            }
            else
            {
                MessageBox.Show("Navigation unavailable. MainForm reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BookingDetailsForm_Load(object sender, EventArgs e)
        {
            CenterContentPanel();
            // No style changes needed here, MDI parent handles it
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