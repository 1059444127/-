using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using dbbase;
using LGInterface.BbxxService;
using LGInterface.DAL;
using LGInterface.Model;
using LGInterface.Util;
using SendPisResult;

namespace LGInterface
{
    public class LGInterface
    {
        IniFiles f = new IniFiles("sz.ini");
        private string ConfigSection = "黄石中心医院";


        public LGInterface()
        {
        }
      
        //exid 放到申请序号
        //处方序号 放到就诊ID
        //CARDNUMBER 这个放到身份证号
        //string sHISName, string Sslbx"门诊号","住院号" , string Ssbz "标记", string Debug, string by
        //该接口用发票号代替门诊号,不存门诊号
        public string LGGetHISINFO(string sHISName, string Sslbx, string Ssbz, string Debug, string by)
        {
            string sqlWhere = $" and f_sqxh = '{Ssbz}'";
            var lst = new T_SQD_DAL().GetList(sqlWhere);
            if (lst.Count == 0)
                return "0";
            else if (lst.Count == 1)
                return GetXml(lst[0]);
            else
            {
                //弹出医嘱选择器
                T_SQD sqd;
                using (ApplicationSelector f = new ApplicationSelector())
                {
                    sqd = null;
                    f.ListSqd = lst;
                    f.ItemSelected += r => sqd = r;
                    f.ShowDialog();
                }

                if (sqd != null)
                    return GetXml(sqd);
                return "0";
            }
        }

        private string GetXml(T_SQD sqd)
        {
            string xml = "";
            try
            {
                xml = "<?xml version=" + (char)34 + "1.0" + (char)34 + " encoding=" + (char)34 + "gbk" + (char)34 +
                      "?>";
                xml = xml + "<LOGENE>";
                xml = xml + "<row ";
                xml = xml + "病人编号=" + (char)34 + sqd.F_BRBH + (char)34 + " ";
                xml = xml + "就诊ID=" + (char)34 + sqd.F_YZID+ (char)34 + " ";
                xml = xml + "申请序号=" + (char)34 + sqd.F_SQXH + (char)34 + " ";
                xml = xml + "门诊号=" + (char)34 + sqd.F_MZH + (char)34 + " ";
                xml = xml + "住院号=" + (char)34 + sqd.F_ZYH + (char)34 + " ";
                xml = xml + "姓名=" + (char)34 + sqd.F_XM + (char)34 + " ";
                xml = xml + "性别=" + (char)34 + sqd.F_XB + (char)34 + " ";
                xml = xml + "年龄=" + (char)34 + sqd.F_NL + (char)34 + " ";
                xml = xml + "婚姻=" + (char)34 + sqd.F_HY + (char)34 + " ";
                xml = xml + "地址=" + (char)34 + sqd.F_DZ + (char)34 + " ";
                xml = xml + "电话=" + (char)34 + sqd.F_DH + (char)34 + " ";
                xml = xml + "病区=" + (char)34 + sqd.F_BQ + (char)34 + " ";
                xml = xml + "床号=" + (char)34 + sqd.F_CH + (char)34 + " ";
                xml = xml + "身份证号=" + (char)34 + sqd.F_SFZH + (char)34 + " ";
                xml = xml + "民族= " + (char)34 + sqd.F_MZ + (char)34 + " ";
                xml = xml + "职业=" + (char)34 + sqd.F_ZY + (char)34 + " ";
                xml = xml + "送检科室=" + (char)34 + sqd.F_SQKS + (char)34 + " ";
                xml = xml + "送检医生=" + (char)34 + sqd.F_SQYS + (char)34 + " ";
                xml = xml + "收费=" + (char)34 + sqd.F_SFZH + (char)34 + " ";

                xml = xml + "标本名称=" + (char)34 + sqd.F_BBMC + (char)34 + " ";
                xml = xml + "送检医院=" + (char)34 + "" + (char)34 + " ";
                xml = xml + "医嘱项目=" + (char)34 + sqd.F_YZXM + (char)34 + " ";
                xml = xml + "备用1=" + (char)34 + "" + (char)34 + " ";
                xml = xml + "备用2=" + (char)34 + "" + (char)34 + " ";
                xml = xml + "费别=" + (char)34 + "" + (char)34 + " ";
                xml = xml + "病人类别=" + (char)34 + sqd.F_BRLB + (char)34 + " ";
                xml = xml + "/>";
                xml = xml + GetBbxx(sqd.F_SQXH);
                xml = xml + "<临床病史><![CDATA[" + "" + "]]></临床病史>";
                xml = xml + "<临床诊断><![CDATA[" + sqd.F_LCZD + "]]></临床诊断>";
                xml = xml + "</LOGENE>";
            }
            catch (Exception e)
            {
                //log.解析返回的xml失败
                log.WriteMyLog("解析接口返回的xml时出错:" + e.Message);
                return "0";
            }
            return xml;
        }

        /// <summary>
        /// 通过WebService获取来自瑞康视图的标本信息
        /// </summary>
        /// <param name="sqxh"></param>
        /// <returns></returns>
        private string GetBbxx(string sqxh)
        {
            var url = f.ReadString("hszxxy", "bbxxurl", @"172.16.80.174:8081/HisBbxxService.asmx");
            HisBbxxService hbs = new HisBbxxService();
            hbs.Url = url;
            var bbxx = hbs.GetBbxx(sqxh);

            return bbxx;
        }
    }
}