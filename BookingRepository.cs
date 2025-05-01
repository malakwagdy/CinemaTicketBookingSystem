using System.Collections.Generic; // Needed for List<Booking>
using System.Linq; // Needed for OrderByDescending (optional)

// Define the namespace your project uses
namespace GUI_DB
{
    /// <summary>
    /// Provides a simple, in-memory storage for confirmed bookings during the application's runtime.
    /// NOTE: Data is lost when the application closes.
    /// </summary>
    public static class BookingRepository // Notice the 'static' keyword
    {
        // --- Private Static Field ---
        // This holds the actual list of bookings.
        // 'private': Only accessible from within this class.
        // 'static': Belongs to the class itself, not an instance. There's only ONE list shared by the whole app.
        // 'readonly': The variable '_confirmedBookings' itself cannot be reassigned to a *different* list after initialization.
        private static readonly List<Booking> _confirmedBookings = new List<Booking>();

        // --- Public Static Methods ---

        /// <summary>
        /// Adds a confirmed booking to the repository.
        /// </summary>
        /// <param name="booking">The Booking object to add.</param>
        public static void AddBooking(Booking booking)
        {
            // Basic validation: Don't add null bookings
            if (booking != null)
            {
                // You could add checks here to prevent duplicate TicketIDs if necessary
                // if (!_confirmedBookings.Any(b => b.TicketId == booking.TicketId))
                // {
                _confirmedBookings.Add(booking);
                // }
            }
        }

        /// <summary>
        /// Retrieves a list of all confirmed bookings.
        /// </summary>
        /// <returns>A new list containing all stored bookings.</returns>
        public static List<Booking> GetBookings()
        {
            // IMPORTANT: Return a *copy* of the list.
            // This prevents code outside this repository from directly modifying the internal list
            // (e.g., prevents accidentally calling .Clear() on the list returned).
            return new List<Booking>(_confirmedBookings);

            // Optional: If you want the list sorted (e.g., newest first)
            // return _confirmedBookings.OrderByDescending(b => b.BookingTime).ToList();
        }

        /// <summary>
        /// Removes all bookings from the repository. Useful for testing or resetting state.
        /// </summary>
        public static void ClearBookings()
        {
            _confirmedBookings.Clear();
        }

        // You could add other methods here if needed, like:
        // public static Booking GetBookingById(string ticketId) { ... }
        // public static void RemoveBooking(string ticketId) { ... }
    }
}