using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LGI.Core.Utils.HL7v2
{
    /// <summary>
    /// HL7节集合,两个|之间的内容为一节
    /// </summary>
    public class Field
    {
        public string HL7String { get; }

        public Segment Segment { get; set; }

        public Field(string fieldString)
        {
            HL7String = fieldString;
            Values = fieldString.Split('^').ToList();
        }

        /// <summary>
        /// 获取HL7节中的值,一个节中的多个值以^分开
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
        {
            get
            {
                try
                {
                    return Values[index];
                }
                catch (Exception e)
                {
                    throw new Exception($"读取HL7时出现错误,所请求的值索引({index})超出了值的个数({Values.Count}),HL7Text={HL7String}.", e);

                }
            }
            set => Values[index] = value;
        }

        public List<string> Values { get; set; }
    }


    /// <summary>
    /// HL7节集合,两个|之间的内容为一节
    /// </summary>
    public class Fields : List<Field>
    {
        public Fields()
        {

        }
        public Fields(Segment segment)
        {
            var fieldStrings = segment.HL7String.Split('|');
            //MSH的第一个field是字段分隔符,默认为'|',因此特殊处理,与标准HL7文档中的序号保持一致
            if (segment.HL7String.StartsWith("MSH"))
            {
                var lst = fieldStrings.ToList();
                lst.Insert(1,"|");
                fieldStrings = lst.ToArray();
            }
            foreach (string fieldString in fieldStrings)
            {
                Field f = new Field(fieldString);
                this.Add(f);
            }
        }
    }
}