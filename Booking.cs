using System;
using System.Collections.Generic;
using System.Linq; // Required for Any()

// Define the namespace your project uses
namespace GUI_DB
{
    /// <summary>
    /// Represents the details of a single confirmed ticket booking.
    /// </summary>
    public class Booking
    {
        // --- Properties ---
        public string TicketId { get; private set; }
        public string MovieTitle { get; private set; }
        public string Showtime { get; private set; }
        public List<string> SelectedSeats { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime BookingTime { get; private set; }   // When the booking was *made*
        public DateTime ReservationDate { get; private set; } // *** NEW: The date the movie is for ***

        // --- Constructor ---
        /// <summary>
        /// Creates a new instance of a Booking.
        /// </summary>
        /// <param name="ticketId">The unique ID for this ticket.</param>
        /// <param name="movieTitle">The title of the movie.</param>
        /// <param name="showtime">The selected showtime.</param>
        /// <param name="reservationDate">The date the movie showing is on.</param> // *** NEW Parameter ***
        /// <param name="selectedSeats">The list of seats booked.</param>
        /// <param name="totalPrice">The total price for all seats in this booking.</param>
        public Booking(string ticketId, string movieTitle, string showtime, DateTime reservationDate, List<string> selectedSeats, decimal totalPrice) // *** Signature Updated ***
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(ticketId))
                throw new ArgumentNullException(nameof(ticketId), "Ticket ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(movieTitle))
                throw new ArgumentNullException(nameof(movieTitle), "Movie Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(showtime))
                throw new ArgumentNullException(nameof(showtime), "Showtime cannot be empty.");
            if (selectedSeats == null)
                throw new ArgumentNullException(nameof(selectedSeats), "Selected seats list cannot be null.");
            if (!selectedSeats.Any())
                throw new ArgumentException("Booking must contain at least one seat.", nameof(selectedSeats));
            if (totalPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(totalPrice), "Total price cannot be negative.");
            // Optional: Validate reservationDate (e.g., not in the past relative to today)
            // if (reservationDate.Date < DateTime.Today)
            //    throw new ArgumentOutOfRangeException(nameof(reservationDate), "Reservation date cannot be in the past.");


            // Assign properties
            TicketId = ticketId;
            MovieTitle = movieTitle;
            Showtime = showtime;
            // Store only the Date part, ignore time component from DateTimePicker
            ReservationDate = reservationDate.Date; // *** Assign NEW Property ***
            SelectedSeats = new List<string>(selectedSeats); // Create copy
            TotalPrice = totalPrice;
            BookingTime = DateTime.Now; // Time the booking object was created
        }

        // --- ToString() Override ---
        /// <summary>
        /// Provides a user-friendly string representation of the booking for display lists.
        /// Includes Movie Title, Showtime, and Reservation Date.
        /// </summary>
        /// <returns>A summary string of the booking.</returns>
        public override string ToString()
        {
            // Example: "Dune Part II - 7:00 PM (10/27/2023)"
            // Using "d" for short date pattern (culture-specific)
            return $"{MovieTitle} - {Showtime} ({ReservationDate:d})";
        }
    }
}