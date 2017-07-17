using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Threading;
using System.IO.Pipes;
using System.Diagnostics;

namespace ImgurUploader
{
    static class Program
	{
		const string Message_NoValidFiles = "No valid files discovered!";

		static ProgressForm _f;
		static Thread _uploadThread;
		static List<string> SubmittedFiles = new List<string>();

		static void PipeListener()
		{
			NamedPipeServerStream server = new NamedPipeServerStream("ImgurUploader9A0", PipeDirection.In, -1,
										   PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
			server.BeginWaitForConnection(FetchFile, server);
		}

		static void FetchFile(IAsyncResult ar)
		{
			PipeListener();
			NamedPipeServerStream server = ar.AsyncState as NamedPipeServerStream;
			server.EndWaitForConnection(ar);
			using (StreamReader reader = new StreamReader(server)) {
				lock (SubmittedFiles) {
					while (!reader.EndOfStream)
						SubmittedFiles.Add(reader.ReadLine());
				}
			}
			server.Dispose();
		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main(string[] args)
		{
			if (args.Length < 1) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				if (CheckForUpdate())
					return;
				Application.Run(new SettingsForm());
				return;
			}

			Mutex mutex = new Mutex(true, "{9A0E5077-1926-42CB-97A2-8DB89861E8B7}");

			if (mutex.WaitOne(TimeSpan.Zero, true)) {
				// Server
				SubmittedFiles.AddRange(args);
				PipeListener();
				Thread.Sleep(500);
				mutex.ReleaseMutex();
				if (CheckForUpdate())
					return;
				InitializeUpload();
			}
			else {
				// Client
				using (NamedPipeClientStream client = new NamedPipeClientStream(".", "ImgurUploader9A0",
													  PipeDirection.Out)) {
					client.Connect();
					using (StreamWriter writer = new StreamWriter(client)) {
						foreach (string file in args)
							writer.WriteLine(file);
						writer.Flush();
					}
				}
			}
        }

		static bool CheckForUpdate()
		{
			if (!Settings.Default.CheckUpdates)
				return false;

			string latestVersion = ImgurUploader.GetLatestRelease();
			if (!string.IsNullOrEmpty(latestVersion) &&
				"v" + Application.ProductVersion != latestVersion) {
				DialogResult result = MessageBox.Show(
					"There is an update available. Would you like to download now?",
					"Imgur Uploader", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes) {
					Process.Start(Settings.Default.DownloadURL);
					return true;
				}
			}
			return false;
		}

		static void InitializeUpload()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

			List<string> validFileExtensions = new List<string> { "png", "jpg", "jpeg", "bmp", "gif" };
				
			List<string> validFiles = SubmittedFiles.Where((f) => File.Exists(f) && 
															validFileExtensions.Contains(
															Path.GetExtension(f).ToLower().Remove(0, 1))).ToList();
			if (validFiles.Count < 1) {
				MessageBox.Show(Message_NoValidFiles);
				return;
			}

			//make progress form
			ImgurUploader i = new ImgurUploader { Files = validFiles };

			i.UpdateStatus += new ImgurUploader.UploadStatusHandler(i_UpdateStatus);

			_f = new ProgressForm();

			_uploadThread = new Thread(i.UploadFiles);
			_uploadThread.Start();

			Application.Run(_f);
		}

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
			if (_uploadThread != null)
	            _uploadThread.Abort();
        }

        static void i_UpdateStatus(object source, ImgurUploaderStatus s)
		{
			_f.UploadMessage  = s.FileBeingProcessed + " " + s.FileProcessMessage;
			_f.UploadProgress = s.UploadProgress;
            _f.FilesMessage   = s.FilesMessage;
            _f.FilesProgress  = s.FilesProgress;
        }
    }
}
