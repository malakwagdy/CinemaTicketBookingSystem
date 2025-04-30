using System.Windows.Forms;

namespace GUI_DB
{
    partial class AdminControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private System.Windows.Forms.TextBox txtMovieTitle;
        private System.Windows.Forms.Button btnAddMovie;
        private System.Windows.Forms.Button btnRemoveMovie;
        private System.Windows.Forms.TextBox txtHallName;
        private System.Windows.Forms.Button btnAddHall;
        private System.Windows.Forms.Button btnRemoveHall;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblHallName;
        private System.Windows.Forms.Panel topPanel;

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
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnRemoveMovie = new System.Windows.Forms.Button();
            this.txtHallName = new System.Windows.Forms.TextBox();
            this.btnAddHall = new System.Windows.Forms.Button();
            this.btnRemoveHall = new System.Windows.Forms.Button();
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblHallName = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();

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
            this.txtMovieTitle.ForeColor = System.Drawing.Color.Black;
            this.txtMovieTitle.BackColor = System.Drawing.Color.White;
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
            this.txtHallName.Location = new System.Drawing.Point(50, 200);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(400, 30);
            this.txtHallName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHallName.ForeColor = System.Drawing.Color.Black;
            this.txtHallName.BackColor = System.Drawing.Color.White;
            this.txtHallName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // lblHallName
            // 
            this.lblHallName.AutoSize = true;
            this.lblHallName.Location = new System.Drawing.Point(50, 175);
            this.lblHallName.Name = "lblHallName";
            this.lblHallName.Size = new System.Drawing.Size(80, 25);
            this.lblHallName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHallName.ForeColor = System.Drawing.Color.White;

            // 
            // btnAddHall
            // 
            this.btnAddHall.Location = new System.Drawing.Point(470, 200);
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
            this.btnRemoveHall.Location = new System.Drawing.Point(640, 200);
            this.btnRemoveHall.Name = "btnRemoveHall";
            this.btnRemoveHall.Size = new System.Drawing.Size(150, 40);
            this.btnRemoveHall.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemoveHall.Text = "Remove Hall";
            this.btnRemoveHall.BackColor = System.Drawing.Color.FromArgb(70, 130, 180); // Steel blue
            this.btnRemoveHall.ForeColor = System.Drawing.Color.White;
            this.btnRemoveHall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 
            // AdminControl
            // 
            this.ClientSize = new System.Drawing.Size(850, 400);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.btnAddMovie);
            this.Controls.Add(this.btnRemoveMovie);
            this.Controls.Add(this.txtHallName);
            this.Controls.Add(this.lblHallName);
            this.Controls.Add(this.btnAddHall);
            this.Controls.Add(this.btnRemoveHall);
            this.Name = "AdminControl";
            this.Text = "Admin Control Panel";
            this.BackColor = System.Drawing.Color.FromArgb(25, 25, 35); // Dark background
        }
    }
}