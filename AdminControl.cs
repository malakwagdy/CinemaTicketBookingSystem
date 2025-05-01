using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static GUI_DB.DatabaseManager;

namespace GUI_DB
{
    // Moved HallInfo class definition *after* AdminControl

    public partial class AdminControl : Form
    {
        // --- FIXED: Added readonly modifier (IDE0044) ---
        private readonly Dictionary<string, HallInfo> hallOccupancy = new Dictionary<string, HallInfo>();
        private readonly List<string> allShowtimes = new List<string>
        {
            "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "6:00 PM", "8:00 PM"
        };
        private readonly MainForm mainForm;
        // ---------------------------------------------
        private DatabaseManager dbManager; // Assuming this is a class for database operations
        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Initialize Default Halls (Example Data)
            //hallOccupancy.Add("Hall 1", new HallInfo("2D", 10, 15, false, 3));
            //hallOccupancy.Add("Hall 2", new HallInfo("3D", 12, 18, true, null));
            //hallOccupancy.Add("Hall 3", new HallInfo("IMAX", 8, 12, false, 0));
            //hallOccupancy.Add("Hall 4", new HallInfo("IMAX", 9, 14, false, 9));

            LoadHallsForMovieAssignment();

            // Hook up event handlers
            cmbHalls.SelectedIndexChanged += CmbHalls_SelectedIndexChanged;
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click;
            chkIsPremium.CheckedChanged += ChkIsPremium_CheckedChanged;

            UpdatePremiumRowsVisibility();
            dtpReleaseDate.Value = DateTime.Today;
        }

        // --- UI Update Logic ---
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

        // --- Hall Loading Logic ---
        private void LoadHallsForMovieAssignment()
        {
            cmbHalls.Items.Clear();
            var availableHalls = hallOccupancy
                .Where(kvp => kvp.Value.Movies.Count < allShowtimes.Count)
                .OrderBy(kvp => kvp.Key);
            foreach (var kvp in availableHalls) { cmbHalls.Items.Add($"{kvp.Key} ({kvp.Value.ScreenType})"); }
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
                if (hallOccupancy.ContainsKey(selectedHallName)) { LoadAvailableShowtimes(selectedHallName); }
                else { cmbShowtimes.Items.Clear(); }
            }
            else { cmbShowtimes.Items.Clear(); }
        }

        private void LoadAvailableShowtimes(string hallName)
        {
            cmbShowtimes.Items.Clear();
            if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
            {
                var scheduledShowtimes = hallInfo.Movies.Select(m => m.Showtime).ToHashSet();
                var availableShowtimes = allShowtimes.Where(st => !scheduledShowtimes.Contains(st)).ToList();
                foreach (var showtime in availableShowtimes) { cmbShowtimes.Items.Add(showtime); }
            }
            cmbShowtimes.SelectedIndex = -1;
        }

        // --- Movie Management ---
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();
            int ageRating = cmbAgeRating.SelectedItem is int selected ? selected : 0;
            string director = txtDirector.Text.Trim();
            string actorsInput = txtActors.Text.Trim();
            string genre = cmbGenre.SelectedItem?.ToString();
            DateTime releaseDate = dtpReleaseDate.Value;
            string selectedHallName = selectedHallItem?.Split('(')[0].Trim();

            if (string.IsNullOrEmpty(movieTitle) || string.IsNullOrEmpty(selectedHallName) ||
                string.IsNullOrEmpty(selectedShowtime) || ageRating == null ||
                string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(director) || string.IsNullOrEmpty(actorsInput))
            {
                MessageBox.Show("Please fill out all movie fields and select all options.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!hallOccupancy.ContainsKey(selectedHallName)) { MessageBox.Show($"Selected hall '{selectedHallName}' does not exist.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            string[] actors = actorsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(actor => actor.Trim()).ToArray();
            if (actors.Length == 0) { MessageBox.Show("Please enter at least one actor.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            hallOccupancy[selectedHallName].Movies.Add((Movie: movieTitle, Showtime: selectedShowtime));
            AddMovieToDatabase(movieTitle, selectedHallName, selectedShowtime, ageRating, director, actors, genre, releaseDate); // Placeholder
            MessageBox.Show($"Movie '{movieTitle}' added to '{selectedHallName}' at '{selectedShowtime}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearMovieFields();
            LoadHallsForMovieAssignment();
        }

        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();
            string selectedHallName = selectedHallItem?.Split('(')[0].Trim();
            DateTime selectedreleaseDate = dtpReleaseDate.Value;

            //if (string.IsNullOrEmpty(movieTitle) || string.IsNullOrEmpty(selectedHallName) || string.IsNullOrEmpty(selectedShowtime)) { MessageBox.Show("Please enter movie title, select hall, and choose showtime to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            RemoveMovieFromDatabase(movieTitle, selectedreleaseDate);
            //if (hallOccupancy.TryGetValue(selectedHallName, out HallInfo hallInfo))
            //{
            //    int removedCount = hallInfo.Movies.RemoveAll(m => m.Movie.Equals(movieTitle, StringComparison.OrdinalIgnoreCase) && m.Showtime == selectedShowtime);
            //    if (removedCount > 0)
            //    {
            //        RemoveMovieFromDatabase(movieTitle, selectedreleaseDate); // Placeholder
            //        MessageBox.Show($"Movie '{movieTitle}' removed from '{selectedHallName}' at '{selectedShowtime}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        ClearMovieFields();
            //        LoadHallsForMovieAssignment();
            //    }
            //    else { MessageBox.Show($"Movie '{movieTitle}' at '{selectedShowtime}' not found in hall '{selectedHallName}'.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            //}
            //else { MessageBox.Show($"Selected hall '{selectedHallName}' does not exist.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- Hall Management ---
        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            string screenType = cmbScreenType.SelectedItem?.ToString();
            bool isFullyPremium = chkIsPremium.Checked;

            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter hall name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
            if (hallOccupancy.ContainsKey(hallName)) { MessageBox.Show($"Hall '{hallName}' already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
            if (string.IsNullOrEmpty(screenType)) { MessageBox.Show("Please select screen type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); cmbScreenType.Focus(); return; }
            if (!int.TryParse(txtTotalRows.Text, out int totalRows) || totalRows <= 0) { MessageBox.Show("Invalid Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtTotalRows.Focus(); return; }
            if (!int.TryParse(txtSeatsPerRow.Text, out int seatsPerRow) || seatsPerRow <= 0) { MessageBox.Show("Invalid Seats Per Row.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtSeatsPerRow.Focus(); return; }

            int premiumRowCount = 0;
            if (!isFullyPremium)
            {
                if (txtPremiumRows.Visible)
                {
                    if (!int.TryParse(txtPremiumRows.Text, out int parsedPremiumRows) || parsedPremiumRows < 0) { MessageBox.Show("Invalid Premium Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
                    if (parsedPremiumRows > totalRows) { MessageBox.Show("Premium Rows cannot exceed Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
                    premiumRowCount = parsedPremiumRows;
                }
                else { premiumRowCount = 0; }
            }

            var newHall = new HallInfo(screenType, totalRows, seatsPerRow, isFullyPremium, premiumRowCount);
            hallOccupancy.Add(hallName, newHall);
            AddHallToDatabase(hallName, newHall); // Placeholder
            MessageBox.Show($"Hall '{hallName}' ({newHall}) added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearHallFields();
            LoadHallsForMovieAssignment();
        }

        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter hall name to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }

            if (hallOccupancy.TryGetValue(hallName, out HallInfo hallInfo))
            {
                if (hallInfo.Movies.Any())
                {
                    // --- FIXED: Correct MessageBox.Show arguments (CS1503) ---
                    var confirmResult = MessageBox.Show(
                        $"Hall '{hallName}' has {hallInfo.Movies.Count} movie(s) scheduled.\n" +
                        $"Removing the hall will also remove these schedules.\n\n" +
                        $"Are you sure you want to remove this hall?", // Text
                        "Confirm Hall Removal", // Caption
                        MessageBoxButtons.YesNo, // Buttons
                        MessageBoxIcon.Warning); // Icon
                    // ---------------------------------------------------------
                    if (confirmResult == DialogResult.No) { return; }
                }
                hallOccupancy.Remove(hallName);
                RemoveHallFromDatabase(hallName); // Placeholder
                MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearHallFields();
                LoadHallsForMovieAssignment();
            }
            else { MessageBox.Show($"Hall '{hallName}' not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- Helper Methods ---
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
        private void AddMovieToDatabase(string title, string hall, string showtime, int ageRating, string director, string[] actors, string genre, DateTime releaseDate)
        {
            //Console.WriteLine($"DB STUB: Adding movie: {title}, Hall: {hall}, Showtime: {showtime}, Age: {ageRating}, Director: {director}, Actors: {string.Join(", ", actors)}, Genre: {genre}, Released: {releaseDate.ToShortDateString()}");
            dbManager = new DatabaseManager();
            dbManager.AddMovie(director, title, genre, ageRating, releaseDate);

        }
        private void RemoveMovieFromDatabase(string title, DateTime releaseDate) {
            //Console.WriteLine($"DB STUB: Removing schedule: Movie: {title}, Hall: {hall}, Showtime: {showtime}"); 
            dbManager = new DatabaseManager();
            dbManager.DeleteMovie(title, releaseDate);
        }
        private void AddHallToDatabase(string hallName, HallInfo hallInfo)
        {
            // --- FIXED: Simplified interpolation (IDE0071) ---
            //Console.WriteLine($"DB STUB: Adding Hall: {hallName}, Details: {hallInfo}");
            // -----------------------------------------------
            dbManager = new DatabaseManager();
            dbManager.AddHall(hallName, hallInfo.ScreenType, hallInfo.TotalRows, hallInfo.SeatsPerRow, hallInfo.IsFullyPremium, hallInfo.PremiumRowCount);

        }
        private void RemoveHallFromDatabase(string hallName) { Console.WriteLine($"DB STUB: Removing Hall: {hallName}"); }

        // --- Form Load ---
        private void AdminControl_Load(object sender, EventArgs e) { }
    }

    // --- FIXED: Moved HallInfo class definition after AdminControl (Designer Error) ---
    public class HallInfo
    {
        public List<(string Movie, string Showtime)> Movies { get; set; } = new List<(string, string)>();
        public string ScreenType { get; set; }
        public int TotalRows { get; set; }
        public int SeatsPerRow { get; set; }
        public bool IsFullyPremium { get; set; }
        public int PremiumRowCount { get; set; }

        public HallInfo(string screenType, int totalRows, int seatsPerRow, bool isFullyPremium, int premiumRowCount)
        {
            ScreenType = screenType;
            TotalRows = totalRows;
            SeatsPerRow = seatsPerRow;
            IsFullyPremium = isFullyPremium;
            PremiumRowCount = isFullyPremium ? 0 : premiumRowCount;
        }

        public override string ToString()
        {
            string details = $"{TotalRows}x{SeatsPerRow}";
            if (IsFullyPremium) { details += ", Fully Premium"; }
            else if (PremiumRowCount != null && PremiumRowCount > 0) { details += $", Premium: {PremiumRowCount} row{(PremiumRowCount == 1 ? "" : "s")}"; }
            // Return details relevant to the hall itself, maybe not the screen type again?
            // Example: "10x15, Premium: 3 rows" or "12x18, Fully Premium"
            return details;
            // Or keep original if preferred: return $"{ScreenType}, {details}";
        }
    }
    // --------------------------------------------------------------------------
}