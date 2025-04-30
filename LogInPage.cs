using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class LogInPage : Form
    {
        private MainForm mainForm;

        public LogInPage(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Add placeholder event handlers
            txtEmailOrUsername.Enter += RemovePlaceholderText;
            txtEmailOrUsername.Leave += AddPlaceholderText;
            txtPassword.Enter += RemovePlaceholderText;
            txtPassword.Leave += AddPlaceholderText;

            // Initialize placeholder text
            AddPlaceholderText(txtEmailOrUsername, EventArgs.Empty);
            AddPlaceholderText(txtPassword, EventArgs.Empty);
        }

        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.ForeColor == Color.Gray)
            {
                textBox.Text = ""; // Clear the placeholder text
                textBox.ForeColor = Color.White; // Set text color to white
            }
        }

        private void AddPlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Restore placeholder text
                if (textBox == txtEmailOrUsername) textBox.Text = "Email or Username";
                else if (textBox == txtPassword) textBox.Text = "Password";

                textBox.ForeColor = Color.Gray; // Set text color to gray for placeholder
            }
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            // Hardcoded credentials
            string hardCodedUsername = "user";
            string hardCodedPassword = "user123";

            // Check if entered credentials match the hardcoded ones
            if (txtEmailOrUsername.Text == hardCodedUsername && txtPassword.Text == hardCodedPassword)
            {
                // Navigate to the CustomerMovieList form
                mainForm.OpenChildForm(new CustomerMovieListForm(mainForm));
            }
            else
            {
                // Show error message
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkRegister_Click(object sender, EventArgs e)
        {
            // Navigate to the Registration Form using MainForm
            mainForm.OpenChildForm(new Form1(mainForm));
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Add any custom painting logic here if needed
        }
    }
}