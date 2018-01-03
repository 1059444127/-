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
            var rtn = prs.ReceiveRequest(@"MSH|^~\&|HIS||JHIP||20171218111643||ORM^O01^ORM_O01|LIUXGHIS201712181116480001306964|P|2.4||||||UNICODE UTF-8
PID||99999901|99999901^^^000000&鄂东医疗集团市中心医院~99999901~11523141|0|CESHI1^测试1111||19280410000000|F|||湖北省黄石市大泉路岭秀新城6栋||^^^^^^13451052237|^^^^^^13451052237||O||99999901|241111111111111111|||^^汉族|湖北黄石黄石港区|||||1010^中国
PV1|1|I|^^998^020101&Z心血管内科|1|||LIUXG^^LIUXG||||||||||LIUXG^^LIUXG|职工医保|99999901|||||||||||||||||||||||020101^^^000000||20171218111643|||||||V
ORC|NW|4469415|||||^^^^^4||20171218111647|||LIUXG^^LIUXG|020101^^^^^^^Z心血管内科||||020101^Z心血管内科^^000000|||0^1
OBR|1|4469415||270300002^内镜组织活检检查与诊断|4|||||||0304^Z病理科^000000||||||||||||病理
NTE|1
DG1|1");
            if(rtn.Contains("ERR"))
                Assert.Fail("测试未通过,返回值:\r\n"+rtn);
            Console.WriteLine(rtn);
        }
    }
}