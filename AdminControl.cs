using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class AdminControl : Form
    {
        private MainForm mainForm;

        // Constructor
        public AdminControl(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Hook up events
            btnAddMovie.Click += BtnAddMovie_Click;
            btnRemoveMovie.Click += BtnRemoveMovie_Click;
            btnAddHall.Click += BtnAddHall_Click;
            btnRemoveHall.Click += BtnRemoveHall_Click;
        }

        // Event: Add Movie
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            if (!string.IsNullOrEmpty(movieTitle))
            {
                // Add movie logic (e.g., save to database or in-memory list)
                MessageBox.Show($"Movie '{movieTitle}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMovieTitle.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a movie title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Remove Movie
        private void BtnRemoveMovie_Click(object sender, EventArgs e)
        {
            string movieTitle = txtMovieTitle.Text.Trim();
            if (!string.IsNullOrEmpty(movieTitle))
            {
                // Remove movie logic (e.g., delete from database or in-memory list)
                MessageBox.Show($"Movie '{movieTitle}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMovieTitle.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a movie title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Add Hall
        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (!string.IsNullOrEmpty(hallName))
            {
                // Add hall logic
                MessageBox.Show($"Hall '{hallName}' added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHallName.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a hall name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event: Remove Hall
        private void BtnRemoveHall_Click(object sender, EventArgs e)
        {
            string hallName = txtHallName.Text.Trim();
            if (!string.IsNullOrEmpty(hallName))
            {
                // Remove hall logic
                MessageBox.Show($"Hall '{hallName}' removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHallName.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a hall name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}