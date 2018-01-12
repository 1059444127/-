using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrisisSmsSender;
using CrisisSmsSender.CrisisService;
using CrisisSmsSender.SmsService;
using dbbase;

namespace PathnetFSWJZ
{
    public partial class hszxyy : Form
    {
        public hszxyy(string blh)
        {
            InitializeComponent();
            F_blh = blh;
        }
        dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");
        string F_blh = "";
        IniFiles f = new IniFiles(Application.StartupPath + "\\sz.ini");


        string bqdm = "";
        string ysmc = "";
        string ysgh = "";
        string bgys = "";
        string shys = "";
        string bgrq = "";
        string sqxh = "";

        private void hszxyy_Load(object sender, EventArgs e)
        {

        }

        private void hszxyy_Shown(object sender, EventArgs e)
        {
            ysmc = f.ReadString("yh", "yhmc", "").Replace("\0", "");
            ysgh = f.ReadString("yh", "yhbh", "").Replace("\0", "");
            DataTable dt = aa.GetDataTable("select * from T_jcxx where F_blh='" + F_blh + "'", "jcxx");
            var dtMsg = aa.GetDataTable($"select * from t_wjz_sms t where f_blh='{F_blh}'", "dt1");
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("未找病理号!");
                this.Close();
            }
            else
            {
                //获取危急值确认情况
                if (dtMsg.Rows.Count > 0)
                {
                    var drMsg = dtMsg.Rows[0];
                    var sendDate = Convert.ToDateTime(drMsg["f_send_date"]);
                    txtLastSendDate.Text = sendDate.ToString("yyyy-MM-dd HH:mm:ss");
                    txtConfirmStatus.Text = drMsg["f_confirm_status"].ToString();

                    //如果超过24小时则危急值接收已超时,文字显示成红色
                    if (txtConfirmStatus.Text == "未确认" && (DateTime.Now - sendDate).TotalHours > 24)
                    {
                        txtConfirmStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtConfirmStatus.ForeColor = Color.Black;
                    }
                }

                txtArea.Text = dt.Rows[0]["F_bq"].ToString();
                txtBlh.Text = F_blh;
                txtLastContent.Text = dt.Rows[0]["F_yl6"].ToString();
                //短信样式:
//                    【鄂东医疗集团】周丹君,1997 - 10 - 22,住院号: 11662917,病理申请号：123456789,床号: 18 病理危机值提示: 1.（右卵巢）多房性粘液性囊腺瘤伴广泛出血、变性。
//                2.（右侧）输卵管组织见出血，血管扩张、淤血。 

                sqxh = dt.Rows[0]["F_sqxh"].ToString();
                txtContent.Text = "【鄂东医疗集团】"+ dt.Rows[0]["F_xm"] + "," + dt.Rows[0]["F_mz"] + ",住院号:" + dt.Rows[0]["F_zyh"] +"申请单号:"+ sqxh +",床号:" + dt.Rows[0]["F_ch"] + " 危机值提示:" + dt.Rows[0]["F_blzd"].ToString()+" 收到请回复：BL"+sqxh;
                bgys = dt.Rows[0]["F_bgys"].ToString().Trim();
                shys = dt.Rows[0]["F_shys"].ToString().Trim();
                bgrq = dt.Rows[0]["F_bgrq"].ToString().Trim();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Trim() == "")
            {
                MessageBox.Show("没有内容可发送");
                return;
            }
            if (txtPhoneNumber.Text.Trim() == "")
            {
                MessageBox.Show("请输入电话号码");
                return;
            }
            if (txtPhoneNumber.Text.Trim().Length != 11)
            {
                MessageBox.Show("电话号码长度有误");
                return;
            }
            //try
            //{
            //    int dh = Convert.ToInt32(textBox7.Text.Trim());
            //}
            //catch
            //{
            //    MessageBox.Show("电话号码有误");
            //    return;
            //}
            if (F_blh == "")
            {
                MessageBox.Show("无法发送");
                return;
            }
           
            if (bgys != ysmc && shys != ysmc)
            {
                MessageBox.Show("非报告医生或审核医生,无法发送");
                return;
            }
            SMSService fswjz = new SMSService();
            string url = f.ReadString("wjz", "url", "");
            if (url != "")
            {
                fswjz.Url = url;
            }
            string msgid = Guid.NewGuid().ToString();
            string ss = fswjz.SendSMS(msgid, txtPhoneNumber.Text, txtContent.Text.Trim(), "B55E51E062EC377E42C7FF0BC0149B57C4BC3E4224F5631148A3852C64DA398E");
            if (ss.ToLower()=="ok")
            {
                aa.ExecuteSQL("update T_jcxx set F_yl6='" + DateTime.Now.ToString("yyyyMMddHHmmss") + " '+'" + txtPhoneNumber.Text + " " + txtContent.Text.Trim() + " " + msgid + "' where F_blh='" + F_blh + "'");

                //记录到数据库
                InsertCrisisRecord(msgid);
                //推送到平台
                CrisisReportService crs = new CrisisReportService();
                crs.Url = f.ReadString("hszxyy", "wjzurl", "http://172.16.80.174:8081/CrisisReportService.asmx");
                crs.ReportCrisis(F_blh,txtContent.Text);

                writebg(txtContent.Text.Trim(), F_blh, "发送危急值", "危急值");
                MessageBox.Show("危机值消息发送完成");
                btnSend.Enabled = false;
            }
            else
            {
                MessageBox.Show(ss);
            }
        }

        private void InsertCrisisRecord(string msgid)
        {
            //插入危急值短信记录
            string sqlAdd = $@"INSERT INTO [dbo].[T_WJZ_SMS]
                               ([f_blh]
                               ,[f_content]
                               ,[f_phone_number]
                               ,[f_send_to_dept]
                               ,[f_send_to_doctor]
                               ,[f_send_to_area]
                               ,[f_message_id]
                                )
                         VALUES
                                (
                               '{txtBlh.Text}', --(<f_blh, varchar(50),>
                               '{txtContent.Text.Trim()}',--<f_content, varchar(1000),>
                               '{txtPhoneNumber.Text}',--<f_phone_number, varchar(50),>
                               '{txtDept.Text}',--<f_send_to_dept, varchar(50),>
                               '{txtDoctor.Text}',--<f_send_to_doctor, varchar(50),>
                               '{txtArea.Text}',--<f_send_to_area, varchar(50),>
                               '{msgid}' --<f_message_id, varchar(100),>)
		                       )";
            string sqlDel = $@"delete T_WJZ_SMS where f_blh='{txtBlh.Text}'";

            aa.ExecuteSQL(sqlDel);
            aa.ExecuteSQL(sqlAdd);
        }

        public void writebg(string F_bgnr, string F_blh, string F_dz, string F_ctmc)
        {
            string pcname = System.Environment.MachineName;
            string czy = f.ReadString("yh", "yhmc", "").Replace("/0", "");
            string czrq = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string BLH = F_blh;
            string dz = F_dz;
            string nr = F_bgnr;
            //string exemc = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string exemc = "pathnetfswjz";
            string ctmc = F_ctmc;

            string[] values = new string[8] { BLH, czrq, czy, pcname, dz, nr, exemc, ctmc };
            string[] fields = new string[8] { "F_blh", "F_rq", "F_czy", "F_wz", "F_dz", "F_nr", "F_exemc", "F_ctmc" };

            if (aa.insertsql("T_bghj", ref fields, ref values))
            { }
            else
            {

            }

        }
    }
}