using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PathnetFSWJZ;

namespace CrisisSmsSender
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //入参格式:args[0]=病理号,操作人员姓名
            if (args.Length > 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new hszxyy(args[0].Split(',')[0]));
            }
            else
            {
                MessageBox.Show("没有传入病理号!");
                Application.Exit();
            }
        }
    }
}