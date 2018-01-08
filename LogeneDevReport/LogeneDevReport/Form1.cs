using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace LogeneDevReport
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Dictionary<string,string> controlTextCache=new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();
            report.LoadLayout("Report1.repx");
            var controls = report.AllControls<XRControl>();
            foreach (XRControl xrControl in controls)
            {
                controlTextCache.Add(xrControl.Name,xrControl.Text);
                xrControl.TextChanged += (o, args) =>
                {
                    var xr = (o as XRControl);
                    if (xr != null && controlTextCache[xr.Name] != xr.Text)
                    {
                        MessageBox.Show("您无权修改报告内容!");
                        xr.Text = controlTextCache[xr.Name];
                    }
                };
            }

            reportDesigner1.OpenReport(report);
        }
    }
}
