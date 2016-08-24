using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ImgurUploader
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            fileListProgressBar.Maximum = uploadProgressBar.Maximum = 1000;
        }

        /// <summary>
        /// 0-1.0 representation of our progress through this current upload
        /// </summary>
        public double UploadProgress { get; set; }
        public string UploadMessage { get; set; }

        /// <summary>
        /// 0-1.0 representation of our progress through the list of files to upload
        /// </summary>
        public double FilesProgress { get; set; }
        public string FilesMessage { get; set; }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            fileListProgressBar.Value = (int)Math.Floor(FilesProgress * 1000);
            uploadProgressBar.Value = (int)Math.Floor(UploadProgress * 1000);
            
            fileListPercent.Text = String.Format("{0:0}%", FilesProgress * 100);
            uploadPercent.Text = String.Format("{0:0}%", UploadProgress * 100);

			uploadName.Text = UploadMessage;
            fileListName.Text = FilesMessage;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
        }
    }
}
