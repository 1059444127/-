using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LGI.Core.Utils.HL7v2
{
    public class HL7v2Reader
    {
        private string _hl7String;

        public string HL7String
        {
            get { return _hl7String; }
        }

        public Segments Segments { get; set; } = null;

        public HL7v2Reader()
        {
            
        }

        public HL7v2Reader(string hl7String)
        {
            _hl7String = hl7String;
            Load(hl7String);
        }

        public void Load(string hl7String)
        {
            _hl7String = hl7String;
            Segments = new Segments(hl7String);
        }

        public Segments this[string pid]
        {
            get
            {
                try
                {
                    return Segments.FindByName(pid);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }

}
