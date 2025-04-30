using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelForm = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.chkTerms = new System.Windows.Forms.CheckBox();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.panelLeft.Controls.Add(this.pictureBoxLeft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(533, 738);
            this.panelLeft.TabIndex = 1;
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLeft.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLeft.Image")));
            this.pictureBoxLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(533, 738);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLeft.TabIndex = 0;
            this.pictureBoxLeft.TabStop = false;
            this.pictureBoxLeft.Click += new System.EventHandler(this.pictureBoxLeft_Click_1);
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.panelRight.Controls.Add(this.panelForm);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(533, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(800, 738);
            this.panelRight.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.ColumnCount = 1;
            this.panelForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelForm.Controls.Add(this.lblTitle, 0, 0);
            this.panelForm.Controls.Add(this.txtFirstName, 0, 1);
            this.panelForm.Controls.Add(this.txtLastName, 0, 2);
            this.panelForm.Controls.Add(this.txtEmail, 0, 3);
            this.panelForm.Controls.Add(this.txtPassword, 0, 4);
            this.panelForm.Controls.Add(this.txtConfirmPassword, 0, 5);
            this.panelForm.Controls.Add(this.chkTerms, 0, 6);
            this.panelForm.Controls.Add(this.btnCreateAccount, 0, 7);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Margin = new System.Windows.Forms.Padding(4);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(67, 62, 67, 62);
            this.panelForm.RowCount = 7;
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.panelForm.Size = new System.Drawing.Size(800, 738);
            this.panelForm.TabIndex = 0;
            this.panelForm.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForm_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(71, 62);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(658, 74);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create an Account";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtFirstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFirstName.ForeColor = System.Drawing.Color.Gray;
            this.txtFirstName.Location = new System.Drawing.Point(67, 142);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(666, 30);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Text = "First Name";
            this.txtFirstName.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtFirstName.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtLastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLastName.ForeColor = System.Drawing.Color.Gray;
            this.txtLastName.Location = new System.Drawing.Point(67, 192);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(666, 30);
            this.txtLastName.TabIndex = 2;
            this.txtLastName.Text = "Last Name";
            this.txtLastName.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtLastName.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.Gray;
            this.txtEmail.Location = new System.Drawing.Point(67, 242);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(666, 30);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Text = "Email Address";
            this.txtEmail.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtEmail.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtPassword.Location = new System.Drawing.Point(67, 292);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(666, 30);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "Password";
            this.txtPassword.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtPassword.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtConfirmPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtConfirmPassword.Location = new System.Drawing.Point(67, 342);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(666, 30);
            this.txtConfirmPassword.TabIndex = 5;
            this.txtConfirmPassword.Text = "Confirm Password";
            this.txtConfirmPassword.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtConfirmPassword.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // chkTerms
            // 
            this.chkTerms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTerms.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkTerms.ForeColor = System.Drawing.Color.White;
            this.chkTerms.Location = new System.Drawing.Point(67, 398);
            this.chkTerms.Margin = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.chkTerms.Name = "chkTerms";
            this.chkTerms.Size = new System.Drawing.Size(666, 44);
            this.chkTerms.TabIndex = 6;
            this.chkTerms.Text = "I agree to the Terms & Conditions";
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnCreateAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCreateAccount.FlatAppearance.BorderSize = 0;
            this.btnCreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateAccount.ForeColor = System.Drawing.Color.White;
            this.btnCreateAccount.Location = new System.Drawing.Point(67, 466);
            this.btnCreateAccount.Margin = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(666, 36);
            this.btnCreateAccount.TabIndex = 7;
            this.btnCreateAccount.Text = "Create Account";
            this.btnCreateAccount.UseVisualStyleBackColor = false;
            this.btnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 738);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modern Register Form";
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        private void pictureBoxLeft_Click_1(object sender, EventArgs e)
        {
            // Optional: Add functionality for the picture box click event here
            // For now, it's empty to resolve the error
        }

        #endregion

        private Panel panelLeft;
        private Panel panelRight;
        private PictureBox pictureBoxLeft;
        private TableLayoutPanel panelForm;
        private Label lblTitle;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private CheckBox chkTerms;
        private Button btnCreateAccount;
    }
}