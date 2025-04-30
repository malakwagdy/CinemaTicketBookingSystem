using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions; // For potential phone number validation

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
            txtFirstName.GotFocus += RemovePlaceholderText; // Use GotFocus for consistency
            txtFirstName.LostFocus += AddPlaceholderText;   // Use LostFocus for consistency
            txtLastName.GotFocus += RemovePlaceholderText;
            txtLastName.LostFocus += AddPlaceholderText;
            txtPassword.GotFocus += RemovePlaceholderText;
            txtPassword.LostFocus += AddPlaceholderText;
            txtConfirmPassword.GotFocus += RemovePlaceholderText;
            txtConfirmPassword.LostFocus += AddPlaceholderText;
            txtEmail.GotFocus += RemovePlaceholderText;
            txtEmail.LostFocus += AddPlaceholderText;
            // --- Hook up new control events ---
            txtPhoneNumber.GotFocus += RemovePlaceholderText;
            txtPhoneNumber.LostFocus += AddPlaceholderText;
            // --- End hook up new control events ---

            // Set default value for DateTimePicker (optional, but good practice)
            // Set it to a value that makes sense, e.g., 18 years ago, or just today's date.
            dtpBirthdate.Value = DateTime.Today; // Or DateTime.Today.AddYears(-18);
            dtpBirthdate.MaxDate = DateTime.Today; // User cannot select a future date


            // Initialize placeholder text
            AddPlaceholderText(txtFirstName, EventArgs.Empty);
            AddPlaceholderText(txtLastName, EventArgs.Empty);
            AddPlaceholderText(txtPassword, EventArgs.Empty);
            AddPlaceholderText(txtConfirmPassword, EventArgs.Empty);
            AddPlaceholderText(txtEmail, EventArgs.Empty);
            // --- Initialize new placeholders ---
            AddPlaceholderText(txtPhoneNumber, EventArgs.Empty);
            // --- End initialize new placeholders ---

            // Set initial PasswordChar state correctly if placeholder exists
            UpdatePasswordChar(txtPassword);
            UpdatePasswordChar(txtConfirmPassword);
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            // --- Enhanced Validation ---
            if (IsPlaceholder(txtFirstName, "First Name") ||
                IsPlaceholder(txtLastName, "Last Name") ||
                IsPlaceholder(txtEmail, "Email Address") ||
                IsPlaceholder(txtPhoneNumber, "Phone Number") || // Check Phone Number
                IsPlaceholder(txtPassword, "Password") ||
                IsPlaceholder(txtConfirmPassword, "Confirm Password"))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email Format Validation (Simple)
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Phone Number Validation (Example - Adjust Regex as needed for your format)
            // This regex allows digits, spaces, hyphens, parentheses, optional leading +
            // Examples: +1 (555) 123-4567, 555-123-4567, 555 123 4567
            string phonePattern = @"^(\+?\d{1,3}[-.\s]?)?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";
            if (!Regex.IsMatch(txtPhoneNumber.Text, phonePattern))
            {
                MessageBox.Show("Please enter a valid phone number format.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return;
            }


            // Birthdate Validation (Example: Check if user is at least 18)
            if (dtpBirthdate.Value > DateTime.Today.AddYears(-18))
            {
                MessageBox.Show("You must be at least 18 years old to register.", "Age Requirement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthdate.Focus();
                return;
            }


            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Optionally clear password fields
                // txtPassword.Clear();
                // txtConfirmPassword.Clear();
                // AddPlaceholderText(txtPassword, EventArgs.Empty);
                // AddPlaceholderText(txtConfirmPassword, EventArgs.Empty);
                txtPassword.Focus();
                return;
            }

            if (!chkTerms.Checked)
            {
                MessageBox.Show("You must agree to the Terms & Conditions.", "Terms Agreement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkTerms.Focus();
                return;
            }

            // --- Get Data (Example) ---
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            DateTime birthDate = dtpBirthdate.Value;
            string password = txtPassword.Text; // Remember to HASH this before saving!

            // --- End Get Data ---


            // Simulate saving the user's registration details
            // **IMPORTANT**: In a real application, HASH the password before saving!
            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last Name: {lastName}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Phone: {phoneNumber}");
            Console.WriteLine($"Birthdate: {birthDate.ToShortDateString()}");
            Console.WriteLine($"Password (Plain): {password} <-- HASH THIS!");


            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Navigate back to the Login Page
            mainForm.OpenChildForm(new LogInPage(mainForm));
        }

        // --- Placeholder Handling ---
        private void RemovePlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.ForeColor == Color.Gray)
            {
                textBox.Text = ""; // Clear the placeholder text
                textBox.ForeColor = Color.White; // Set the text color to white for user input

                // Handle password fields specifically
                UpdatePasswordChar(textBox);
            }
        }

        private void AddPlaceholderText(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ForeColor = Color.Gray; // Set the text color to gray to indicate placeholder

                // Restore placeholder text based on which TextBox triggered the event
                if (textBox == txtFirstName) textBox.Text = "First Name";
                else if (textBox == txtLastName) textBox.Text = "Last Name";
                else if (textBox == txtPassword) textBox.Text = "Password";
                else if (textBox == txtConfirmPassword) textBox.Text = "Confirm Password";
                else if (textBox == txtEmail) textBox.Text = "Email Address";
                else if (textBox == txtPhoneNumber) textBox.Text = "Phone Number"; // Added

                // Handle password fields specifically
                UpdatePasswordChar(textBox);
            }
        }

        // Helper to check if a textbox contains placeholder text
        private bool IsPlaceholder(TextBox textBox, string placeholder)
        {
            return string.IsNullOrWhiteSpace(textBox.Text) || (textBox.ForeColor == Color.Gray && textBox.Text == placeholder);
        }

        // Helper to set PasswordChar based on whether it's placeholder or actual input
        private void UpdatePasswordChar(TextBox textBox)
        {
            if (textBox == txtPassword || textBox == txtConfirmPassword)
            {
                // Use PasswordChar only if it's NOT placeholder text
                textBox.UseSystemPasswordChar = (textBox.ForeColor != Color.Gray);
            }
        }
        // --- End Placeholder Handling ---


        // --- Validation Helpers ---
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {
            // Custom painting if needed, otherwise can be empty
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Actions to perform when the form loads, if any
        }
    }
}