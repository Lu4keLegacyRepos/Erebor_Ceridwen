using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phoenix.EreborPlugin.Healing
{
    public class HurtedPatient : EventArgs
    {
        public Patient pati { get; set; }
        public bool selfHurted { get; set; }
        public bool crystalOff { get; set; }

    }
}
