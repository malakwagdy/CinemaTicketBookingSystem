# Cinema Ticket Booking System

A comprehensive, desktop-based application for booking and managing cinema tickets. This system provides a seamless experience for users to browse movies and book seats, along with a powerful control panel for administrators to manage movie listings, showtimes, and cinema halls.

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

---

## 📖 Table of Contents

-   [Features](#-features)
    -   [User View Features](#user-view-features)
    -   [Admin Control Panel Features](#admin-control-panel-features)
-   [Technology Stack](#-technology-stack)
-   [Getting Started](#-getting-started)
    -   [Prerequisites](#prerequisites)
    -   [Setup Instructions](#setup-instructions)
-   [How to Use](#-how-to-use)
-   [Contributing](#-contributing)
-   [License](#-license)

---

## ✨ Features

The application is split into two main interfaces: a user-facing view for booking tickets and an admin panel for management.

### User View Features

-   **Browse Movie Listings**: View all movies currently showing, with details like genre and director.
-   **Advanced Filtering**: Easily filter the movie list by Date, Genre, and Age Rating to find the perfect film.
-   **View Showtimes**: See all available showtimes for each movie on a selected day.
-   **Booking System**: A complete booking process allowing users to select a showtime and reserve seats.
-   **My Reservations**: A dedicated section for users to view their past and upcoming ticket bookings.

### Admin Control Panel Features

-   **Movie Management**:
    -   **Add Movies**: Add new movies to the database with complete details including Title, Director, Actors, Release Date, Age Rating, and Genre.
    -   **Update & Remove Movies**: Easily update details for existing movies or remove them from the listings.
    -   **Assign Showtimes**: Link movies to specific cinema halls and set their showtimes.

-   **Cinema Hall Management**:
    -   **Add Halls**: Create new cinema halls with a specified name, screen type (e.g., Standard, 3D, IMAX).
    -   **Define Seating Layout**: Configure the seating arrangement for each hall, including the total number of rows, seats per row, and the number of premium rows.
    -   **Remove Halls**: Decommission halls that are no longer in use.

---

## 💻 Technology Stack

-   **Application Framework**: .NET Framework with Windows Forms / WPF
-   **Programming Language**: C#
-   **Database**: SQL Server

---

## 🚀 Getting Started

Follow these instructions to set up the project on your local machine for development and testing.

### Prerequisites

-   **IDE**: Visual Studio 2019 or later
-   **Framework**: .NET Framework 4.7.2 or higher
-   **Database Server**: Microsoft SQL Server Management Studio

### Setup Instructions

1.  **Clone the repository:**
    ```sh
    git clone https://github.com/malakwagdy/CinemaTicketBookingSystem.git
    cd CinemaTicketBookingSystem
    ```

2.  **Set up the Database:**
    -   A SQL script to create the required database schema is included in the project (`FullDBCreation.sql`).
    -   Open your database management tool (like SQL Server Management Studio).
    -   Create a new, empty database.
    -   Run the provided SQL script against your new database. This will create all the necessary tables and relationships.

3.  **Configure the Database Connection:**
    -   Open the project solution in Visual Studio.
    -   Navigate to the database manager file (e.g., `DatabaseManager.cs` or similar).
    -   Locate the connection string variable.
    -   **Important:** You must update the connection string to point to your local database server and the database you just created. For example:
        ```csharp
        // Before
        private string connectionString = "Server=SERVER_NAME;Database=DB_NAME;User Id=USER;Password=PASSWORD;";

        // After
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=MyCinemaDB;Integrated Security=True;";
        ```

4.  **Build and Run the Project:**
    -   Build the solution in Visual Studio (Build > Build Solution).
    -   Run the project by pressing `F5` or clicking the "Start" button. The application should now launch and connect to your database.

---

## 📖 How to Use

1.  **Admin Login**: Launch the application and log in with admin credentials to access the **Admin Control Panel**. Here you can add halls and then add movies with their showtimes.
2.  **User View**: Relaunch the application and use it as a regular user. You will see the movies and halls you added.
3.  **Browse and Filter**: Use the filters on the left to find a movie you're interested in.
4.  **Book Tickets**: Select an available showtime to proceed with the booking process.
5.  **View Bookings**: Check the "My Reservations" panel to see a summary of your booked tickets.

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to open an issue to discuss new features or report bugs.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AdditionalFeature`)
3.  Commit your Changes (`git commit -m 'Add some AdditionalFeature'`)
4.  Push to the Branch (`git push origin feature/AdditionalFeature`)
5.  Open a Pull Request

---

## 📜 License

This project is distributed under the MIT License. See the `LICENSE` file for more information.
