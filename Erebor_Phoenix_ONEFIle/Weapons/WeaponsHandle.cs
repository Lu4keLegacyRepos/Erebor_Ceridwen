using Phoenix.WorldData;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.Weapons
{
    public class WeaponsHandle
    {
        public List<WeaponSet> weapons { get; set; }
        public WeaponSet ActualWeapon;

        public WeaponsHandle()
        {
            weapons = new List<WeaponSet>();
            
        }
        public void fillListBox(ListBox lb)
        {
            lb.Items.Clear();
            if (weapons.Count == 0) return;
            foreach (WeaponSet e in weapons)
            {
                lb.Items.Add(e.Name);
            }

        }
        public void Add()
        {
            UO.PrintInformation("Zamer zbran");
            UOItem weap=  new UOItem(UIManager.TargetObject());
            UO.PrintInformation("Zamer stit");
            UOItem shiel = new UOItem(UIManager.TargetObject());
            if (weap.Serial == 0xFFFFFFFF && shiel.Serial == 0xFFFFFFFF) return;
            weapons.Add(new WeaponSet(weap,shiel));
            if (weapons.Count > 0 && ActualWeapon==null) ActualWeapon = weapons[0];
        }
        public void Add(string Code)
        {
            if (Code == "NULL") return;
            string[] tmp = Code.Split('_');
            foreach (string s in tmp)
            {
                weapons.Add(new WeaponSet(s));
            }
            if (weapons.Count > 0 && ActualWeapon == null)
            {
                ActualWeapon = weapons[0];
            }
        }

        public override string ToString()
        {
            string tmp = "";
            if (weapons.Count < 1) return "NULL";
            foreach (WeaponSet e in weapons)
            {
                tmp += e.ToString() + "_";
            }
            tmp = tmp.Remove(tmp.Length - 1);
            return tmp;
        }
        public void Remove(int index)
        {
            if (index >= 0 && index >= weapons.Count) return;
            weapons.RemoveAt(index);

        }

        public void SwitchWeapons()
        {
            SwitchWeapons(0);
        }
        private void SwitchWeapons(int tempCyclus)
        {
            if (tempCyclus > (weapons.Count == 0 ? 100 : weapons.Count + 5))
            {
                UO.PrintError("Nemas u sebe zadnou zbran ze seznamu");
                return;
            }
            if (weapons.Count < 1)
            {
                UO.PrintError("Neams nastaveny zbrane");
                return;
            }
            int indxActualW = weapons.IndexOf(ActualWeapon==null?weapons[0]:ActualWeapon);
            if (indxActualW < weapons.Count)
            {
                if (indxActualW + 1 == weapons.Count)
                {
                    ActualWeapon = weapons[0];
                }
                else
                {
                    ActualWeapon = weapons[indxActualW + 1];
                }
                if ((new UOItem(ActualWeapon.Weapon)).Exist)
                    ActualWeapon.Equip();
                else
                {
                    UO.Wait(100);
                    SwitchWeapons(tempCyclus++);
                }
            }
        }

    }
}
