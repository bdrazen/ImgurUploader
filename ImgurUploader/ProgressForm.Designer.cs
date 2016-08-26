namespace ImgurUploader
{
    partial class ProgressForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
			this.fileListProgressBar = new System.Windows.Forms.ProgressBar();
			this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
			this.uploadName = new System.Windows.Forms.Label();
			this.uploadPercent = new System.Windows.Forms.Label();
			this.fileListName = new System.Windows.Forms.Label();
			this.fileListPercent = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.updateTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// fileListProgressBar
			// 
			this.fileListProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileListProgressBar.Location = new System.Drawing.Point(12, 73);
			this.fileListProgressBar.Name = "fileListProgressBar";
			this.fileListProgressBar.Size = new System.Drawing.Size(396, 23);
			this.fileListProgressBar.TabIndex = 0;
			// 
			// uploadProgressBar
			// 
			this.uploadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uploadProgressBar.Location = new System.Drawing.Point(12, 25);
			this.uploadProgressBar.Name = "uploadProgressBar";
			this.uploadProgressBar.Size = new System.Drawing.Size(396, 23);
			this.uploadProgressBar.TabIndex = 0;
			// 
			// uploadName
			// 
			this.uploadName.AutoSize = true;
			this.uploadName.Location = new System.Drawing.Point(12, 51);
			this.uploadName.Name = "uploadName";
			this.uploadName.Size = new System.Drawing.Size(0, 13);
			this.uploadName.TabIndex = 1;
			// 
			// uploadPercent
			// 
			this.uploadPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uploadPercent.Location = new System.Drawing.Point(345, 51);
			this.uploadPercent.Name = "uploadPercent";
			this.uploadPercent.Size = new System.Drawing.Size(63, 13);
			this.uploadPercent.TabIndex = 1;
			this.uploadPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// fileListName
			// 
			this.fileListName.AutoSize = true;
			this.fileListName.Location = new System.Drawing.Point(12, 99);
			this.fileListName.Name = "fileListName";
			this.fileListName.Size = new System.Drawing.Size(0, 13);
			this.fileListName.TabIndex = 1;
			// 
			// fileListPercent
			// 
			this.fileListPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.fileListPercent.Location = new System.Drawing.Point(333, 99);
			this.fileListPercent.Name = "fileListPercent";
			this.fileListPercent.Size = new System.Drawing.Size(75, 13);
			this.fileListPercent.TabIndex = 1;
			this.fileListPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(333, 125);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// updateTimer
			// 
			this.updateTimer.Enabled = true;
			this.updateTimer.Interval = 500;
			this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 168);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.fileListPercent);
			this.Controls.Add(this.uploadPercent);
			this.Controls.Add(this.fileListName);
			this.Controls.Add(this.uploadName);
			this.Controls.Add(this.uploadProgressBar);
			this.Controls.Add(this.fileListProgressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ProgressForm";
			this.Text = "Imgur Upload Progress";
			this.Load += new System.EventHandler(this.ProgressForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar fileListProgressBar;
        private System.Windows.Forms.ProgressBar uploadProgressBar;
        private System.Windows.Forms.Label uploadName;
        private System.Windows.Forms.Label uploadPercent;
        private System.Windows.Forms.Label fileListName;
		private System.Windows.Forms.Label fileListPercent;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Timer updateTimer;
    }
}