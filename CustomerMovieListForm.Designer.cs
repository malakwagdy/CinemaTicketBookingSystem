using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class CustomerMovieListForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelLeft;
        private Panel panelRight;
        private FlowLayoutPanel flowLayoutPanelMovies;
        private Label lblTitle;
        private ComboBox cmbGenre;
        private TextBox txtYear, txtDirector, txtActor;
        private FlowLayoutPanel filterLayout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }




        private void InitializeComponent()
        {
            // Initialize controls
            this.panelLeft = new Panel();
            this.panelRight = new Panel();
            this.flowLayoutPanelMovies = new FlowLayoutPanel();
            this.cmbGenre = new ComboBox();
            this.txtYear = new TextBox();
            this.txtDirector = new TextBox();
            this.txtActor = new TextBox();
            this.filterLayout = new FlowLayoutPanel();

            // === PanelLeft ===
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 260;
            this.panelLeft.BackColor = Color.FromArgb(25, 25, 40);
            this.panelLeft.Padding = new Padding(10);
            this.panelLeft.Controls.Add(filterLayout);

            // === Filter Layout ===
            this.filterLayout.Dock = DockStyle.Fill;
            this.filterLayout.FlowDirection = FlowDirection.TopDown;
            this.filterLayout.WrapContents = false;
            this.filterLayout.AutoScroll = true;

            AddFilterControl("Age Rating", CreateRadioGroup(new[] { "All", "G", "PG", "PG-13", "R" }, "Age_"));
           


            this.cmbGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGenre.Items.AddRange(new object[] { "All", "Action", "Comedy", "Drama", "Fantasy", "Sci-Fi" });
            this.cmbGenre.SelectedIndex = 0;
            AddFilterControl("Genre", this.cmbGenre);

            // === PanelRight ===
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.BackColor = Color.FromArgb(45, 45, 60);
            this.panelRight.Controls.Add(flowLayoutPanelMovies);

            // === Movie Panel ===
            this.flowLayoutPanelMovies.Dock = DockStyle.Fill;
            this.flowLayoutPanelMovies.AutoScroll = true;
            this.flowLayoutPanelMovies.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanelMovies.WrapContents = false;
            this.flowLayoutPanelMovies.Padding = new Padding(15);

            // === Form ===
            this.Text = "Movie Listings";
            this.ClientSize = new Size(1100, 700);
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        void AddFilterControl(string labelText, Control control)
        {
            var label = new Label
            {
                Text = labelText,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Height = 20,
                AutoSize = false
            };
            control.Width = 220;
            control.Margin = new Padding(0, 0, 0, 10);
            filterLayout.Controls.Add(label);
            filterLayout.Controls.Add(control);
        }

        private Panel CreateRadioGroup(string[] options, string tagPrefix)
        {
            var panel = new Panel
            {
                Width = 220,
                Height = options.Length * 25,
                BackColor = Color.Transparent
            };

            int y = 0;
            foreach (var opt in options)
            {
                var rb = new RadioButton
                {
                    Text = opt,
                    Tag = tagPrefix + opt.Replace(" ", ""),
                    ForeColor = Color.White,
                    Location = new Point(0, y),
                    Width = 220
                };
                if (opt == "All" || opt == "All Times") rb.Checked = true;
                panel.Controls.Add(rb);
                y += 25;
            }

            return panel;
        }
    }
}
