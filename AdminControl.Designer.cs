using System.Windows.Forms;

namespace GUI_DB
{
    partial class AdminControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private System.Windows.Forms.TextBox txtMovieTitle;
        private System.Windows.Forms.ComboBox cmbHalls; // Dropdown for halls
        private System.Windows.Forms.ComboBox cmbShowtimes; // Dropdown for showtimes
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Button btnRemoveMovie;
        private System.Windows.Forms.TextBox txtHallName;
        private System.Windows.Forms.Button btnAddHall;
        private System.Windows.Forms.Button btnRemoveHall;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblHallName;
        private System.Windows.Forms.Label lblSelectHall;
        private System.Windows.Forms.Label lblSelectShowtime;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.ComboBox cmbScreenType; // Dropdown for screen type
        private System.Windows.Forms.Label lblScreenType;

        // New Fields
        private System.Windows.Forms.TextBox txtAgeRating;
        private System.Windows.Forms.Label lblAgeRating;
        private System.Windows.Forms.TextBox txtDirector;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.TextBox txtActors;
        private System.Windows.Forms.Label lblActors;


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
        /// Initialize the components.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container(); // Ensure components is initialized properly.

            this.txtMovieTitle = new System.Windows.Forms.TextBox();
            this.cmbHalls = new System.Windows.Forms.ComboBox();
            this.cmbShowtimes = new System.Windows.Forms.ComboBox();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnRemoveMovie = new System.Windows.Forms.Button();
            this.txtHallName = new System.Windows.Forms.TextBox();
            this.btnAddHall = new System.Windows.Forms.Button();
            this.btnRemoveHall = new System.Windows.Forms.Button();
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblSelectHall = new System.Windows.Forms.Label();
            this.lblSelectShowtime = new System.Windows.Forms.Label();
            this.lblHallName = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.cmbScreenType = new System.Windows.Forms.ComboBox();
            this.lblScreenType = new System.Windows.Forms.Label();
            this.txtAgeRating = new System.Windows.Forms.TextBox();
            this.lblAgeRating = new System.Windows.Forms.Label();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.lblDirector = new System.Windows.Forms.Label();
            this.txtActors = new System.Windows.Forms.TextBox();
            this.lblActors = new System.Windows.Forms.Label();

            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Darker purple
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 60;
            this.topPanel.Controls.Add(new Label
            {
                Text = "Admin Control Panel",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });

            // 
            // txtMovieTitle
            // 
            this.txtMovieTitle.Location = new System.Drawing.Point(50, 100);
            this.txtMovieTitle.Name = "txtMovieTitle";
            this.txtMovieTitle.Size = new System.Drawing.Size(400, 30);
            this.txtMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMovieTitle.ForeColor = System.Drawing.Color.White;
            this.txtMovieTitle.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Eggplant purple
            this.txtMovieTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Location = new System.Drawing.Point(50, 75);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(85, 25);
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMovieTitle.ForeColor = System.Drawing.Color.White;
            this.lblMovieTitle.Text = "Movie Title";

            // 
            // cmbHalls
            // 
            this.cmbHalls.Location = new System.Drawing.Point(50, 170);
            this.cmbHalls.Name = "cmbHalls";
            this.cmbHalls.Size = new System.Drawing.Size(400, 30);
            this.cmbHalls.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbHalls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // 
            // lblSelectHall
            // 
            this.lblSelectHall.AutoSize = true;
            this.lblSelectHall.Location = new System.Drawing.Point(50, 145);
            this.lblSelectHall.Name = "lblSelectHall";
            this.lblSelectHall.Size = new System.Drawing.Size(100, 25);
            this.lblSelectHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectHall.ForeColor = System.Drawing.Color.White;
            this.lblSelectHall.Text = "Select Hall";

            // 
            // cmbShowtimes
            // 
            this.cmbShowtimes.Location = new System.Drawing.Point(50, 240);
            this.cmbShowtimes.Name = "cmbShowtimes";
            this.cmbShowtimes.Size = new System.Drawing.Size(400, 30);
            this.cmbShowtimes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbShowtimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // 
            // lblSelectShowtime
            // 
            this.lblSelectShowtime.AutoSize = true;
            this.lblSelectShowtime.Location = new System.Drawing.Point(50, 215);
            this.lblSelectShowtime.Name = "lblSelectShowtime";
            this.lblSelectShowtime.Size = new System.Drawing.Size(140, 25);
            this.lblSelectShowtime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectShowtime.ForeColor = System.Drawing.Color.White;
            this.lblSelectShowtime.Text = "Select Showtime";

            // 
            // btnAddMovie
            // 
            this.btnAddMovie.Location = new System.Drawing.Point(470, 100);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(150, 40);
            this.btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel blue
            this.btnAddMovie.ForeColor = System.Drawing.Color.White;
            this.btnAddMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // btnRemoveMovie
            // 
            this.btnRemoveMovie.Location = new System.Drawing.Point(640, 100);
            this.btnRemoveMovie.Name = "btnRemoveMovie";
            this.btnRemoveMovie.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveMovie.Text = "Remove Movie";
            this.btnRemoveMovie.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel blue
            this.btnRemoveMovie.ForeColor = System.Drawing.Color.White;
            this.btnRemoveMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // txtHallName
            // 
            this.txtHallName.Location = new System.Drawing.Point(50, 340);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(400, 30);
            this.txtHallName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHallName.ForeColor = System.Drawing.Color.White;
            this.txtHallName.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Eggplant purple
            this.txtHallName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // lblHallName
            // 
            this.lblHallName.AutoSize = true;
            this.lblHallName.Location = new System.Drawing.Point(50, 315);
            this.lblHallName.Name = "lblHallName";
            this.lblHallName.Size = new System.Drawing.Size(80, 25);
            this.lblHallName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHallName.ForeColor = System.Drawing.Color.White;
            this.lblHallName.Text = "Hall Name";

            // 
            // btnAddHall
            // 
            this.btnAddHall.Location = new System.Drawing.Point(470, 340);
            this.btnAddHall.Name = "btnAddHall";
            this.btnAddHall.Size = new System.Drawing.Size(150, 40);
            this.btnAddHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddHall.Text = "Add Hall";
            this.btnAddHall.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel blue
            this.btnAddHall.ForeColor = System.Drawing.Color.White;
            this.btnAddHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // btnRemoveHall
            // 
            this.btnRemoveHall.Location = new System.Drawing.Point(640, 340);
            this.btnRemoveHall.Name = "btnRemoveHall";
            this.btnRemoveHall.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveHall.Text = "Remove Hall";
            this.btnRemoveHall.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel blue
            this.btnRemoveHall.ForeColor = System.Drawing.Color.White;
            this.btnRemoveHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // lblScreenType
            // 
            this.lblScreenType.AutoSize = true;
            this.lblScreenType.Location = new System.Drawing.Point(50, 385);
            this.lblScreenType.Name = "lblScreenType";
            this.lblScreenType.Size = new System.Drawing.Size(110, 25);
            this.lblScreenType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScreenType.ForeColor = System.Drawing.Color.White;
            this.lblScreenType.Text = "Screen Type";

            // 
            // cmbScreenType
            // 
            this.cmbScreenType.Location = new System.Drawing.Point(50, 410);
            this.cmbScreenType.Name = "cmbScreenType";
            this.cmbScreenType.Size = new System.Drawing.Size(400, 30);
            this.cmbScreenType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbScreenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScreenType.Items.AddRange(new object[] { "2D", "3D", "IMAX" });

            // 
            // New Fields: Age Rating
            // 
            this.txtAgeRating.Location = new System.Drawing.Point(50, 480);
            this.txtAgeRating.Name = "txtAgeRating";
            this.txtAgeRating.Size = new System.Drawing.Size(400, 30);
            this.txtAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAgeRating.ForeColor = System.Drawing.Color.White;
            this.txtAgeRating.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Eggplant purple
            this.txtAgeRating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblAgeRating.AutoSize = true;
            this.lblAgeRating.Location = new System.Drawing.Point(50, 455);
            this.lblAgeRating.Name = "lblAgeRating";
            this.lblAgeRating.Size = new System.Drawing.Size(90, 25);
            this.lblAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAgeRating.ForeColor = System.Drawing.Color.White;
            this.lblAgeRating.Text = "Age Rating";

            // 
            // New Fields: Director
            // 
            this.txtDirector.Location = new System.Drawing.Point(50, 550);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(400, 30);
            this.txtDirector.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDirector.ForeColor = System.Drawing.Color.White;
            this.txtDirector.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Eggplant purple
            this.txtDirector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblDirector.AutoSize = true;
            this.lblDirector.Location = new System.Drawing.Point(50, 525);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(75, 25);
            this.lblDirector.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDirector.ForeColor = System.Drawing.Color.White;
            this.lblDirector.Text = "Director";

            // 
            // New Fields: Actors
            // 
            this.txtActors.Location = new System.Drawing.Point(50, 620);
            this.txtActors.Multiline = true;
            this.txtActors.Name = "txtActors";
            this.txtActors.Size = new System.Drawing.Size(400, 60);
            this.txtActors.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtActors.ForeColor = System.Drawing.Color.White;
            this.txtActors.BackColor = System.Drawing.Color.FromArgb(76, 0, 153); // Eggplant purple
            this.txtActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(50, 595);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(65, 25);
            this.lblActors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblActors.ForeColor = System.Drawing.Color.White;
            this.lblActors.Text = "Actors";

            // AdminControl Form
            // 
            this.ClientSize = new System.Drawing.Size(850, 700);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.cmbHalls);
            this.Controls.Add(this.lblSelectHall);
            this.Controls.Add(this.cmbShowtimes);
            this.Controls.Add(this.lblSelectShowtime);
            this.Controls.Add(this.txtAgeRating);
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
            // Add all controls to the form
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.cmbHalls);
            this.Controls.Add(this.lblSelectHall);
            this.Controls.Add(this.cmbShowtimes);
            this.Controls.Add(this.lblSelectShowtime);
            this.Controls.Add(this.txtAgeRating);
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
            this.Name = "AdminControl";
            this.Text = "Admin Control Panel";
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 35); // Dark background
        }
    }
}