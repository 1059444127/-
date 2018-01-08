using System;
using System.Collections.Generic;
using System.Text;
using dbbase;

namespace 黄石危急值回传
{
    /// <summary>
    /// 危急值回传类
    /// </summary>
    public class CrisisReporter
    {
        private IniFiles f = new IniFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\sz.ini");
        dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");

        /// <summary>
        /// 上报危急值
        /// </summary>
        /// <param name="blh">病理号</param>
        public void ReportCrisis(string blh)
        {
            string sql = "select * from t_jcxx t where t.f_blh='"+blh+"' ";
            var dtJcxx = aa.GetDataTable(sql,"dt");
            if(dtJcxx.Rows.Count<0)
                throw new Exception("没有找到病理号["+blh+"]对应的jcxx");

            var drJcxx = dtJcxx.Rows[0];
            var crisisMessage = drJcxx["f_bz"].ToString();
        }

        /// <summary>
        /// 撤销已上报的危急值
        /// </summary>
        /// <param name="blh">病理号</param>
        public void CancelReportCrisis(string blh)
        {
            
        }
    }
}
