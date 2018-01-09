using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace LogeneDevReport
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Dictionary<string,string> controlTextCache=new Dictionary<string, string>();
        public Form1()
        {
            XtraReport.FilterComponentProperties += XtraReport_FilterComponentProperties;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XtraReport1 report = new XtraReport1();

            report.DesignerLoaded += (s1, e1) => {
                IDesignerHost host = report.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                foreach (IComponent c in host.Container.Components)
                {
                    ComponentDesigner designer = host.GetDesigner(c) as ComponentDesigner;
                    if (designer != null)
                    {
                        designer.ActionLists.Clear();
                        designer.Verbs.Clear();
                    }
                }
                IComponentChangeService serv = report.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                serv.ComponentAdded += (s2, e2) => {
                    ComponentDesigner designer = host.GetDesigner(e2.Component) as ComponentDesigner;
                    if (designer != null)
                    {
                        designer.ActionLists.Clear();
                        designer.Verbs.Clear();
                    }
                };
            };

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

        private void XtraReport_FilterComponentProperties(object sender, FilterComponentPropertiesEventArgs e)
        {
            // The following code hides some properties for a specific report element.
            if (sender is XtraReport1 && e.Component is XRControl)
            {
                HideProperty("Text", e);
                HideProperty("DataBindings", e);
            }
        }

        static void HideProperty(String propertyName,
            FilterComponentPropertiesEventArgs filterComponentProperties)
        {
            PropertyDescriptor oldPropertyDescriptor =
                filterComponentProperties.Properties[propertyName] as PropertyDescriptor;
            if (oldPropertyDescriptor != null)
            {
                // Substitute the current property descriptor 
                // with a custom one with the BrowsableAttribute.No attribute.
                filterComponentProperties.Properties[propertyName] = TypeDescriptor.CreateProperty(
                    oldPropertyDescriptor.ComponentType,
                    oldPropertyDescriptor,
                    new Attribute[] { BrowsableAttribute.No });
            }
            else
            {
                // If the property descriptor can not be substituted, 
                // remove it from the Properties collection.
                filterComponentProperties.Properties.Remove(propertyName);
            }
        }
    }
}
