using System.Drawing;
using System.Windows.Forms;

namespace GUI_DB
{
    partial class TicketConfirmationForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblHeader;
        private Label lblMovie;
        private Label lblShowtime;
        private Label lblTicketId;
        private Label lblUnitPrice;
        private Label lblTotalPrice;
        private ListBox lstSeats;
        private Button btnConfirm;
        private Button btnCancel;
        private Panel contentPanel;
        private Label lblReservationDate;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.lblReservationDate = new System.Windows.Forms.Label();
            this.lblMovie = new System.Windows.Forms.Label();
            this.lblShowtime = new System.Windows.Forms.Label();
            this.lblTicketId = new System.Windows.Forms.Label();
            this.lstSeats = new System.Windows.Forms.ListBox();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(186, -1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(350, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Ticket Confirmation";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.Transparent;
            this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contentPanel.Controls.Add(this.lblReservationDate);
            this.contentPanel.Controls.Add(this.lblHeader);
            this.contentPanel.Controls.Add(this.lblMovie);
            this.contentPanel.Controls.Add(this.lblShowtime);
            this.contentPanel.Controls.Add(this.lblTicketId);
            this.contentPanel.Controls.Add(this.lstSeats);
            this.contentPanel.Controls.Add(this.lblUnitPrice);
            this.contentPanel.Controls.Add(this.lblTotalPrice);
            this.contentPanel.Controls.Add(this.btnConfirm);
            this.contentPanel.Controls.Add(this.btnCancel);
            this.contentPanel.Location = new System.Drawing.Point(331, 12);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(686, 662);
            this.contentPanel.TabIndex = 0;
            // 
            // lblReservationDate
            // 
            this.lblReservationDate.AutoSize = true;
            this.lblReservationDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReservationDate.ForeColor = System.Drawing.Color.White;
            this.lblReservationDate.Location = new System.Drawing.Point(419, 50);
            this.lblReservationDate.Name = "lblReservationDate";
            this.lblReservationDate.Size = new System.Drawing.Size(168, 28);
            this.lblReservationDate.TabIndex = 5;
            this.lblReservationDate.Text = "Reservation Date: ";
            // 
            // lblMovie
            // 
            this.lblMovie.AutoSize = true;
            this.lblMovie.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMovie.ForeColor = System.Drawing.Color.White;
            this.lblMovie.Location = new System.Drawing.Point(10, 50);
            this.lblMovie.Name = "lblMovie";
            this.lblMovie.Size = new System.Drawing.Size(0, 28);
            this.lblMovie.TabIndex = 1;
            // 
            // lblShowtime
            // 
            this.lblShowtime.AutoSize = true;
            this.lblShowtime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblShowtime.ForeColor = System.Drawing.Color.White;
            this.lblShowtime.Location = new System.Drawing.Point(10, 80);
            this.lblShowtime.Name = "lblShowtime";
            this.lblShowtime.Size = new System.Drawing.Size(0, 28);
            this.lblShowtime.TabIndex = 2;
            // 
            // lblTicketId
            // 
            this.lblTicketId.AutoSize = true;
            this.lblTicketId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketId.ForeColor = System.Drawing.Color.White;
            this.lblTicketId.Location = new System.Drawing.Point(10, 110);
            this.lblTicketId.Name = "lblTicketId";
            this.lblTicketId.Size = new System.Drawing.Size(0, 28);
            this.lblTicketId.TabIndex = 3;
            // 
            // lstSeats
            // 
            this.lstSeats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lstSeats.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstSeats.ForeColor = System.Drawing.Color.White;
            this.lstSeats.ItemHeight = 28;
            this.lstSeats.Location = new System.Drawing.Point(222, 153);
            this.lstSeats.Name = "lstSeats";
            this.lstSeats.Size = new System.Drawing.Size(250, 144);
            this.lstSeats.TabIndex = 4;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUnitPrice.ForeColor = System.Drawing.Color.White;
            this.lblUnitPrice.Location = new System.Drawing.Point(10, 250);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(0, 28);
            this.lblUnitPrice.TabIndex = 5;
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPrice.ForeColor = System.Drawing.Color.White;
            this.lblTotalPrice.Location = new System.Drawing.Point(10, 280);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(0, 28);
            this.lblTotalPrice.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(278, 396);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(140, 40);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Confirm Booking";
            this.btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(278, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // TicketConfirmationForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1280, 718);
            this.Controls.Add(this.contentPanel);
            this.Name = "TicketConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ticket Confirmation";
            this.Load += new System.EventHandler(this.TicketConfirmationForm_Load);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
