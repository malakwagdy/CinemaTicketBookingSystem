using System;
using System.Drawing;
using System.Windows.Forms;
using static GUI_DB.DatabaseManager;

namespace GUI_DB
{
    public partial class LogInPage : Form
    {
        private MainForm mainForm;
        DatabaseManager db = new DatabaseManager();
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
                if (textBox == txtEmailOrUsername) textBox.Text = "Email";
                else if (textBox == txtPassword) textBox.Text = "Password";

                textBox.ForeColor = Color.Gray; // Set text color to gray for placeholder
            }
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            string msg =db.Login(txtEmailOrUsername.Text,txtPassword.Text);
            User currentUser = db.GetUserById(txtEmailOrUsername.Text);
            bool isAdmin = currentUser.userType;

            if (msg == "Invalid email or password.")
            {
                MessageBox.Show(msg, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                //mainForm.OpenChildForm(new LogInPage(mainForm));
            }
            else 
            {
                GlobalVariable.setCurrentlyLoggedIN(txtEmailOrUsername.Text);
                MessageBox.Show("Login Successful!", "Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Check user type and navigate accordingly
                if (!isAdmin)
                {
                    // Open Admin Control Form
                    mainForm.OpenChildForm(new AdminControl(mainForm));
                } 
                else
                {
                    // Open Customer Movie List Form
                    mainForm.OpenChildForm(new CustomerMovieListForm(mainForm));
                }
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

        private void LogInPage_Load(object sender, EventArgs e)
        {

        }
    }
}