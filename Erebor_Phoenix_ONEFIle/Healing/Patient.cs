using Phoenix.WorldData;
using System;
using System.Runtime.Remoting.Activation;
using System.Xml.Serialization;

namespace Phoenix.EreborPlugin.Healing
{
    [Serializable]
    public class Patient
    {
        [XmlIgnore]
        public UOCharacter chara;// { get; set; }
        public uint character {
            get
            {
                return (uint)chara.Serial;
            }
            set
            {
                chara = new UOCharacter(value);
            }
        }
        public int equip { get; set; }
        public Patient(uint ch, int eq)
        {
            this.character = ch;
            this.equip = eq;
        }
        public Patient()
        {

        }
        public Patient(string createString)
        {
            string[] tm = createString.Split(';');
            character = new UOCharacter(uint.Parse(tm[0].Substring(2), System.Globalization.NumberStyles.HexNumber));
            equip = int.Parse(tm[1]);

        }
        public override string ToString()
        {
            return chara.Serial.ToString() + ";" + equip.ToString();
        }

    }
}
