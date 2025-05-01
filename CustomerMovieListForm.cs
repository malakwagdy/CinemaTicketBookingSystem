using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static GUI_DB.DatabaseManager;

namespace GUI_DB
{
    public partial class CustomerMovieListForm : Form
    {
        private MainForm mainForm;
        private DateTimePicker dtpReservationDate; // Date Picker for reservation date
        private DatabaseManager dbManager; // Assuming this is a class for database operations


        // Filters
        private string currentAgeFilter = "All";
        private string selectedGenre = "All";
        // No currentTimeFilter used, remove if desired (CS0414 warning)
        // private string currentTimeFilter = "All";

        public CustomerMovieListForm(MainForm form)
        {
            mainForm = form ?? throw new ArgumentNullException(nameof(form));
            dbManager = new DatabaseManager();
            InitializeComponent(); // Creates btnBack, filterLayout, cmbGenre, panelReservations etc. AND dtpReservationDate

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

            // --- Setup & Add Date Picker ---
            ConfigureAndAddDatePicker(); // Encapsulate date picker setup

            // --- Add Other Filters ---
            var ageRatingLabels = new Dictionary<int, string>
            {
                { -1, "All" },
                { 10, "PG" },
                { 13, "PG-13" },
                { 17, "R" }
             };

            AddFilterControl("Genre", cmbGenre); // cmbGenre is instantiated in Designer

            // --- Populate Genre ComboBox ---
            PopulateGenreComboBox();

            // --- Initialize Events for filters ---
            // Check if container and cmbGenre exist
            if (panelFilterControlsContainer != null && cmbGenre != null && dtpReservationDate != null)
            {
                InitializeFilterEvents();
            }
            else
            {
                MessageBox.Show("Filter controls container, Genre ComboBox, or Date Picker not initialized properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // --- End Initialize Events ---

            // --- Initialize Reservations List ---
            if (lstReservations != null)
            {
                lstReservations.DrawMode = DrawMode.OwnerDrawFixed;
                lstReservations.DrawItem += LstReservations_DrawItem;
                lstReservations.SelectedIndexChanged += LstReservations_SelectedIndexChanged;
                LoadReservations();
            }
            else
            {
                MessageBox.Show("Reservations listbox not initialized properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // --- End Initialize Reservations List ---


            LoadMovies(); // Initial movie load
        }

        // --- Configure and Add Date Picker ---
        private void ConfigureAndAddDatePicker()
        {
            if (dtpReservationDate == null)
            {
                // If not instantiated in Designer, do it here (belt and braces)
                dtpReservationDate = new DateTimePicker();
                // Set properties again just in case
                dtpReservationDate.Font = new Font("Segoe UI", 9F);
                dtpReservationDate.Format = DateTimePickerFormat.Short;
                dtpReservationDate.Name = "dtpReservationDate";
                // Add any other styling from Designer here if needed
            }
            // Set MinDate to today and default Value to today
            dtpReservationDate.MinDate = DateTime.Today;
            dtpReservationDate.Value = DateTime.Today;

            // Add it to the layout
            AddFilterControl("Date", dtpReservationDate);

            // Optional: Add event handler if movie list should reload on date change
            // dtpReservationDate.ValueChanged += (s, e) => LoadMovies();
        }


        // --- Back Button Click Event Handler ---
        private void BtnBack_Click(object sender, EventArgs e)
        {
            mainForm?.OpenChildForm(new LogInPage(mainForm));
        }

        // --- Helper to Populate Genre ComboBox ---
        private void PopulateGenreComboBox()
        {
            if (cmbGenre == null) return;
            cmbGenre.Items.Clear();
            cmbGenre.Items.Add("All");
            cmbGenre.Items.Add("Action");
            cmbGenre.Items.Add("Comedy");
            cmbGenre.Items.Add("Drama");
            cmbGenre.Items.Add("Fantasy");
            cmbGenre.Items.Add("Sci-Fi");
            cmbGenre.SelectedItem = "All";
        }


        // --- Helper Methods for UI Construction ---
        private void AddFilterControl(string labelText, Control control)
        {
            if (panelFilterControlsContainer == null) return;

            Label label = new Label
            {
                Text = labelText + ":",
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Margin = new Padding(0, 15, 0, 3) // Space above label
            };

            var existingControls = panelFilterControlsContainer.Controls.OfType<Control>().ToList();
            int topPosition = existingControls.Any()
                              ? existingControls.Max(c => c.Bottom) + 5
                              : (this.btnBack != null ? this.btnBack.Bottom + 15 : panelFilterControlsContainer.Padding.Top);

            label.Location = new Point(panelFilterControlsContainer.Padding.Left, topPosition);
            panelFilterControlsContainer.Controls.Add(label); // Add label first

            control.Location = new Point(panelFilterControlsContainer.Padding.Left, label.Bottom + 3);
            control.Width = panelFilterControlsContainer.ClientSize.Width - panelFilterControlsContainer.Padding.Left - panelFilterControlsContainer.Padding.Right;
            panelFilterControlsContainer.Controls.Add(control); // Add control
        }

        private FlowLayoutPanel CreateRadioGroup(int[] options, string tagPrefix)
        {
            FlowLayoutPanel radioPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            bool first = true;
            foreach (int option in options)
            {
                RadioButton rb = new RadioButton
                {
                    Text = option.ToString(), // Display the numeric value as text
                    Tag = tagPrefix + option.ToString(), // Store the numeric value in the tag
                    ForeColor = Color.White,
                    AutoSize = true,
                    Checked = first,
                    Margin = new Padding(3, 0, 3, 5) // Add some spacing
                };
                radioPanel.Controls.Add(rb);
                first = false;
            }
            return radioPanel;
        }


        // --- Core Logic ---
        private void LoadMovies()
        {
            if (flowLayoutPanelMovies == null) return;
            flowLayoutPanelMovies.SuspendLayout(); // Suspend layout for performance
            flowLayoutPanelMovies.Controls.Clear();

            Movie[] movies = null;

            // Handle filtering by genre and numeric age rating
            if (selectedGenre != "All" && currentAgeFilter != "-1")
            {
                // Filter by both genre and numeric age rating
                if (int.TryParse(currentAgeFilter, out int ageRating))
                {
                    movies = dbManager
                        .getMoviesByGenre(selectedGenre)
                        .Where(movie => movie.AgeRating == ageRating)
                        .ToArray();
                }
                else
                {
                    movies = dbManager.getMoviesByGenre(selectedGenre); // Fallback
                }
            }
            else if (selectedGenre != "All")
            {
                // Filter by genre only
                movies = dbManager.getMoviesByGenre(selectedGenre);
            }
            else if (currentAgeFilter != "-1")
            {
                // Filter by numeric age rating only
                if (int.TryParse(currentAgeFilter, out int ageRating))
                {
                    movies = dbManager.getMoviesByAgeRating(ageRating);
                }
                else
                {
                    movies = dbManager.getAllMovies(); // Fallback
                }
            }
            else
            {
                // Fetch all movies if no filters are applied
                movies = dbManager.getAllMovies();
            }

            AddMoviesToFlowLayoutPanel(movies); // Populate the UI
        }

        private void AddMoviesToFlowLayoutPanel(Movie[] movies)
        {
            int availableWidth = flowLayoutPanelMovies.ClientSize.Width;
            if (flowLayoutPanelMovies.VerticalScroll.Visible)
            {
                availableWidth -= SystemInformation.VerticalScrollBarWidth;
            }
            availableWidth -= (flowLayoutPanelMovies.Padding.Left + flowLayoutPanelMovies.Padding.Right);
            availableWidth = Math.Max(20, availableWidth);

            foreach (DatabaseManager.Movie movie in movies)
            {
                Panel moviePanel = new Panel
                {
                    Width = availableWidth - 20, // Margin
                    Height = 100,
                    BackColor = Color.FromArgb(60, 60, 75),
                    Margin = new Padding(10),
                    Padding = new Padding(10)
                };
                Label lblTitle = new Label
                {
                    Text = $"{movie.Title} ({movie.AgeRating}, {movie.ReleaseDate.Year})",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Height = 28,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                Label lblDetails = new Label
                {
                    Text = $"Genre: {movie.Genre} | Director: {movie.Director}",
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

                var showtimes = dbManager.GethowtimesForMovie(movie.MovieID);
               
                if (showtimes != null && showtimes.Any())
                {
                    foreach (var showtime in showtimes)
                    {
                        LinkLabel linkShowtime = new LinkLabel
                        {
                            Text = showtime.startTime.ToString("hh:mm tt"),
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9F, FontStyle.Underline),
                            LinkColor = Color.SkyBlue,
                            ActiveLinkColor = Color.White,
                            VisitedLinkColor = Color.Plum,
                            Margin = new Padding(0, 0, 8, 4)
                        };
                        linkShowtime.Click += (s, e) =>
                        {
                            DateTime selectedDate = dtpReservationDate?.Value.Date ?? DateTime.Today; // Get the date part, default to today
                            OpenSeatingChart(movie.Title, showtime.startTime.ToString("hh:mm tt"), selectedDate); // Pass date
                        };
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

                // Add controls to moviePanel
                moviePanel.Controls.Add(showtimesPanel);
                moviePanel.Controls.Add(lblDetails);
                moviePanel.Controls.Add(lblTitle);

                flowLayoutPanelMovies.Controls.Add(moviePanel);
            }
            flowLayoutPanelMovies.ResumeLayout(true);
        }

        // *** MODIFIED Signature: Add DateTime parameter ***
        private void OpenSeatingChart(string movieTitle, string showtime, DateTime reservationDate)
        {
            if (mainForm != null && dtpReservationDate != null) // Also check dtpReservationDate exists
            {
                // *** Pass date to SeatingChartForm constructor ***
                SeatingChartForm seatingChart = new SeatingChartForm(mainForm, movieTitle, showtime, reservationDate);
                mainForm.OpenChildForm(seatingChart);
            }
            else
            {
                MessageBox.Show("Navigation unavailable. MainForm or Date Picker reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Filter Logic(Redundant) ---
        private bool PassesFilters(DatabaseManager.Movie movie)
        {
            // NOTE: Currently, date selected doesn't filter the *movie list* itself.
            bool ageOk = currentAgeFilter == "All" || movie.AgeRating.ToString() == currentAgeFilter;
            bool genreOk = selectedGenre == "All" || movie.Genre == selectedGenre;
            return ageOk && genreOk;
        }

        private void InitializeFilterEvents()
        {
            // Genre ComboBox
            if (cmbGenre != null)
            {
                cmbGenre.SelectedIndexChanged += (s, e) => {
                    selectedGenre = cmbGenre.SelectedItem?.ToString() ?? "All";
                    LoadMovies();
                };
            }

            // Define age rating options (numeric values mapped to their labels)
            var ageRatingLabels = new Dictionary<int, string>
             {
                    { -1, "All" },  // -1 represents "All" (no filter)
                    { 7, "G" },     // General Audience
                    { 13, "PG-13" },   // Parental Guidance
                    { 16, "PG-16" },// Parents Strongly Cautioned
                    { 18, "M" }     // Restricted
             };

            // Create a ComboBox for age ratings
            ComboBox cmbAgeRating = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75))))),
                Font = new Font("Segoe UI", 9F),           // Font style and size (same as Genre dropdown)
                Width = 150,                               // Adjust width to match Genre dropdown
                Margin = new Padding(0, 5, 0, 5)           // Add margin for spacing

            };

            // Add age ratings to the ComboBox
            foreach (var kvp in ageRatingLabels)
            {
                cmbAgeRating.Items.Add(new KeyValuePair<int, string>(kvp.Key, kvp.Value));
            }

            // Display the age rating label (e.g., "All", "G", "PG") in the ComboBox
            cmbAgeRating.DisplayMember = "Value";
            cmbAgeRating.ValueMember = "Key";

            // Set default selected item to "All"
            cmbAgeRating.SelectedIndex = 0;

            // Handle selection changes
            cmbAgeRating.SelectedIndexChanged += (s, e) =>
            {
                var selectedKeyValue = (KeyValuePair<int, string>)cmbAgeRating.SelectedItem;
                currentAgeFilter = selectedKeyValue.Key.ToString(); // Update the current filter
                LoadMovies(); // Reload movies based on the new filter
            };

            // Add the ComboBox to the filter panel
            AddFilterControl("Age Rating", cmbAgeRating);

            // DateTimePicker
            if (dtpReservationDate != null)
            {
                dtpReservationDate.ValueChanged += (s, e) =>
                {
                    LoadMovies(); // Reload movies when the date changes
                };
            }
        }

        private void OnAgeFilterChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked && rb.Tag != null)
            {
                string tag = rb.Tag.ToString();
                if (int.TryParse(tag, out int numericTag) || tag == "All")
                {
                    currentAgeFilter = tag; // Save as string (numeric or "All")
                    LoadMovies();
                }
            }
        }


        // --- Reservations Logic ---
        private void LoadReservations()
        {
            if (lstReservations == null) return;

            // Get the currently logged in user's email (you'll need to set this somewhere)
            string currentUserEmail = GlobalVariable.CurrentlyLoggedIN; // Replace with actual user email
          
            lstReservations.SuspendLayout();
            object selectedItem = lstReservations.SelectedItem;
            lstReservations.Items.Clear();

            // Use the DatabaseManager to get bookings
            var dbManager = new DatabaseManager();
            var bookings = dbManager.GetBookingsByUser(currentUserEmail)
                                  .OrderBy(b => b.bookingDate) // Changed from ReservationDate
                                  .ToList();

            if (!bookings.Any())
            {
                lblReservationsHeader.Text = "My Reservations (None)";
            }
            else
            {
                lblReservationsHeader.Text = $"My Reservations ({bookings.Count})";
                foreach (var booking in bookings)
                {
                    // Format the booking display text
                    string displayText = $"Booking #{booking.bookingID} - {booking.bookingDate.ToString("MMM dd, yyyy")} - ${booking.totalPrice}";
                    lstReservations.Items.Add(displayText);
                }

                if (selectedItem != null && lstReservations.Items.Contains(selectedItem))
                {
                    lstReservations.SelectedItem = selectedItem;
                }
            }
            lstReservations.ResumeLayout(true);
        }

        private void LstReservations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReservations.SelectedItem is Booking selectedBooking)
            {
                if (mainForm != null)
                {
                    BookingDetailsForm detailsForm = new BookingDetailsForm(mainForm, selectedBooking);
                    mainForm.OpenChildForm(detailsForm);
                }
                else
                {
                    MessageBox.Show("Navigation unavailable. MainForm reference is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lstReservations.ClearSelected(); // Deselect after navigating
            }
        }

        private void LstReservations_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            if (lb == null) return;

            Color itemBackgroundColor = lb.BackColor;
            Color itemForegroundColor = lb.ForeColor;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                itemBackgroundColor = Color.FromArgb(80, 80, 100);
                itemForegroundColor = Color.White;
            }

            e.Graphics.FillRectangle(new SolidBrush(itemBackgroundColor), e.Bounds);

            string text = (lb.Items[e.Index] is Booking booking)
                          ? booking.ToString() // Uses updated ToString() with date
                          : lb.Items[e.Index].ToString();

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            Rectangle textBounds = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top, e.Bounds.Width - 4, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, text, e.Font, textBounds, itemForegroundColor, flags);

            e.DrawFocusRectangle();
        }

        // --- Form Load Event Handler ---
        private void CustomerMovieListForm_Load_1(object sender, EventArgs e)
        {
            LoadReservations();
            LoadMovies();
        }

        
    }

    
    

   
}