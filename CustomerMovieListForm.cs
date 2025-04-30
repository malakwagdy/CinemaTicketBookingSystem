using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class CustomerMovieListForm : Form
    {
        private MainForm mainForm;

        // Filters
        private string currentTimeFilter = "All"; // Keep for potential future use
        private string currentAgeFilter = "All";
        private string selectedGenre = "All"; // *** ADDED BACK ***

        public CustomerMovieListForm(MainForm form)
        {
            mainForm = form;
            InitializeComponent(); // Creates btnBack, filterLayout, cmbGenre etc.

            // --- Hook up Back Button Event ---
            if (this.btnBack != null)
            {
                this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            }
            else
            {
                MessageBox.Show("Back button was not initialized correctly.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // --- End Hook up Back Button ---

            // --- Add Filters ---
            AddFilterControl("Age Rating", CreateRadioGroup(new[] { "All", "G", "PG", "PG-13", "R" }, "Age_"));
            AddFilterControl("Genre", cmbGenre); // *** ADDED BACK - Use the instance from Designer ***

            // --- Populate Genre ComboBox --- // *** ADDED BACK ***
            cmbGenre.Items.Add("All");
            cmbGenre.Items.Add("Action");
            cmbGenre.Items.Add("Comedy");
            cmbGenre.Items.Add("Drama");
            cmbGenre.Items.Add("Fantasy");
            cmbGenre.Items.Add("Sci-Fi");
            cmbGenre.SelectedItem = "All"; // Set default selection

            // --- Initialize Events for filters ---
            // Check cmbGenre exists along with filterLayout
            if (filterLayout != null && cmbGenre != null)
            {
                InitializeFilterEvents();
            }
            else
            {
                MessageBox.Show("Filter controls are not initialized properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadMovies(); // Initial movie load
        }

        // --- Back Button Click Event Handler (Keep as is) ---
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
            {
                mainForm.OpenChildForm(new CinemaListForm(mainForm));
            }
            else
            {
                MessageBox.Show("Navigation unavailable. MainForm reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Helper Methods for UI Construction (Keep AddFilterControl & CreateRadioGroup) ---

        private void AddFilterControl(string labelText, Control control)
        {
            Label label = new Label
            {
                Text = labelText + ":",
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 3) // Add space above label
            };

            // Position below the last control added in the filter panel
            int topPosition = filterLayout.Controls.OfType<Control>().Any()
                              ? filterLayout.Controls.OfType<Control>().Max(c => c.Bottom) + 5 // Below last control
                              : (this.btnBack != null ? this.btnBack.Bottom + 15 : filterLayout.Padding.Top); // Below back button

            label.Location = new Point(filterLayout.Padding.Left, topPosition);
            control.Location = new Point(filterLayout.Padding.Left, label.Bottom + 3); // Position control below label
            // Make control use most of the panel width, respecting padding
            control.Width = filterLayout.ClientSize.Width - filterLayout.Padding.Left - filterLayout.Padding.Right;

            filterLayout.Controls.Add(label);
            filterLayout.Controls.Add(control);
        }

        private FlowLayoutPanel CreateRadioGroup(string[] options, string tagPrefix)
        {
            FlowLayoutPanel radioPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false
            };

            bool first = true;
            foreach (string option in options)
            {
                RadioButton rb = new RadioButton
                {
                    Text = option,
                    Tag = tagPrefix + option,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Checked = first
                };
                radioPanel.Controls.Add(rb);
                first = false;
            }
            return radioPanel;
        }


        // --- Core Logic (LoadMovies, OpenSeatingChart remain largely the same) ---

        private void LoadMovies()
        {
            flowLayoutPanelMovies.SuspendLayout();
            flowLayoutPanelMovies.Controls.Clear();
            List<Movie> movies = MovieRepository.GetMovies();

            int availableWidth = flowLayoutPanelMovies.ClientSize.Width - flowLayoutPanelMovies.Padding.Left - flowLayoutPanelMovies.Padding.Right;
            if (flowLayoutPanelMovies.VerticalScroll.Visible)
            {
                availableWidth -= SystemInformation.VerticalScrollBarWidth;
            }
            availableWidth = Math.Max(20, availableWidth);

            foreach (var movie in movies)
            {
                // Apply Age Rating AND Genre filters
                if (!PassesFilters(movie)) continue;

                // --- Movie Panel Creation (Identical to previous version) ---
                Panel moviePanel = new Panel
                {
                    Width = availableWidth - 20,
                    Height = 100,
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
                    AutoSize = false,
                    Height = 28,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                Label lblDetails = new Label
                {
                    Text = $"Genre: {movie.Genre} | Director: {movie.Director} | Star: {movie.StarActor}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.Gray,
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Height = 22,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                FlowLayoutPanel showtimesPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    Padding = new Padding(0, 5, 0, 0)
                };

                if (movie.Showtimes != null && movie.Showtimes.Any())
                {
                    foreach (var showtime in movie.Showtimes)
                    {
                        LinkLabel linkShowtime = new LinkLabel
                        {
                            Text = showtime,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9F, FontStyle.Underline),
                            LinkColor = Color.SkyBlue,
                            ActiveLinkColor = Color.White,
                            VisitedLinkColor = Color.Plum,
                            Margin = new Padding(0, 0, 8, 4)
                        };
                        linkShowtime.Click += (s, e) => OpenSeatingChart(movie.Title, showtime);
                        showtimesPanel.Controls.Add(linkShowtime);
                    }
                }
                else
                {
                    Label noShowtimesLabel = new Label
                    {
                        Text = "No showtimes available",
                        ForeColor = Color.DarkGray,
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        AutoSize = true
                    };
                    showtimesPanel.Controls.Add(noShowtimesLabel);
                }

                moviePanel.Controls.Add(showtimesPanel);
                moviePanel.Controls.Add(lblDetails);
                moviePanel.Controls.Add(lblTitle);

                flowLayoutPanelMovies.Controls.Add(moviePanel);
            }
            flowLayoutPanelMovies.ResumeLayout();
        }


        private void OpenSeatingChart(string movieTitle, string showtime)
        {
            if (mainForm != null)
            {
                SeatingChartForm seatingChart = new SeatingChartForm(mainForm, movieTitle, showtime);
                mainForm.OpenChildForm(seatingChart);
            }
            else
            {
                MessageBox.Show("Navigation unavailable. MainForm reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Filter Logic (Updated) ---

        private bool PassesFilters(Movie movie)
        {
            // Time filter (if used)
            // bool timeOk = currentTimeFilter == "All";

            // Age Rating Filter
            bool ageOk = currentAgeFilter == "All" || movie.AgeRating == currentAgeFilter;

            // Genre Filter // *** ADDED BACK ***
            bool genreOk = selectedGenre == "All" || movie.Genre == selectedGenre;

            // Return result based on active filters
            return ageOk && genreOk; // && timeOk;
        }

        // --- Event Handlers for Filters (Updated) ---

        private void InitializeFilterEvents()
        {
            // Genre ComboBox Event Handler // *** ADDED BACK ***
            cmbGenre.SelectedIndexChanged += (s, e) =>
            {
                selectedGenre = cmbGenre.SelectedItem?.ToString() ?? "All"; // Handle null selection
                LoadMovies(); // Reload movies when genre changes
            };

            // Radio Buttons (Age Rating) - Logic remains the same
            Action<Control> setupRadioEvents = null;
            setupRadioEvents = (parent) => {
                foreach (Control c in parent.Controls)
                {
                    if (c is RadioButton rb && rb.Tag != null)
                    {
                        string tag = rb.Tag.ToString();
                        if (tag.StartsWith("Age_"))
                        {
                            rb.CheckedChanged -= OnAgeFilterChanged; // Prevent multiple subscriptions
                            rb.CheckedChanged += OnAgeFilterChanged;
                        }
                    }
                    else if (c.HasChildren)
                    {
                        setupRadioEvents(c); // Recursively check containers
                    }
                }
            };
            setupRadioEvents(this.filterLayout); // Search within the filter panel
        }

        // Specific handler for Age filter radio buttons (Keep as is)
        private void OnAgeFilterChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked && rb.Tag != null)
            {
                string tag = rb.Tag.ToString();
                if (tag.StartsWith("Age_"))
                {
                    currentAgeFilter = tag.Replace("Age_", "");
                    LoadMovies();
                }
            }
        }

        // --- Form Load Event Handler (Keep as is) ---
        private void CustomerMovieListForm_Load_1(object sender, EventArgs e)
        {
            // No specific logic needed here now
        }
    }

    // --- Data Classes (Keep as is) ---
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
                new Movie { Title = "Oppenheimer", AgeRating = "R", Genre = "Drama", ReleaseYear = 2023, Director = "Christopher Nolan", StarActor = "Cillian Murphy", Showtimes = new List<string>{ "11:00 AM", "4:30 PM" }},
                 new Movie { Title = "Poor Things", AgeRating = "R", Genre = "Comedy", ReleaseYear = 2023, Director = "Yorgos Lanthimos", StarActor = "Emma Stone", Showtimes = new List<string>{ "6:30 PM", "9:00 PM" }},
                 new Movie { Title = "The Matrix", AgeRating = "R", Genre = "Sci-Fi", ReleaseYear = 1999, Director = "Wachowskis", StarActor = "Keanu Reeves", Showtimes = new List<string>{ "8:30 PM" }},
                 new Movie { Title = "Finding Nemo", AgeRating = "G", Genre = "Comedy", ReleaseYear = 2003, Director = "Andrew Stanton", StarActor = "Albert Brooks", Showtimes = null },
            };
        }
    }
}