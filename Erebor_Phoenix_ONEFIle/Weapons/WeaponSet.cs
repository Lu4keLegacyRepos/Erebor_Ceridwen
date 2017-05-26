using Phoenix.WorldData;
using System;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.Weapons
{
    public delegate void Equip();
    [Serializable]
    public class WeaponSet
    {
        private UOItem weapon;
        private UOItem shield;
        public uint Weapon
        {
            get { return weapon; }
            set
            {
                weapon = new UOItem(value);
            }
        }
        public uint Shield
        {
            get { return shield; }
            set
            {
                shield = new UOItem(value);
            }
        }
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
                if (weapon.Exist) weapon.Click();
                if (shield.Exist) shield.Click();
                UO.Wait(200);
                Name = weapon == null ? "NULL" : weapon.Name == "" ? weapon.Serial.ToString() : weapon.Name;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public WeaponSet()
        {

        }
        public WeaponSet(string StringCode)
        {
            if (StringCode == "NULL") return;
            string[] tmp = StringCode.Split(';');
            Name = tmp[0];
            Weapon = new UOItem(uint.Parse(tmp[1].Substring(2), System.Globalization.NumberStyles.HexNumber));
            Shield = new UOItem(uint.Parse(tmp[2].Substring(2), System.Globalization.NumberStyles.HexNumber));
        }

        public static implicit operator WeaponSet(uint x)
        {
            return new WeaponSet() {Weapon=x};
        }

        public void Equip()
        {
            if (shield.Serial != 0x00 && !(shield.Layer == Layer.LeftHand)) shield.Equip();
            if (weapon.Serial != 0x00) weapon.Equip();
        }
        public override string ToString()
        {
            if (weapon.Serial == 0xFFFF && shield.Serial == 0xFFFF) return "NULL";
            return Name+";"+weapon.Serial.ToString()+";"+shield.Serial.ToString();
        }
    }
}
