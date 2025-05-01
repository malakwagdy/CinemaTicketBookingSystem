// AdminControl.cs - Full Code

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI_DB
{
    // Helper class to store detailed information about a hall
    public class HallInfo
    {
        public List<(string Movie, string Showtime)> Movies { get; set; } = new List<(string, string)>();
        public string ScreenType { get; set; }
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
        public bool IsFullyPremium { get; set; }
        public int? PremiumRowCount { get; set; }

        public HallInfo(string screenType, int totalRows, int seatsPerRow, bool isFullyPremium, int? premiumRowCount)
        {
            ScreenType = screenType;
            TotalRows = totalRows;
            SeatsPerRow = seatsPerRow;
            IsFullyPremium = isFullyPremium;
            PremiumRowCount = isFullyPremium ? null : premiumRowCount;
        }

        public override string ToString()
        {
            string details = $"{TotalRows}x{SeatsPerRow}";
            if (IsFullyPremium)
            {
                details += ", Fully Premium";
            }
            else if (PremiumRowCount.HasValue && PremiumRowCount.Value > 0)
            {
                details += $", Premium: {PremiumRowCount.Value} row{(PremiumRowCount.Value == 1 ? "" : "s")}";
            }
            return $"{ScreenType}, {details}";
        }
    }

    public partial class AdminControl : Form
    {
        private Dictionary<string, HallInfo> hallOccupancy = new Dictionary<string, HallInfo>();
        private List<string> allShowtimes = new List<string>
        {
            "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "6:00 PM", "8:00 PM"
        };
        private MainForm mainForm;

        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Initialize Default Halls (Example Data)
            hallOccupancy.Add("Hall 1", new HallInfo("2D", 10, 15, false, 3));
            hallOccupancy.Add("Hall 2", new HallInfo("3D", 12, 18, true, null));
            hallOccupancy.Add("Hall 3", new HallInfo("IMAX", 8, 12, false, 0));
            hallOccupancy.Add("Hall 4", new HallInfo("IMAX", 9, 14, false, 9));

            LoadHallsForMovieAssignment();

            // Hook up event handlers
            cmbHalls.SelectedIndexChanged += CmbHalls_SelectedIndexChanged;
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click;
            chkIsPremium.CheckedChanged += ChkIsPremium_CheckedChanged;

            UpdatePremiumRowsVisibility();
        }

        // --- UI Update Logic ---
        private void UpdatePremiumRowsVisibility()
        {
            bool showPremiumRowsInput = !chkIsPremium.Checked;
            lblPremiumRows.Visible = showPremiumRowsInput;
            txtPremiumRows.Visible = showPremiumRowsInput;
            if (!showPremiumRowsInput)
            {
                txtPremiumRows.Clear();
            }
        }

        private void ChkIsPremium_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePremiumRowsVisibility();
        }

        // --- Hall Loading Logic ---
        private void LoadHallsForMovieAssignment()
        {
            cmbHalls.Items.Clear();
            var availableHalls = hallOccupancy
                .Where(kvp => kvp.Value.Movies.Count < allShowtimes.Count)
                .OrderBy(kvp => kvp.Key);
            foreach (var kvp in availableHalls)
            {
                cmbHalls.Items.Add($"{kvp.Key} ({kvp.Value.ScreenType})");
            }
            cmbHalls.SelectedIndex = -1;
            cmbShowtimes.Items.Clear();
            cmbShowtimes.SelectedIndex = -1;
        }

        // --- Showtime Loading Logic ---
        private void CmbHalls_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedHallItem))
            {
                string selectedHallName = selectedHallItem.Split('(')[0].Trim();
                if (hallOccupancy.ContainsKey(selectedHallName))
                {
                    LoadAvailableShowtimes(selectedHallName);
                }
                else
                {
                    cmbShowtimes.Items.Clear();
                }
            }
            else
            {
                cmbShowtimes.Items.Clear();
            }
        }

        private void LoadAvailableShowtimes(string hallName)
        {
            cmbShowtimes.Items.Clear();
            if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
            {
                var scheduledShowtimes = hallInfo.Movies.Select(m => m.Showtime).ToHashSet();
                var availableShowtimes = allShowtimes
                    .Where(showtime => !scheduledShowtimes.Contains(showtime))
                    .ToList();
                foreach (var showtime in availableShowtimes)
                {
                    cmbShowtimes.Items.Add(showtime);
                }
            }
            cmbShowtimes.SelectedIndex = -1;
        }

        // --- Movie Management ---
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();
            // --- MODIFIED: Read from ComboBox ---
            string ageRating = cmbAgeRating.SelectedItem?.ToString();
            // --- End Modification ---
            string director = txtDirector.Text.Trim();
            string actorsInput = txtActors.Text.Trim();

            string selectedHallName = selectedHallItem?.Split('(')[0].Trim();

            // --- MODIFIED: Validation for ComboBox ---
            if (string.IsNullOrEmpty(movieTitle) ||
                string.IsNullOrEmpty(selectedHallName) ||
                string.IsNullOrEmpty(selectedShowtime) ||
                string.IsNullOrEmpty(ageRating) || // Check if an item was selected
                string.IsNullOrEmpty(director) ||
                string.IsNullOrEmpty(actorsInput))
            {
                MessageBox.Show("Please fill out movie title, director, actors, and select a hall, showtime, and age rating.", // Updated message
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // --- End Modification ---

            if (!hallOccupancy.ContainsKey(selectedHallName))
            {
                MessageBox.Show($"Selected hall '{selectedHallName}' does not exist.",
                                "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] actors = actorsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(actor => actor.Trim())
                                         .ToArray();
            if (actors.Length == 0)
            {
                MessageBox.Show("Please enter at least one actor, separated by commas.",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hallOccupancy[selectedHallName].Movies.Add((Movie: movieTitle, Showtime: selectedShowtime));

            // --- TODO: Add Movie and Schedule to the Database ---
            // The ageRating variable now correctly holds the selected string ("7", "13", etc.)
            AddMovieToDatabase(movieTitle, selectedHallName, selectedShowtime, ageRating, director, actors);
            // -----------------------------------------------------

            MessageBox.Show($"Movie '{movieTitle}' added to '{selectedHallName}' at '{selectedShowtime}' successfully!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearMovieFields();
            LoadHallsForMovieAssignment();
        }

        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();

            string selectedHallName = selectedHallItem?.Split('(')[0].Trim();

            if (string.IsNullOrEmpty(movieTitle) ||
                string.IsNullOrEmpty(selectedHallName) ||
                string.IsNullOrEmpty(selectedShowtime))
            {
                MessageBox.Show("Please enter a movie title, select the hall, and choose the showtime to remove.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hallOccupancy.TryGetValue(selectedHallName, out HallInfo hallInfo))
            {
                int removedCount = hallInfo.Movies.RemoveAll(m =>
                    m.Movie.Equals(movieTitle, StringComparison.OrdinalIgnoreCase) &&
                    m.Showtime == selectedShowtime);

                if (removedCount > 0)
                {
                    // --- TODO: Remove Movie Schedule from Database ---
                    RemoveMovieFromDatabase(movieTitle, selectedHallName, selectedShowtime);
                    // -------------------------------------------------
                    MessageBox.Show($"Movie '{movieTitle}' removed from '{selectedHallName}' at '{selectedShowtime}' successfully!",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearMovieFields();
                    LoadHallsForMovieAssignment();
                }
                else
                {
                    MessageBox.Show($"Movie '{movieTitle}' at '{selectedShowtime}' was not found scheduled in hall '{selectedHallName}'.",
                                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Selected hall '{selectedHallName}' does not exist.",
                               "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Hall Management ---
        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            string screenType = cmbScreenType.SelectedItem?.ToString();
            bool isFullyPremium = chkIsPremium.Checked;

            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter a name for the hall.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
            if (hallOccupancy.ContainsKey(hallName)) { MessageBox.Show($"A hall with the name '{hallName}' already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
            if (string.IsNullOrEmpty(screenType)) { MessageBox.Show("Please select a screen type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); cmbScreenType.Focus(); return; }
            if (!int.TryParse(txtTotalRows.Text, out int totalRows) || totalRows <= 0) { MessageBox.Show("Please enter a valid positive number for Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtTotalRows.Focus(); return; }
            if (!int.TryParse(txtSeatsPerRow.Text, out int seatsPerRow) || seatsPerRow <= 0) { MessageBox.Show("Please enter a valid positive number for Seats Per Row.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtSeatsPerRow.Focus(); return; }

            int? premiumRowCount = null;
            if (!isFullyPremium)
            {
                if (txtPremiumRows.Visible)
                {
                    if (!int.TryParse(txtPremiumRows.Text, out int parsedPremiumRows) || parsedPremiumRows < 0) { MessageBox.Show("Please enter a valid non-negative number for Premium Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
                    if (parsedPremiumRows > totalRows) { MessageBox.Show("Number of Premium Rows cannot exceed Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
                    premiumRowCount = parsedPremiumRows;
                }
                else { premiumRowCount = 0; }
            }

            var newHall = new HallInfo(screenType, totalRows, seatsPerRow, isFullyPremium, premiumRowCount);
            hallOccupancy.Add(hallName, newHall);

            // --- TODO: Add Hall to Database ---
            AddHallToDatabase(hallName, newHall);
            // ----------------------------------

            MessageBox.Show($"Hall '{hallName}' ({newHall.ToString()}) added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearHallFields();
            LoadHallsForMovieAssignment();
        }

        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter the name of the hall to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }

            if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
            {
                if (hallInfo.Movies.Any())
                {
                    var confirmResult = MessageBox.Show($"Hall '{hallName}' has {hallInfo.Movies.Count} movie(s) scheduled.\nRemoving the hall will also remove these schedules.\n\nAre you sure you want to remove this hall?", "Confirm Hall Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.No) { return; }
                }
                hallOccupancy.Remove(hallName);

                // --- TODO: Remove Hall from Database ---
                RemoveHallFromDatabase(hallName);
                // ---------------------------------------
                MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearHallFields();
                LoadHallsForMovieAssignment();
            }
            else { MessageBox.Show($"Hall with the name '{hallName}' was not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- Helper Methods ---
        private void ClearMovieFields()
        {
            txtMovieTitle.Clear();
            // --- MODIFIED: Clear ComboBox ---
            cmbAgeRating.SelectedIndex = -1; // Deselect item
            cmbAgeRating.Text = "";          // Clear displayed text
            // --- End Modification ---
            txtDirector.Clear();
            txtActors.Clear();
            cmbHalls.SelectedIndex = -1;
            cmbShowtimes.Items.Clear();
            cmbShowtimes.SelectedIndex = -1;
            cmbShowtimes.Text = "";
        }

        private void ClearHallFields()
        {
            txtHallName.Clear();
            cmbScreenType.SelectedIndex = -1;
            txtTotalRows.Clear();
            txtSeatsPerRow.Clear();
            chkIsPremium.Checked = false;
            txtHallName.Focus();
        }

        // --- Database Interaction Placeholders ---
        private void AddMovieToDatabase(string title, string hall, string showtime, string ageRating, string director, string[] actors)
        {
            Console.WriteLine($"DB STUB: Adding movie: {title}, Hall: {hall}, Showtime: {showtime}, Age: {ageRating}, Director: {director}, Actors: {string.Join(", ", actors)}");
        }
        private void RemoveMovieFromDatabase(string title, string hall, string showtime)
        {
            Console.WriteLine($"DB STUB: Removing schedule: Movie: {title}, Hall: {hall}, Showtime: {showtime}");
        }
        private void AddHallToDatabase(string hallName, HallInfo hallInfo)
        {
            Console.WriteLine($"DB STUB: Adding Hall: {hallName}, Type: {hallInfo.ScreenType}, Rows: {hallInfo.TotalRows}, Seats: {hallInfo.SeatsPerRow}, Premium: {hallInfo.IsFullyPremium}, PremiumRows: {hallInfo.PremiumRowCount?.ToString() ?? "N/A"}");
        }
        private void RemoveHallFromDatabase(string hallName)
        {
            Console.WriteLine($"DB STUB: Removing Hall: {hallName}");
        }

        // --- Form Load ---
        private void AdminControl_Load(object sender, EventArgs e) { }
    }
}