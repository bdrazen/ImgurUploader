﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace ImgurUploader
{
	public partial class AuthorizeForm : Form
	{
		public AuthorizeForm()
		{
			InitializeComponent();
		}

		private void AuthorizeForm_Load(object sender, EventArgs e)
		{
			PopulateAccount();
		}

		private void btnAuthorize_Click(object sender, EventArgs e)
		{
			Process.Start(Settings.Default.AuthURL + "?response_type=pin&client_id="
						+ Settings.Default.ClientID);
		}

		private void txtPIN_TextChanged(object sender, EventArgs e)
		{
			btnActivate.Enabled = txtPIN.TextLength > 0 ? true : false;
		}

		private void btnActivate_Click(object sender, EventArgs e)
		{
			btnActivate.Enabled = false;
			try {
				ImgurUploader.Authorize(GrantType.Pin, txtPIN.Text.Trim());
			} catch (AuthorizationException ex) {
				MessageBox.Show("Something went wrong, could not authorize");
				btnActivate.Enabled = true;
				return;
			}
			txtPIN.Clear();
			PopulateAccount();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			Settings settings = Settings.Default;
			settings.UserName = "";
			settings.AccessToken = "";
			settings.RefreshToken = "";
			settings.Save();
			pnlAccount.Visible = false;
		}

		void PopulateAccount()
		{
			Settings settings = Settings.Default;
			if (settings.UserName != "")
			{
				txtAccount.Text = settings.UserName;
				pnlAccount.Visible = true;
			}
		}

	}
}
