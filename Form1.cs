using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    public partial class Form1 : Form
    {
        private MainForm mainForm;

        public Form1(MainForm form)
        {
            InitializeComponent();
            mainForm = form;

            // Hook up placeholder events
            txtFirstName.Enter += RemovePlaceholderText;
            txtFirstName.Leave += AddPlaceholderText;
            txtLastName.Enter += RemovePlaceholderText;
            txtLastName.Leave += AddPlaceholderText;
            txtPassword.Enter += RemovePlaceholderText;
            txtPassword.Leave += AddPlaceholderText;
            txtConfirmPassword.Enter += RemovePlaceholderText;
            txtConfirmPassword.Leave += AddPlaceholderText;
            txtEmail.Enter += RemovePlaceholderText;
            txtEmail.Leave += AddPlaceholderText;

            // Initialize placeholder text
            AddPlaceholderText(txtFirstName, EventArgs.Empty);
            AddPlaceholderText(txtLastName, EventArgs.Empty);
            AddPlaceholderText(txtPassword, EventArgs.Empty);
            AddPlaceholderText(txtConfirmPassword, EventArgs.Empty);
            AddPlaceholderText(txtEmail, EventArgs.Empty);
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || txtFirstName.Text == "First Name" ||
                string.IsNullOrWhiteSpace(txtLastName.Text) || txtLastName.Text == "Last Name" ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text == "Password" ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text) || txtConfirmPassword.Text == "Confirm Password" ||
                string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail.Text == "Email Address")
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!chkTerms.Checked)
            {
                MessageBox.Show("You must agree to the Terms & Conditions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Simulate saving the user's registration details (e.g., save to a database)
            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Navigate back to the Login Page
            mainForm.OpenChildForm(new LogInPage(mainForm));
        }

        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.ForeColor == Color.Gray)
            {
                textBox.Text = ""; // Clear the placeholder text
                textBox.ForeColor = Color.White; // Set the text color to white for user input
            }
        }

        private void AddPlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Restore placeholder text based on which TextBox triggered the event
                if (textBox == txtFirstName) textBox.Text = "First Name";
                else if (textBox == txtLastName) textBox.Text = "Last Name";
                else if (textBox == txtPassword) textBox.Text = "Password";
                else if (textBox == txtConfirmPassword) textBox.Text = "Confirm Password";
                else if (textBox == txtEmail) textBox.Text = "Email Address";

                textBox.ForeColor = Color.Gray; // Set the text color to gray to indicate placeholder
            }
        }

        private void BtnBackToLogin_Click(object sender, EventArgs e)
        {
            // Navigate back to the Login Page
            mainForm.OpenChildForm(new LogInPage(mainForm));
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}