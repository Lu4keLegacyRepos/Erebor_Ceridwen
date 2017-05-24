using Phoenix.WorldData;
using System;

namespace Phoenix.EreborPlugin
{
    public class Rune
    {
        public string Name { get; set; }
        public Serial id { get; set; }
        public Serial[] containers { get; set; }
        public string[] containersName { get; set; }
        public Rune(string name, Serial id, Serial[] subContainers,string[] contNames)
        {
            Name = name;
            this.id = id;
            containers = subContainers;
            containersName = contNames;
        }
        public Rune(string Code)
        {
          //  Notepad.WriteLine(Code);
            string[] tmp = Code.Split(';');
            Name = tmp[0];
            id = new Serial(uint.Parse(tmp[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
            containers =new Serial[] { new Serial(uint.Parse(tmp[2].Substring(2), System.Globalization.NumberStyles.HexNumber))
                    ,new Serial(uint.Parse(tmp[3].Substring(2), System.Globalization.NumberStyles.HexNumber))
                    ,new Serial(uint.Parse(tmp[4].Substring(2), System.Globalization.NumberStyles.HexNumber))};
            containersName = new string[] { tmp[5], tmp[6] };
        }
        public void Recall()
        {
            new UOItem(id).WaitTarget();
            UO.Cast(StandardSpell.Recall);
        }
        public void RecallSvitek()
        {
            DateTime n = DateTime.Now;
            UOItem svitek = new UOItem(0x00);
            DateTime start = DateTime.Now;
            while (svitek.Name != "Recall")
            {
                svitek = new UOItem(World.Player.Backpack.AllItems.FindType(0x1F4C));
                svitek.Click();
                if (DateTime.Now - n > TimeSpan.FromSeconds(2)) return;
            }
            new UOItem(id).WaitTarget();
            svitek.Use();

        }
        public void Gate()
        {
            new UOItem(id).WaitTarget();
            UO.Cast(StandardSpell.GateTravel);
        }
        public bool Equals(string Rune)
        {
            if (this.Name == Rune) return true;
            return false;
        }
        public bool Equals(Serial Rune)
        {
            if (this.id == Rune) return true;
            return false;
        }
        public override string ToString()
        {
            return Name + ";" + id + ";" + containers[0] + ";" + containers[1] + ";" + containers[2] + ";" + containersName[0] + ";" + containersName[1];
        }



    }
}
