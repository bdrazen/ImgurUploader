namespace ImgurUploader
{
	partial class AuthorizeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizeForm));
			this.btnAuthorize = new System.Windows.Forms.Button();
			this.txtPIN = new System.Windows.Forms.TextBox();
			this.btnActivate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRemove = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAccount = new System.Windows.Forms.TextBox();
			this.pnlAccount = new System.Windows.Forms.Panel();
			this.pnlAccount.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnAuthorize
			// 
			this.btnAuthorize.Location = new System.Drawing.Point(115, 40);
			this.btnAuthorize.Name = "btnAuthorize";
			this.btnAuthorize.Size = new System.Drawing.Size(219, 32);
			this.btnAuthorize.TabIndex = 0;
			this.btnAuthorize.Text = "Authorize Account";
			this.btnAuthorize.UseVisualStyleBackColor = true;
			this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
			// 
			// txtPIN
			// 
			this.txtPIN.Location = new System.Drawing.Point(115, 115);
			this.txtPIN.Name = "txtPIN";
			this.txtPIN.Size = new System.Drawing.Size(138, 20);
			this.txtPIN.TabIndex = 1;
			this.txtPIN.TextChanged += new System.EventHandler(this.txtPIN_TextChanged);
			// 
			// btnActivate
			// 
			this.btnActivate.Enabled = false;
			this.btnActivate.Location = new System.Drawing.Point(259, 113);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(75, 23);
			this.btnActivate.TabIndex = 2;
			this.btnActivate.Text = "Activate";
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(115, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "PIN:";
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(296, 34);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(26, 23);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "X";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(103, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Authorized Account:";
			// 
			// txtAccount
			// 
			this.txtAccount.Enabled = false;
			this.txtAccount.Location = new System.Drawing.Point(103, 36);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(187, 20);
			this.txtAccount.TabIndex = 6;
			// 
			// pnlAccount
			// 
			this.pnlAccount.Controls.Add(this.label2);
			this.pnlAccount.Controls.Add(this.txtAccount);
			this.pnlAccount.Controls.Add(this.btnRemove);
			this.pnlAccount.Location = new System.Drawing.Point(12, 142);
			this.pnlAccount.Name = "pnlAccount";
			this.pnlAccount.Size = new System.Drawing.Size(425, 74);
			this.pnlAccount.TabIndex = 7;
			this.pnlAccount.Visible = false;
			// 
			// AuthorizeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 227);
			this.Controls.Add(this.pnlAccount);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnActivate);
			this.Controls.Add(this.txtPIN);
			this.Controls.Add(this.btnAuthorize);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "AuthorizeForm";
			this.Text = "Authorization";
			this.Load += new System.EventHandler(this.AuthorizeForm_Load);
			this.pnlAccount.ResumeLayout(false);
			this.pnlAccount.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnAuthorize;
		private System.Windows.Forms.TextBox txtPIN;
		private System.Windows.Forms.Button btnActivate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAccount;
		private System.Windows.Forms.Panel pnlAccount;
	}
}