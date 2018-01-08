using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using dbbase;
using LGInterface.Model;

namespace LGInterface.DAL
{
    /// <summary>
    /// 数据访问类:T_SQD
    /// </summary>
    public class T_SQD_DAL
    {
        dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");

        public T_SQD_DAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_SQD GetModel(string sqlWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_SQD ");
            strSql.Append($" where 1 = 1" + sqlWhere);

            var dt = aa.GetDataTable(strSql.ToString(),"dt");
            if (dt.Rows.Count > 0)
                return DataRowToModel(dt.Rows[0]);
            return null;
        }

        public List<T_SQD> GetList(string sqlWhere)
        {
            var lst = new List<T_SQD>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from T_SQD ");
            strSql.Append($" where 1 = 1" + sqlWhere);

            var dt = aa.GetDataTable(strSql.ToString(), "dt");
            foreach (DataRow dtRow in dt.Rows)
            {
                lst.Add(DataRowToModel(dtRow));
            }
            return lst;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_SQD DataRowToModel(DataRow row)
        {
            Model.T_SQD model = new Model.T_SQD();
            if (row != null)
            {
                if (row["F_ID"] != null && row["F_ID"].ToString() != "")
                {
                    model.F_ID = int.Parse(row["F_ID"].ToString());
                }
                if (row["F_JSRQ"] != null)
                {
                    model.F_JSRQ = row["F_JSRQ"].ToString();
                }
                if (row["F_BRBH"] != null)
                {
                    model.F_BRBH = row["F_BRBH"].ToString();
                }
                if (row["F_YZID"] != null)
                {
                    model.F_YZID = row["F_YZID"].ToString();
                }
                if (row["F_SQXH"] != null)
                {
                    model.F_SQXH = row["F_SQXH"].ToString();
                }
                if (row["F_BRLB"] != null)
                {
                    model.F_BRLB = row["F_BRLB"].ToString();
                }
                if (row["F_MZH"] != null)
                {
                    model.F_MZH = row["F_MZH"].ToString();
                }
                if (row["F_ZYH"] != null)
                {
                    model.F_ZYH = row["F_ZYH"].ToString();
                }
                if (row["F_XM"] != null)
                {
                    model.F_XM = row["F_XM"].ToString();
                }
                if (row["F_XB"] != null)
                {
                    model.F_XB = row["F_XB"].ToString();
                }
                if (row["F_XBBM"] != null)
                {
                    model.F_XBBM = row["F_XBBM"].ToString();
                }
                if (row["F_NL"] != null)
                {
                    model.F_NL = row["F_NL"].ToString();
                }
                if (row["F_CSRQ"] != null)
                {
                    model.F_CSRQ = row["F_CSRQ"].ToString();
                }
                if (row["F_HY"] != null)
                {
                    model.F_HY = row["F_HY"].ToString();
                }
                if (row["F_DZ"] != null)
                {
                    model.F_DZ = row["F_DZ"].ToString();
                }
                if (row["F_DH"] != null)
                {
                    model.F_DH = row["F_DH"].ToString();
                }
                if (row["F_BQ"] != null)
                {
                    model.F_BQ = row["F_BQ"].ToString();
                }
                if (row["F_CH"] != null)
                {
                    model.F_CH = row["F_CH"].ToString();
                }
                if (row["F_SFZH"] != null)
                {
                    model.F_SFZH = row["F_SFZH"].ToString();
                }
                if (row["F_MZ"] != null)
                {
                    model.F_MZ = row["F_MZ"].ToString();
                }
                if (row["F_ZY"] != null)
                {
                    model.F_ZY = row["F_ZY"].ToString();
                }
                if (row["F_SJDW"] != null)
                {
                    model.F_SJDW = row["F_SJDW"].ToString();
                }
                if (row["F_SJKS"] != null)
                {
                    model.F_SJKS = row["F_SJKS"].ToString();
                }
                if (row["F_SJYS"] != null)
                {
                    model.F_SJYS = row["F_SJYS"].ToString();
                }
                if (row["F_SF"] != null)
                {
                    model.F_SF = row["F_SF"].ToString();
                }
                if (row["F_BBMC"] != null)
                {
                    model.F_BBMC = row["F_BBMC"].ToString();
                }
                if (row["F_QCBW"] != null)
                {
                    model.F_QCBW = row["F_QCBW"].ToString();
                }
                if (row["F_YZXM"] != null)
                {
                    model.F_YZXM = row["F_YZXM"].ToString();
                }
                if (row["F_BY1"] != null)
                {
                    model.F_BY1 = row["F_BY1"].ToString();
                }
                if (row["F_BY2"] != null)
                {
                    model.F_BY2 = row["F_BY2"].ToString();
                }
                if (row["F_FB"] != null)
                {
                    model.F_FB = row["F_FB"].ToString();
                }
                if (row["F_LCZL"] != null)
                {
                    model.F_LCZL = row["F_LCZL"].ToString();
                }
                if (row["F_LCZD"] != null)
                {
                    model.F_LCZD = row["F_LCZD"].ToString();
                }
                if (row["F_KSDM"] != null)
                {
                    model.F_KSDM = row["F_KSDM"].ToString();
                }
                if (row["F_KSMC"] != null)
                {
                    model.F_KSMC = row["F_KSMC"].ToString();
                }
                if (row["F_YYDM"] != null)
                {
                    model.F_YYDM = row["F_YYDM"].ToString();
                }
                if (row["F_YYMC"] != null)
                {
                    model.F_YYMC = row["F_YYMC"].ToString();
                }
                if (row["F_SQRQ"] != null)
                {
                    model.F_SQRQ = row["F_SQRQ"].ToString();
                }
                if (row["F_RYRQ"] != null)
                {
                    model.F_RYRQ = row["F_RYRQ"].ToString();
                }
                if (row["F_SQYSBM"] != null)
                {
                    model.F_SQYSBM = row["F_SQYSBM"].ToString();
                }
                if (row["F_SQYS"] != null)
                {
                    model.F_SQYS = row["F_SQYS"].ToString();
                }
                if (row["F_SQKSBM"] != null)
                {
                    model.F_SQKSBM = row["F_SQKSBM"].ToString();
                }
                if (row["F_SQKS"] != null)
                {
                    model.F_SQKS = row["F_SQKS"].ToString();
                }
                if (row["F_BQDM"] != null)
                {
                    model.F_BQDM = row["F_BQDM"].ToString();
                }
                if (row["F_BAH"] != null)
                {
                    model.F_BAH = row["F_BAH"].ToString();
                }
                if (row["F_JKDABH"] != null)
                {
                    model.F_JKDABH = row["F_JKDABH"].ToString();
                }
                if (row["F_JKKH"] != null)
                {
                    model.F_JKKH = row["F_JKKH"].ToString();
                }
                if (row["F_FPH"] != null)
                {
                    model.F_FPH = row["F_FPH"].ToString();
                }
                if (row["F_JZH"] != null)
                {
                    model.F_JZH = row["F_JZH"].ToString();
                }
                if (row["F_JZCS"] != null)
                {
                    model.F_JZCS = row["F_JZCS"].ToString();
                }
                if (row["F_JZLB"] != null)
                {
                    model.F_JZLB = row["F_JZLB"].ToString();
                }
                if (row["F_JZLSH"] != null)
                {
                    model.F_JZLSH = row["F_JZLSH"].ToString();
                }
                if (row["F_YBKH"] != null)
                {
                    model.F_YBKH = row["F_YBKH"].ToString();
                }
                if (row["F_JZKH"] != null)
                {
                    model.F_JZKH = row["F_JZKH"].ToString();
                }
                if (row["F_YZH"] != null)
                {
                    model.F_YZH = row["F_YZH"].ToString();
                }
                if (row["F_YZXMBM"] != null)
                {
                    model.F_YZXMBM = row["F_YZXMBM"].ToString();
                }
                if (row["F_YZXMMC"] != null)
                {
                    model.F_YZXMMC = row["F_YZXMMC"].ToString();
                }
                if (row["F_ZXKSBM"] != null)
                {
                    model.F_ZXKSBM = row["F_ZXKSBM"].ToString();
                }
                if (row["F_ZXKS"] != null)
                {
                    model.F_ZXKS = row["F_ZXKS"].ToString();
                }
                if (row["F_SQDLX"] != null)
                {
                    model.F_SQDLX = row["F_SQDLX"].ToString();
                }
                if (row["F_JCMD"] != null)
                {
                    model.F_JCMD = row["F_JCMD"].ToString();
                }
                if (row["F_LCBS"] != null)
                {
                    model.F_LCBS = row["F_LCBS"].ToString();
                }
                if (row["F_LCZZ"] != null)
                {
                    model.F_LCZZ = row["F_LCZZ"].ToString();
                }
                if (row["F_SSRQ"] != null)
                {
                    model.F_SSRQ = row["F_SSRQ"].ToString();
                }
                if (row["F_SZSJ"] != null)
                {
                    model.F_SZSJ = row["F_SZSJ"].ToString();
                }
                if (row["F_YZLXBM"] != null)
                {
                    model.F_YZLXBM = row["F_YZLXBM"].ToString();
                }
                if (row["F_YZLXMC"] != null)
                {
                    model.F_YZLXMC = row["F_YZLXMC"].ToString();
                }
                if (row["F_ZDLB"] != null)
                {
                    model.F_ZDLB = row["F_ZDLB"].ToString();
                }
                if (row["F_ZDLBBM"] != null)
                {
                    model.F_ZDLBBM = row["F_ZDLBBM"].ToString();
                }
                if (row["F_messagehead"] != null)
                {
                    model.F_messagehead = row["F_messagehead"].ToString();
                }
                if (row["F_messageid"] != null)
                {
                    model.F_messageid = row["F_messageid"].ToString();
                }
                if (row["F_msgxml"] != null)
                {
                    model.F_msgxml = row["F_msgxml"].ToString();
                }
                if (row["F_msg_bz"] != null)
                {
                    model.F_msg_bz = row["F_msg_bz"].ToString();
                }
                if (row["F_DJZT"] != null)
                {
                    model.F_DJZT = row["F_DJZT"].ToString();
                }
                if (row["F_sqdzt"] != null)
                {
                    model.F_sqdzt = row["F_sqdzt"].ToString();
                }
                if (row["F_lowTime"] != null)
                {
                    model.F_lowTime = row["F_lowTime"].ToString();
                }
                if (row["F_highTime"] != null)
                {
                    model.F_highTime = row["F_highTime"].ToString();
                }
                if (row["F_priority"] != null)
                {
                    model.F_priority = row["F_priority"].ToString();
                }
                if (row["F_zylsh"] != null)
                {
                    model.F_zylsh = row["F_zylsh"].ToString();
                }
                if (row["F_mzlsh"] != null)
                {
                    model.F_mzlsh = row["F_mzlsh"].ToString();
                }
                if (row["F_room"] != null)
                {
                    model.F_room = row["F_room"].ToString();
                }
                if (row["F_imageurl"] != null)
                {
                    model.F_imageurl = row["F_imageurl"].ToString();
                }
                if (row["F_jbzdbh"] != null)
                {
                    model.F_jbzdbh = row["F_jbzdbh"].ToString();
                }
                if (row["F_zyjcjg"] != null)
                {
                    model.F_zyjcjg = row["F_zyjcjg"].ToString();
                }
                if (row["F_icd10"] != null)
                {
                    model.F_icd10 = row["F_icd10"].ToString();
                }
                if (row["F_jbzd"] != null)
                {
                    model.F_jbzd = row["F_jbzd"].ToString();
                }
                if (row["F_yzbh"] != null)
                {
                    model.F_yzbh = row["F_yzbh"].ToString();
                }
                if (row["F_fbbm"] != null)
                {
                    model.F_fbbm = row["F_fbbm"].ToString();
                }
                if (row["F_patientid"] != null)
                {
                    model.F_patientid = row["F_patientid"].ToString();
                }
                if (row["F_empiid"] != null)
                {
                    model.F_empiid = row["F_empiid"].ToString();
                }
                if (row["F_zlh"] != null)
                {
                    model.F_zlh = row["F_zlh"].ToString();
                }
                if (row["F_hybm"] != null)
                {
                    model.F_hybm = row["F_hybm"].ToString();
                }
                if (row["F_zrysbm"] != null)
                {
                    model.F_zrysbm = row["F_zrysbm"].ToString();
                }
                if (row["F_zrys"] != null)
                {
                    model.F_zrys = row["F_zrys"].ToString();
                }
                if (row["F_zrysTel"] != null)
                {
                    model.F_zrysTel = row["F_zrysTel"].ToString();
                }
                if (row["F_sqysTel"] != null)
                {
                    model.F_sqysTel = row["F_sqysTel"].ToString();
                }
                if (row["F_zrks"] != null)
                {
                    model.F_zrks = row["F_zrks"].ToString();
                }
                if (row["F_zrksbm"] != null)
                {
                    model.F_zrksbm = row["F_zrksbm"].ToString();
                }
                if (row["F_bqbm"] != null)
                {
                    model.F_bqbm = row["F_bqbm"].ToString();
                }
                if (row["F_sqdxmnr"] != null)
                {
                    model.F_sqdxmnr = row["F_sqdxmnr"].ToString();
                }
                if (row["F_priorityCode"] != null)
                {
                    model.F_priorityCode = row["F_priorityCode"].ToString();
                }
                if (row["F_xmpy"] != null)
                {
                    model.F_xmpy = row["F_xmpy"].ToString();
                }
                if (row["F_mzbm"] != null)
                {
                    model.F_mzbm = row["F_mzbm"].ToString();
                }
                if (row["F_bfh"] != null)
                {
                    model.F_bfh = row["F_bfh"].ToString();
                }
                if (row["F_sjdwbm"] != null)
                {
                    model.F_sjdwbm = row["F_sjdwbm"].ToString();
                }
                if (row["F_brlbbm"] != null)
                {
                    model.F_brlbbm = row["F_brlbbm"].ToString();
                }
                if (row["F_sqdtxpath"] != null)
                {
                    model.F_sqdtxpath = row["F_sqdtxpath"].ToString();
                }
                if (row["F_GUID"] != null)
                {
                    model.F_GUID = row["F_GUID"].ToString();
                }
                if (row["F_TXZT"] != null)
                {
                    model.F_TXZT = row["F_TXZT"].ToString();
                }
                if (row["F_XZCS"] != null && row["F_XZCS"].ToString() != "")
                {
                    model.F_XZCS = int.Parse(row["F_XZCS"].ToString());
                }
                if (row["F_XZBZ"] != null)
                {
                    model.F_XZBZ = row["F_XZBZ"].ToString();
                }
                if (row["F_loadPath"] != null)
                {
                    model.F_loadPath = row["F_loadPath"].ToString();
                }
            }
            return model;
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

