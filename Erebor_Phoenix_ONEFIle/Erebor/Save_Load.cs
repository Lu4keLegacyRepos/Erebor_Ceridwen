using Phoenix.EreborPlugin.Erebor;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin
{
    public static class Save_Load
    {
        public static List<string> tosave;
        public static void Save(string fileName)
        {
            int i = 0;
            try
            {
                tosave= new List<string>();
              //  tosave.Add(Main.instance.GWWidth.ToString()); ++i;
              //  tosave.Add(Main.instance.GWHeight.ToString()); ++i;
                tosave.Add(Main.instance.ActualClass.hidDelay.ToString());++i;
                tosave.Add(Main.instance.HitTrack.ToString()); ++i;
                tosave.Add(Main.instance.PrintAnim.ToString());++i;
                tosave.Add(Main.instance.AutoDrink.ToString());++i;
                tosave.Add(Main.instance.Spells.AutoArrow.ToString()); ++i;
                tosave.Add(Main.instance.Amorf.Amorf.ToString()); ++i;
                tosave.Add(Main.instance.CorpseHide.ToString()); ++i;
                tosave.Add(Main.instance.DoLot.ToString()); ++i;
                tosave.Add(Main.instance.GoldLimit.ToString()); ++i;
                tosave.Add(Main.instance.HitBandage.ToString()); ++i;
                tosave.Add(Main.instance.ActualClass.criticalHits.ToString()); ++i;
                tosave.Add(Main.instance.ActualClass.minHP.ToString()); ++i;
                tosave.Add(Main.instance.Lot.LotBag== null ? "NULL" : Main.instance.Lot.LotBag.Serial.ToString()); ++i;
                tosave.Add(Main.instance.Lot.CarvTool == null ? "NULL" : Main.instance.Lot.CarvTool.Serial.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Food.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Leather.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Feathers.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Reageants.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Bolts.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Gems.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Extend1.ToString()); ++i;
                tosave.Add(Main.instance.Lot.Extend2.ToString()); ++i;
                tosave.Add(Main.instance.Lot.extend1_type == null ? "NULL" : Main.instance.Lot.extend1_type.ToString()); ++i;
                tosave.Add(Main.instance.Lot.extend2_type == null ? "NULL" : Main.instance.Lot.extend2_type.ToString()); ++i;
                tosave.Add(Main.instance.AHeal == null ? "NULL" : Main.instance.AHeal.ToString()); ++i;
                tosave.Add(Main.instance.Weapons == null ? "NULL" : Main.instance.Weapons.ToString()); ++i;
                //tosave.Add(Main.instance.Weapons.ActualWeapon == null ? "NULL" : Main.instance.Weapons.ActualWeapon.ToString()); ++i;
                tosave.Add(Main.instance.EqipSet == null ? "NULL" : Main.instance.EqipSet.ToString()); ++i;
                tosave.Add(Main.instance.ExHotKeys == null ? "NULL" : Main.instance.ExHotKeys.ToString()); ++i;
                tosave.Add(RuneTree.instance == null ? "NULL" : RuneTree.instance.ToString()); ++i;
                tosave.Add(Main.instance.Track == null ? "NULL" : Main.instance.Track.ToString());++i;
                Serializer.SerializeObject(tosave, fileName);

                UO.Print("XML Ulozeno");
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message+" , id radku:"+i);
            }
  
        }
        /// <summary>
        /// Load only windows size
        /// </summary>
        public static void LoadWindowSize()
        {
            int i = 0;

            List<string> loaded = new List<string>();
            try
            {
                if (!Serializer.Deserialize(out loaded, "Wsize.xml")) return;
                Main.instance.GWWidth = int.Parse(loaded[i++]);
                Main.instance.GWHeight = int.Parse(loaded[i++]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " , id radku:" + i);
            }


        }
        public static void SaveWindowSize()
        {
            int i = 0;
            try
            {
                tosave = new List<string>();
                tosave.Add(Main.instance.GWWidth.ToString()); ++i;
                tosave.Add(Main.instance.GWHeight.ToString()); ++i;
                Serializer.SerializeObject(tosave, "Wsize.xml");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " , id radku:" + i);
            }
        }
public static void Load(string fileName)
        {
            int i = 0;

            List<string> loaded = new List<string>();
            try
            {
                if(!Serializer.Deserialize(out loaded, fileName))return;
                //Main.instance.GWWidth = int.Parse(loaded[i++]);
               // Main.instance.GWHeight = int.Parse(loaded[i++]);
                Main.instance.ActualClass.hidDelay = uint.Parse(loaded[i++]);
                Main.instance.HitTrack = Boolean.Parse(loaded[i++]);
                Main.instance.PrintAnim = Boolean.Parse(loaded[i++]);
                Main.instance.AutoDrink = Boolean.Parse(loaded[i++]);
                Main.instance.Spells.AutoArrow = Boolean.Parse(loaded[i++]);
                Main.instance.Amorf.Amorf = Boolean.Parse(loaded[i++]);
                Main.instance.CorpseHide = Boolean.Parse(loaded[i++]);
                Main.instance.DoLot = Boolean.Parse(loaded[i++]);
                Main.instance.GoldLimit = ushort.Parse(loaded[i++]);
                Main.instance.HitBandage = Boolean.Parse(loaded[i++]);
                Main.instance.ActualClass.criticalHits = uint.Parse(loaded[i++]);
                Main.instance.ActualClass.minHP = uint.Parse(loaded[i++]);
                Main.instance.Lot.LotBag = loaded[i]=="NULL"?new UOItem(0x00): new UOItem(uint.Parse(loaded[i].Substring(2), System.Globalization.NumberStyles.HexNumber));
                i++;
                Main.instance.Lot.CarvTool = loaded[i] == "NULL" ? new UOItem(0x00) : new UOItem(uint.Parse(loaded[i].Substring(2), System.Globalization.NumberStyles.HexNumber));
                i++;
                Main.instance.Lot.Food = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Leather = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Feathers = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Reageants = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Bolts = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Gems = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Extend1 = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.Extend2 = Boolean.Parse(loaded[i++]);
                Main.instance.Lot.extend1_type = new Graphic(ushort.Parse(loaded[i++].Substring(2), System.Globalization.NumberStyles.HexNumber));
                Main.instance.Lot.extend2_type = new Graphic(ushort.Parse(loaded[i++].Substring(2), System.Globalization.NumberStyles.HexNumber));
                Main.instance.AHeal.Add(loaded[i++]);
                Main.instance.Weapons.Add(loaded[i++]);
                //Main.instance.Weapons.ActualWeapon = new WeaponSet(loaded[i++]);
                Main.instance.EqipSet.Add(loaded[i++]);
                Main.instance.ExHotKeys.Add(loaded[i++]);
                RuneTree.instance.Add(loaded[i++]);
                Main.instance.Track.Add(loaded[i++]);

                Erebor.Erebor.instance.BeginInvoke(new Erebor.CheckAll(Erebor.Erebor.instance.CheckAll));




            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " , id radku:" + i);
            }



        }
    }
}
