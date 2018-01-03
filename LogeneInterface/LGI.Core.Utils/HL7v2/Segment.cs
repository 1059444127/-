using System;
using System.Collections.Generic;

namespace LGI.Core.Utils.HL7v2
{
    /// <summary>
    /// HL7��,����HL7�����һ�д���
    /// </summary>
    public class Segment
    {
        private string _hl7String;

        public string HL7String
        {
            get { return _hl7String; }
        }

        public Segment(string segmentString)
        {
            _hl7String = segmentString;
            Fields =new Fields(this);
        }

        public Field this[int i]
        {
            get
            {
                try
                {
                    return Fields[i];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new Exception($"��ȡHL7ʱ���ִ���,������Ľ�����({i})�������ܽ���({Fields.Count}),HL7Text={HL7String}.",e);
                }
            }
            set => Fields[i] = value;
        }

        public Fields Fields { get; set; }
        
    }

    /// <summary>
    /// HL7���伯��
    /// </summary>
    public class Segments : List<Segment>
    {
        public Segments()
        { }

        public Segments(string hl7String)
        {
            var lines = hl7String.Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var segment = new Segment(line);
                this.Add(segment);
            }
        }

        /// <summary>
        /// ������ָ������Ϊ��ͷ�����ж���
        /// </summary>
        /// <param name="segmantName"></param>
        /// <returns></returns>
        public Segments FindByName(string segmentName)
        {
            Segments segmentList = new Segments();
            foreach (Segment segment in this)
            {
                if (segment[0][0].Trim('\n') == segmentName)
                    segmentList.Add(segment);
            }
            return segmentList;
        }

        /// <summary>
        /// ������ָ������Ϊ��ͷ�����ж���
        /// </summary>
        /// <param name="segmantName"></param>
        /// <returns></returns>
        public Segments this[string segmantName] => FindByName(segmantName);
    }
}