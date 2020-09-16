using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace nexe
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void nodeExited(object sender, EventArgs e)
        {
        }

        static void Main(string[] args)
        {
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 0);

            var _nodeProcess = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    //WorkingDirectory = location,
                    //FileName = @"&quot;%systemdrive%\Program Files\nodejs\node.exe&quot; --max-old-space-size=4096",
                    //FileName = @"C:\Program Files\nodejs\node.exe",
                    //Arguments = @" --max-old-space-size=4096 C:\ntest\app.js"
                    FileName = args[0],
                    Arguments = @" --max-old-space-size=4096 " + args[1]
                }
            };

            //_nodeProcess.EnableRaisingEvents = true;
            //_nodeProcess.Exited += nodeExited;

            _nodeProcess.Start();

            string stderrStr = _nodeProcess.StandardError.ReadToEnd();
            string stdoutStr = _nodeProcess.StandardOutput.ReadToEnd();

            if (!String.IsNullOrEmpty(stderrStr))
            {
                //LogInfoMessage(stderrStr);
            }

            //LogInfoMessage(stdoutStr);
            
            _nodeProcess.WaitForExit();
            _nodeProcess.Close();
            nodeExited(_nodeProcess, new EventArgs());
        }
    }
}
