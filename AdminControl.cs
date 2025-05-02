using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
// Remove the 'using static GUI_DB.DatabaseManager;' if you consistently create instances
// using static GUI_DB.DatabaseManager; // Can be removed if dbManager instance is always used

namespace GUI_DB
{
    public partial class AdminControl : Form
    {
        // Keep the in-memory representation for UI purposes (like showing available showtimes)
        // but rely on DB for definitive state during add/remove operations.
        private readonly Dictionary<string, HallInfo> hallOccupancy = new Dictionary<string, HallInfo>();
        private readonly List<string> allShowtimes = new List<string>
        {
            "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "6:00 PM", "8:00 PM"
        };
        private readonly MainForm mainForm;
        private DatabaseManager dbManager; // Keep a single instance or create as needed

        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            dbManager = new DatabaseManager(); // Initialize DatabaseManager instance

            LoadHallsDataFromDatabase(); // Load initial hall data (both DB and memory)

            cmbHalls.SelectedIndexChanged += CmbHalls_SelectedIndexChanged;
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click; // Ensure this handler is attached
            chkIsPremium.CheckedChanged += ChkIsPremium_CheckedChanged;

            UpdatePremiumRowsVisibility();
            dtpReleaseDate.Value = DateTime.Today;
        }

        // --- New Method: Load Hall Data from DB into both ComboBox and hallOccupancy ---
        private void LoadHallsDataFromDatabase()
        {
            cmbHalls.Items.Clear();
            hallOccupancy.Clear(); // Clear in-memory store before reloading

            try
            {
                var hallsFromDb = dbManager.getAllHalls(); // Get all halls from DB

                foreach (var hall in hallsFromDb)
                {
                    // Add to ComboBox for movie assignment
                    cmbHalls.Items.Add($"{hall.hallName} ({hall.screenType})");

                    // Rebuild the in-memory hallOccupancy dictionary
                    // NOTE: We don't have row/seat/premium info from getAllHalls() directly.
                    // If needed, HallInfo needs adjustment or DB needs to provide more details.
                    // For now, create a basic HallInfo. A better approach might be to fetch
                    // full details if HallInfo is critical beyond just existence/screen type.
                    // Let's assume we don't need full HallInfo details for hallOccupancy here,
                    // or we only populate what we can.
                    // **This part highlights a potential mismatch between HallInfo and getAllHalls()**
                    // For simplicity, let's assume HallInfo isn't fully populated here,
                    // primarily used for tracking movie assignments.
                    if (!hallOccupancy.ContainsKey(hall.hallName))
                    {
                        // Example: Create HallInfo with placeholder data or fetch more if needed
                        // For now, screenType is the only common info readily available.
                        // The row/seat counts would ideally come from the DB during this load.
                        // Let's create it with placeholder values, assuming they aren't critical
                        // for the hall removal logic itself.
                        hallOccupancy.Add(hall.hallName, new HallInfo(hall.screenType, 0, 0, false, 0));
                        // TODO: Enhance LoadHallsDataFromDatabase to get full HallInfo if required elsewhere.
                    }

                    // TODO: Populate hallInfo.Movies by querying showtimes for each hall if needed here.
                    // This could be performance-intensive if done on every load.
                    // It might be better to load showtimes only when a hall is selected (as done in CmbHalls_SelectedIndexChanged).
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading halls: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Reset selection in ComboBoxes
            cmbHalls.SelectedIndex = -1;
            cmbShowtimes.Items.Clear();
            cmbShowtimes.SelectedIndex = -1;
        }


        private void UpdatePremiumRowsVisibility()
        {
            bool showPremiumRowsInput = !chkIsPremium.Checked;
            lblPremiumRows.Visible = showPremiumRowsInput;
            txtPremiumRows.Visible = showPremiumRowsInput;
            if (!showPremiumRowsInput) { txtPremiumRows.Clear(); }
        }

        private void ChkIsPremium_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePremiumRowsVisibility();
        }

        // --- Modified LoadHallsForMovieAssignment to use the unified loading method ---
        private void LoadHallsForMovieAssignment()
        {
            // Now just calls the main loading method
            LoadHallsDataFromDatabase();
        }


        // --- CmbHalls_SelectedIndexChanged: Load showtimes based on selected hall ---
        private void CmbHalls_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedHallItem))
            {
                cmbShowtimes.Items.Clear();
                cmbShowtimes.SelectedIndex = -1;
                return;
            }

            string selectedHallName = selectedHallItem.Split('(')[0].Trim();
            // Use the DatabaseManager to get the actual showtimes for the selected hall
            LoadAvailableShowtimesFromDb(selectedHallName);
        }

        // --- Modified LoadAvailableShowtimes to query the DB ---
        private void LoadAvailableShowtimesFromDb(string hallName)
        {
            cmbShowtimes.Items.Clear();
            cmbShowtimes.SelectedIndex = -1; // Reset selection

            try
            {
                int hallId = dbManager.GetHallIDByName(hallName);
                if (hallId <= 0) return; // Hall not found in DB

                // We need a way to get showtimes *per hall* from the DB Manager
                // Assuming GetShowtimesForMovie is NOT what we want here.
                // We need a hypothetical GetShowtimesForHall(hallId) method.
                // Let's simulate this or adjust logic based on available methods.

                // **Alternative using hallOccupancy (if kept up-to-date):**
                // If hallOccupancy accurately reflects schedules, the original logic can work.
                // However, relying solely on DB is safer.

                // **Let's assume we *need* to query the DB for scheduled times for this hall**
                // We don't have a direct DB method for this in the provided DatabaseManager.
                // OPTION 1: Add a GetShowtimesByHallID to DatabaseManager.
                // OPTION 2: Simulate for now by using the original logic with hallOccupancy,
                //           acknowledging it depends on hallOccupancy being accurate.

                // --- Using original logic (dependent on hallOccupancy) ---
                if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
                {
                    // Get scheduled times *from the in-memory representation*
                    var scheduledShowtimes = hallInfo.Movies
                                                 .Select(m => m.Showtime.ToString("hh:mm tt")) // Format consistently
                                                 .ToHashSet();

                    var availableShowtimes = allShowtimes
                        .Where(st => !scheduledShowtimes.Contains(st)) // Compare strings
                        .ToList();

                    foreach (var showtime in availableShowtimes)
                    {
                        cmbShowtimes.Items.Add(showtime); // Add as string "hh:mm tt"
                    }
                }
                // --- End using original logic ---

                // **Recommended approach would be a DB query here.**
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading showtimes for {hallName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- BtnAddMovie_Click ---
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();

            // --- Field Validation ---
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtimeStr = cmbShowtimes.SelectedItem?.ToString();
            int ageRating = (cmbAgeRating.SelectedItem is string ageStr && int.TryParse(ageStr, out int parsedAge)) ? parsedAge : 0; // Safer parsing
            string director = txtDirector.Text.Trim();
            string actorsInput = txtActors.Text.Trim();
            string genre = cmbGenre.SelectedItem?.ToString();
            DateTime releaseDate = dtpReleaseDate.Value.Date; // Use only Date part

            if (string.IsNullOrEmpty(movieTitle) || string.IsNullOrEmpty(selectedHallItem) || ageRating == 0 ||
                string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(director) || string.IsNullOrEmpty(actorsInput) ||
                string.IsNullOrEmpty(selectedShowtimeStr))
            {
                MessageBox.Show("Please fill out all movie fields and select all options, including a valid showtime.", "Input Error", MessageBoxButtons.OK, Warning);
                return;
            }

            string selectedHallName = selectedHallItem.Split('(')[0].Trim();
            string[] actors = actorsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(actor => actor.Trim())
                                          .Where(actor => !string.IsNullOrEmpty(actor)) // Ensure no empty strings after split/trim
                                          .ToArray();

            if (actors.Length == 0)
            {
                MessageBox.Show("Please enter at least one valid actor name.", "Input Error", MessageBoxButtons.OK, Warning);
                return;
            }

            if (!DateTime.TryParse(selectedShowtimeStr, out DateTime parsedShowtime)) // Use TryParse
            {
                MessageBox.Show("Invalid showtime selected.", "Input Error", MessageBoxButtons.OK, Warning);
                return;
            }

            try
            {
                int selectedHallID = dbManager.GetHallIDByName(selectedHallName);
                if (selectedHallID <= 0)
                {
                    MessageBox.Show($"Selected hall '{selectedHallName}' does not exist in the database.", "Data Error", MessageBoxButtons.OK, Error);
                    return;
                }

                // Optional: Check for showtime conflicts in the DB before adding
                // (Requires a suitable DatabaseManager method)

                // Add movie and related data to the database
                AddMovieToDatabase(movieTitle, selectedHallID, parsedShowtime, ageRating, director, actors, genre, releaseDate);

                // Update in-memory representation AFTER successful DB operation
                if (hallOccupancy.TryGetValue(selectedHallName, out HallInfo hallInfo))
                {
                    hallInfo.Movies.Add((Movie: movieTitle, Showtime: parsedShowtime));
                }
                else
                {
                    // This case shouldn't happen if LoadHallsDataFromDatabase is correct, but handle defensively
                    Console.WriteLine($"Warning: Hall '{selectedHallName}' not found in hallOccupancy dictionary after movie add.");
                    // Optionally, reload hall data here if needed
                }


                MessageBox.Show($"Movie '{movieTitle}' added to '{selectedHallName}' at '{selectedShowtimeStr}' successfully!", "Success", MessageBoxButtons.OK, Information);
                ClearMovieFields();
                // Refresh available showtimes for the *current* hall, as one was just taken
                LoadAvailableShowtimesFromDb(selectedHallName);
                // No need to reload all halls unless movie addition changes hall availability criteria (it doesn't here)
                // LoadHallsForMovieAssignment(); // Generally not needed here
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding movie: {ex.Message}", "Database Error", MessageBoxButtons.OK, Error);
            }
        }

        // --- AddMovieToDatabase (Helper) ---
        private void AddMovieToDatabase(string title, int hallID, DateTime showtime, int ageRating, string director, string[] actors, string genre, DateTime releaseDate)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();

            // Add movie, get its ID
            int currentMovieID = dbManager.AddMovie(director, title, genre, ageRating, releaseDate.Date); // Ensure Date only

            if (currentMovieID <= 0)
            {
                throw new Exception("Failed to add movie to the database."); // Or handle more gracefully
            }

            // Add showtime
            string addShowtimeResult = dbManager.AddShowtime(showtime, currentMovieID, hallID);
            if (!addShowtimeResult.Contains("successfully")) // Check result string
            {
                throw new Exception($"Failed to add showtime: {addShowtimeResult}");
            }


            // Add actors (assuming AddMoviesActors handles potential failures internally or returns status)
            dbManager.AddMoviesActors(currentMovieID, actors);
        }

        // --- BtnRemoveMovie_Click ---
        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();

            string movieTitle = txtMovieTitle.Text.Trim();
            DateTime selectedReleaseDate = dtpReleaseDate.Value.Date; // Use Date only

            if (string.IsNullOrEmpty(movieTitle))
            {
                MessageBox.Show("Please enter the title of the movie to remove.", "Input Error", MessageBoxButtons.OK, Warning);
                txtMovieTitle.Focus();
                return;
            }

            // Confirmation
            var confirmResult = MessageBox.Show(
                $"Are you sure you want to remove the movie '{movieTitle}' (Release Date: {selectedReleaseDate:yyyy-MM-dd})?\n" +
                $"This will remove the movie, its showtimes, and related records.",
                "Confirm Movie Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            try
            {
                string result = RemoveMovieFromDatabase(movieTitle, selectedReleaseDate);
                MessageBox.Show(result, "Movie Removal", MessageBoxButtons.OK, result.Contains("successfully") ? Information : Error);

                if (result.Contains("successfully"))
                {
                    ClearMovieFields();
                    // Reload halls/showtimes as movie removal might affect scheduling
                    LoadHallsForMovieAssignment();
                    // Also need to update hallOccupancy in-memory store potentially
                    // (Requires iterating or more complex logic to find which hall/showtime was removed)
                    // Simplest is often to reload all hall data.
                    LoadHallsDataFromDatabase(); // Reload to reflect potential changes
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while removing the movie: {ex.Message}", "Error", MessageBoxButtons.OK, Error);
            }
        }

        // --- RemoveMovieFromDatabase (Helper) ---
        private string RemoveMovieFromDatabase(string title, DateTime releaseDate)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();
            int currentMovieID = dbManager.getMovieID(title, releaseDate.Date); // Get movie ID by title and date
            dbManager.DeleteMovieActor(currentMovieID); // Pass Date only
            dbManager.DeleteShowtimeByMovieID(currentMovieID); // Pass Date only
            dbManager.DeleteTicketByMovieID(currentMovieID); // Pass Date only
            return dbManager.DeleteMovie(title, releaseDate.Date); // Pass Date only
        }


        // --- BtnAddHall_Click ---
        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();

            // --- Input Validation ---
            string hallName = txtHallName.Text.Trim();
            string screenType = cmbScreenType.SelectedItem?.ToString();
            bool isFullyPremium = chkIsPremium.Checked;

            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter hall name.", "Input Error", MessageBoxButtons.OK, Warning); txtHallName.Focus(); return; }
            // Check DB for existing name instead of just memory
            if (dbManager.GetHallIDByName(hallName) > 0) { MessageBox.Show($"Hall '{hallName}' already exists in the database.", "Input Error", MessageBoxButtons.OK, Error); txtHallName.Focus(); return; }
            if (string.IsNullOrEmpty(screenType)) { MessageBox.Show("Please select screen type.", "Input Error", MessageBoxButtons.OK, Warning); cmbScreenType.Focus(); return; }
            if (!int.TryParse(txtTotalRows.Text, out int totalRows) || totalRows <= 0) { MessageBox.Show("Invalid Total Rows (must be positive number).", "Input Error", MessageBoxButtons.OK, Warning); txtTotalRows.Focus(); return; }
            if (!int.TryParse(txtSeatsPerRow.Text, out int seatsPerRow) || seatsPerRow <= 0) { MessageBox.Show("Invalid Seats Per Row (must be positive number).", "Input Error", MessageBoxButtons.OK, Warning); txtSeatsPerRow.Focus(); return; }

            int premiumRowCount = 0;
            if (!isFullyPremium && txtPremiumRows.Visible) // Only parse if visible and not fully premium
            {
                if (!int.TryParse(txtPremiumRows.Text, out int parsedPremiumRows) || parsedPremiumRows < 0)
                {
                    MessageBox.Show("Invalid Premium Rows (must be zero or positive).", "Input Error", MessageBoxButtons.OK, Warning);
                    txtPremiumRows.Focus();
                    return;
                }
                if (parsedPremiumRows > totalRows)
                {
                    MessageBox.Show("Premium Rows cannot exceed Total Rows.", "Input Error", MessageBoxButtons.OK, Warning);
                    txtPremiumRows.Focus();
                    return;
                }
                premiumRowCount = parsedPremiumRows;
            }
            else if (isFullyPremium)
            {
                premiumRowCount = totalRows; // If fully premium, conceptually all rows are premium
            }


            try
            {
                // Call the database method to add the hall and its seats
                string result = AddHallToDatabase(hallName, screenType, totalRows, seatsPerRow, isFullyPremium, premiumRowCount);

                MessageBox.Show(result, "Add Hall", MessageBoxButtons.OK, result.Contains("Successfully created") ? Information : Error);

                if (result.Contains("Successfully created"))
                {
                    // Update in-memory store AFTER successful DB operation
                    var newHallInfo = new HallInfo(screenType, totalRows, seatsPerRow, isFullyPremium, isFullyPremium ? 0 : premiumRowCount); // Use correct premium count for HallInfo
                    hallOccupancy.Add(hallName, newHallInfo);

                    ClearHallFields();
                    LoadHallsForMovieAssignment(); // Refresh the hall list in the UI
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the hall: {ex.Message}", "Error", MessageBoxButtons.OK, Error);
            }
        }


        // --- AddHallToDatabase (Helper) ---
        private string AddHallToDatabase(string hallName, string screenType, int totalRows, int seatsPerRow, bool isFullyPremium, int premiumRowCount)
        {
            // Ensure dbManager is ready
            if (dbManager == null) dbManager = new DatabaseManager();
            // Use the stored procedure wrapper method in DatabaseManager
            return dbManager.AddHall(hallName, screenType, totalRows, seatsPerRow, isFullyPremium, premiumRowCount);
        }


        // --- BtnRemoveHall_Click (REVISED) ---
        //private void BtnRemoveHall_Click(object sender, EventArgs e)
        //{
        //    string hallName = txtHallName.Text.Trim();
        //    if (string.IsNullOrEmpty(hallName))
        //    {
        //        MessageBox.Show("Please enter the name of the hall to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txtHallName.Focus();
        //        return;
        //    }

        //    // Ensure dbManager is ready
        //    if (dbManager == null) dbManager = new DatabaseManager();

        //    int hallID = 0;
        //    try
        //    {
        //        // 1. Check if hall exists in the DATABASE and get its ID
        //        hallID = dbManager.GetHallIDByName(hallName);

        //        if (hallID <= 0) // Hall not found in the database
        //        {
        //            MessageBox.Show($"Hall '{hallName}' not found in the database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // 2. Confirmation (Consider checking DB for showtimes if critical)
        //        //    For now, we use the original confirmation prompt style. A DB check would be safer.
        //        //    Example DB Check (requires new DatabaseManager method like HasShowtimes(hallID)):
        //        //    bool hasMovies = dbManager.HasShowtimes(hallID);
        //        //    if (hasMovies) { // Prompt }

        //        // Using simple confirmation for now:
        //        var confirmResult = MessageBox.Show(
        //            $"Are you sure you want to remove the hall '{hallName}' (ID: {hallID})?\n" +
        //            $"This will remove the hall and potentially associated showtimes/tickets (depending on DB constraints).",
        //            "Confirm Hall Removal",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Warning);

        //        if (confirmResult == DialogResult.No)
        //        {
        //            return; // User cancelled
        //        }

        //        // 3. Perform Deletion in Database using the obtained Hall ID
        //        string deleteResult = dbManager.DeleteHall(hallID);

        //        // 4. Process Result
        //        if (deleteResult.Contains("successfully")) // Check success message from DB method
        //        {
        //            // Remove from in-memory dictionary AFTER successful DB deletion
        //            if (hallOccupancy.ContainsKey(hallName))
        //            {
        //                hallOccupancy.Remove(hallName);
        //            }

        //            MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            ClearHallFields();
        //            LoadHallsForMovieAssignment(); // Reload the hall list in the UI
        //        }
        //        else
        //        {
        //            // Show the specific error message from DeleteHall
        //            MessageBox.Show($"Failed to remove hall '{hallName}'. Reason: {deleteResult}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (SqlException sqlEx) // Catch specific SQL errors if helpful
        //    {
        //        // Check for foreign key constraint errors (e.g., if showtimes exist and aren't cascade deleted)
        //        if (sqlEx.Number == 547) // Foreign key violation number
        //        {
        //            MessageBox.Show($"Cannot remove hall '{hallName}' because it still has scheduled showtimes or related records that must be removed first.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        else
        //        {
        //            MessageBox.Show($"A database error occurred while removing hall '{hallName}': {sqlEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex) // Catch general errors
        //    {
        //        MessageBox.Show($"An error occurred while removing hall '{hallName}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (string.IsNullOrEmpty(hallName))
            {
                MessageBox.Show("Please enter hall name to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHallName.Focus();
                return;
            }

            if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
            {
                if (hallInfo.Movies.Any())
                {
                    var confirmResult = MessageBox.Show(
                        $"Hall '{hallName}' has {hallInfo.Movies.Count} movie(s) scheduled.\n" +
                        $"Removing the hall will also remove these schedules.\n\n" +
                        $"Are you sure you want to remove this hall?",
                        "Confirm Hall Removal",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.No) { return; }
                }
                hallOccupancy.Remove(hallName);
                RemoveHallFromDatabase(hallName);
                MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearHallFields();
                LoadHallsForMovieAssignment();
            }
            else
            {
                MessageBox.Show($"Hall '{hallName}' not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveHallFromDatabase(string hallName)
        {
            dbManager = new DatabaseManager();
            int currentHallID = dbManager.GetHallIDByName(hallName);
            dbManager.DeleteShowtime(currentHallID);
            dbManager.DeleteTicketByHallID(currentHallID);
            dbManager.DeleteSeat(currentHallID);
            dbManager.DeleteHall(currentHallID);
        }

        // --- UI Clearing Methods ---
        private void ClearMovieFields()
        {
            txtMovieTitle.Clear();
            cmbAgeRating.SelectedIndex = -1;
            cmbAgeRating.Text = "";
            txtDirector.Clear();
            txtActors.Clear();
            cmbGenre.SelectedIndex = -1;
            cmbGenre.Text = "";
            dtpReleaseDate.Value = DateTime.Today;
            cmbHalls.SelectedIndex = -1; // Keep hall selection if desired, or reset like this
            cmbShowtimes.Items.Clear();
            cmbShowtimes.SelectedIndex = -1;
            cmbShowtimes.Text = "";
        }

        private void ClearHallFields()
        {
            txtHallName.Clear();
            cmbScreenType.SelectedIndex = -1;
            cmbScreenType.Text = ""; // Clear text too
            txtTotalRows.Clear();
            txtSeatsPerRow.Clear();
            chkIsPremium.Checked = false; // Resets premium rows visibility via event
            txtPremiumRows.Clear(); // Explicitly clear premium rows text
            txtHallName.Focus();
        }

        private void AdminControl_Load(object sender, EventArgs e)
        {
            // Can add initialization logic here if needed when the form loads
            LoadComboBoxDefaults();
        }

        // Helper to load default items into combo boxes that don't change often
        private void LoadComboBoxDefaults()
        {
            // Example: Populate Age Rating ComboBox
            cmbAgeRating.Items.Clear();
            cmbAgeRating.Items.AddRange(new object[] { "3", "7", "12", "15", "18" }); // Or load from DB/config

            // Example: Populate Genre ComboBox
            cmbGenre.Items.Clear();
            cmbGenre.Items.AddRange(new object[] { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Thriller", "Romance", "Animation", "Family" }); // Or load

            // Example: Populate Screen Type ComboBox
            cmbScreenType.Items.Clear();
            cmbScreenType.Items.AddRange(new object[] { "Standard", "IMAX", "VIP", "3D" }); // Or load
        }

        // Constants for MessageBox clarity (optional but good practice)
        const MessageBoxButtons OK = MessageBoxButtons.OK;
        const MessageBoxIcon Information = MessageBoxIcon.Information;
        const MessageBoxIcon Warning = MessageBoxIcon.Warning;
        const MessageBoxIcon Error = MessageBoxIcon.Error;

    } // End of AdminControl class

    // HallInfo class remains the same
    public class HallInfo
    {
        public List<(string Movie, DateTime Showtime)> Movies { get; set; } = new List<(string, DateTime)>();
        public string ScreenType { get; set; }
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
        public bool IsFullyPremium { get; set; }
        public int PremiumRowCount { get; set; } // Number of rows designated as premium (if not fully premium)

        public HallInfo(string screenType, int totalRows, int seatsPerRow, bool isFullyPremium, int premiumRowCount)
        {
            ScreenType = screenType;
            TotalRows = totalRows;
            SeatsPerRow = seatsPerRow;
            IsFullyPremium = isFullyPremium;
            // If fully premium, PremiumRowCount maybe should be TotalRows or 0 depending on interpretation.
            // Let's stick to the original: it's the count specified *if not* fully premium.
            PremiumRowCount = isFullyPremium ? 0 : premiumRowCount;
            Movies = new List<(string Movie, DateTime Showtime)>(); // Initialize movie list
        }

        public override string ToString()
        {
            string details = $"{ScreenType}, {TotalRows}x{SeatsPerRow} seats";
            if (IsFullyPremium)
            {
                details += ", Fully Premium";
            }
            // Show premium row count only if > 0 and NOT fully premium
            else if (PremiumRowCount > 0)
            {
                details += $", Premium: {PremiumRowCount} row{(PremiumRowCount == 1 ? "" : "s")}";
            }
            return details;
        }
    }
}