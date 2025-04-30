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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.filterLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.flowLayoutPanelMovies = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.txtActor = new System.Windows.Forms.TextBox();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(40)))));
            this.panelLeft.Controls.Add(this.filterLayout);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeft.Size = new System.Drawing.Size(260, 700);
            this.panelLeft.TabIndex = 1;
            // 
            // filterLayout
            // 
            this.filterLayout.AutoScroll = true;
            this.filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.filterLayout.Location = new System.Drawing.Point(10, 10);
            this.filterLayout.Name = "filterLayout";
            this.filterLayout.Size = new System.Drawing.Size(240, 680);
            this.filterLayout.TabIndex = 0;
            this.filterLayout.WrapContents = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.panelRight.Controls.Add(this.flowLayoutPanelMovies);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(260, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(840, 700);
            this.panelRight.TabIndex = 0;
            // 
            // flowLayoutPanelMovies
            // 
            this.flowLayoutPanelMovies.AutoScroll = true;
            this.flowLayoutPanelMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMovies.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMovies.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMovies.Name = "flowLayoutPanelMovies";
            this.flowLayoutPanelMovies.Padding = new System.Windows.Forms.Padding(15);
            this.flowLayoutPanelMovies.Size = new System.Drawing.Size(840, 700);
            this.flowLayoutPanelMovies.TabIndex = 0;
            this.flowLayoutPanelMovies.WrapContents = false;
            // 
            // cmbGenre
            // 
            this.cmbGenre.Location = new System.Drawing.Point(0, 0);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(121, 28);
            this.cmbGenre.TabIndex = 0;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(0, 0);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(100, 26);
            this.txtYear.TabIndex = 0;
            // 
            // txtDirector
            // 
            this.txtDirector.Location = new System.Drawing.Point(0, 0);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(100, 26);
            this.txtDirector.TabIndex = 0;
            // 
            // txtActor
            // 
            this.txtActor.Location = new System.Drawing.Point(0, 0);
            this.txtActor.Name = "txtActor";
            this.txtActor.Size = new System.Drawing.Size(100, 26);
            this.txtActor.TabIndex = 0;
            // 
            // CustomerMovieListForm
            // 
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "CustomerMovieListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie Listings";
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

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
