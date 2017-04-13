using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntDemoWeb.Common
{
    public class PdfConvertor
    {
        private static List<string> list = new List<string>();
        private static bool running = false;

        public static void AddTask(string pdfFile)
        {
            if (list.Count >= 2)
                throw new Exception("pdf文件任务量已达最大，请稍后重试");

            list.Add(pdfFile);

            if (running == false)
            {
                running = true;
                Logger.Log("run");
                Run();           
            }
                
        }

        public static void Run()
        {
            while (list.Any())
            {
                var item = list[0];

                System.Threading.Thread.Sleep(5000);
                Logger.Log(item);

                list.Remove(item);
            }
            running = false;
        }
    }
}