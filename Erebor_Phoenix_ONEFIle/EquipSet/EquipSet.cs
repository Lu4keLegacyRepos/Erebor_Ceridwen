using Phoenix.WorldData;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.EquipSet
{
    public class EquipSet
    {
        public List<EqSet> equipy { get; set; }

        public EquipSet()
        {
            equipy = new List<EqSet>();

        }

        public void Remove(int index)
        {
            if (index >= 0 && index >= equipy.Count) return;
            equipy.RemoveAt(index);

        }

        public void fillListBox(ListBox lb)
        {
            lb.Items.Clear();
            foreach (EqSet e in equipy)
            {
                lb.Items.Add(e.getSetName);
            }

        }

        public void Add()
        {
            UO.PrintInformation("Zmaer Bagl se setem");
            UOItem bag = new UOItem(UIManager.TargetObject());
            if (bag.Items.Count() > 0)
            {
                bag.Click();
                equipy.Add(new EqSet(bag));
            }
        }
        public void Add(string Code)
        {
            if (Code == "NULL") return;
            string[] tmp = Code.Split('_');
            foreach(string s in tmp)
            {
                equipy.Add(new EqSet(s));
            }
            
        }

        public override string ToString()
        {
            string tmp = "";
            if (equipy.Count < 1) return "NULL";
            foreach (EqSet e in equipy)
            {
                tmp += e.ToString() + "_";
            }

            tmp = tmp.Remove(tmp.Length - 1);
            return tmp;
        }
    }
}
