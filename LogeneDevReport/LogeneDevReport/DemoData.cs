using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogeneDevReport
{
    public class DemoData
    {
        [DisplayName("病理诊断")]
        public string Blzd { get; set; }

        [DisplayName("肉眼所见")]
        public string Rysj { get; set; }
    }
}
