using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GUI_DB
{

    public class DatabaseManager
    {
        //InitializeComponent();
        public string connectionString =
            "Data Source=DESKTOP-PD4DI32;Initial Catalog = DatabasBroject; Integrated Security = True; Trust Server Certificate=True";

        //SqlConnection con = new SqlConnection(connectionString);
        //con.Open();
        public struct Seat
        {
            public int Seatnumber;
            public char rowNumber;
            public string seatType;
            public int hallID;

            public Seat(int SeatNumber, char rowNumber, string seatType,int hallId)
            {
                this.Seatnumber = SeatNumber;
                this.rowNumber = rowNumber;
                this.seatType = seatType;
                this.hallID = hallId;
            }
        }

        public struct User
        {
            public string email;
            public string userPassword;
            public Boolean userType;
            public string phoneNumber;
            public string firstName;
            public string lastName;
            public DateTime birthDate;
            public int Age;

            public User(string email, string userPassword, Boolean userType, string phoneNumber, string firstName,
                string lastName, DateTime birthDate)
            {
                this.email = email;
                this.userPassword = userPassword;
                this.userType = userType;
                this.phoneNumber = phoneNumber;
                this.firstName = firstName;
                this.lastName = lastName;
                this.birthDate = birthDate;
                this.Age = 0;
            }
            public User(string email, string userPassword, Boolean userType, string phoneNumber, string firstName,
                string lastName, DateTime birthDate, int age)
            {
                this.email = email;
                this.userPassword = userPassword;
                this.userType = userType;
                this.phoneNumber = phoneNumber;
                this.firstName = firstName;
                this.lastName = lastName;
                this.birthDate = birthDate;
                this.Age = age;
            }
        }

        public int calculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
                age--;

            return age;
        }

        public User GetUserById(string email)
        {
            string query = @"SELECT * FROM Users WHERE Email= @email";
            User user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User(
                                    reader["Email"].ToString(),
                                    reader["UserPassword"].ToString(),
                                    Convert.ToBoolean(reader["UserType"]),
                                    reader["PhoneNumber"].ToString(),
                                    reader["FirstName"].ToString(),
                                    reader["LastName"].ToString(),
                                    Convert.ToDateTime(reader["BirthDate"]),
                                    calculateAge(Convert.ToDateTime(reader["BirthDate"]))
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

            return user;
        }
        public struct Movie
        {
            public int movieID;
            public string director;
            public string title;
            public string genre;
            public int ageRating;
            public DateTime releaseDate;
            public Movie(int movieID, string director, string title, string genre, int ageRating, DateTime releaseDate)
            {
                this.movieID = movieID;
                this.director = director;
                this.title = title;
                this.genre = genre;
                this.ageRating = ageRating;
                this.releaseDate = releaseDate;
            }
        }

        public struct Hall
        {
            public int hallID;
            public string hallName;
            public string screenType;
            public int cinemaID;

            public Hall(int hallID, string hallName, string screentype,int cinemaID)
            {
                this.hallID = hallID;
                this.hallName = hallName;
                this.screenType = screentype;
                this.cinemaID = cinemaID;
            }
        }

        public struct Booking
        {
            public int bookingID;
            public float totalPrice;
            public DateTime bookingDate;
            public string customerID;

            public Booking(int bookingID, float totalPrice, DateTime bookingDate, string customerID)
            {
                this.bookingID = bookingID;
                this.totalPrice = totalPrice;
                this.bookingDate = bookingDate;
                this.customerID = customerID;
            }
        }

        public struct Ticket
        {
            public int ticketID;
            public string bookingID;
            public DateTime startTime;
            public int seatNumber;
            public char rowNumber;
            public int movieID;
            public int hallID;
            public float Price;
        }

        public struct Showtime
        {
            public DateTime startTime;
            public string adminID;
            public double price;
            public int hallID;
            public int movieID;
        }

        public Movie[] getAllMovies()
        {
            string query = @"SELECT * FROM Movies";
            List<Movie> movies = new List<Movie>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Use the constructor to initialize the Movie struct
                            Movie movie = new Movie(
                                Convert.ToInt32(reader["movieID"]),
                                reader["director"].ToString(),
                                reader["title"].ToString(),
                                reader["genre"].ToString(),
                                Convert.ToInt32(reader["ageRating"]),
                                Convert.ToDateTime(reader["releaseDate"])
                            );
                            movies.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return movies.ToArray();
        }

        public void calculatePrice(Ticket ticket)
        {
            string seatType = null;
            string query = @"SELECT * FROM Seat WHERE SeatNumber = @seatNumber AND RowNumber = @rowNumber AND HallID = @hallID";
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@seatNumber", ticket.seatNumber);
                        command.Parameters.AddWithValue("@rowNumber", ticket.rowNumber);
                        command.Parameters.AddWithValue("@hallID", ticket.hallID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               seatType = reader["seatType"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            if (seatType == "standard")
            {
                if (ticket.startTime.Hour < 14)
                    ticket.Price = 120;
                else
                    ticket.Price = 160;
            }
            else
            {
                if (ticket.startTime.Hour < 14)
                    ticket.Price = 180;
                else
                    ticket.Price = 150;
            }
        }
        
        public Movie[] getMoviesByGenre(string genre)
        {
            string query = @"SELECT * FROM Movies WHERE Genre= @genre";
            List<Movie> movies = new List<Movie>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@genre", genre);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Use the constructor to initialize the Movie struct
                                Movie movie = new Movie(
                                    Convert.ToInt32(reader["movieID"]),
                                    reader["director"].ToString(),
                                    reader["title"].ToString(),
                                    reader["genre"].ToString(),
                                    Convert.ToInt32(reader["ageRating"]),
                                    Convert.ToDateTime(reader["releaseDate"])
                                );
                                movies.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return movies.ToArray();
        }

        public Movie[] getMoviesByAgeRating(int rating)
        {
            string query = @"SELECT * FROM Movies WHERE AgeRating=@rating";
            List<Movie> movies = new List<Movie>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@rating", rating);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Use the constructor to initialize the Movie struct
                                Movie movie = new Movie(
                                    Convert.ToInt32(reader["movieID"]),
                                    reader["director"].ToString(),
                                    reader["title"].ToString(),
                                    reader["genre"].ToString(),
                                    Convert.ToInt32(reader["ageRating"]),
                                    Convert.ToDateTime(reader["releaseDate"])
                                );
                                movies.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return movies.ToArray();
        }

        public string Login(string Email, string Password)
        {
            string query = @"SELECT FirstName FROM Users WHERE Email = @Email AND UserPassword = @Password";

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                // Open the connection
                connection.Open();
                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Password", Password);

                string firstName = null;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    firstName = reader["FirstName"].ToString();
                }
                reader.Close();
                //return firstName;
                if (firstName == null)
                {
                    return"Invalid email or password.";
                }
                else
                {
                    return "Welcome, " + firstName;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return null;
        }

        public Seat[] GetSeatsByHall(int hallID)
        {   List<Seat> seats = new List<Seat>();
            string query=@"SELECT * FROM Seat WHERE HallID = @hallID";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hallID", hallID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Seat seat = new Seat(
                                    Convert.ToInt32(reader["SeatNumber"]),
                                    Convert.ToChar(reader["RowNumber"]),
                                    reader["seatType"].ToString(),
                                    Convert.ToInt32(reader["HallID"])
                                );
                                seats.Add(seat);
                            }
                        }
                    }
                }
            }
             
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return seats.ToArray();
        }
                        
        
        public string Register(string Email, string UserPassword,string PhoneNumber, string FirstName, string LastName, string date)
        {
            string returnstring = null;
            int count= 0;
            string query = @"SELECT COUNT(*) FROM Users WHERE Email = @Email";
            try
            {

                SqlConnection connection = new SqlConnection(connectionString);
                // Open the connection
                connection.Open();
                // Create a command
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Email", Email);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = reader.GetInt32(0); // Access the first column
                }
                reader.Close();
                if (count > 0)
                {
                    returnstring = "Email already exists";
                    return returnstring;
                }

                else if (UserPassword.Length < 8) {
                    returnstring = "Password must be 8 digits or more";
                    return returnstring;
                }
                else if (PhoneNumber.Length != 11) {
                    returnstring = "Invalid Phone number";
                    return returnstring;
                }
                else if (string.IsNullOrEmpty(FirstName) | string.IsNullOrEmpty(LastName)) {
                    returnstring = "Names cannot be null";
                    return returnstring;
                }
                else
                {
                    query = @"INSERT INTO Users (Email,UserPassword,UserType,PhoneNumber,FirstName,LastName,BirthDate) VALUES (@Email,@Password,1,@PhoneNumber,@FirstName,@LastName,@BirthDate)";
                    SqlCommand command1 = new SqlCommand(query, connection);
                    command1.Parameters.AddWithValue("@Email", Email);
                    command1.Parameters.AddWithValue("@Password", UserPassword);
                    command1.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command1.Parameters.AddWithValue("@FirstName", FirstName);
                    command1.Parameters.AddWithValue("@LastName", LastName);
                    command1.Parameters.Add(new SqlParameter("@BirthDate", SqlDbType.Date) { Value = DateTime.Parse(date) });
                    command1.ExecuteNonQuery();
                    returnstring = "Registered Successfully";
                    return returnstring;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return null;

        }



        public string AddSeat(int seatNumber, char rowNumber, string seatType, int hallID)
        {
            string returnstring = null;
            string query = @"INSERT INTO Seat (SeatNumber, RowNumber, SeatType, HallID) 
                         VALUES (@SeatNumber, @RowNumber, @SeatType, @HallID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        command.Parameters.AddWithValue("@RowNumber", rowNumber);
                        command.Parameters.AddWithValue("@SeatType", seatType);
                        command.Parameters.AddWithValue("@HallID", hallID);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Seat added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the seat.";
            }

            return returnstring;
        }


        public string DeleteSeat(int seatNumber, string rowNumber, string hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Seat WHERE SeatNumber = @SeatNumber AND RowNumber = @RowNumber AND HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        command.Parameters.AddWithValue("@RowNumber", rowNumber);
                        command.Parameters.AddWithValue("@HallID", hallID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Seat deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Seat not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the seat.";
            }

            return returnstring;
        }
        //it is what it isss.......
        public Seat[] getReservedSeats(DateTime startTime, int HallID, int MovieID)
        {   List<Seat> seats = new List<Seat>();
             string query = @"SELECT SeatNumber, RowNumber FROM Ticket WHERE StartTime=@startTime AND HallID=@HallID AND MovieID = @movieID";
             try
             {
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {
                     connection.Open();
                     using (SqlCommand command = new SqlCommand(query, connection))
                     {
                         command.Parameters.AddWithValue("@startTime", startTime);
                         command.Parameters.AddWithValue("@HallID", HallID);
                         command.Parameters.AddWithValue("@MovieID", MovieID);
                         using (SqlDataReader reader = command.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 Seat seat = new Seat(
                                     Convert.ToInt32(reader["SeatNumber"]),
                                     Convert.ToChar(reader["RowNumber"]),
                                     reader["seatType"].ToString(),
                                    Convert.ToInt32(reader["HallID"])
                                 );
                                 seats.Add(seat);
                             }
                         }
                     }
                 }
             }
             
             catch (Exception ex)
             {
                 // Handle exception (e.g., log the error)
                 Console.WriteLine("An error occurred: " + ex.Message);
             }

             return seats.ToArray();
         }

         
        public string AddMovie(int movieID, string director, string title, string genre, int ageRating, DateTime releaseDate)
        {
            string returnstring = null;
            string query = @"INSERT INTO Movie (MovieID, Director, Title, Genre, AgeRating, ReleaseDate) 
                     VALUES (@MovieID, @Director, @Title, @Genre, @AgeRating, @ReleaseDate)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        command.Parameters.AddWithValue("@Director", director);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Genre", genre);
                        command.Parameters.AddWithValue("@AgeRating", ageRating);
                        command.Parameters.AddWithValue("@ReleaseDate", releaseDate);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Movie added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the movie.";
            }

            return returnstring;
        }

        public string DeleteMovie(string movieName, DateTime releaseDate)
        {
            string returnstring = null;
            string query = @"DELETE FROM Movie WHERE Title = @movieName AND ReleaseDate = @releaseDate";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@movieName", movieName);
                        command.Parameters.AddWithValue("@releaseDate", releaseDate);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Movie deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Movie not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the movie.";
            }

            return returnstring;
        }

        public int GetHallIDByName(string hallName, int CurrentlyEditingCinemaID)
        {
            int hallID = 0;
            string query = @"SELECT HallID FROM Hall WHERE HallName = @HallName AND CinemaID = @CinemaID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HallName", hallName);
                        command.Parameters.AddWithValue("@CinemaID", CurrentlyEditingCinemaID);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            hallID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.WriteLine("Failed to add the hall.");
            }
            return hallID;
        }
        
        public string AddHall(int hallID, string hallName, string screenType, int cinemaID)
        {
            string returnstring = null;
            string query = @"INSERT INTO Hall (HallID, HallName, ScreenType, CinemaID) 
                     VALUES (@HallID, @HallName, @ScreenType, @CinemaID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@HallID", hallID);
                        command.Parameters.AddWithValue("@HallName", hallName);
                        command.Parameters.AddWithValue("@ScreenType", screenType);
                        command.Parameters.AddWithValue("@CinemaID", cinemaID);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Hall added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the hall.";
            }

            return returnstring;
        }

        public string DeleteHall(int hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Hall WHERE HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@HallID", hallID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Hall deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Hall not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the hall.";
            }

            return returnstring;
        }
        public Showtime[] GetShowtimesForMovie(int movieID)
        {
            string query = @"SELECT * FROM Showtimes WHERE MovieID = @movieID";
            List<Showtime> showtimes = new List<Showtime>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@movieID", movieID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Showtime showtime = new Showtime
                                {
                                    startTime = Convert.ToDateTime(reader["StartTime"]),
                                    adminID = reader["AdminID"].ToString(),
                                    price = Convert.ToDouble(reader["Price"]),
                                    hallID = Convert.ToInt32(reader["HallID"]),
                                    movieID = Convert.ToInt32(reader["MovieID"])
                                };
                                showtimes.Add(showtime);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return showtimes.ToArray();
        }
        public string AddMoviesActors(string movieID, string actor)
        {
            string returnstring = null;
            string query = @"INSERT INTO MoviesActors (MovieID, Actor) 
                     VALUES (@MovieID, @Actor)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        command.Parameters.AddWithValue("@Actor", actor);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "MoviesActors entry added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the MoviesActors entry.";
            }

            return returnstring;
        }

        public string DeleteMovieActor(string movieID, string actor)
        {
            string returnstring = null;
            string query = @"DELETE FROM MoviesActors WHERE MovieID = @MovieID AND Actor = @Actor";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        command.Parameters.AddWithValue("@Actor", actor);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "MoviesActors entry deleted successfully!";
                        }
                        else
                        {
                            returnstring = "MoviesActors entry not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the MoviesActors entry.";
            }

            return returnstring;
        }
        public string AddBooking(int bookingID, float totalPrice, DateTime bookingDate, string customerID)
        {
            string returnstring = null;
            string query = @"INSERT INTO Booking (BookingID, TotalPrice, BookingDate, CustomerID) 
                     VALUES (@BookingID, @TotalPrice, @BookingDate, @CustomerID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@BookingID", bookingID);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@BookingDate", bookingDate);
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Booking added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the booking.";
            }

            return returnstring;
        }

        public string DeleteBooking(int bookingID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Booking WHERE BookingID = @BookingID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@BookingID", bookingID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Booking deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Booking not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the booking.";
            }

            return returnstring;
        }
        public string AddShowtime(DateTime startTime, string adminID, string hallID, string movieID)
        {
            string returnstring = null;
            string query = @"INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) 
                     VALUES (@StartTime, @AdminID, @HallID, @MovieID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@AdminID", adminID);
                        command.Parameters.AddWithValue("@HallID", hallID);
                        command.Parameters.AddWithValue("@MovieID", movieID);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Showtime added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the showtime.";
            }

            return returnstring;
        }

        public string DeleteShowtime(DateTime startTime, int hallID, int movieID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Showtimes 
                     WHERE StartTime = @StartTime AND HallID = @HallID AND MovieID = @MovieID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@HallID", hallID);
                        command.Parameters.AddWithValue("@MovieID", movieID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Showtime deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Showtime not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the showtime.";
            }

            return returnstring;
        }
        public string confirmBooking(Ticket[] tickets, String CurrentlyLoggedIN)
        {   
            int bookingID = 0;
            float totalPrice = 0;
            string returnstring = null;
            string query = @"INSERT INTO Booking  (TotalPrice, BookingDate, CustomerID) 
                            VALUES ( @TotalPrice, @BookingDate, @CustomerID);
                            SELECT SCOPE_IDENTITY();";
            DateTime now = DateTime.Now;
            foreach (Ticket ticket in tickets)
            {
                calculatePrice(ticket);
                totalPrice += ticket.Price;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    { 
                        command.Parameters.AddWithValue("@TotalPrice",totalPrice ); 
                        command.Parameters.AddWithValue("@BookingDate", now);
                        command.Parameters.AddWithValue("CustomerID", CurrentlyLoggedIN);
                        command.ExecuteNonQuery();
                        returnstring = "Booking Confirmed";
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            bookingID = Convert.ToInt32(result); 
                        }
                    }
                    
                    query = @"INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price) 
                     VALUES ( @BookingID, @StartTime, @SeatNumber, @RowNumber, @MovieID, @HallID, @Price)";
                    foreach(Ticket ticket in tickets){
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@StartTime", ticket.startTime);
                            command.Parameters.AddWithValue("@SeatNumber", ticket.seatNumber);
                            command.Parameters.AddWithValue("@RowNumber", ticket.rowNumber);
                            command.Parameters.AddWithValue("@MovieID", ticket.movieID);
                            command.Parameters.AddWithValue("@HallID", ticket.hallID);
                            command.Parameters.AddWithValue("@Price", ticket.Price);

                            // Execute the query
                            command.ExecuteNonQuery();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the ticket.";
            }

            return returnstring;
        }

        public string DeleteTicket(string ticketID, string bookingID, DateTime startTime, int seatNumber, string rowNumber, string movieID, string hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Ticket 
                     WHERE TicketID = @TicketID AND BookingID = @BookingID AND StartTime = @StartTime 
                     AND SeatNumber = @SeatNumber AND RowNumber = @RowNumber AND MovieID = @MovieID AND HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@TicketID", ticketID);
                        command.Parameters.AddWithValue("@BookingID", bookingID);
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        command.Parameters.AddWithValue("@RowNumber", rowNumber);
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        command.Parameters.AddWithValue("@HallID", hallID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Ticket deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Ticket not found.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the ticket.";
            }

            return returnstring;
        }
        
        public string AddCinema(int cinemaID, string cinemaName, string cLocation)
        {
            string returnstring = null;
            string query = @"INSERT INTO Cinema (CinemaID, CinemaName, CLocation) 
                     VALUES (@CinemaID, @CinemaName, @CLocation)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@CinemaID", cinemaID);
                        command.Parameters.AddWithValue("@CinemaName", cinemaName);
                        command.Parameters.AddWithValue("@CLocation", cLocation);

                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = "Cinema added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the cinema.";
            }

            return returnstring;
        }

        public string DeleteCinema(int cinemaID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Cinema WHERE CinemaID = @CinemaID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@CinemaID", cinemaID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            returnstring = "Cinema deleted successfully!";
                        }
                        else
                        {
                            returnstring = "Cinema not found.";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to delete the cinema.";
            }

            return returnstring;
        }
    }

}
