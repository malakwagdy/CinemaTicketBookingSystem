using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class CustomerMovieListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Declare the controls that remain/are added back
        private System.Windows.Forms.Panel filterLayout; // Holds filters and back button
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMovies;
        private System.Windows.Forms.Button btnBack; // *** The Back Button ***
        private System.Windows.Forms.ComboBox cmbGenre; // *** ADDED BACK ***

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterLayout = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.flowLayoutPanelMovies = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbGenre = new System.Windows.Forms.ComboBox(); // *** ADDED BACK Instantiation ***
            this.filterLayout.SuspendLayout();
            this.SuspendLayout();

            //
            // filterLayout
            //
            this.filterLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.filterLayout.Controls.Add(this.btnBack);
            // Note: Filter controls (Age Radio, Genre Combo) will be added dynamically via AddFilterControl
            this.filterLayout.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterLayout.Location = new System.Drawing.Point(0, 0);
            this.filterLayout.Name = "filterLayout";
            this.filterLayout.Padding = new System.Windows.Forms.Padding(10);
            this.filterLayout.Size = new System.Drawing.Size(220, 600);
            this.filterLayout.TabIndex = 0;

            //
            // btnBack
            //
            this.btnBack.BackColor = System.Drawing.Color.Gray;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(13, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 40);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            // Click event handler is attached in the main .cs file

            //
            // flowLayoutPanelMovies
            //
            this.flowLayoutPanelMovies.AutoScroll = true;
            this.flowLayoutPanelMovies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.flowLayoutPanelMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMovies.Location = new System.Drawing.Point(this.filterLayout.Width, 0);
            this.flowLayoutPanelMovies.Name = "flowLayoutPanelMovies";
            this.flowLayoutPanelMovies.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelMovies.Size = new System.Drawing.Size(780, 600);
            this.flowLayoutPanelMovies.TabIndex = 1;

            //
            // cmbGenre (Configuration mainly happens in .cs AddFilterControl)
            // *** ADDED BACK basic properties for consistency ***
            //
            this.cmbGenre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.cmbGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; // Prevent typing
            this.cmbGenre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGenre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbGenre.ForeColor = System.Drawing.Color.White;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(180, 28); // Example size, can be adjusted in AddFilterControl

            //
            // CustomerMovieListForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.flowLayoutPanelMovies);
            this.Controls.Add(this.filterLayout);
            this.Name = "CustomerMovieListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Movie and Showtime";
            this.Load += new System.EventHandler(this.CustomerMovieListForm_Load_1);
            this.filterLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

    }
}