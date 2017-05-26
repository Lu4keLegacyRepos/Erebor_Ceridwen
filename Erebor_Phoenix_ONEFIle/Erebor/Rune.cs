using Phoenix.WorldData;
using System;

namespace Phoenix.EreborPlugin
{
    [Serializable]
    public class Rune
    {
        private Serial ID;
        private Serial[] Containers;
        public string Name { get; set; }
        public uint id { get
            {
                return ID;
            }
            set
            {
                ID = new Serial(value);
            }
        }
        public uint[] containers
        {
            get
            {
                
                uint[] tmp= new uint[3];
                for(int i=0;i<Containers.Length;i++)
                {
                    tmp[i] = Containers[i];
                }
                return tmp;
            }
            set
            {
                Serial[] tmp = new Serial[3];
                for(int u=0;u<value.Length;u++)
                {
                    tmp[u] = value[u];
                }
                Containers = tmp;
            }
        }
        public string[] containersName { get; set; }
        public Rune()
        {

        }
        public Rune(string name, uint id, uint[] subContainers,string[] contNames)
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
            id = uint.Parse(tmp[1]);
            /*containers =new Serial[] { new Serial(uint.Parse(tmp[2].Substring(2), System.Globalization.NumberStyles.HexNumber))
                    ,new Serial(uint.Parse(tmp[3].Substring(2), System.Globalization.NumberStyles.HexNumber))
                    ,new Serial(uint.Parse(tmp[4].Substring(2), System.Globalization.NumberStyles.HexNumber))};*/
            containers = new uint[] { (uint.Parse(tmp[2]))
                        ,(uint.Parse(tmp[3]))
                        ,(uint.Parse(tmp[4]))};
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
