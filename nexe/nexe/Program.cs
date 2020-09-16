using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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

        static string getFreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port.ToString();
        }

        static void Main(string[] args)
        {
            //if (args.Length < 2) return;
            //Console.Title = "nexe node " + args[1];

            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 0);

            string port_redis = getFreeTcpPort();

            new Thread(() =>
            {
                ProcessStartInfo r = new ProcessStartInfo("redis-db.exe");
                r.UseShellExecute = false;
                r.Arguments = "--port " + port_redis + " --bind 127.0.0.1";
                Process.Start(r);
                ;
            }).Start();

            var _nodeProcess = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardInput = false,
                    RedirectStandardError = true,
                    //WorkingDirectory = @"C:\tfs\LogoPrint\Mascot.LPA-branch-dev-branch-nvt3\Mascot.LPA.ProxySearch\",
                    //FileName = @"&quot;%systemdrive%\Program Files\nodejs\node.exe&quot; --max-old-space-size=4096",
                    //FileName = @"C:\Program Files\nodejs\node.exe",
                    //Arguments = @" --max-old-space-size=4096 C:\ntest\app.js"
                    //FileName = @"C:\Program Files\nodejs\node.exe",
                    //Arguments = @" --max-old-space-size=4096 ""C:\tfs\LogoPrint\Mascot.LPA-branch-dev-branch-nvt3\Mascot.LPA.ProxySearch\v1\cache.js"""
                    
                    //FileName = args[0],
                    //Arguments = @" --max-old-space-size=4096 " + args[1]

                    FileName = @"C:\Program Files\nodejs\node.exe",
                    Arguments = @"--max-old-space-size=4096 app.js --PORT_REDIS=" + port_redis
                }
            };

            _nodeProcess.Start();

            string stderrStr = _nodeProcess.StandardError.ReadToEnd();
            ////string stdoutStr = _nodeProcess.StandardOutput.ReadToEnd();

            if (!String.IsNullOrEmpty(stderrStr))
            {
                ShowWindow(h, 1);
                Console.Write(stderrStr);
                Console.ReadLine();
            }
            else
            {
                _nodeProcess.WaitForExit();
                _nodeProcess.Close();
                nodeExited(_nodeProcess, new EventArgs());
            }
        }
    }
}
