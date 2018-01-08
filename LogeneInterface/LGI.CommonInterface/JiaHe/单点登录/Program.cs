using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using LGI.Core.Model;

namespace 单点登录
{
    static class Program
    {
        private static PathnetEntities db;
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region MyRegion

//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Form1());

            #endregion

            string yhm;

            //获取要启动的exe文件名
            string exeName="pathnetrpt";
            var sourceName = AppDomain.CurrentDomain.FriendlyName.ToUpper().Replace(".EXE","");
            if (sourceName.Split('_').Length > 1)
                exeName = sourceName.Split('_')[1];


            //验证病理exe是否存在
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $"\\{exeName}.exe") == false)
            {
                MessageBox.Show("未找到朗珈病理系统启动程序,请将单点登录程序放在朗珈主程序文件夹中.");
                Application.Exit();
            }

            //验证入参
            if (args.Length == 0)
            {
                MessageBox.Show("无法启动:没有传入用户登录信息.");
                Application.Exit();
            }
            if (args[0].Split('|').Length != 2)
            {
                MessageBox.Show("无法启动:用户登录参数的数量不正确.");
                Application.Exit();
            }

            //解密用户名
            var userInfo = args[0].Split('|');
            yhm = AESDecrypt(userInfo[0], userInfo[1]);

            //查询对应的用户
            db = ContextPool.GetContext();
            var yh = db.T_YH.SingleOrDefault(o => o.F_YHM == yhm);

            //如果用户不存在,新建用户并弹出提示
            if (yh == null)
            {
                yh = CreateYh(yhm);
                MessageBox.Show("您没有朗珈病理系统登录权限,已为您自动新建朗珈用户,请联系朗珈病理系统管理员为您修改权限和密码.");
            }

            //调用PathQc
            Process.Start( exeName + ".exe", yh.F_YHM + "," + yh.F_YHMM);
        }

        private static T_YH CreateYh(string yhm)
        {
            T_YH yh = new T_YH();
            yh.F_YHM = yh.F_YHBH = yhm;
            yh.F_YHMC = "平台用户_" + yhm;
            yh.F_YHQX = 8;
            yh.F_YHMM = yhm;

            yh.F_SFZH = yh.F_DHHM = yh.F_FJH = yh.F_CXTS =  yh.F_YH_BY1 = yh.F_YH_BY2= "";
            yh.F_DXFPYS = 0;

            db.T_YH.Add(yh);
            db.SaveChanges();

            return yh;
        }

        /// <summary>   
        /// AES解密   
        /// </summary>   
        /// <param name="text">已加密密码</param>  
        /// <param name="iv">随机密钥</param>   
        /// <returns>返回解密后的原码</returns>   
        public static string AESDecrypt(string text, string iv)
        {
            //解密密钥，16位    
            string password = "GOODWILLCIS-JHIP";
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
    }
}