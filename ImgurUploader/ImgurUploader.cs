using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Net;
using System.Diagnostics;
using System.Xml;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ImgurUploader
{
    public class ImgurUploader
    {
        public delegate void UploadStatusHandler(object source, ImgurUploaderStatus s);
        public event UploadStatusHandler UpdateStatus;

        public List<string> Files { get; set; }

        public void UploadFiles()
        {
			string accessToken = Settings.Default.AccessToken;
			ImgurUploaderStatus status = new ImgurUploaderStatus();
			UploadResponse albumResponse = null;
			HttpWebRequest hr;

			try {
				if (Files.Count > 1) {
					// Create album
					status = new ImgurUploaderStatus { FileProcessMessage = "Creating album..." };
					UpdateStatus(this, status);

					hr = WebRequest.Create(Settings.Default.AlbumURL) as HttpWebRequest;
					hr.Method = "POST";
					hr.KeepAlive = true;
					hr.Credentials = CredentialCache.DefaultCredentials;
					string authHeader = accessToken != "" ? "Bearer " + accessToken : "Client-ID " + Settings.Default.ClientID;
					hr.Headers.Add("Authorization", authHeader);

					using (HttpWebResponse response = hr.GetResponse() as HttpWebResponse) {
						string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
						albumResponse = JsonConvert.DeserializeObject<UploadResponse>(responseString);

						if (!albumResponse.success) {
							MessageBox.Show("Imgur didn't like the request.", "Error");
							Application.Exit();
							return;
						}
					}

					hr = null;
				}

				int currentFileNumber = 0;
				foreach (string file in Files) {
					currentFileNumber++;
					string TotalMessage = String.Format("Dealing with file {0}/{1}", currentFileNumber, Files.Count);
					double TotalProgress = (currentFileNumber - 1) / (Files.Count * 1.0f);
					status =
						new ImgurUploaderStatus {
							UploadProgress = 0,
							FilesMessage = TotalMessage,
							FilesProgress = TotalProgress,
							FileBeingProcessed = Path.GetFileName(file)
						};

					status.FileProcessMessage = "Verifying existence";
					UpdateStatus(this, status);

					if (!File.Exists(file))
						MessageBox.Show("File could not be found.", "Error");

					status.FileProcessMessage = "Verifying file integrity";
					UpdateStatus(this, status);

					try { Image.FromFile(file); }
					catch (ArgumentException) { MessageBox.Show("File did not appear to be an image.", "Error"); }

					hr = WebRequest.Create(Settings.Default.UploadURL) as HttpWebRequest;
					string bound = "----------------------------" + DateTime.Now.Ticks.ToString("x");
					hr.ContentType = "multipart/form-data; boundary=" + bound;
					hr.Method = "POST";
					hr.KeepAlive = true;
					hr.Credentials = CredentialCache.DefaultCredentials;
					string authHeader = accessToken != "" ? "Bearer " + accessToken : "Client-ID " + Settings.Default.ClientID;
					hr.Headers.Add("Authorization", authHeader);

					byte[] boundBytes = Encoding.ASCII.GetBytes("\r\n--" + bound + "\r\n");

					string headerTemplate =
							"Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

					byte[] headerBytes = Encoding.UTF8.GetBytes(
							String.Format(headerTemplate, "image", file));

					using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
						//calculate the total length we expect to send
						long contentLength = 0;
						contentLength += headerBytes.Length;
						contentLength += fs.Length;
						contentLength += boundBytes.Length * 2;
						byte[] albumData = null;
						if (albumResponse != null) {
							albumData = Encoding.UTF8.GetBytes(
								"Content-Disposition: form-data; name=\"album\"\r\n\r\n" +
								(accessToken != "" ? albumResponse.data.id : albumResponse.data.deletehash));
							contentLength += albumData.Length + boundBytes.Length;
						}
						hr.ContentLength = contentLength;

						using (Stream s = hr.GetRequestStream()) {
							s.Write(boundBytes, 0, boundBytes.Length);

							if (albumData != null) {
								// Add album ID
								s.Write(albumData, 0, albumData.Length);
								s.Write(boundBytes, 0, boundBytes.Length);
							}


							//write the files to the request stream
							status.FileProcessMessage = "Sending File...";
							UpdateStatus(this, status);
							s.Write(headerBytes, 0, headerBytes.Length);

							int bytesRead = 0;
							long bytesSoFar = 0;
							byte[] buffer = new byte[10240];
							while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0) {
								bytesSoFar += bytesRead;
								s.Write(buffer, 0, bytesRead);
								Debug.WriteLine(String.Format("sending file data {0:0.000}%", (bytesSoFar * 100.0f) / fs.Length));
								status.UploadProgress = (bytesSoFar * 1.0f) / fs.Length;
								status.FilesProgress = TotalProgress + 1 / (Files.Count * 1.0f) * status.UploadProgress;
								UpdateStatus(this, status);
							}
							s.Write(boundBytes, 0, boundBytes.Length);
						}
					}

					status.FileProcessMessage = "File sent, waiting for response...";
					UpdateStatus(this, status);

					using (HttpWebResponse response = hr.GetResponse() as HttpWebResponse) {
						string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
						UploadResponse imgur = JsonConvert.DeserializeObject<UploadResponse>(responseString);

						if (!imgur.success) {
							MessageBox.Show("Imgur didn't like the request.", "Error");
							Application.Exit();
							return;
						}
						else if (albumResponse == null) {
							if (accessToken == "")
								Process.Start("http://imgur.com/delete/" + imgur.data.deletehash);
							Process.Start(imgur.data.link);
						}
					}

					status.FileProcessMessage = "All finished, what's next?";
					status.UploadProgress = 1;
					UpdateStatus(this, status);

					hr = null;
				}

				if (albumResponse != null) {
					// TODO: Figure out deletion for anynymous albums
					//if (accessToken == "")
					//	Process.Start("http://imgur.com/{???}" + albumResponse.data.deletehash);
					Process.Start("http://imgur.com/a/" + albumResponse.data.id + "/layout/grid");
				}

				Application.Exit();
			}
			catch (WebException ex) {
				HttpWebResponse response = ex.Response as HttpWebResponse;
				if (response != null && response.StatusCode == HttpStatusCode.Forbidden)
					AttemptReauthorize(status);
				else {
					MessageBox.Show("Something went wrong during upload. Check your network connection.", "Error");
					Application.Exit();
				}
			}
        }

        #pragma warning disable 0649 // Fields assigned to by JSON deserialization
        class UploadResponse
        {
            public bool success;
            public Data data;
            public class Data
            {
                public string id;
                public string link;
                public string deletehash;
            }
        }

		class AuthResponse
		{
			public string access_token;
			public string refresh_token;
			public string expires_in;
			public string token_type;
			public string account_username;
		}

		class GithubRelease
		{
			public string tag_name;
		}
		#pragma warning restore 0649

		void AttemptReauthorize(ImgurUploaderStatus status)
		{
				status.FileProcessMessage = "Access token expired, re-authorizing...";
				UpdateStatus(this, status);
				try {
					Authorize(GrantType.RefreshToken, Settings.Default.RefreshToken);
				}
				catch (AuthorizationException aEx) {
					MessageBox.Show("Could not authorize. Try re-authorizing your account.", "Error");
					Application.Exit();
					Application.Run(new SettingsForm());
					return;
				}
				this.UploadFiles();
		}

		public static void Authorize(GrantType grantType, string key)
		{
			using (WebClient client = new WebClient()) {
				string typeParam = grantType == GrantType.Pin ? "pin" : "refresh_token";
				string clientID = Settings.Default.ClientID;
				string data = "client_id=" + clientID + "&" +
							  "client_secret=" + Settings.Default.ClientSecret + "&" +
							  "grant_type=" + typeParam + "&" +
							  typeParam + "=" + key;

				client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
				client.Headers["Authorization"] = "Client-ID " + clientID;
				string response;
				try {
					response = client.UploadString(Settings.Default.TokenURL, data);
				}
				catch (Exception ex) {
					throw new AuthorizationException();
				}
				AuthResponse imgur = JsonConvert.DeserializeObject<AuthResponse>(response);
				Settings settings = Settings.Default;
				settings.UserName = imgur.account_username;
				settings.AccessToken = imgur.access_token;
				settings.RefreshToken = imgur.refresh_token;
				settings.Save();
			}
		}

		public static string GetLatestRelease()
		{
			using (WebClient client = new WebClient()) {
				try {
					client.Headers["User-Agent"] = "ImgurUploader";
					string response = client.DownloadString(Settings.Default.UpdateURL);
					return JsonConvert.DeserializeObject<GithubRelease[]>(response)[0].tag_name;
				}
				catch (Exception ex) {
					return "";
				}
			}
		}
    }

    public enum ImageFormatSize
    {
        Small,Large,Original
    }

	public enum GrantType
	{
		Pin,RefreshToken
	}

	[Serializable]
	public class ImgurUploadInfo
	{
		public string image_hash { get; set; }
		public string delete_hash { get; set; }
		public string original_image { get; set; }
		public string imgur_page { get; set; }
		public string delete_page { get; set; }
		public string file { get; set; }
	}

    public class ImgurUploaderStatus
    {
        public string FileBeingProcessed { get; set; }
        public string FileProcessMessage { get; set; }
        public double UploadProgress { get; set; }

        public string FilesMessage { get; set; }
        public double FilesProgress { get; set; }
    }

	public class AuthorizationException : Exception { }
}
