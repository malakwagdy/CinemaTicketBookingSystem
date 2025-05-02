using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq; // Needed for Any()
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;

namespace GUI_DB
{
    public partial class BookingDetailsForm : Form
    {
        private MainForm mainForm;
        private DatabaseManager.Booking currentBooking;
        


        public BookingDetailsForm(MainForm mainForm, DatabaseManager.Booking bookingToShow)
        {
            InitializeComponent();

            this.mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            this.currentBooking = bookingToShow;

            this.btnBack.Click += BtnBack_Click;

            RenderBookingDetails();
        }

        private void RenderBookingDetails()
        {
            float totalPrice = 0;
            List<int> ticketIDs = new List<int>();
            List<string> seatIDs = new List<string>();
            DateTime startTime = new DateTime();
            int hallID = 0; // Initialize to a default value
            List<string> seatTypes = new List<string>(); // Added to include seat types


            if (currentBooking.bookingID == null) return;

            DatabaseManager dbManager = new DatabaseManager();


            var ticketInfo = dbManager.GetSingleTicketInfo(currentBooking.bookingID);

            if (ticketInfo.Length > 0)
            {
                foreach (DatabaseManager.Ticket ticket in ticketInfo)
                {

                    ticketIDs.Add(ticket.ticketID);
                    seatIDs.Add(dbManager.ConvertToSeatId(ticket.rowNumber, ticket.seatNumber));
                    totalPrice += ticket.Price;
                    startTime = ticket.startTime;
                    hallID = ticket.hallID;
                    string seatType = dbManager.GetSeatType(ticket.seatNumber, ticket.rowNumber);
                    seatTypes.Add(seatType);

                }


            }
            // Check and use the values
            else
            {
                Console.WriteLine("No ticket found for this booking");
            }


            string movieTitle = dbManager.GetMovieNameByBookingID(currentBooking.bookingID);
            string ticketIDsString = string.Join(", ", ticketIDs);
            string seatIDsString = string.Join("\n ", seatIDs);

            // Populate Labels
            lblMovie.Text = $"🎬 Movie: {movieTitle}";
            lblShowtime.Text = $"🕒 Showtime: {startTime.ToString()}";
            lblReservationDate.Text = $"📅 Reservation Date: {currentBooking.bookingDate:D}"; // *** Set Date Label text (Long Date) ***
            lblTicketId.Text = $"🧾 Ticket IDs: {ticketIDsString}";
            lblTotalPrice.Text = $"💰 Total Price: ${totalPrice.ToString()}"; // Format as currency
            lblHallID.Text = $"📺 Hall ID: {hallID}";


            // Populate Seats
            lstSeats.Items.Clear();
            if (seatIDs != null && seatIDs.Any())
            {
                for (int i = 0; i < seatIDs.Count; i++)
                {
                    lstSeats.Items.Add($"{seatIDs[i],-5} {seatTypes[i]}"); // Add each seatID to the list box
                }
            }
            else
            {
                lstSeats.Items.Add("   No seats listed.");
            }

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