using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace AudioExtractor
{
    public class Extractor
    {
        public void Extract()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "external\\youtube-dl.exe");
            startInfo.Arguments = "-o " + Path.Combine("C:\\Temp", @"%(title)s.mp3") + 
                " --extract-audio --audio-format \"mp3\" --batch-file " + @"F:\Code\C#\AudioExtractor\external\testurls.txt" + 
                " -iw";

            try
            {
                using (Process youtubedlExe = Process.Start(startInfo))
                {
                    youtubedlExe.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                string e = ex.Message;
            }
        }
    }
}
