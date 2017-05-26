using Phoenix.Communication;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Phoenix.EreborPlugin.EquipSet
{
    public delegate void Move(ushort amount,UOItem item);
    [Serializable]
    public class EqSet
    {
        private UOItem dropBagl;
        public string SetName { get; set; }
        public List<Serial> set { get; set; }
        private Dictionary<Serial, Layer> checkList = new Dictionary<Serial, Layer>();
        public EqSet()
        {

        }
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
                
                //temp.Move(ushort.MaxValue, dropBagl);
                Move mo = new Move(Mov);
                mo.BeginInvoke(1, new UOItem(s),null, null);
                //new Thread(Mov).Start();
                UO.Wait(100);
            }

        }

        public void Mov(ushort Amount, UOItem item )
        {
            item.Move(Amount, dropBagl);
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
