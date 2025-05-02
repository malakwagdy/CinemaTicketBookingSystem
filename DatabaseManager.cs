using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace GUI_DB
{

    public class DatabaseManager
    {
        //InitializeComponent();
        public string connectionString =
            "Data Source=DESKTOP-PD4DI32;Initial Catalog=DatabasBroject;Integrated Security=True;Trust Server Certificate=True";

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
        
        public struct Movie
        {
            public int MovieID;
            public string Director;
            public string Title;
            public string Genre;
            public int AgeRating;
            public DateTime ReleaseDate;
            public string adminID;
            
            public Movie(int movieID, string director, string title, string genre, int ageRating, DateTime releaseDate)
            {
                this.MovieID = movieID;
                this.Director = director;
                this.Title = title;
                this.Genre = genre;
                this.AgeRating = ageRating;
                this.ReleaseDate = releaseDate;
                this.adminID = "admin@email.com";
            }
        }

        public struct Hall
        {
            public int hallID;
            public string hallName;
            public string screenType;
            public int cinemaID;
            public string adminID;

            public Hall(int hallID, string hallName, string screentype,int cinemaID)
            {
                this.hallID = hallID;
                this.hallName = hallName;
                this.screenType = screentype;
                this.cinemaID = cinemaID;
                this.adminID = "admin@email.com";
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
            public Ticket(int ticketID, string bookingID, DateTime startTime, int seatNumber, char rowNumber, int movieID, int hallID, float price)
            {
                this.ticketID = ticketID;
                this.bookingID = bookingID;
                this.startTime = startTime;
                this.seatNumber = seatNumber;
                this.rowNumber = rowNumber;
                this.movieID = movieID;
                this.hallID = hallID;
                this.Price = 0;
            }
        }

        public struct Showtime
        {
            public DateTime startTime;
            public string adminID;
            public double price;
            public int hallID;
            public int movieID;
        }

        public int calculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
                age--;

            return age;
        }
        public Hall[] getAllHalls()
        {
            string query = @"SELECT * FROM Hall WHERE CinemaID= @CinemaID";
            List<Hall> halls = new List<Hall>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CinemaID", 1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Use the constructor to initialize the Movie struct
                                Hall hall = new Hall(
                                    Convert.ToInt32(reader["HallID"]),
                                    reader["HallName"].ToString(),
                                    reader["ScreenType"].ToString(),
                                    Convert.ToInt32(reader["CinemaID"])
                                );
                                halls.Add(hall);
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
            return halls.ToArray();
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
                                    // price = Convert.ToDouble(reader["Price"]),
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
        
        public Movie[] getAllMovies()
        {
            string query = @"SELECT * FROM Movie";
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
                                Convert.ToInt32(reader["MovieID"]),
                                reader["Director"].ToString(),
                                reader["Title"].ToString(),
                                reader["Genre"].ToString(),
                                Convert.ToInt32(reader["AgeRating"]),
                                Convert.ToDateTime(reader["ReleaseDate"])
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

        public float calculatePrice(Ticket ticket)
        {
            string seatType = null;
            string screenType = null;
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

            query = @"SELECT * FROM Hall WHERE HallID = @hallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hallID", ticket.hallID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                screenType = reader["ScreenType"].ToString();
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

            if (screenType == "IM")
            {
                if (seatType == "Standard")
                {
                    if (ticket.startTime.Hour < 14)
                        ticket.Price = 200;
                    else
                        ticket.Price = 240;
                }
                else
                {
                    if (ticket.startTime.Hour < 14)
                        ticket.Price = 260;
                    else
                        ticket.Price = 300;
                }
            }
            else
            {
                if (seatType == "Standard")
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
                        ticket.Price = 220;
                }
            }
            return ticket.Price;
        }

        public int getMovieID(string title, DateTime releaseDate)
        {
            string query = @"SELECT * FROM Movie WHERE Title = @title AND ReleaseDate = @releaseDate";
            int movieID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@releaseDate", releaseDate.Date);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            movieID = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return movieID;
        }
        //public int getMovieIDByHallID(string title, DateTime releaseDate)
        //{
        //    string query = @"SELECT * FROM Movie WHERE Title = @title AND ReleaseDate = @releaseDate";
        //    int movieID = 0;

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@title", title);
        //                command.Parameters.AddWithValue("@releaseDate", releaseDate.Date);
        //                object result = command.ExecuteScalar();
        //                if (result != null)
        //                {
        //                    movieID = Convert.ToInt32(result);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception (e.g., log the error)
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }

        //    return movieID;
        //}
        public Movie[] getMoviesByGenre(string genre)
        {
            string query = @"SELECT * FROM Movie WHERE Genre= @genre";
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
                                    Convert.ToInt32(reader["MovieID"]),
                                    reader["Director"].ToString(),
                                    reader["Title"].ToString(),
                                    reader["Genre"].ToString(),
                                    Convert.ToInt32(reader["AgeRating"]),
                                    Convert.ToDateTime(reader["ReleaseDate"])
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
            string query = @"SELECT * FROM Movie WHERE AgeRating=@rating";
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
                                    Convert.ToInt32(reader["MovieID"]),
                                    reader["Director"].ToString(),
                                    reader["Title"].ToString(),
                                    reader["Genre"].ToString(),
                                    Convert.ToInt32(reader["AgeRating"]),
                                    Convert.ToDateTime(reader["ReleaseDate"])
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
                    return "Invalid email or password.";
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
                                    reader["SeatType"].ToString(),
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
                        
        
        public string Register(string Email, string UserPassword,string PhoneNumber, string FirstName, string LastName, DateTime date)
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
                    command1.Parameters.AddWithValue("@BirthDate", date);
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


        public string DeleteSeat(int hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Seat WHERE HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        //command.Parameters.AddWithValue("@RowNumber", rowNumber);
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
                                     reader["SeatType"].ToString(),
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

         
        public int AddMovie(string director, string title, string genre, int ageRating, DateTime releaseDate)
        {
            int returnstring = 0;
            string query = @"INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID) 
             VALUES (@Director, @Title, @Genre, @AgeRating, @ReleaseDate, 'admin@email.com'); SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@MovieID", movieID);
                        command.Parameters.AddWithValue("@Director", director);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Genre", genre);
                        command.Parameters.AddWithValue("@AgeRating", ageRating);
                        command.Parameters.AddWithValue("@ReleaseDate", releaseDate);

                        // Execute the query
                        // command.ExecuteNonQuery();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            returnstring = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

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

        public int GetHallIDByName(string hallName)
        {
            int hallID = 0;
            string query = @"SELECT HallID FROM Hall WHERE HallName = @HallName AND CinemaID = 1";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HallName", hallName);
                        //command.Parameters.AddWithValue("@CinemaID", CurrentlyEditingCinema);
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
        
        public string AddHall(string hallName, string ScreenType ,int NumOfRows, int SeatsPerRow,bool IsPremium,int numOfPremiumRows)
        {
            string returnstring = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CreateHallWithSeats", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@HallName", hallName);
                        command.Parameters.AddWithValue("@ScreenType", ScreenType);
                        command.Parameters.AddWithValue("@CinemaID", 1);
                        command.Parameters.AddWithValue("@AdminID", "admin@email.com");
                        command.Parameters.AddWithValue("@NumRows", NumOfRows);
                        command.Parameters.AddWithValue("@SeatsPerRow", SeatsPerRow);
                        command.Parameters.AddWithValue("@IsPremiumHall", IsPremium);
                        command.Parameters.AddWithValue("@PremiumRowsFromEnd", numOfPremiumRows);
                        // Execute the query
                        command.ExecuteNonQuery();
                        returnstring = $"Successfully created hall '{hallName}' with {NumOfRows * SeatsPerRow} seats";
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

        public string AddMoviesActors(int movieID, string [] actors)
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
                        foreach (string actor in actors)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnstring = "Failed to add the MoviesActors entry.";
            }

            return returnstring;
        }

        public string DeleteMovieActor(int movieID)
        {
            string returnstring = null;
            string query = @"DELETE FROM MoviesActors WHERE MovieID = @MovieID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        //command.Parameters.AddWithValue("@Actor", actor);

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
        public string AddShowtime(DateTime startTime, int movieID, int hallID)
        {
            string returnstring = null;
            string query = @"INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) 
             VALUES (@StartTime, 'admin@email.com', @HallID, @MovieID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@StartTime", startTime);
                        //command.Parameters.AddWithValue("@AdminID", adminID);
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

        //public string DeleteShowtime(DateTime startTime, int hallID, int movieID)
        //{
        //    string returnstring = null;
        //    string query = @"DELETE FROM Showtimes 
        //             WHERE StartTime = @StartTime AND HallID = @HallID AND MovieID = @MovieID";

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                // Add parameters to prevent SQL injection
        //                command.Parameters.AddWithValue("@StartTime", startTime);
        //                command.Parameters.AddWithValue("@HallID", hallID);
        //                command.Parameters.AddWithValue("@MovieID", movieID);

        //                // Execute the query
        //                int rowsAffected = command.ExecuteNonQuery();

        //                if (rowsAffected > 0)
        //                {
        //                    returnstring = "Showtime deleted successfully!";
        //                }
        //                else
        //                {
        //                    returnstring = "Showtime not found.";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        //        returnstring = "Failed to delete the showtime.";
        //    }

        //    return returnstring;
        //}
        public string DeleteShowtime(int hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Showtimes 
                     WHERE  HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@StartTime", startTime);
                        command.Parameters.AddWithValue("@HallID", hallID);
                        //command.Parameters.AddWithValue("@MovieID", movieID);

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

        public string DeleteShowtimeByMovieID(int movieID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Showtimes 
                     WHERE  MovieID = @MovieID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@StartTime", startTime);
                        //command.Parameters.AddWithValue("@HallID", hallID);
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
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@BookingDate", now);
                        command.Parameters.AddWithValue("CustomerID", CurrentlyLoggedIN);
                        object result = command.ExecuteScalar();
                        returnstring = "Booking Confirmed";

                        if (result != null)
                        {
                            bookingID = Convert.ToInt32(result);
                        }
                    }

                    query = @"INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price) 
                            VALUES ( @BookingID, @StartTime, @SeatNumber, @RowNumber, @MovieID, @HallID, @Price)";
                    foreach (Ticket ticket in tickets)
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL injection
                            command.Parameters.AddWithValue("@BookingID", bookingID);
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

        public string DeleteTicketByMovieID(int movieID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Ticket 
                     WHERE MovieID = @MovieID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@TicketID", ticketID);
                        //command.Parameters.AddWithValue("@BookingID", bookingID);
                        //command.Parameters.AddWithValue("@StartTime", startTime);
                        //command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        //command.Parameters.AddWithValue("@RowNumber", rowNumber);
                        command.Parameters.AddWithValue("@MovieID", movieID);
                        //command.Parameters.AddWithValue("@HallID", hallID);

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

        public string DeleteTicketByHallID(int hallID)
        {
            string returnstring = null;
            string query = @"DELETE FROM Ticket 
                     WHERE HallID = @HallID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        //command.Parameters.AddWithValue("@TicketID", ticketID);
                        //command.Parameters.AddWithValue("@BookingID", bookingID);
                        //command.Parameters.AddWithValue("@StartTime", startTime);
                        //command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                        //command.Parameters.AddWithValue("@RowNumber", rowNumber);
                        //command.Parameters.AddWithValue("@MovieID", movieID);
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
            string query = @"INSERT INTO Cinema (CinemaID, CinemaName, CLocation, AdminID) 
                     VALUES (@CinemaID, @CinemaName, @CLocation, 'admin@email.com')";

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
        
        public string CreateHallSeats(int hallID, int numRows, int seatsPerRow, bool isPremiumHall , int premiumRowsFromEnd )
        {
            string returnString = null;
    
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CreateHallSeats", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                
                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@HallID", hallID);
                        command.Parameters.AddWithValue("@NumRows", numRows);
                        command.Parameters.AddWithValue("@SeatsPerRow", seatsPerRow);
                        command.Parameters.AddWithValue("@IsPremiumHall", isPremiumHall); // Convert bool to bit
                        command.Parameters.AddWithValue("@PremiumRowsFromEnd", premiumRowsFromEnd);
                        command.Parameters.AddWithValue("@AdminID"," admin@email.com");

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        returnString = $"Successfully created {numRows * seatsPerRow} seats for Hall ID {hallID}";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                returnString = "Failed to create hall seats.";
            }

            return returnString;
        }
    
     public Booking[] GetBookingsByUser(string customerEmail)
        {
            List<Booking> bookings = new List<Booking>();

            // Fixed query - removed line break and added proper spacing
            string query = @"SELECT * FROM Booking WHERE CustomerID = @CustomerID ORDER BY BookingDate DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Make sure customerEmail is not null or empty
                        if (string.IsNullOrEmpty(customerEmail))
                        {
                            MessageBox.Show("No customer email provided", "Error");
                            return bookings.ToArray();
                        }

                        // Explicitly specify parameter type and size
                        command.Parameters.AddWithValue("@CustomerID", customerEmail);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bookings.Add(new Booking(
                                    Convert.ToInt32(reader["BookingID"]),
                                    Convert.ToSingle(reader["TotalPrice"]),
                                    Convert.ToDateTime(reader["BookingDate"]),
                                    reader["CustomerID"].ToString()
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving bookings: {ex.Message}", "Database Error");
            }

            return bookings.ToArray();
        }
        
        private string ConvertToSeatId(char rowNumber, int seatNumber)
        {
            return $"{rowNumber}{seatNumber}";
        }
        
        public HashSet<string> GetReservedSeatsCombined(DateTime startTime, int hallID, int movieID)
        {
            var reservedSeats = new HashSet<string>();
            string query = @"SELECT SeatNumber, RowNumber FROM Ticket 
                   WHERE StartTime=@startTime AND HallID=@HallID AND MovieID=@movieID";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@startTime", startTime);
                        command.Parameters.AddWithValue("@HallID", hallID);
                        command.Parameters.AddWithValue("@MovieID", movieID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                char row = Convert.ToChar(reader["RowNumber"]);
                                int number = Convert.ToInt32(reader["SeatNumber"]);
                                reservedSeats.Add(ConvertToSeatId(row, number));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reserved seats: {ex.Message}");
            }

            return reservedSeats;
        }

        public Dictionary<string, string> GetSeatsByHallCombined(int hallID) // <seatId, seatType>
        {
            var seats = new Dictionary<string, string>();
            string query = @"SELECT SeatNumber, RowNumber, SeatType FROM Seat WHERE HallID = @hallID";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hallID", hallID);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                char row = Convert.ToChar(reader["RowNumber"]);
                                int number = Convert.ToInt32(reader["SeatNumber"]);
                                string type = reader["SeatType"].ToString();

                                seats.Add(ConvertToSeatId(row, number), type);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seats: {ex.Message}");
            }

            return seats;
        }

    }

}
