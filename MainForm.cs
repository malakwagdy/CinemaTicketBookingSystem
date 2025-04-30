using System;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Set the MainForm size and make it non-resizable
            this.Size = new System.Drawing.Size(1300, 700); // Adjust width and height as needed
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Make the form non-resizable
            this.MaximizeBox = false; // Disable the maximize button
            IsMdiContainer = true; // Set the form as an MDI container

            // Open the Login Page as the default child form
            OpenChildForm(new LogInPage(this));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // You can add any initialization code here if needed
        }

        public void OpenChildForm(Form childForm)
        {
            // Close existing child forms
            foreach (Form form in MdiChildren)
            {
                form.Close();
            }

            // Set the new child form
            childForm.MdiParent = this;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }
    }
}