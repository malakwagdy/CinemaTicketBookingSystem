using System.Windows.Forms;
using System.Drawing;

namespace GUI_DB
{
    partial class AdminControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls (Existing)
        private System.Windows.Forms.TextBox txtMovieTitle;
        private System.Windows.Forms.ComboBox cmbHalls;
        private System.Windows.Forms.ComboBox cmbShowtimes;
        //private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Button btnRemoveMovie;
        private System.Windows.Forms.Button btnUpdateMovie;
        private System.Windows.Forms.TextBox txtHallName;
        private System.Windows.Forms.Button btnAddHall;
        private System.Windows.Forms.Button btnRemoveHall;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblHallName;
        private System.Windows.Forms.Label lblSelectHall;
        private System.Windows.Forms.Label lblSelectShowtime;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.ComboBox cmbScreenType;
        private System.Windows.Forms.Label lblScreenType;
        private System.Windows.Forms.Label lblTopPanel;
        private System.Windows.Forms.Label lblAgeRating;
        private System.Windows.Forms.TextBox txtDirector;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.TextBox txtActors;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.ComboBox cmbAgeRating;

        // Hall Controls (Existing)
        private System.Windows.Forms.TextBox txtTotalRows;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.TextBox txtSeatsPerRow;
        private System.Windows.Forms.Label lblSeatsPerRow;
        private System.Windows.Forms.CheckBox chkIsPremium;
        private System.Windows.Forms.Label lblPremiumRows;
        private System.Windows.Forms.TextBox txtPremiumRows;

        // --- ADDED: Genre and Release Date Controls ---
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.ComboBox cmbGenre;
        private System.Windows.Forms.Label lblReleaseDate;
        private System.Windows.Forms.DateTimePicker dtpReleaseDate;
        // ---------------------------------------------

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMovieTitle = new System.Windows.Forms.TextBox();
            this.cmbHalls = new System.Windows.Forms.ComboBox();
            this.cmbShowtimes = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnRemoveMovie = new System.Windows.Forms.Button();
            this.btnUpdateMovie = new System.Windows.Forms.Button();
            this.txtHallName = new System.Windows.Forms.TextBox();
            this.btnAddHall = new System.Windows.Forms.Button();
            this.btnRemoveHall = new System.Windows.Forms.Button();
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblSelectHall = new System.Windows.Forms.Label();
            this.lblSelectShowtime = new System.Windows.Forms.Label();
            this.lblHallName = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblTopPanel = new System.Windows.Forms.Label();
            this.cmbScreenType = new System.Windows.Forms.ComboBox();
            this.lblScreenType = new System.Windows.Forms.Label();
            this.lblAgeRating = new System.Windows.Forms.Label();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.lblDirector = new System.Windows.Forms.Label();
            this.txtActors = new System.Windows.Forms.TextBox();
            this.lblActors = new System.Windows.Forms.Label();
            this.txtTotalRows = new System.Windows.Forms.TextBox();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.lblSeatsPerRow = new System.Windows.Forms.Label();
            this.txtSeatsPerRow = new System.Windows.Forms.TextBox();
            this.chkIsPremium = new System.Windows.Forms.CheckBox();
            this.lblPremiumRows = new System.Windows.Forms.Label();
            this.txtPremiumRows = new System.Windows.Forms.TextBox();
            this.cmbAgeRating = new System.Windows.Forms.ComboBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.lblReleaseDate = new System.Windows.Forms.Label();
            this.dtpReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMovieTitle
            // 
            this.txtMovieTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtMovieTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMovieTitle.ForeColor = System.Drawing.Color.White;
            this.txtMovieTitle.Location = new System.Drawing.Point(50, 100);
            this.txtMovieTitle.Name = "txtMovieTitle";
            this.txtMovieTitle.Size = new System.Drawing.Size(395, 34);
            this.txtMovieTitle.TabIndex = 1;
            // 
            // cmbHalls
            // 
            this.cmbHalls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHalls.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbHalls.FormattingEnabled = true;
            this.cmbHalls.Location = new System.Drawing.Point(50, 170);
            this.cmbHalls.Name = "cmbHalls";
            this.cmbHalls.Size = new System.Drawing.Size(395, 36);
            this.cmbHalls.TabIndex = 3;
            // 
            // cmbShowtimes
            // 
            this.cmbShowtimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShowtimes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbShowtimes.FormattingEnabled = true;
            this.cmbShowtimes.Location = new System.Drawing.Point(50, 240);
            this.cmbShowtimes.Name = "cmbShowtimes";
            this.cmbShowtimes.Size = new System.Drawing.Size(395, 36);
            this.cmbShowtimes.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(893, 237);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(351, 36);
            this.comboBox2.TabIndex = 34;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddMovie.ForeColor = System.Drawing.Color.White;
            this.btnAddMovie.Location = new System.Drawing.Point(553, 330);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(170, 40);
            this.btnAddMovie.TabIndex = 15;
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.UseVisualStyleBackColor = false;
            // 
            // btnRemoveMovie
            // 
            this.btnRemoveMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnRemoveMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveMovie.ForeColor = System.Drawing.Color.White;
            this.btnRemoveMovie.Location = new System.Drawing.Point(893, 330);
            this.btnRemoveMovie.Name = "btnRemoveMovie";
            this.btnRemoveMovie.Size = new System.Drawing.Size(170, 40);
            this.btnRemoveMovie.TabIndex = 16;
            this.btnRemoveMovie.Text = "Remove Movie";
            this.btnRemoveMovie.UseVisualStyleBackColor = false;
            // 
            // btnUpdateMovie
            // 
            this.btnUpdateMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnUpdateMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdateMovie.ForeColor = System.Drawing.Color.White;
            this.btnUpdateMovie.Location = new System.Drawing.Point(553, 330);
            this.btnUpdateMovie.Name = "btnUpdateMovie";
            this.btnUpdateMovie.Size = new System.Drawing.Size(170, 40);
            this.btnUpdateMovie.TabIndex = 15;
            this.btnUpdateMovie.Text = "Update Movie";
            this.btnUpdateMovie.UseVisualStyleBackColor = false;
            // 
            // txtHallName
            // 
            this.txtHallName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtHallName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHallName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHallName.ForeColor = System.Drawing.Color.White;
            this.txtHallName.Location = new System.Drawing.Point(50, 505);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(395, 34);
            this.txtHallName.TabIndex = 17;
            // 
            // btnAddHall
            // 
            this.btnAddHall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddHall.ForeColor = System.Drawing.Color.White;
            this.btnAddHall.Location = new System.Drawing.Point(783, 613);
            this.btnAddHall.Name = "btnAddHall";
            this.btnAddHall.Size = new System.Drawing.Size(170, 40);
            this.btnAddHall.TabIndex = 28;
            this.btnAddHall.Text = "Add Hall";
            this.btnAddHall.UseVisualStyleBackColor = false;
            // 
            // btnRemoveHall
            // 
            this.btnRemoveHall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnRemoveHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveHall.ForeColor = System.Drawing.Color.White;
            this.btnRemoveHall.Location = new System.Drawing.Point(408, 613);
            this.btnRemoveHall.Name = "btnRemoveHall";
            this.btnRemoveHall.Size = new System.Drawing.Size(170, 40);
            this.btnRemoveHall.TabIndex = 29;
            this.btnRemoveHall.Text = "Remove Hall";
            this.btnRemoveHall.UseVisualStyleBackColor = false;
            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMovieTitle.ForeColor = System.Drawing.Color.White;
            this.lblMovieTitle.Location = new System.Drawing.Point(45, 69);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(120, 28);
            this.lblMovieTitle.TabIndex = 0;
            this.lblMovieTitle.Text = "Movie Title";
            // 
            // lblSelectHall
            // 
            this.lblSelectHall.AutoSize = true;
            this.lblSelectHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectHall.ForeColor = System.Drawing.Color.White;
            this.lblSelectHall.Location = new System.Drawing.Point(50, 139);
            this.lblSelectHall.Name = "lblSelectHall";
            this.lblSelectHall.Size = new System.Drawing.Size(179, 28);
            this.lblSelectHall.TabIndex = 2;
            this.lblSelectHall.Text = "Select Empty Hall";
            // 
            // lblSelectShowtime
            // 
            this.lblSelectShowtime.AutoSize = true;
            this.lblSelectShowtime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectShowtime.ForeColor = System.Drawing.Color.White;
            this.lblSelectShowtime.Location = new System.Drawing.Point(50, 209);
            this.lblSelectShowtime.Name = "lblSelectShowtime";
            this.lblSelectShowtime.Size = new System.Drawing.Size(169, 28);
            this.lblSelectShowtime.TabIndex = 4;
            this.lblSelectShowtime.Text = "Select Showtime";
            // 
            // lblHallName
            // 
            this.lblHallName.AutoSize = true;
            this.lblHallName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHallName.ForeColor = System.Drawing.Color.White;
            this.lblHallName.Location = new System.Drawing.Point(50, 474);
            this.lblHallName.Name = "lblHallName";
            this.lblHallName.Size = new System.Drawing.Size(112, 28);
            this.lblHallName.TabIndex = 16;
            this.lblHallName.Text = "Hall Name";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.topPanel.Controls.Add(this.lblTopPanel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1655, 60);
            this.topPanel.TabIndex = 30;
            // 
            // lblTopPanel
            // 
            this.lblTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopPanel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTopPanel.ForeColor = System.Drawing.Color.White;
            this.lblTopPanel.Location = new System.Drawing.Point(0, 0);
            this.lblTopPanel.Name = "lblTopPanel";
            this.lblTopPanel.Size = new System.Drawing.Size(1655, 60);
            this.lblTopPanel.TabIndex = 0;
            this.lblTopPanel.Text = "Admin Control Panel";
            this.lblTopPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbScreenType
            // 
            this.cmbScreenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScreenType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbScreenType.FormattingEnabled = true;
            this.cmbScreenType.Items.AddRange(new object[] {
            "2D",
            "3D",
            "IMAX"});
            this.cmbScreenType.Location = new System.Drawing.Point(465, 503);
            this.cmbScreenType.Name = "cmbScreenType";
            this.cmbScreenType.Size = new System.Drawing.Size(395, 36);
            this.cmbScreenType.TabIndex = 19;
            // 
            // lblScreenType
            // 
            this.lblScreenType.AutoSize = true;
            this.lblScreenType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScreenType.ForeColor = System.Drawing.Color.White;
            this.lblScreenType.Location = new System.Drawing.Point(465, 472);
            this.lblScreenType.Name = "lblScreenType";
            this.lblScreenType.Size = new System.Drawing.Size(126, 28);
            this.lblScreenType.TabIndex = 18;
            this.lblScreenType.Text = "Screen Type";
            // 
            // lblAgeRating
            // 
            this.lblAgeRating.AutoSize = true;
            this.lblAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAgeRating.ForeColor = System.Drawing.Color.White;
            this.lblAgeRating.Location = new System.Drawing.Point(470, 139);
            this.lblAgeRating.Name = "lblAgeRating";
            this.lblAgeRating.Size = new System.Drawing.Size(117, 28);
            this.lblAgeRating.TabIndex = 8;
            this.lblAgeRating.Text = "Age Rating";
            // 
            // txtDirector
            // 
            this.txtDirector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtDirector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirector.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDirector.ForeColor = System.Drawing.Color.White;
            this.txtDirector.Location = new System.Drawing.Point(470, 100);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(360, 34);
            this.txtDirector.TabIndex = 7;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDirector.ForeColor = System.Drawing.Color.White;
            this.lblDirector.Location = new System.Drawing.Point(470, 69);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(90, 28);
            this.lblDirector.TabIndex = 6;
            this.lblDirector.Text = "Director";
            // 
            // txtActors
            // 
            this.txtActors.AcceptsReturn = true;
            this.txtActors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActors.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtActors.ForeColor = System.Drawing.Color.White;
            this.txtActors.Location = new System.Drawing.Point(893, 100);
            this.txtActors.Multiline = true;
            this.txtActors.Name = "txtActors";
            this.txtActors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtActors.Size = new System.Drawing.Size(321, 80);
            this.txtActors.TabIndex = 10;
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblActors.ForeColor = System.Drawing.Color.White;
            this.lblActors.Location = new System.Drawing.Point(893, 69);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(263, 28);
            this.lblActors.TabIndex = 10;
            this.lblActors.Text = "Actors (comma-separated)";
            // 
            // txtTotalRows
            // 
            this.txtTotalRows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtTotalRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalRows.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtTotalRows.ForeColor = System.Drawing.Color.White;
            this.txtTotalRows.Location = new System.Drawing.Point(893, 505);
            this.txtTotalRows.MaxLength = 3;
            this.txtTotalRows.Name = "txtTotalRows";
            this.txtTotalRows.Size = new System.Drawing.Size(60, 34);
            this.txtTotalRows.TabIndex = 21;
            this.txtTotalRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRows.ForeColor = System.Drawing.Color.White;
            this.lblTotalRows.Location = new System.Drawing.Point(869, 474);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(115, 28);
            this.lblTotalRows.TabIndex = 20;
            this.lblTotalRows.Text = "Total Rows";
            // 
            // lblSeatsPerRow
            // 
            this.lblSeatsPerRow.AutoSize = true;
            this.lblSeatsPerRow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSeatsPerRow.ForeColor = System.Drawing.Color.White;
            this.lblSeatsPerRow.Location = new System.Drawing.Point(990, 474);
            this.lblSeatsPerRow.Name = "lblSeatsPerRow";
            this.lblSeatsPerRow.Size = new System.Drawing.Size(145, 28);
            this.lblSeatsPerRow.TabIndex = 22;
            this.lblSeatsPerRow.Text = "Seats Per Row";
            // 
            // txtSeatsPerRow
            // 
            this.txtSeatsPerRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtSeatsPerRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSeatsPerRow.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtSeatsPerRow.ForeColor = System.Drawing.Color.White;
            this.txtSeatsPerRow.Location = new System.Drawing.Point(1036, 505);
            this.txtSeatsPerRow.MaxLength = 3;
            this.txtSeatsPerRow.Name = "txtSeatsPerRow";
            this.txtSeatsPerRow.Size = new System.Drawing.Size(60, 34);
            this.txtSeatsPerRow.TabIndex = 23;
            this.txtSeatsPerRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkIsPremium
            // 
            this.chkIsPremium.AutoSize = true;
            this.chkIsPremium.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.chkIsPremium.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkIsPremium.Location = new System.Drawing.Point(983, 563);
            this.chkIsPremium.Name = "chkIsPremium";
            this.chkIsPremium.Size = new System.Drawing.Size(170, 32);
            this.chkIsPremium.TabIndex = 25;
            this.chkIsPremium.Text = "Fully Premium";
            this.chkIsPremium.UseVisualStyleBackColor = true;
            // 
            // lblPremiumRows
            // 
            this.lblPremiumRows.AutoSize = true;
            this.lblPremiumRows.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPremiumRows.ForeColor = System.Drawing.Color.White;
            this.lblPremiumRows.Location = new System.Drawing.Point(1141, 472);
            this.lblPremiumRows.Name = "lblPremiumRows";
            this.lblPremiumRows.Size = new System.Drawing.Size(153, 28);
            this.lblPremiumRows.TabIndex = 26;
            this.lblPremiumRows.Text = "Premium Rows";
            this.lblPremiumRows.Visible = false;
            // 
            // txtPremiumRows
            // 
            this.txtPremiumRows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtPremiumRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPremiumRows.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPremiumRows.ForeColor = System.Drawing.Color.White;
            this.txtPremiumRows.Location = new System.Drawing.Point(1179, 505);
            this.txtPremiumRows.MaxLength = 3;
            this.txtPremiumRows.Name = "txtPremiumRows";
            this.txtPremiumRows.Size = new System.Drawing.Size(60, 34);
            this.txtPremiumRows.TabIndex = 27;
            this.txtPremiumRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPremiumRows.Visible = false;
            // 
            // cmbAgeRating
            // 
            this.cmbAgeRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbAgeRating.FormattingEnabled = true;
            this.cmbAgeRating.Items.AddRange(new object[] {
            7,
            13,
            16,
            18});
            this.cmbAgeRating.Location = new System.Drawing.Point(470, 170);
            this.cmbAgeRating.Name = "cmbAgeRating";
            this.cmbAgeRating.Size = new System.Drawing.Size(360, 36);
            this.cmbAgeRating.TabIndex = 9;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGenre.ForeColor = System.Drawing.Color.White;
            this.lblGenre.Location = new System.Drawing.Point(470, 209);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(68, 28);
            this.lblGenre.TabIndex = 11;
            this.lblGenre.Text = "Genre";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenre.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Items.AddRange(new object[] {
            "Action",
            "Adventure",
            "Animation",
            "Comedy",
            "Crime",
            "Documentary",
            "Drama",
            "Family",
            "Fantasy",
            "History",
            "Horror",
            "Music",
            "Mystery",
            "Romance",
            "Sci-Fi",
            "Thriller",
            "War",
            "Western"});
            this.cmbGenre.Location = new System.Drawing.Point(470, 240);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(360, 36);
            this.cmbGenre.TabIndex = 12;
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AutoSize = true;
            this.lblReleaseDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblReleaseDate.ForeColor = System.Drawing.Color.White;
            this.lblReleaseDate.Location = new System.Drawing.Point(893, 197);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(135, 28);
            this.lblReleaseDate.TabIndex = 13;
            this.lblReleaseDate.Text = "Release Date";
            // 
            // dtpReleaseDate
            // 
            this.dtpReleaseDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpReleaseDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReleaseDate.Location = new System.Drawing.Point(1034, 197);
            this.dtpReleaseDate.Name = "dtpReleaseDate";
            this.dtpReleaseDate.Size = new System.Drawing.Size(180, 34);
            this.dtpReleaseDate.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(183, 330);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 40);
            this.button1.TabIndex = 31;
            this.button1.Text = "Update Movie";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.buttonUpdateMovie_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox1.Location = new System.Drawing.Point(1146, 338);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 32);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "Update Movie";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // AdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1655, 720);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpReleaseDate);
            this.Controls.Add(this.lblReleaseDate);
            this.Controls.Add(this.cmbGenre);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.cmbAgeRating);
            this.Controls.Add(this.txtPremiumRows);
            this.Controls.Add(this.lblPremiumRows);
            this.Controls.Add(this.chkIsPremium);
            this.Controls.Add(this.txtSeatsPerRow);
            this.Controls.Add(this.lblSeatsPerRow);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.txtTotalRows);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.cmbHalls);
            this.Controls.Add(this.lblSelectHall);
            this.Controls.Add(this.cmbShowtimes);
            this.Controls.Add(this.lblSelectShowtime);
            this.Controls.Add(this.lblAgeRating);
            this.Controls.Add(this.txtDirector);
            this.Controls.Add(this.lblDirector);
            this.Controls.Add(this.txtActors);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.txtHallName);
            this.Controls.Add(this.lblHallName);
            this.Controls.Add(this.btnAddHall);
            this.Controls.Add(this.btnRemoveHall);
            this.Controls.Add(this.lblScreenType);
            this.Controls.Add(this.cmbScreenType);
            this.Controls.Add(this.btnAddMovie);
            this.Controls.Add(this.btnRemoveMovie);
            this.Controls.Add(this.btnUpdateMovie);
            this.Controls.Add(this.topPanel);
            this.MinimumSize = new System.Drawing.Size(868, 767);
            this.Name = "AdminControl";
            this.Text = "Admin Control Panel";
            this.Load += new System.EventHandler(this.AdminControl_Load);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button button1;
        private CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox2; // Correctly declared once


        // Declarations added at the top
    }
}