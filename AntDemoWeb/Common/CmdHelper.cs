using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace AntDemoWeb.Common
{
    public class CmdHelper
    {
        public static string ExecutCmd(string cmd, string args)
        {
            StringBuilder result = new StringBuilder();

            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.PriorityClass = ProcessPriorityClass.Normal;
                result.Append(p.StandardError.ReadToEnd());
                result.Append(p.StandardOutput.ReadToEnd());
                p.WaitForExit();
            }

            return result.ToString();
        }
    }
}