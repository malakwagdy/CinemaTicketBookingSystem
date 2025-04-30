using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class AdminControl : Form
    {
        // Dictionary to track hall occupancy
        private Dictionary<string, (List<(string Movie, string Showtime)> Movies, string ScreenType)> hallOccupancy
            = new Dictionary<string, (List<(string, string)>, string)>()
        {
            { "Hall 1", (new List<(string, string)>(), "2D") },
            { "Hall 2", (new List<(string, string)>(), "3D") },
            { "Hall 3", (new List<(string, string)>(), "IMAX") }
        };

        // All showtime slots (2 hours each)
        private List<string> allShowtimes = new List<string>
        {
            "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "6:00 PM", "8:00 PM"
        };

        private MainForm mainForm;

        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Load halls and showtimes
            LoadHalls();
            cmbHalls.SelectedIndexChanged += (s, e) =>
            {
                string selectedHall = cmbHalls.SelectedItem?.ToString()?.Split('(')[0].Trim(); // Extract hall name
                if (!string.IsNullOrEmpty(selectedHall))
                {
                    LoadShowtimes(selectedHall);
                }
            };

            // Hook up events
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click;
        }

        // Load halls into dropdown
        private void LoadHalls()
        {
            cmbHalls.Items.Clear();
            foreach (var hall in hallOccupancy.Keys)
            {
                if (hallOccupancy[hall].Movies.Count == 0) // Filter for empty halls
                {
                    cmbHalls.Items.Add($"{hall} ({hallOccupancy[hall].ScreenType})"); // Show hall name with screen type
                }
            }
        }

        // Load available showtimes based on selected hall
        private void LoadShowtimes(string selectedHall)
        {
            cmbShowtimes.Items.Clear();
            var availableShowtimes = allShowtimes.Where(showtime =>
                !hallOccupancy[selectedHall].Movies.Any(entry => entry.Showtime == showtime)).ToList();

            foreach (var showtime in availableShowtimes)
            {
                cmbShowtimes.Items.Add(showtime);
            }
        }

        // Event: Add Movie
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHall = cmbHalls.SelectedItem?.ToString()?.Split('(')[0].Trim(); // Extract hall name
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();
            string ageRating = txtAgeRating.Text.Trim();
            string director = txtDirector.Text.Trim();
            string actorsInput = txtActors.Text.Trim();

            if (!string.IsNullOrEmpty(movieTitle) &&
                !string.IsNullOrEmpty(selectedHall) &&
                !string.IsNullOrEmpty(selectedShowtime) &&
                !string.IsNullOrEmpty(ageRating) &&
                !string.IsNullOrEmpty(director) &&
                !string.IsNullOrEmpty(actorsInput))
            {
                string[] actors = actorsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(actor => actor.Trim())
                                             .ToArray();

                // Call backend or database logic to add the movie
                AddMovieToDatabase(movieTitle, selectedHall, selectedShowtime, ageRating, director, actors);

                MessageBox.Show($"Movie '{movieTitle}' added to hall '{selectedHall}' at '{selectedShowtime}' successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearMovieFields();
                cmbHalls.SelectedIndex = -1;
                cmbShowtimes.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please fill out all fields to add a movie.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Remove Movie
        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHall = cmbHalls.SelectedItem?.ToString()?.Split('(')[0].Trim(); // Extract hall name
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(movieTitle) && !string.IsNullOrEmpty(selectedHall) && !string.IsNullOrEmpty(selectedShowtime))
            {
                hallOccupancy[selectedHall].Movies.RemoveAll(entry => entry.Movie == movieTitle && entry.Showtime == selectedShowtime);
                MessageBox.Show($"Movie '{movieTitle}' removed from hall '{selectedHall}' at '{selectedShowtime}' successfully!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearMovieFields();
                cmbHalls.SelectedIndex = -1;
                cmbShowtimes.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please enter a movie title, select a hall, and choose a showtime.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Add Hall
        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            string screenType = cmbScreenType.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(hallName) && !hallOccupancy.ContainsKey(hallName) && !string.IsNullOrEmpty(screenType))
            {
                hallOccupancy.Add(hallName, (new List<(string, string)>(), screenType));
                MessageBox.Show($"Hall '{hallName}' with screen type '{screenType}' added successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHallName.Clear();
                cmbScreenType.SelectedIndex = -1;
                LoadHalls();
            }
            else
            {
                MessageBox.Show("Please enter a valid and unique hall name and select a screen type.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Remove Hall
        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (!string.IsNullOrEmpty(hallName) && hallOccupancy.ContainsKey(hallName))
            {
                hallOccupancy.Remove(hallName);
                MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHallName.Clear();
                LoadHalls();
            }
            else
            {
                MessageBox.Show("Please enter a valid hall name that exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddMovieToDatabase(string title, string hall, string showtime, string ageRating, string director, string[] actors)
        {
            // Placeholder for backend/database logic to add the movie
            Console.WriteLine($"Adding movie: {title}, Hall: {hall}, Showtime: {showtime}, Age Rating: {ageRating}, Director: {director}, Actors: {string.Join(", ", actors)}");
        }

        private void ClearMovieFields()
        {
            txtMovieTitle.Clear();
            txtAgeRating.Clear();
            txtDirector.Clear();
            txtActors.Clear();
        }
    }
}