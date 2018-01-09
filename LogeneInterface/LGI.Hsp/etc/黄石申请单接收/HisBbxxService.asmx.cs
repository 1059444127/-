using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using LGI.Core.Utils;

namespace 黄石申请单接收
{
    /// <summary>
    /// HisBbxxService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class HisBbxxService : System.Web.Services.WebService
    {
        private const string BbxxOdbcConnStr = "BbxxOdbcConnStr";

        [WebMethod]
        public string GetBbxx(string sqxh)
        {
            string connStr = ConfigurationManager.AppSettings[BbxxOdbcConnStr];

            string sql = $"select * from v_PATHOLOGICAL_apply v where v.apply_no='{sqxh}'";
            var dtBbxx = OdbcOracleHelper.GetTable(connStr, sql);

            StringBuilder sb = new StringBuilder();
            if (dtBbxx.Rows.Count > 0)
            {
                sb.Append("<BBLB>");
                foreach (DataRow row in dtBbxx.Rows)
                {
                    sb.Append("<row ");
                    sb.Append($" F_BBXH = {row["item_no"]} ");
                    sb.Append($" F_BBTMH = '' ");
                    sb.Append($" F_BBMC = {row["item_name"]} ");
                    sb.Append($" F_CQBW = ");
                    sb.Append($" F_BZ =  ");
                    sb.Append($" F_LTSJ =  ");
                    sb.Append($" F_GDSJ =  ");
                    sb.Append($" F_JSSJ =  ");
                    sb.Append($" F_JSY =  ");
                    sb.Append($" F_BBZT =  ");
                    sb.Append($" F_BBPJ = ");
                    sb.Append($" F_PJR =  ");
                    sb.Append($" F_PJSJ = ");
                    sb.Append("/>");
                }
                sb.Append("</BBLB>");
            }

            return sb.ToString();

        }
    }
}
