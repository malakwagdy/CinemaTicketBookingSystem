using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class CinemaListForm : Form
    {
        private MainForm mainForm;

        public CinemaListForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            LoadCinemas();
        }

        private void LoadCinemas()
        {
            List<string> cinemas = CinemaRepository.GetCinemas(); // placeholder backend

            foreach (string cinema in cinemas)
            {
                Button btn = new Button
                {
                    Text = cinema,
                    Width = 300,
                    Height = 40,
                    Margin = new Padding(10),
                    BackColor = System.Drawing.Color.FromArgb(60, 60, 75),
                    ForeColor = System.Drawing.Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                btn.Click += (s, e) =>
                {
                    MessageBox.Show($"Selected Cinema: {cinema}\n(This can later open CustomerMovieListForm specific to cinema)", "Cinema Selected");
                    mainForm.OpenChildForm(new CustomerMovieListForm(mainForm));
                };

                flowLayoutPanelCinemas.Controls.Add(btn);
            }
        }
    }

    public static class CinemaRepository
    {
        public static List<string> GetCinemas()
        {
            // Placeholder list – Replace with real DB call later
            return new List<string>
            {
                "Downtown Cinema",
                "Mall Galaxy 8",
                "Sunset Cineplex",
                "MegaCine Plaza",
                "Skyline Theaters"
            };
        }
    }
}
