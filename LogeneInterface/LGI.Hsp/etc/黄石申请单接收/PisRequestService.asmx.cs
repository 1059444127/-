using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using LGI.Core.Model;
using LGI.Core.Utils;
using LGI.Core.Utils.HL7v2;

//using NextLevelSeven.Core;

namespace 黄石申请单接收
{
    /// <summary>
    /// PisRequestService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PisRequestService : System.Web.Services.WebService
    {
        private static string _mshCode;

        [WebMethod]
        public string ReceiveRequest(string requestXml)
        {
            try
            {
                Logger.Info("收到请求:"+requestXml);
                SaveRequestMessage(requestXml);
                return $@"MSH|^~\&|LG|LG|||20150123210520|||{_mshCode}|P|2.4 
                    MSA|AA|{_mshCode}";
            }
            catch (Exception e)
            {
                var errMessage = "接收申请单时出现错误:" + e.Message;
                var errCode = "";
                Logger.Error("接收申请单时出现错误:" + e);
                return 
                    $@"MSH|^~\&|LG|LG|||20150123210520|||{_mshCode}|P|2.4 
                    MSA|AA|{_mshCode}
                    ERR|||||{errCode}|||{errMessage}";
            }
        }

        private static void SaveRequestMessage(string requestXml)
        {
            HL7v2Reader reader = new HL7v2Reader(requestXml);

            //患者信息
            var pid = reader["PID"][0];
            //挂号信息
            var pv1 = reader["PV1"][0];
            //申请单信息
            var orc = reader["ORC"][0];
            var dg1List = reader["DG1"];
            //医嘱信息列表
            var obrList = reader["OBR"];

            _mshCode = reader["MSH"][0][10][0];

            T_SQD sqd = new T_SQD();

            sqd.F_msgxml = reader.HL7String;
            sqd.F_SQRQ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            sqd.F_BRBH = pid[2][0]; //患者id
            sqd.F_JZH = pv1[19][0]; //就诊ID
            sqd.F_SQXH = orc[2][0]; //申请单ID
            var pType = pv1[2][0]; //E:急诊 I: 住院 O:门诊 T:体检
            switch (pType)
            {
                case "I": //住院
                    sqd.F_ZYH = pv1[19][0];
                    sqd.F_BRLB = "住院";
                    break;
                case "E":
                    sqd.F_MZH = pv1[19][0];
                    sqd.F_BRLB = "急诊";
                    break;
                case "O":
                    sqd.F_MZH = pv1[19][0];
                    sqd.F_BRLB = "门诊";
                    break;
                case "T":
                    sqd.F_MZH = pv1[19][0];
                    sqd.F_BRLB = "体检";
                    break;
                default:
                    sqd.F_MZH = pv1[19][0];
                    sqd.F_BRLB = "其他";
                    break;
            }
            sqd.F_XM = pid[5][1]; //姓名
            sqd.F_XBBM = pid[8][0]; //性别 F: Female M:Male O: Others 其余见HL7 定义
            switch (sqd.F_XBBM)
            {
                case "F":
                    sqd.F_XB = "女";
                    break;
                case "M":
                    sqd.F_XB = "男";
                    break;
                default:
                    sqd.F_XB = "其它";
                    break;
            }
            var nlString = pid[7][0]; //年龄 根据出生日期计算 yyyyMMddhhmmss,取年的整数
            sqd.F_CSRQ = pid[7][0].Substring(0, 8); //出生日期
            try
            {
                sqd.F_NL =
                    (DateTime.Now.Year - DateTime.ParseExact(nlString, "yyyyMMddHHmmss", CultureInfo.CurrentCulture).Year)
                    .ToString();
            }
            catch (Exception)
            {
            }
            var hy = pid[16][0]; //婚姻 M^己婚或 S^未婚或 O^其他
            switch (hy)
            {
                case "M":
                    hy = "已婚";
                    break;
                case "S":
                    hy = "未婚";
                    break;
                default:
                    hy = "其他";
                    break;
            }
            sqd.F_HY = hy;

            sqd.F_DZ = pid[11][0]; //地址
            sqd.F_DH = pid[13][6]; //电话
            sqd.F_BQ = pv1[3][0]; //病区
            sqd.F_CH = pv1[3][2]; //床号
            sqd.F_SFZH = pid[19][0]; //身份证号
            sqd.F_SQYSBM = orc[17][0]; //开单医生编码
            sqd.F_SQKS = orc[17][1]; //开单科室
            sqd.F_SQYS = orc[12][2]; //开单医生
            sqd.F_SFZH = orc[20][0]; //总费用
            if (dg1List.Any())
            {
                try
                {
                    sqd.F_LCZD = dg1List[0][4][0];
                }
                catch (Exception)
                {
                }
            }



            var dbContext = ContextPool.GetContext();
            foreach (Segment obr in obrList)
            {
                var sqdClone = sqd.Clone();
                sqdClone.F_YZID = obr[4][0];
                sqdClone.F_YZXM = obr[4][1];
                dbContext.T_SQD.Add(sqdClone);
            }
            dbContext.SaveChanges();
        }

        [WebMethod]
        public string Test(string pwd)
        {
            if (pwd != "4s3c2a1p")
                return "wrong password!";

            return ReceiveRequest(
                @"MSH|^~\&|HIS||JHIP||20171218111643||ORM^O01^ORM_O01|LIUXGHIS201712181116480001306964|P|2.4||||||UNICODE UTF-8
PID||99999901|99999901^^^000000&鄂东医疗集团市中心医院~99999901~11523141|0|CESHI1^测试1111||19280410000000|F|||湖北省黄石市大泉路岭秀新城6栋||^^^^^^13451052237|^^^^^^13451052237||O||99999901|241111111111111111|||^^汉族|湖北黄石黄石港区|||||1010^中国
PV1|1|I|^^998^020101&Z心血管内科|1|||LIUXG^^LIUXG||||||||||LIUXG^^LIUXG|职工医保|99999901|||||||||||||||||||||||020101^^^000000||20171218111643|||||||V
ORC|NW|4469415|||||^^^^^4||20171218111647|||LIUXG^^LIUXG|020101^^^^^^^Z心血管内科||||020101^Z心血管内科^^000000|||0^1
OBR|1|4469415||270300002^内镜组织活检检查与诊断|4|||||||0304^Z病理科^000000||||||||||||病理
NTE|1
DG1|1");
        }
    }



}
