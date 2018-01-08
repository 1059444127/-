using Microsoft.VisualStudio.TestTools.UnitTesting;
using 黄石申请单接收;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 黄石申请单接收.Tests
{
    [TestClass()]
    public class CrisisReportServiceTests
    {
        [TestMethod()]
        public void ReportCrisisTest()
        {
            CrisisReportService service= new CrisisReportService();
            service.ReportCrisis("1708226600");
        }
    }
}