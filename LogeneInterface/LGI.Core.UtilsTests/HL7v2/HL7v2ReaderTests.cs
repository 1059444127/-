using Microsoft.VisualStudio.TestTools.UnitTesting;
using LGI.Core.Utils.HL7v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGI.Core.Utils.HL7v2.Tests
{
    [TestClass()]
    public class HL7v2ReaderTests
    {
        #region 测试数据

        public static string _testHl7String =
            @"
MSH|^~\&|HIS||JHIP||20171218111643||ORM^O01^ORM_O01|LIUXGHIS201712181116480001306964|P|2.4||||||UNICODE UTF-8
PID||99999901|99999901^^^000000&鄂东医疗集团市中心医院~99999901~11523141|0|CESHI1^测试1111||19280410000000|F|||湖北省黄石市大泉路岭秀新城6栋||^^^^^^13451052237|^^^^^^13451052237||O||99999901|241111111111111111|||^^汉族|湖北黄石黄石港区|||||1010^中国
PV1|1|I|^^998^020101&Z心血管内科|1|||LIUXG^^LIUXG||||||||||LIUXG^^LIUXG|职工医保|99999901|||||||||||||||||||||||020101^^^000000||20171218111643|||||||V
ORC|NW|4469415|||||^^^^^4||20171218111647|||LIUXG^^LIUXG|020101^^^^^^^Z心血管内科||||020101^Z心血管内科^^000000|||0^1
OBR|1|4469415||270300002^内镜组织活检检查与诊断|4|||||||0304^Z病理科^000000||||||||||||病理
NTE|1
DG1|1";

            #endregion

        [TestMethod()]
        public void HL7v2ReaderTest()
        {
            HL7v2Reader reader = new HL7v2Reader(_testHl7String);
            //获取开头为PID的段落的第一行,第六个节中的第一个值
            var namePy = reader.Segments["PID"][0][5][0];
            var name=reader.Segments["PID"][0][5][1];
            Assert.AreEqual("CESHI1", namePy);
            Assert.AreEqual("测试1111", name);
        }
    }
}