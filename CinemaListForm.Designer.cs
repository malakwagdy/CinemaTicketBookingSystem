using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class CinemaListForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private FlowLayoutPanel flowLayoutPanelCinemas;
        private Panel centerPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.flowLayoutPanelCinemas = new FlowLayoutPanel();
            this.centerPanel = new Panel();

            // === Form ===
            this.BackColor = Color.FromArgb(30, 30, 45);
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Available Cinemas";

            // === lblTitle ===
            this.lblTitle.Text = "Select a Cinema";
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 60;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // === flowLayoutPanel ===
            this.flowLayoutPanelCinemas.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanelCinemas.WrapContents = false;
            this.flowLayoutPanelCinemas.AutoSize = true;
            this.flowLayoutPanelCinemas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelCinemas.Padding = new Padding(10);

            // === centerPanel ===
            this.centerPanel.Dock = DockStyle.Fill;
            this.centerPanel.BackColor = Color.Transparent;
            this.centerPanel.Controls.Add(flowLayoutPanelCinemas);
            this.centerPanel.Resize += (s, e) =>
            {
                // Center the panel manually
                flowLayoutPanelCinemas.Left = (centerPanel.ClientSize.Width - flowLayoutPanelCinemas.Width) / 2;
                flowLayoutPanelCinemas.Top = (centerPanel.ClientSize.Height - flowLayoutPanelCinemas.Height) / 2;
            };

            // === Controls ===
            this.Controls.Add(centerPanel);
            this.Controls.Add(lblTitle);
        }
    }
}
