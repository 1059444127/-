using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using 黄石危急值回传;

namespace 黄石申请单接收
{
    /// <summary>
    /// CrisisReportService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class CrisisReportService : System.Web.Services.WebService
    {
        /// <summary>
        /// 根据病理号回传危急值,报告状态必须为已审核,且t_jcxx.f_bz不能为空
        /// </summary>
        /// <param name="blh">病理号</param>
        /// <param name="crisis">危急值内容</param>
        /// <returns></returns>
        [WebMethod]
        public void ReportCrisis(string blh,string crisis)
        {
            CrisisReporter.ReportCrisis(blh, crisis);
        }


        /// <summary>
        /// 撤销已上报的危急值
        /// </summary>
        /// <param name="blh">病理号</param>
        /// <returns></returns>
        [WebMethod]
        public void CancelReportCrisis(string blh)
        {
        }
    }
}
