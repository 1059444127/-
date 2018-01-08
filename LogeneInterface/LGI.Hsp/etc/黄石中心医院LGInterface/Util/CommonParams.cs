using System;
using System.Collections.Generic;
using System.Text;
using SendPisResult;

namespace LGInterface.Util
{
    public static class CommonParams
    {
        private static IniFiles f = new IniFiles("sz.ini");

        /// <summary>
        /// 标本送检人
        /// </summary>
        public static string SampleReceivePersonName => f.ReadString("yh", "yhmc", "").Replace("\0", "").Trim();
    }
}
