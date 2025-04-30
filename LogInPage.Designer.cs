using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class LogInPage : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInPage));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelForm = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtEmailOrUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.linkRegister = new System.Windows.Forms.LinkLabel();
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
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(600, 923);
            this.panelLeft.TabIndex = 1;
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLeft.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLeft.Image")));
            this.pictureBoxLeft.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(600, 923);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLeft.TabIndex = 0;
            this.pictureBoxLeft.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.panelRight.Controls.Add(this.panelForm);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(600, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(900, 923);
            this.panelRight.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.ColumnCount = 1;
            this.panelForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelForm.Controls.Add(this.lblTitle, 0, 0);
            this.panelForm.Controls.Add(this.txtEmailOrUsername, 0, 1);
            this.panelForm.Controls.Add(this.txtPassword, 0, 2);
            this.panelForm.Controls.Add(this.btnLogIn, 0, 3);
            this.panelForm.Controls.Add(this.linkRegister, 0, 4);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(75, 77, 75, 77);
            this.panelForm.RowCount = 5;
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelForm.Size = new System.Drawing.Size(900, 923);
            this.panelForm.TabIndex = 0;
            this.panelForm.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForm_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(79, 77);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(742, 92);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Log In";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEmailOrUsername
            // 
            this.txtEmailOrUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtEmailOrUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmailOrUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmailOrUsername.ForeColor = System.Drawing.Color.Gray;
            this.txtEmailOrUsername.Location = new System.Drawing.Point(75, 177);
            this.txtEmailOrUsername.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.txtEmailOrUsername.Name = "txtEmailOrUsername";
            this.txtEmailOrUsername.Size = new System.Drawing.Size(750, 34);
            this.txtEmailOrUsername.TabIndex = 1;
            this.txtEmailOrUsername.Text = "Email or Username";
            this.txtEmailOrUsername.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtEmailOrUsername.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(75)))));
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtPassword.Location = new System.Drawing.Point(75, 239);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(750, 34);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "Password";
            this.txtPassword.GotFocus += new System.EventHandler(this.RemovePlaceholderText);
            this.txtPassword.LostFocus += new System.EventHandler(this.AddPlaceholderText);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnLogIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogIn.FlatAppearance.BorderSize = 0;
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogIn.ForeColor = System.Drawing.Color.White;
            this.btnLogIn.Location = new System.Drawing.Point(75, 308);
            this.btnLogIn.Margin = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(750, 45);
            this.btnLogIn.TabIndex = 3;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.BtnLogIn_Click);
            // 
            // linkRegister
            // 
            this.linkRegister.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkRegister.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkRegister.ForeColor = System.Drawing.Color.White;
            this.linkRegister.Location = new System.Drawing.Point(79, 826);
            this.linkRegister.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkRegister.Name = "linkRegister";
            this.linkRegister.Size = new System.Drawing.Size(742, 20);
            this.linkRegister.TabIndex = 4;
            this.linkRegister.TabStop = true;
            this.linkRegister.Text = "Don\'t have an account? Register for Free !!!";
            this.linkRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkRegister.Click += new System.EventHandler(this.LinkRegister_Click);
            // 
            // LogInPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 923);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LogInPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modern Log In Form";
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelLeft;
        private Panel panelRight;
        private PictureBox pictureBoxLeft;
        private TableLayoutPanel panelForm;
        private Label lblTitle;
        private TextBox txtEmailOrUsername;
        private TextBox txtPassword;
        private Button btnLogIn;
        private LinkLabel linkRegister;
    }
}