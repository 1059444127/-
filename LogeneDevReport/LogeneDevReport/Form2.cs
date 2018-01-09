using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.Parameters;

namespace LogeneDevReport
{
    public partial class Form2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public DemoData demoData = new DemoData();

        public Form2()
        {
            InitializeComponent();
            demoData.Blzd = "1111111";
            demoData.Rysj = "2222222222";

            Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            report.LoadLayout("Report1.repx");
            report.Parameters.Add(
                new Parameter
                {
                    Name = "肉眼所见",
                    Value = demoData.Rysj
                });
            report.Parameters.Add(
                new Parameter
                {
                    Name = "病理诊断",
                    Value = demoData.Blzd
                });
            reportDesigner1.OpenReport(report);
        }
    }
}