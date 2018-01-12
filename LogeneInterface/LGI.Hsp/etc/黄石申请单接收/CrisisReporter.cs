using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LGI.Core.Model;
using LGI.Core.Utils;
using 黄石申请单接收.JhCrisisService;

namespace 黄石危急值回传
{
    /// <summary>
    /// 危急值回传类
    /// </summary>
    public static class CrisisReporter
    {
        /// <summary>
        /// 上报危急值
        /// </summary>
        /// <param name="blh">病理号</param>
        public static void ReportCrisis(string blh)
        {

        }

        public static void ReportCrisis(string blh,string crisis)
        {
            string inXml = "";
            var dbContext = ContextPool.GetContext();
            var jcxx = dbContext.T_JCXX.SingleOrDefault(o => o.F_BLH == blh);
            if (jcxx == null)
                throw new Exception($"病理号[{blh}]不存在");

            inXml = $@"
                    <REQUEST>
                        <CRITICALVALUES_TYPE></CRITICALVALUES_TYPE>
                        <PARITEMNAME>{jcxx.F_YZXM}</PARITEMNAME>
                        <REPORT_DATE_TIME>{jcxx.F_SPARE5}</REPORT_DATE_TIME>
                        <SPECIMEN_NO></SPECIMEN_NO>
                        <PATIENT_ID>{jcxx.F_BRBH}</PATIENT_ID>
                        <REMARK>{crisis}</REMARK>
                        <CRITICALVALUES_NO>{jcxx.F_BLH}</CRITICALVALUES_NO>
                    </REQUEST>
                    ";

            try
            {
                CRITICALVALUESBSSoapClient cs = new CRITICALVALUESBSSoapClient();
                var rtnByte = cs.JHEmrSynchroExeJhCriticalValues(inXml);
                var rtn = Encoding.Default.GetString(rtnByte);
                if (rtn.Contains("失败"))
                    throw new Exception("服务端返回错误:" + rtn);
            }
            catch (Exception e)
            {
                Logger.Error($"[{blh}]危急值上传失败\r\n" + e);
                throw new Exception($"[{blh}]危急值上传失败\r\n" + e);
            }
        }

        /// <summary>
        /// 撤销已上报的危急值
        /// </summary>
        /// <param name="blh">病理号</param>
        public static void CancelReportCrisis(string blh)
        {
            
        }
    }
}
