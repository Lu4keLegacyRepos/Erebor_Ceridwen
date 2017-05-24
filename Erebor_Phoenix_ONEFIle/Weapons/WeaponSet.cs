using Phoenix.WorldData;
using System;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.Weapons
{
    public delegate void Equip();
    public class WeaponSet
    {
        public UOItem Weapon { get; private set; }
        public UOItem Shield { get; private set; }
        private string setName;

        public string Name { get
            {
                if (setName=="") return ToString();
                return setName;

            }
            set { setName = value; }
        }



        public WeaponSet(UOItem weapon, UOItem shield)
        {
            if (weapon.Serial == 0xFFFFFFFF && shield.Serial == 0xFFFFFFFF) return;
            try
            {
                Weapon = weapon;
                Shield =shield;
                UO.Wait(200);
                if (Weapon.Exist) Weapon.Click();
                if (Shield.Exist) Shield.Click();
                UO.Wait(200);
                Name = Weapon == null ? "NULL" : Weapon.Name == "" ? Weapon.Serial.ToString() : Weapon.Name;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public WeaponSet(string StringCode)
        {
            if (StringCode == "NULL") return;
            string[] tmp = StringCode.Split(';');
            Name = tmp[0];
            Weapon = new UOItem(uint.Parse(tmp[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
            Shield = new UOItem(uint.Parse(tmp[2].Substring(2), System.Globalization.NumberStyles.HexNumber));
        }



        public void Equip()
        {
            if (Shield.Serial != 0x00 && !(Shield.Layer == Layer.LeftHand)) Shield.Equip();
            if (Weapon.Serial != 0x00) Weapon.Equip();
        }
        public override string ToString()
        {
            if (Weapon.Serial == 0xFFFF && Shield.Serial == 0xFFFF) return "NULL";
            return Name+";"+Weapon.Serial.ToString()+";"+Shield.Serial.ToString();
        }
    }
}
