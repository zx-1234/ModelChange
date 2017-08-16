namespace ModelChange.Commands
{
    partial class ChangesInformationForm
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
            this.changesdataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.changesdataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // changesdataGridView
            // 
            this.changesdataGridView.AllowUserToAddRows = false;
            this.changesdataGridView.AllowUserToDeleteRows = false;
            this.changesdataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.changesdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.changesdataGridView.Location = new System.Drawing.Point(-19, 2);
            this.changesdataGridView.Name = "changesdataGridView";
            this.changesdataGridView.ReadOnly = true;
            this.changesdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.changesdataGridView.Size = new System.Drawing.Size(691, 122);
            this.changesdataGridView.TabIndex = 1;
            // 
            // ChangesInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 124);
            this.Controls.Add(this.changesdataGridView);
            this.Name = "ChangesInformationForm";
            this.Text = "ChangesInfoForm";
            ((System.ComponentModel.ISupportInitialize)(this.changesdataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView changesdataGridView;
    }
}