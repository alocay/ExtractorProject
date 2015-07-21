using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioExtractor
{
    public enum AudioFormat
    {
        mp3,
        aac,
        m4a
    }

    public class Extractor : ViewModel
    {
        private readonly string BatchUrlFile = "batch-urls.txt";
        private readonly string DefaultOutputPath =  Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private readonly string DefaultStatusMessage = "Add youtube URLs and hit Extract to start!";

        private BackgroundWorker worker = new BackgroundWorker();
        private string outputPath;
        private string status;
        private bool inProgress;
        private AudioFormat currentAudioFormat = AudioFormat.m4a;
        private Process extractProcess = null;
        private Object processLock = new Object();

        public AudioFormat CurrentAudioFormat
        {
            get
            {
                return this.currentAudioFormat;
            }
            set
            {
                this.currentAudioFormat = value;
                OnPropertyChanged("CurrentAudioFormat");
            }
        }

        public bool InProgress
        {
            get 
            { 
                return this.inProgress; 
            }
            set 
            {
                this.inProgress = value;
                OnPropertyChanged("InProgress");
            }
        }

        public string Status
        {
            get 
            { 
                return this.status; 
            }
            set 
            { 
                this.status = value;
                OnPropertyChanged("Status");
            }
        }

        public string OutputPath
        {
          get 
          { 
              return this.outputPath; 
          }
          set 
          { 
              this.outputPath = value;
              OnPropertyChanged("OutputPath");
          }
        }

        public Extractor() 
        {
            this.OutputPath = this.DefaultOutputPath;
            this.Status = this.DefaultStatusMessage;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.WorkerReportsProgress = false;
            this.worker.DoWork += this.RunProcesAsync;
            this.worker.RunWorkerCompleted += this.ProcessComplete;
        }

        public void Extract(string[] urls)
        {
            string[] cleanedUrls = this.CleanupUrls(urls);

            if (!Directory.Exists(this.outputPath))
            {
                this.Status = "Output path not found!";
                return;
            }

            if (cleanedUrls.Length == 0)
            {
                this.Status = "No URLs provided!";
                return;
            }

            try
            {
                this.CreateBatchFile(cleanedUrls);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "external\\youtube-dl.exe");
                startInfo.Arguments = "-o " + Path.Combine(this.outputPath, @"%(title)s." + this.currentAudioFormat.ToString()) +
                    " --extract-audio --prefer-ffmpeg --audio-format \"" + this.currentAudioFormat.ToString() + "\" --batch-file " + this.BatchUrlFile +
                    " -iw";
                
                this.Status = "Processing...";
                this.InProgress = true;

                worker.RunWorkerAsync(startInfo);

            }
            catch (IOException ex)
            {
                this.Status = "Error: " + ex.Message;
            }
        }

        private void OnProcessDisposed(object sender, EventArgs e)
        {
            lock (processLock)
            {
                this.extractProcess.Disposed -= this.OnProcessDisposed;
                this.extractProcess = null;
            }
        }

        public void CancelAll()
        {
            this.worker.CancelAsync();

            lock (processLock)
            {
                if (this.extractProcess != null)
                {
                    this.extractProcess.Disposed -= this.OnProcessDisposed;
                    this.extractProcess.Kill();
                }
            }
        }

        private void RunProcesAsync(object sender, DoWorkEventArgs e)
        {
            ProcessStartInfo startInfo = (ProcessStartInfo)e.Argument;
            string message = string.Empty;

            try
            {
                using (this.extractProcess = Process.Start(startInfo))
                {
                    this.extractProcess.Disposed += this.OnProcessDisposed;
                    this.extractProcess.WaitForExit();
                }

                message = "Complete!";
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
            }

            e.Result = message;
        }

        private void ProcessComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Status = (string)e.Result;
            this.InProgress = false;
        }

        private void CreateBatchFile(string[] urls)
        {
            using (StreamWriter writer = new StreamWriter(this.BatchUrlFile, false))
            {
                foreach (string url in urls)
                {
                    writer.WriteLine(url.Trim());
                }
            }
        }

        private string[] CleanupUrls(string[] urls)
        {
            return urls.Where(u => !string.IsNullOrEmpty(u)).ToArray();
        }
    }
}
