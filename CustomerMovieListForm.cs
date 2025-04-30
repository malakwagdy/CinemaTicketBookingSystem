using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class CustomerMovieListForm : Form
    {
        private MainForm mainForm; // Reference to the parent MainForm

        private string currentTimeFilter = "All";
        private string currentAgeFilter = "All";
        private string selectedGenre = "All";
        private string selectedYear = "";
        private string selectedDirector = "";
        private string selectedActor = "";

        // Constructor with MainForm reference
        public CustomerMovieListForm(MainForm form)
        {
            mainForm = form; // Store the reference to MainForm
            InitializeComponent();

            // Ensure controls are initialized before setting up events
            if (cmbGenre != null && txtYear != null && txtDirector != null && txtActor != null)
            {
                InitializeFilterEvents();
            }
            else
            {
                MessageBox.Show("One or more controls are not initialized properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadMovies();
        }


        private void LoadMovies()
        {
            flowLayoutPanelMovies.Controls.Clear();
            List<Movie> movies = MovieRepository.GetMovies();

            foreach (var movie in movies)
            {
                if (!PassesFilters(movie)) continue;

                Panel moviePanel = new Panel
                {
                    Height = 100,
                    Width = flowLayoutPanelMovies.Width - 40,
                    BackColor = Color.FromArgb(60, 60, 75),
                    Margin = new Padding(10),
                    Padding = new Padding(10)
                };

                Label lblTitle = new Label
                {
                    Text = $"{movie.Title} ({movie.AgeRating}, {movie.ReleaseYear})",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Top,
                    Height = 28
                };

                FlowLayoutPanel showtimesPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight
                };

                foreach (var showtime in movie.Showtimes)
                {
                    LinkLabel linkShowtime = new LinkLabel
                    {
                        Text = showtime,
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Underline), // Underlined text
                        LinkColor = Color.LightGray, // Grey color
                        ActiveLinkColor = Color.White, // Slightly brighter color when active
                        VisitedLinkColor = Color.Gray, // Subtle grey for visited
                        Margin = new Padding(0, 0, 2, 0)
                    };

                    linkShowtime.Click += (s, e) => OpenSeatingChart(movie.Title, showtime);
                    showtimesPanel.Controls.Add(linkShowtime);
                }

                Label lblDetails = new Label
                {
                    Text = $"Genre: {movie.Genre} | Director: {movie.Director} | Star: {movie.StarActor}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Top,
                    Height = 22
                };

                moviePanel.Controls.Add(lblDetails);
                moviePanel.Controls.Add(showtimesPanel);
                moviePanel.Controls.Add(lblTitle);
                flowLayoutPanelMovies.Controls.Add(moviePanel);
            }
        }


        private void OpenSeatingChart(string movieTitle, string showtime)
        {
            SeatingChartForm seatingChart = new SeatingChartForm(mainForm, movieTitle, showtime);
            mainForm.OpenChildForm(seatingChart);
        }



        private bool PassesFilters(Movie movie)
        {
            bool timeOk = currentTimeFilter == "All" || movie.Showtimes.Any(timeStr =>
            {
                if (DateTime.TryParse(timeStr, out DateTime time))
                {
                    if (currentTimeFilter == "Morning")
                        return time.Hour < 12;
                    else if (currentTimeFilter == "Afternoon")
                        return time.Hour >= 12 && time.Hour < 17;
                    else if (currentTimeFilter == "Evening")
                        return time.Hour >= 17;
                    else
                        return true;
                }
                return false;
            });

            bool ageOk = currentAgeFilter == "All" || movie.AgeRating == currentAgeFilter;
            bool genreOk = selectedGenre == "All" || movie.Genre == selectedGenre;
            bool yearOk = string.IsNullOrWhiteSpace(selectedYear) || movie.ReleaseYear.ToString() == selectedYear.Trim();
            bool directorOk = string.IsNullOrWhiteSpace(selectedDirector) || movie.Director.ToLower().Contains(selectedDirector);
            bool actorOk = string.IsNullOrWhiteSpace(selectedActor) || movie.StarActor.ToLower().Contains(selectedActor);

            return timeOk && ageOk && genreOk && yearOk && directorOk && actorOk;
        }

        private void InitializeFilterEvents()
        {
            cmbGenre.SelectedIndexChanged += (s, e) =>
            {
                selectedGenre = cmbGenre.SelectedItem.ToString();
                LoadMovies();
            };

            txtYear.TextChanged += (s, e) =>
            {
                if (txtYear.ForeColor != Color.Gray)
                {
                    selectedYear = txtYear.Text;
                    LoadMovies();
                }
            };

            txtYear.GotFocus += (s, e) =>
            {
                if (txtYear.Text == "e.g. 2024")
                {
                    txtYear.Text = "";
                    txtYear.ForeColor = Color.White;
                }
            };

            txtYear.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtYear.Text))
                {
                    txtYear.Text = "e.g. 2024";
                    txtYear.ForeColor = Color.Gray;
                    selectedYear = "";
                    LoadMovies();
                }
            };

            txtDirector.TextChanged += (s, e) =>
            {
                selectedDirector = txtDirector.Text.ToLower();
                LoadMovies();
            };

            txtActor.TextChanged += (s, e) =>
            {
                selectedActor = txtActor.Text.ToLower();
                LoadMovies();
            };

            foreach (Control c in panelLeft.Controls)
            {
                if (c is RadioButton rb && rb.Tag != null)
                {
                    if (rb.Tag.ToString().StartsWith("Time"))
                        rb.CheckedChanged += (s, e) =>
                        {
                            if (rb.Checked)
                            {
                                currentTimeFilter = rb.Tag.ToString().Replace("Time_", "");
                                LoadMovies();
                            }
                        };

                    if (rb.Tag.ToString().StartsWith("Age"))
                        rb.CheckedChanged += (s, e) =>
                        {
                            if (rb.Checked)
                            {
                                currentAgeFilter = rb.Tag.ToString().Replace("Age_", "");
                                LoadMovies();
                            }
                        };
                }
            }
        }

        private void CustomerMovieListForm_Load(object sender, EventArgs e)
        {
            // Optional: Add logic for form load if needed
        }
    }

    public class Movie
    {
        public string Title { get; set; }
        public List<string> Showtimes { get; set; }
        public string AgeRating { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string StarActor { get; set; }
    }

    public static class MovieRepository
    {
        public static List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Title = "Snow White", AgeRating = "G", Genre = "Fantasy", ReleaseYear = 2025, Director = "Jane Doe", StarActor = "Lily James", Showtimes = new List<string>{ "10:00 AM", "2:00 PM", "6:00 PM" }},
                new Movie { Title = "Guns Akimbo", AgeRating = "R", Genre = "Action", ReleaseYear = 2020, Director = "Jason Howden", StarActor = "Daniel Radcliffe", Showtimes = new List<string>{ "12:00 PM", "4:00 PM", "8:00 PM" }},
                new Movie { Title = "Dune Part II", AgeRating = "PG-13", Genre = "Sci-Fi", ReleaseYear = 2023, Director = "Denis Villeneuve", StarActor = "Timothée Chalamet", Showtimes = new List<string>{ "3:30 PM", "7:00 PM" }},
                new Movie { Title = "Barbie", AgeRating = "PG", Genre = "Comedy", ReleaseYear = 2023, Director = "Greta Gerwig", StarActor = "Margot Robbie", Showtimes = new List<string>{ "1:00 PM", "5:00 PM" }},
                new Movie { Title = "Oppenheimer", AgeRating = "R", Genre = "Drama", ReleaseYear = 2023, Director = "Christopher Nolan", StarActor = "Cillian Murphy", Showtimes = new List<string>{ "11:00 AM", "4:30 PM" }}
            };
        }
    }
}