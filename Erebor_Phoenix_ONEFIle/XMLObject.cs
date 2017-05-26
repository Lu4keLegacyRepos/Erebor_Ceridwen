using Phoenix.EreborPlugin.EquipSet;
using Phoenix.EreborPlugin.Healing;
using Phoenix.EreborPlugin.Weapons;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Phoenix.EreborPlugin
{
    [Serializable]
    public class XMLObject
    {
        private string path = Core.Directory + @"\Profiles\XML\";
        public XMLObject()
        {

        }


        [XmlElement]
        public int GWWidth { get; set; }
        [XmlElement]
        public int GWHeight { get; set; }
        [XmlElement]
        public uint hidDelay { get; set; }
        [XmlElement]
        public bool hidTrack { get; set; }
        [XmlElement]
        public bool printAnim { get; set; }
        [XmlElement]
        public bool autoDrink { get; set; }
        [XmlElement]
        public bool autoArrow { get; set; }
        [XmlElement]
        public bool autoMorf { get; set; }
        [XmlElement]
        public bool corpseHide { get; set; }
        [XmlElement]
        public bool lot { get; set; }
        [XmlElement]
        public ushort goldLimit { get; set; }
        [XmlElement]
        public bool hitBandage { get; set; }
        [XmlElement]
        public uint criticalHits { get; set; }
        [XmlElement]
        public uint minHp { get; set; }
        [XmlElement]
        public uint  LotBag { get; set; }
        [XmlElement]
        public uint CarvTool { get; set; }
        [XmlElement]
        public bool lotFood { get; set; }
        [XmlElement]
        public bool lotLeather { get; set; }
        [XmlElement]
        public bool lotFeather { get; set; }
        [XmlElement]
        public bool lotRegeants { get; set; }
        [XmlElement]
        public bool lotBolts { get; set; }
        [XmlElement]
        public bool lotGems { get; set; }
        [XmlElement]
        public bool lotExtend1 { get; set; }
        [XmlElement]
        public bool lotExtend2 { get; set; }
        [XmlElement]
        public Graphic lotExtend1Type { get; set; }
        [XmlElement]
        public Graphic lotExtend2Type { get; set; }
        [XmlArray]
        public List<Patient> patients { get; set; }
        [XmlArray]
        public List<WeaponSet> weapons { get; set; }
        [XmlArray]
        public List<EqSet> equipy { get; set; }
        [XmlArray]
        public List<Rune> runy { get; set; }
        [XmlArray]
        public List<string> hotkeys { get; set; }
        [XmlArray]
        public List<string> trackTrack { get; set; }
        [XmlArray]
        public List<string> trackIgnored { get; set; }


        private void GetData()
        {
            GWWidth = Main.instance.GWWidth;
            GWHeight = Main.instance.GWHeight;
            hidDelay = Main.instance.ActualClass.hidDelay;
            hidTrack = Main.instance.HitTrack;
            printAnim = Main.instance.PrintAnim;
            autoDrink = Main.instance.AutoDrink;
            autoArrow = Main.instance.Spells.AutoArrow;
            autoMorf = Main.instance.Amorf.Amorf;
            corpseHide = Main.instance.CorpseHide;
            lot = Main.instance.DoLot;
            goldLimit = Main.instance.GoldLimit;
            hitBandage = Main.instance.HitBandage;
            criticalHits = Main.instance.ActualClass.criticalHits;
            minHp = Main.instance.ActualClass.minHP;
            LotBag = Main.instance.Lot.LotBag;
            CarvTool = Main.instance.Lot.CarvTool;
            lotFood = Main.instance.Lot.Food;
            lotLeather = Main.instance.Lot.Leather;
            lotFeather = Main.instance.Lot.Feathers;
            lotRegeants = Main.instance.Lot.Reageants;
            lotBolts = Main.instance.Lot.Bolts;
            lotGems = Main.instance.Lot.Gems;
            lotExtend1 = Main.instance.Lot.Extend1;
            lotExtend2 = Main.instance.Lot.Extend2;
            lotExtend1Type = Main.instance.Lot.extend1_type;
            lotExtend2Type = Main.instance.Lot.extend2_type;
            patients = Main.instance.AHeal.HealedPlayers;
            weapons = Main.instance.Weapons.weapons;
            equipy = Main.instance.EqipSet.equipy;
            hotkeys = Main.instance.ExHotKeys.swHotkeys;
            runy = RuneTree.instance.Runes;
            trackTrack = Main.instance.Track.Tracked;
            trackIgnored = Main.instance.Track.Ignored;
        }
        public void SetData()
        {
            Main.instance.ActualClass.hidDelay = hidDelay;
            Main.instance.HitTrack = hidTrack;
            Main.instance.PrintAnim = printAnim;
            Main.instance.AutoDrink = autoDrink;
            Main.instance.Spells.AutoArrow = autoArrow;
            Main.instance.Amorf.Amorf = autoMorf;
            Main.instance.CorpseHide = corpseHide;
            Main.instance.DoLot = lot;
            Main.instance.GoldLimit = goldLimit;
            Main.instance.HitBandage = hitBandage;
            Main.instance.ActualClass.criticalHits = criticalHits;
            Main.instance.ActualClass.minHP = minHp;
            Main.instance.Lot.LotBag = new UOItem(LotBag);
            Main.instance.Lot.CarvTool = new UOItem(CarvTool);
            Main.instance.Lot.Food = lotFood;
            Main.instance.Lot.Leather = lotLeather;
            Main.instance.Lot.Feathers = lotFeather;
            Main.instance.Lot.Reageants = lotRegeants;
            Main.instance.Lot.Bolts = lotBolts;
            Main.instance.Lot.Gems = lotGems;
            Main.instance.Lot.Extend1 = lotExtend1;
            Main.instance.Lot.Extend2 = lotExtend2;
            Main.instance.Lot.extend1_type = lotExtend1Type;
            Main.instance.Lot.extend2_type = lotExtend2Type;
            Main.instance.AHeal.HealedPlayers = patients;
            Main.instance.Weapons.weapons = weapons;
            Main.instance.EqipSet.equipy = equipy;
            Main.instance.ExHotKeys.swHotkeys = hotkeys;
            RuneTree.instance.Runes = runy;
            Main.instance.Track.Tracked = trackTrack;
            Main.instance.Track.Ignored = trackIgnored;

            

        }

        public void SetWindowData()
        {
            if (GWHeight < 100 || GWHeight < 100) return;
            Main.instance.GWWidth = GWWidth;
            Main.instance.GWHeight = GWHeight;
        }

        public void Serialize(string filename)
        {
            GetData();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var serializer = new XmlSerializer(GetType());
            File.Delete(path + filename);
            using (var stream = File.OpenWrite(path + filename))
            {
                serializer.Serialize(stream, this);
            }

        }

        public XMLObject Deserialize(string filename)//, XMLObject XMLOBJ)
        {
            XMLObject XMLOBJ=null;
            try
            {
                var serializer = new XmlSerializer(GetType());
                using (var stream = File.OpenRead(path + filename))
                {
                    XMLOBJ = (XMLObject)serializer.Deserialize(stream);
                }
                //XMLOBJ.SetData();

            }
            catch
            {
                UO.PrintError(filename + " neexistuje");
            }
            return XMLOBJ;

        }

    }
}
