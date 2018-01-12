using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebRrefenceTest.BbxxService;

namespace WebRrefenceTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HisBbxxService service=new HisBbxxService();
            service.Url = "172.1.1.100:57792/HisBbxxService.asmx";
            var o = service.GetBbxx("123");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
