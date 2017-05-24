using Phoenix.Communication;
using Phoenix.WorldData;
using System.Collections.Generic;
using System.Threading;

namespace Phoenix.EreborPlugin.EquipSet
{
    public class EqSet
    {
        private UOItem dropBagl, tempItem;
        private string SetName;
        private List<Serial> set;
        private Dictionary<Serial, Layer> checkList = new Dictionary<Serial, Layer>();
        public EqSet(UOItem SetBAG)
        {
            set = new List<Serial>() { SetBAG.Serial };
            SetBAG.Click();
            SetBAG.Use();
            foreach (UOItem it in SetBAG.Items)
            {
                it.Click();
                set.Add(it.Serial);
            }
            SetName = SetBAG.Name;
        }
        public EqSet(string Code)
        {
            string[] temp;
            temp = Code.Split(';');
            SetName = temp[0];
            set = new List<Serial>();
            foreach (string s in temp)
            {
                if (s == SetName) continue;
                set.Add(uint.Parse(s.Substring(2), System.Globalization.NumberStyles.HexNumber));
            }

        }
        public string getSetName
        {
            get
            {
                return SetName;
            }

        }
        public void Dress(UOItem dropBag)
        {
            dropBagl = dropBag;
            UOItem tmp;
            checkAndStore();
            foreach (Serial s in set)
            {
                if (s == set[0]) continue;
                tmp = new UOItem(s);
                if (tmp.Layer == Layer.LeftHand || tmp.Layer == Layer.RightHand)
                {
                    tmp.Equip();
                }
                else tmp.Use();
            }
            checkAndStore_();

        }
        public void DressOnly()
        {
            UOItem tmp;
            foreach (Serial s in set)
            {
                if (s == set[0]) continue;
                tmp = new UOItem(s);
                if (tmp.Layer == Layer.LeftHand || tmp.Layer == Layer.RightHand) tmp.Equip();
                else tmp.Use();
            }
        }

        public void checkAndStore(LayersCollection layers = null)
        {
            checkList.Clear();
            if (layers == null) layers = World.Player.Layers;
            foreach (UOItem it in layers)
            {
                if (checkList.ContainsKey(it.Serial) || it.Layer == Layer.None) continue;
                checkList.Add(it.Serial, it.Layer);
            }
        }
        public void checkAndStore_()
        {
            foreach (Serial s in checkList.Keys)
            {
                tempItem = new UOItem(s);
                //temp.Move(ushort.MaxValue, dropBagl);
                new Thread(Mov).Start();
                UO.Wait(10);
            }

        }

        private void Mov()
        {
            tempItem.Move(ushort.MaxValue, dropBagl);
        }
        public override string ToString()
        {
            string temp = SetName+";";
            foreach (Serial s in set)
            {
                temp += s.ToString() + ";";
            }
            temp = temp.Substring(0, temp.Length - 1);
            return temp;
        }
    }
}
