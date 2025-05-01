using System.Windows.Forms; // Added for clarity, though implicitly used

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
        private System.Windows.Forms.Label lblTopPanel; // Explicitly declare the label for the top panel
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
            this.lblTopPanel = new System.Windows.Forms.Label();
            this.cmbScreenType = new System.Windows.Forms.ComboBox();
            this.lblScreenType = new System.Windows.Forms.Label();
            this.txtAgeRating = new System.Windows.Forms.TextBox();
            this.lblAgeRating = new System.Windows.Forms.Label();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.lblDirector = new System.Windows.Forms.Label();
            this.txtActors = new System.Windows.Forms.TextBox();
            this.lblActors = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
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
            this.txtMovieTitle.Size = new System.Drawing.Size(400, 34);
            this.txtMovieTitle.TabIndex = 1;
            // 
            // cmbHalls
            // 
            this.cmbHalls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHalls.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbHalls.FormattingEnabled = true;
            this.cmbHalls.Location = new System.Drawing.Point(50, 170);
            this.cmbHalls.Name = "cmbHalls";
            this.cmbHalls.Size = new System.Drawing.Size(400, 36);
            this.cmbHalls.TabIndex = 3;
            // 
            // cmbShowtimes
            // 
            this.cmbShowtimes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShowtimes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbShowtimes.FormattingEnabled = true;
            this.cmbShowtimes.Location = new System.Drawing.Point(50, 240);
            this.cmbShowtimes.Name = "cmbShowtimes";
            this.cmbShowtimes.Size = new System.Drawing.Size(400, 36);
            this.cmbShowtimes.TabIndex = 5;
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddMovie.ForeColor = System.Drawing.Color.White;
            this.btnAddMovie.Location = new System.Drawing.Point(470, 98);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(150, 40);
            this.btnAddMovie.TabIndex = 13;
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.UseVisualStyleBackColor = false;
            // 
            // btnRemoveMovie
            // 
            this.btnRemoveMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnRemoveMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveMovie.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveMovie.ForeColor = System.Drawing.Color.White;
            this.btnRemoveMovie.Location = new System.Drawing.Point(640, 98);
            this.btnRemoveMovie.Name = "btnRemoveMovie";
            this.btnRemoveMovie.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveMovie.TabIndex = 14;
            this.btnRemoveMovie.Text = "Remove Movie";
            this.btnRemoveMovie.UseVisualStyleBackColor = false;
            // 
            // txtHallName
            // 
            this.txtHallName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtHallName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHallName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHallName.ForeColor = System.Drawing.Color.White;
            this.txtHallName.Location = new System.Drawing.Point(50, 434);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(400, 34);
            this.txtHallName.TabIndex = 7;
            // 
            // btnAddHall
            // 
            this.btnAddHall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnAddHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAddHall.ForeColor = System.Drawing.Color.White;
            this.btnAddHall.Location = new System.Drawing.Point(470, 432);
            this.btnAddHall.Name = "btnAddHall";
            this.btnAddHall.Size = new System.Drawing.Size(150, 40);
            this.btnAddHall.TabIndex = 15;
            this.btnAddHall.Text = "Add Hall";
            this.btnAddHall.UseVisualStyleBackColor = false;
            // 
            // btnRemoveHall
            // 
            this.btnRemoveHall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnRemoveHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveHall.ForeColor = System.Drawing.Color.White;
            this.btnRemoveHall.Location = new System.Drawing.Point(640, 432);
            this.btnRemoveHall.Name = "btnRemoveHall";
            this.btnRemoveHall.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveHall.TabIndex = 16;
            this.btnRemoveHall.Text = "Remove Hall";
            this.btnRemoveHall.UseVisualStyleBackColor = false;
            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMovieTitle.ForeColor = System.Drawing.Color.White;
            this.lblMovieTitle.Location = new System.Drawing.Point(50, 75);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(120, 28);
            this.lblMovieTitle.TabIndex = 2;
            this.lblMovieTitle.Text = "Movie Title";
            // 
            // lblSelectHall
            // 
            this.lblSelectHall.AutoSize = true;
            this.lblSelectHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectHall.ForeColor = System.Drawing.Color.White;
            this.lblSelectHall.Location = new System.Drawing.Point(50, 145);
            this.lblSelectHall.Name = "lblSelectHall";
            this.lblSelectHall.Size = new System.Drawing.Size(113, 28);
            this.lblSelectHall.TabIndex = 4;
            this.lblSelectHall.Text = "Select Hall";
            // 
            // lblSelectShowtime
            // 
            this.lblSelectShowtime.AutoSize = true;
            this.lblSelectShowtime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectShowtime.ForeColor = System.Drawing.Color.White;
            this.lblSelectShowtime.Location = new System.Drawing.Point(50, 215);
            this.lblSelectShowtime.Name = "lblSelectShowtime";
            this.lblSelectShowtime.Size = new System.Drawing.Size(169, 28);
            this.lblSelectShowtime.TabIndex = 6;
            this.lblSelectShowtime.Text = "Select Showtime";
            // 
            // lblHallName
            // 
            this.lblHallName.AutoSize = true;
            this.lblHallName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHallName.ForeColor = System.Drawing.Color.White;
            this.lblHallName.Location = new System.Drawing.Point(50, 409);
            this.lblHallName.Name = "lblHallName";
            this.lblHallName.Size = new System.Drawing.Size(112, 28);
            this.lblHallName.TabIndex = 8;
            this.lblHallName.Text = "Hall Name";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.topPanel.Controls.Add(this.lblTopPanel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(850, 60);
            this.topPanel.TabIndex = 0;
            // 
            // lblTopPanel
            // 
            this.lblTopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopPanel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTopPanel.ForeColor = System.Drawing.Color.White;
            this.lblTopPanel.Location = new System.Drawing.Point(0, 0);
            this.lblTopPanel.Name = "lblTopPanel";
            this.lblTopPanel.Size = new System.Drawing.Size(850, 60);
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
            this.cmbScreenType.Location = new System.Drawing.Point(50, 496);
            this.cmbScreenType.Name = "cmbScreenType";
            this.cmbScreenType.Size = new System.Drawing.Size(400, 36);
            this.cmbScreenType.TabIndex = 10;
            // 
            // lblScreenType
            // 
            this.lblScreenType.AutoSize = true;
            this.lblScreenType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScreenType.ForeColor = System.Drawing.Color.White;
            this.lblScreenType.Location = new System.Drawing.Point(50, 471);
            this.lblScreenType.Name = "lblScreenType";
            this.lblScreenType.Size = new System.Drawing.Size(126, 28);
            this.lblScreenType.TabIndex = 9;
            this.lblScreenType.Text = "Screen Type";
            // 
            // txtAgeRating
            // 
            this.txtAgeRating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtAgeRating.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAgeRating.ForeColor = System.Drawing.Color.White;
            this.txtAgeRating.Location = new System.Drawing.Point(456, 238);
            this.txtAgeRating.Name = "txtAgeRating";
            this.txtAgeRating.Size = new System.Drawing.Size(400, 34);
            this.txtAgeRating.TabIndex = 11;
            // 
            // lblAgeRating
            // 
            this.lblAgeRating.AutoSize = true;
            this.lblAgeRating.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAgeRating.ForeColor = System.Drawing.Color.White;
            this.lblAgeRating.Location = new System.Drawing.Point(465, 207);
            this.lblAgeRating.Name = "lblAgeRating";
            this.lblAgeRating.Size = new System.Drawing.Size(117, 28);
            this.lblAgeRating.TabIndex = 12;
            this.lblAgeRating.Text = "Age Rating";
            // 
            // txtDirector
            // 
            this.txtDirector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtDirector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDirector.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDirector.ForeColor = System.Drawing.Color.White;
            this.txtDirector.Location = new System.Drawing.Point(456, 170);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(400, 34);
            this.txtDirector.TabIndex = 17;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDirector.ForeColor = System.Drawing.Color.White;
            this.lblDirector.Location = new System.Drawing.Point(456, 145);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(90, 28);
            this.lblDirector.TabIndex = 18;
            this.lblDirector.Text = "Director";
            // 
            // txtActors
            // 
            this.txtActors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.txtActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActors.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtActors.ForeColor = System.Drawing.Color.White;
            this.txtActors.Location = new System.Drawing.Point(451, 310);
            this.txtActors.Multiline = true;
            this.txtActors.Name = "txtActors";
            this.txtActors.Size = new System.Drawing.Size(400, 60);
            this.txtActors.TabIndex = 19;
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblActors.ForeColor = System.Drawing.Color.White;
            this.lblActors.Location = new System.Drawing.Point(451, 285);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(73, 28);
            this.lblActors.TabIndex = 20;
            this.lblActors.Text = "Actors";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(104, 568);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 34);
            this.textBox1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 537);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 28);
            this.label1.TabIndex = 22;
            this.label1.Text = "Number of Rows";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(261, 537);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 28);
            this.label2.TabIndex = 23;
            this.label2.Text = "Seats Per Row";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(301, 568);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(44, 34);
            this.textBox2.TabIndex = 24;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox1.Location = new System.Drawing.Point(489, 499);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(156, 33);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Is Premium";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(850, 700);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
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
            this.Controls.Add(this.topPanel);
            this.Name = "AdminControl";
            this.Text = "Admin Control Panel";
            this.Load += new System.EventHandler(this.AdminControl_Load);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private CheckBox checkBox1;
    }
}