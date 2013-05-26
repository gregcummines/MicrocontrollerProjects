namespace ChickenCoopBaseStation
{
    partial class ErrorStatusForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblErrorStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblErrorStatus
            // 
            this.lblErrorStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            this.lblErrorStatus.Location = new System.Drawing.Point(12, 9);
            this.lblErrorStatus.Name = "lblErrorStatus";
            this.lblErrorStatus.Size = new System.Drawing.Size(297, 48);
            this.lblErrorStatus.TabIndex = 0;
            this.lblErrorStatus.Text = "dfgsfdgsdfgsl;dfkjgl;skdfjgkl;sdfjgkl;sdjfgl;kjsdfkl;jgskld;fjgl;skjdfgl;jsd;fg";
            // 
            // ErrorStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 66);
            this.Controls.Add(this.lblErrorStatus);
            this.MaximizeBox = false;
            this.Name = "ErrorStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Status Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblErrorStatus;
    }
}