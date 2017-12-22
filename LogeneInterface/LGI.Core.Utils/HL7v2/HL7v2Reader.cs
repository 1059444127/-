using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LGI.Core.Utils.HL7v2
{
    public class HL7v2Reader
    {
        public Segments Segments { get; set; } = null;

        public HL7v2Reader()
        {
            
        }

        public HL7v2Reader(string hl7String)
        {
            Load(hl7String);
        }

        public void Load(string hl7String)
        {
            Segments = new Segments(hl7String);
        }
    }

    /// <summary>
    /// HL7段落集合
    /// </summary>
    public class Segments : List<Segment>
    {
        public Segments()
        { }

        public Segments(string hl7String)
        {
            var lines = hl7String.Split(new[]{'\r'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var segment = new Segment(line);
                this.Add(segment);
            }
        }

        /// <summary>
        /// 返回以指定名称为开头的所有段落
        /// </summary>
        /// <param name="segmantName"></param>
        /// <returns></returns>
        public Segments FindByName(string segmentName)
        {
            Segments segmentList =new Segments();
            foreach (Segment segment in this)
            {
                if(segment[0][0].Trim('\n')==segmentName)
                    segmentList.Add(segment);
            }
            return segmentList;
        }

        /// <summary>
        /// 返回以指定名称为开头的所有段落
        /// </summary>
        /// <param name="segmantName"></param>
        /// <returns></returns>
        public Segments this[string segmantName] => FindByName(segmantName);
    }


    /// <summary>
    /// HL7段,就是HL7里面的一行代码
    /// </summary>
    public class Segment
    {
        public Segment()
        {
        }
        public Segment(string segmentString)
        {
         Fields=new Fields(segmentString);   
        }

        public Field this[int i]
        {
            get => Fields[i];
            set => Fields[i] = value;
        }
        public Fields Fields { get; set; }
        
    }

    /// <summary>
    /// HL7节集合,两个|之间的内容为一节
    /// </summary>
    public class Fields : List<Field>
    {
        public Fields()
        {

        }
        public Fields(string segmentString)
        {
            var fieldStrings = segmentString.Split('|');
            foreach (string fieldString in fieldStrings)
            {
                Field f = new Field(fieldString);
                this.Add(f);
            }
        }
    }

    /// <summary>
    /// HL7节集合,两个|之间的内容为一节
    /// </summary>
    public class Field
    {
        public Field()
        {
        }
        public Field(string fieldString)
        {
            Values = fieldString.Split('^').ToList();
        }

        /// <summary>
        /// 获取HL7节中的值,一个节中的多个值以^分开
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        public List<string> Values { get; set; }
    }
    
}
