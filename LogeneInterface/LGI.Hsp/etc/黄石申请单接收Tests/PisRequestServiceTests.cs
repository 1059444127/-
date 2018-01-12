using LGInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 黄石申请单接收;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 黄石申请单接收.Tests
{
    [TestClass()]
    public class PisRequestServiceTests
    {
        [TestMethod()]
        public void ReceiveRequestTest()
        {
            PisRequestService prs = new PisRequestService();
            var rtn = prs.ReceiveRequest(@"MSH|^~\&|HIS||JHIP||20180104145556||ORM^O01^ORM_O01|3012HIS201801041455170001541896|P|2.4||||||UNICODE UTF-8
PID||01885053|01885053^^^000000&鄂东医疗集团市中心医院~01885053~11662744|0|^胡运生||19520113000000|M||||||||O||01885053|420222195201137215|||^^汉族|湖北省|||||1010^中国
PV1|1|I|^^45^020116&Z呼吸内科Ⅱ|1|||3012^^黄永军||||||||||3012^^黄永军|城乡医保|01885053|||||||||||||||||||||||020116^^^000000||20180104145556|||||||V
ORC|NW|4513998|||||^^^^^4||20180104145515|||3012^^黄永军|020116^^^^^^^Z呼吸内科Ⅱ||||020116^Z呼吸内科Ⅱ^^000000|||0^1
OBR|1|4513998||270200001A^体液细胞学检查与诊断|4|||||||0304^Z病理科^000000||||||||||||病理
NTE|1|双肺呼吸音低，心律不齐||^^^胸闷喘气，咳嗽咳痰
DG1|1|||胸腔积液
");
            if(rtn.Contains("ERR"))
                Assert.Fail("测试未通过,返回值:\r\n"+rtn);
            Console.WriteLine(rtn);
        }
    }
}