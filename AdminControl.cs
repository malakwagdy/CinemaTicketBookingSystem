using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static GUI_DB.DatabaseManager;

namespace GUI_DB
{
    public partial class AdminControl : Form
    {
        private readonly Dictionary<string, HallInfo> hallOccupancy = new Dictionary<string, HallInfo>();
        private readonly List<string> allShowtimes = new List<string>
        {
            "10:00 AM", "12:00 PM", "2:00 PM", "4:00 PM", "6:00 PM", "8:00 PM"
        };
        private readonly MainForm mainForm;
        private DatabaseManager dbManager;

        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            LoadHallsForMovieAssignment();

            cmbHalls.SelectedIndexChanged += CmbHalls_SelectedIndexChanged;
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click;
            chkIsPremium.CheckedChanged += ChkIsPremium_CheckedChanged;

            UpdatePremiumRowsVisibility();
            dtpReleaseDate.Value = DateTime.Today;
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
                var availableShowtimes = allShowtimes
                    .Select(st => DateTime.Parse(st))
                    .Where(st => !scheduledShowtimes.Contains(st))
                    .ToList();
                foreach (var showtime in availableShowtimes)
                {
                    cmbShowtimes.Items.Add(showtime.ToString("hh:mm tt"));
                }
            }
            cmbShowtimes.SelectedIndex = -1;
        }

        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            dbManager = new DatabaseManager();
            string movieTitle = txtMovieTitle.Text.Trim();
            string selectedHallItem = cmbHalls.SelectedItem?.ToString();
            string selectedShowtime = cmbShowtimes.SelectedItem?.ToString();
            int ageRating = cmbAgeRating.SelectedItem is int selected ? selected : 0;
            string director = txtDirector.Text.Trim();
            string actorsInput = txtActors.Text.Trim();
            string genre = cmbGenre.SelectedItem?.ToString();
            DateTime releaseDate = dtpReleaseDate.Value;
            string selectedHallName = selectedHallItem?.Split('(')[0].Trim();
            int selectedHallID = dbManager.GetHallIDByName(selectedHallName);

            if (string.IsNullOrEmpty(movieTitle) || string.IsNullOrEmpty(selectedHallName) || ageRating == 0 ||
                string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(director) || string.IsNullOrEmpty(actorsInput) || string.IsNullOrEmpty(selectedShowtime))
            {
                MessageBox.Show("Please fill out all movie fields and select all options, including a valid showtime.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!hallOccupancy.ContainsKey(selectedHallName))
            {
                MessageBox.Show($"Selected hall '{selectedHallName}' does not exist.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] actors = actorsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(actor => actor.Trim()).ToArray();
            if (actors.Length == 0)
            {
                MessageBox.Show("Please enter at least one actor.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime parsedShowtime = DateTime.Parse(selectedShowtime);
            hallOccupancy[selectedHallName].Movies.Add((Movie: movieTitle, Showtime: parsedShowtime));

            AddMovieToDatabase(movieTitle, selectedHallID, parsedShowtime, ageRating, director, actors, genre, releaseDate);

            MessageBox.Show($"Movie '{movieTitle}' added to '{selectedHallName}' at '{selectedShowtime}' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearMovieFields();
            LoadHallsForMovieAssignment();
        }

        private void AddMovieToDatabase(string title, int hallID, DateTime showtime, int ageRating, string director, string[] actors, string genre, DateTime releaseDate)
        {
            dbManager = new DatabaseManager();
            

            int currentMovieID = dbManager.AddMovie(director, title, genre, ageRating, releaseDate);

            dbManager.AddShowtime(showtime, currentMovieID, hallID);

            dbManager.AddMoviesActors(currentMovieID, actors);
        }

        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            DateTime selectedreleaseDate = dtpReleaseDate.Value;

            RemoveMovieFromDatabase(movieTitle, selectedreleaseDate);
        }

        private void RemoveMovieFromDatabase(string title, DateTime releaseDate)
        {
            dbManager = new DatabaseManager();
            dbManager.DeleteMovie(title, releaseDate);
        }

        //private void BtnAddHall_Click(object sender, EventArgs e)
        //{
        //    string hallName = txtHallName.Text.Trim();
        //    string screenType = cmbScreenType.SelectedItem?.ToString();
        //    bool isFullyPremium = chkIsPremium.Checked;

        //    if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter hall name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
        //    if (hallOccupancy.ContainsKey(hallName)) { MessageBox.Show($"Hall '{hallName}' already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }
        //    if (string.IsNullOrEmpty(screenType)) { MessageBox.Show("Please select screen type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); cmbScreenType.Focus(); return; }
        //    if (!int.TryParse(txtTotalRows.Text, out int totalRows) || totalRows <= 0) { MessageBox.Show("Invalid Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtTotalRows.Focus(); return; }
        //    if (!int.TryParse(txtSeatsPerRow.Text, out int seatsPerRow) || seatsPerRow <= 0) { MessageBox.Show("Invalid Seats Per Row.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtSeatsPerRow.Focus(); return; }

        //    int premiumRowCount = 0;
        //    if (!isFullyPremium)
        //    {
        //        if (txtPremiumRows.Visible)
        //        {
        //            if (!int.TryParse(txtPremiumRows.Text, out int parsedPremiumRows) || parsedPremiumRows < 0) { MessageBox.Show("Invalid Premium Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
        //            if (parsedPremiumRows > totalRows) { MessageBox.Show("Premium Rows cannot exceed Total Rows.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPremiumRows.Focus(); return; }
        //            premiumRowCount = parsedPremiumRows;
        //        }
        //        else { premiumRowCount = 0; }
        //    }

        //    var newHall = new HallInfo(screenType, totalRows, seatsPerRow, isFullyPremium, premiumRowCount);
        //    hallOccupancy.Add(hallName, newHall);
        //    AddHallToDatabase(hallName, newHall);
        //    MessageBox.Show($"Hall '{hallName}' ({newHall}) added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    ClearHallFields();
        //    LoadHallsForMovieAssignment();
        //}

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

        private void AddHallToDatabase(string hallName, HallInfo hallInfo)
        {
            // --- FIXED: Simplified interpolation (IDE0071) ---
            //Console.WriteLine($"DB STUB: Adding Hall: {hallName}, Details: {hallInfo}");
            // -----------------------------------------------
            dbManager = new DatabaseManager();
            dbManager.AddHall(hallName, hallInfo.ScreenType, hallInfo.TotalRows, hallInfo.SeatsPerRow, hallInfo.IsFullyPremium, hallInfo.PremiumRowCount);

        }

        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (string.IsNullOrEmpty(hallName)) { MessageBox.Show("Please enter hall name to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtHallName.Focus(); return; }

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
            else { MessageBox.Show($"Hall '{hallName}' not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void RemoveHallFromDatabase(string hallName)
        {
            dbManager = new DatabaseManager();
            int currentHallID = dbManager.GetHallIDByName(hallName);
            dbManager.DeleteHall(currentHallID);
        }

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

        private void AdminControl_Load(object sender, EventArgs e) { }
    }

    public class HallInfo
    {
        public List<(string Movie, DateTime Showtime)> Movies { get; set; } = new List<(string, DateTime)>();
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
            return details;
        }
    }
}