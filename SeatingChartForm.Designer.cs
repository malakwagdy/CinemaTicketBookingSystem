using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class SeatingChartForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Panel screenPanel;
        private Label lblScreen;
        private Panel seatingPanel;
        private TableLayoutPanel seatLayout;
        private Panel centerWrapperPanel;
        private Panel controlPanel;
        private Button btnBack;
        private Button btnContinue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            screenPanel = new Panel();
            lblScreen = new Label();
            seatingPanel = new Panel();
            seatLayout = new TableLayoutPanel();
            centerWrapperPanel = new Panel();
            controlPanel = new Panel();
            btnBack = new Button();
            btnContinue = new Button();

            // === lblTitle ===
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Height = 50;
            lblTitle.Text = "Movie Title - Showtime";

            // === screenPanel ===
            screenPanel.Dock = DockStyle.Top;
            screenPanel.Height = 50;
            screenPanel.BackColor = Color.FromArgb(50, 50, 70);
            screenPanel.Padding = new Padding(10);
            screenPanel.Controls.Add(lblScreen);

            // === lblScreen ===
            lblScreen.Dock = DockStyle.Fill;
            lblScreen.Text = "Screen";
            lblScreen.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblScreen.ForeColor = Color.White;
            lblScreen.TextAlign = ContentAlignment.MiddleCenter;

            // === seatLayout ===
            seatLayout.AutoSize = true;
            seatLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            seatLayout.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            seatLayout.BackColor = Color.Transparent;
            seatLayout.Anchor = AnchorStyles.None;

            // === centerWrapperPanel ===
            centerWrapperPanel.Dock = DockStyle.Fill;
            centerWrapperPanel.AutoScroll = true;
            centerWrapperPanel.BackColor = Color.Transparent;
            centerWrapperPanel.Controls.Add(seatLayout);

            seatLayout.Location = new Point(
                (centerWrapperPanel.Width - seatLayout.Width) / 2,
                (centerWrapperPanel.Height - seatLayout.Height) / 2
            );

            centerWrapperPanel.Resize += (s, e) =>
            {
                seatLayout.Left = (centerWrapperPanel.ClientSize.Width - seatLayout.Width) / 2;
                seatLayout.Top = (centerWrapperPanel.ClientSize.Height - seatLayout.Height) / 2;
            };

            // === controlPanel ===
            controlPanel.Dock = DockStyle.Left;
            controlPanel.Width = 150;
            controlPanel.BackColor = Color.FromArgb(35, 35, 50);
            controlPanel.Padding = new Padding(10);

            // === btnBack ===
            btnBack.Text = "Back";
            btnBack.Width = 120;
            btnBack.Height = 40;
            btnBack.BackColor = Color.Gray;
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Click += BtnBack_Click;
            btnBack.Top = 20;
            btnBack.Left = 10;

            // === btnContinue ===
            btnContinue.Text = "Continue";
            btnContinue.Width = 120;
            btnContinue.Height = 40;
            btnContinue.BackColor = Color.MediumSlateBlue;
            btnContinue.ForeColor = Color.White;
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.Click += BtnContinue_Click;
            btnContinue.Top = 80;
            btnContinue.Left = 10;

            controlPanel.Controls.Add(btnBack);
            controlPanel.Controls.Add(btnContinue);

            // === seatingPanel ===
            seatingPanel.Dock = DockStyle.Fill;
            seatingPanel.BackColor = Color.FromArgb(45, 45, 60);
            seatingPanel.Padding = new Padding(20);
            seatingPanel.Controls.Add(centerWrapperPanel);

            // === Form ===
            this.Text = "Seating Chart";
            this.BackColor = Color.FromArgb(45, 45, 60);
            this.ClientSize = new Size(1000, 700);
            this.Controls.Add(seatingPanel);
            this.Controls.Add(controlPanel);
            this.Controls.Add(screenPanel);
            this.Controls.Add(lblTitle);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
