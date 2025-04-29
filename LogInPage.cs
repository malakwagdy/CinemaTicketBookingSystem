using GUI_DB;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class LogInPage : Form
    {
        public LogInPage()
        {
            InitializeComponent();
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
            // Logic for logging in (to be implemented)
            MessageBox.Show("Log In button clicked!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LinkRegister_Click(object sender, EventArgs e)
        {
            // Logic to navigate to the Register Page
            Form1 registerPage = new Form1(); // Assuming Form1 is the Register Page
            registerPage.Show();
            this.Hide(); // Hide the Login Page while showing the Register Page
        }
    }
}