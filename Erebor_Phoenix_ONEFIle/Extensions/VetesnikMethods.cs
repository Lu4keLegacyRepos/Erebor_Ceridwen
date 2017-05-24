using Phoenix.WorldData;
using Phoenix.Communication.Packets;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Phoenix;

namespace Phoenix.EreborPlugin.Extensions
{
    public class VetesnikMethods
    {
        public List<string> temp = new List<string>();
        public int exp { get; private set; }

        public class popis
        {
            string typ;
            string cast;
            public popis(string typp, string castt)
            {
                typ = typp;
                cast = castt;
            }
            public string getTyp()
            { return typ; }
            public string getCast()
            { return cast; }
        }




        [Command]
        public void id()
        {
            UOItem bagl = new UOItem(UIManager.TargetObject());
            foreach (UOItem it in bagl.Items)
            {
                UO.Wait(100);
                it.WaitTarget();
                UO.Say("identifikace");
                UO.Wait(100);
            }
        }
        [Command]
        public void presun(int a)
        {
            UOItem bagl = new UOItem(UIManager.TargetObject());
            UOItem kam = new UOItem(UIManager.TargetObject());
            int aa = a;

            foreach (UOItem it in bagl.Items)
            {
                it.Move(1, kam);
                UO.Wait(100);
                a--;
                if (a < 0) break;
            }
        }

        [Command]
        public void vypisObsah()
        {

            Dictionary<ushort, string> materials = new Dictionary<ushort, string>();
            materials.Add(0x099A, "Copper");
            materials.Add(0x0763, "Iron");
            materials.Add(0x097F, "Verite");
            materials.Add(0x0985, "Valorite");
            materials.Add(0x0989, "Obsidian");
            materials.Add(0x0999, "Adamantium");

            Dictionary<string, string> prop = new Dictionary<string, string>();
            prop.Add("Provocation", "Provo");
            prop.Add("sile", "str");
            prop.Add("Archery", "Archery");
            prop.Add("inteligenci", "int");
            prop.Add("Macefighting", "crushing");
            prop.Add("Fencing", "1Hand");
            prop.Add("Mining", "mining");
            prop.Add("obratnosti", "dex");
            prop.Add("Veterinary", "vet");
            prop.Add("Anatomy", "anatomy");
            prop.Add("zblizka", "blokace/odraz zblizka");
            prop.Add("Peacemaking", "peace");
            prop.Add("Taming", "taming");
            prop.Add("Resist", "resist");
            prop.Add("zviratum", "zvirata/pavouci");
            prop.Add("SpiritSpeak", "SS");
            prop.Add("Healing", "healing");
            prop.Add("Fishing", "fish");
            prop.Add("Hiding", "hid");
            prop.Add("zivotu za", "hp/10");
            prop.Add("vysatych zivotu", "vysati hp");
            prop.Add("Enticement", "entic");
            prop.Add("Tactics", "tactics");
            prop.Add("Magery", "magery");
            prop.Add("staminy za", "stam/10");
            prop.Add("zdalky", "blokace/odraz zdalky");
            prop.Add("negativniho", "blokace/odraz kouzla");
            prop.Add("many za", "mana/10");
            prop.Add("golemum", "oci/golemove");
            prop.Add(" humanoidnim ", "humaci");
            prop.Add("Lumberjacking", "lumber");
            prop.Add("demonum", "nemrtvi/demoni");
            prop.Add("drakum", "draci");
            prop.Add("bojovym", "ke vsem boj skillum");
            prop.Add("paralyzovani", "para");
            prop.Add("vysate many", "vysati many");








            Dictionary<ushort, popis> typ = new Dictionary<ushort, popis>();
            typ.Add(0x13F9, new popis("staff", "zbran"));
            typ.Add(0x13F8, new popis("staff", "zbran"));
            typ.Add(0x13FF, new popis(" Katana", "zbran"));
            typ.Add(0x13FE, new popis(" Katana", "zbran"));
            typ.Add(0x143F, new popis(" Hala", "zbran"));
            typ.Add(0x143E, new popis(" Hala", "zbran"));
            typ.Add(0x13FB, new popis(" LBA", "zbran"));
            typ.Add(0x13FA, new popis(" LBA", "zbran"));
            typ.Add(0x1438, new popis(" Kladivo", "zbran"));
            typ.Add(0x1439, new popis(" Kladivo", "zbran"));
            typ.Add(0x13BA, new popis(" Vik", "zbran"));
            typ.Add(0x13B9, new popis(" Vik", "zbran"));
            typ.Add(0x13B1, new popis(" Luk", "zbran"));
            typ.Add(0x13B2, new popis(" Luk", "zbran"));
            typ.Add(0x13B6, new popis(" Scimitar", "zbran"));
            typ.Add(0x13B5, new popis(" Scimitar", "zbran"));
            typ.Add(0x1401, new popis(" Kryss", "zbran"));
            typ.Add(0x1400, new popis(" Kryss", "zbran"));
            typ.Add(0x1416, new popis("Plate", "hrud"));
            typ.Add(0x1415, new popis("Plate", "hrud"));
            typ.Add(0x1B77, new popis("Plate", "stit"));
            typ.Add(0x1B76, new popis("Plate", "stit"));
            typ.Add(0x1411, new popis("Plate", "nohy"));
            typ.Add(0x141A, new popis("Plate", "nohy"));
            typ.Add(0x1410, new popis("Plate", "rukavy"));
            typ.Add(0x1417, new popis("Plate", "rukavy"));
            typ.Add(0x1419, new popis("Plate", "helma"));
            typ.Add(0x1412, new popis("Plate", "helma"));
            typ.Add(0x140D, new popis("Plate", "helma"));
            typ.Add(0x140C, new popis("Plate", "helma"));
            typ.Add(0x140F, new popis("Plate", "helma"));
            typ.Add(0x140E, new popis("Plate", "helma"));
            typ.Add(0x1414, new popis("Plate", "rukavice"));
            typ.Add(0x1418, new popis("Plate", "rukavice"));
            typ.Add(0x1413, new popis("Plate", "gorget"));
            typ.Add(0x13BF, new popis("Chain", "hrud"));
            typ.Add(0x1B7B, new popis("Chain", "stit"));
            typ.Add(0x13BE, new popis("Chain", "nohy"));
            typ.Add(0x13CD, new popis("Chain", "rukavy"));
            typ.Add(0x13BB, new popis("Chain", "helma"));
            typ.Add(0x13C6, new popis("Chain", "rukavice"));
            typ.Add(0x1454, new popis("Bone", "hrud"));
            typ.Add(0x144F, new popis("Bone", "hrud"));
            typ.Add(0x1452, new popis("Bone", "nohy"));
            typ.Add(0x1457, new popis("Bone", "nohy"));
            typ.Add(0x1456, new popis("Bone", "helma"));
            typ.Add(0x1451, new popis("Bone", "helma"));
            typ.Add(0x1453, new popis("Bone", "rukavy"));
            typ.Add(0x144E, new popis("Bone", "rukavy"));
            typ.Add(0x1450, new popis("Bone", "rukavice"));
            typ.Add(0x1455, new popis("Bone", "rukavice"));
            typ.Add(0x13EC, new popis("Ring", "hrud"));
            typ.Add(0x13ED, new popis("Ring", "hrud"));
            typ.Add(0x13F0, new popis("Ring", "nohy"));
            typ.Add(0x13F1, new popis("Ring", "nohy"));
            typ.Add(0x13EE, new popis("Ring", "rukavy"));
            typ.Add(0x13EF, new popis("Ring", "rukavy"));
            typ.Add(0x13EB, new popis("Ring", "rukavice"));
            typ.Add(0x13F2, new popis("Ring", "rukavice"));
            typ.Add(0x140A, new popis("Ring", "helma"));
            typ.Add(0x140B, new popis("Ring", "helma"));
            typ.Add(0x13C7, new popis("Ring/Chain", "gorget"));
            typ.Add(0x13E2, new popis("Studdent", "hrud"));
            typ.Add(0x13DB, new popis("Studdent", "hrud"));
            typ.Add(0x13E1, new popis("Studdent", "nohy"));
            typ.Add(0x13DA, new popis("Studdent", "nohy"));
            typ.Add(0x13D4, new popis("Studdent", "ramena"));
            typ.Add(0x13DC, new popis("Studdent", "ramena"));
            typ.Add(0x13DD, new popis("Studdent", "rukavice"));
            typ.Add(0x13D5, new popis("Studdent", "rukavice"));
            typ.Add(0x1DBA, new popis("Studdent", "helma"));
            typ.Add(0x1DB9, new popis("Studdent", "helma"));
            typ.Add(0x13D6, new popis("Studdent", "gorget"));



            UOItem bagl = new UOItem(UIManager.TargetObject());
            string mat = "";
            string cast = "";
            string typp = "";
            string vl1 = "";
            string vl2 = "";
            string dur = "";
            string sort1 = "";
            string sort2 = "";

            //Notepad.WriteLine("Nazev|Material|Cast|Typ|Vlastnost 1|Vlastnost 2|tag1|tag2|Durabilita|Serial");

            foreach (UOItem it in bagl.AllItems)
            {

                if (materials.ContainsKey(it.Color))
                    mat = materials[it.Color];
                else mat = "Obleceni/Sperk";
                if (typ.ContainsKey(it.Graphic))
                {
                    typp = typ[it.Graphic].getTyp();
                    cast = typ[it.Graphic].getCast();
                }
                Core.RegisterServerMessageCallback(0x1C, onItemClick);
                it.Click();
                UO.Wait(200);
                Core.UnregisterServerMessageCallback(0x1C, onItemClick);

                /* UO.Print(temp[0]);//nazev
                 UO.Print(temp[1]);//durabilita
                     UO.Print(temp[2]);//vlastnost 1
                     UO.Print(temp[3]);//vlastnost 2
                  
                  if(temp.Count<15)
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        Notepad.WriteLine(temp[i]);
                    }
                    temp.Clear();
                }*/
                if (temp.Count == 1)
                {
                    continue;
                }
                if (temp.Count == 2)
                {
                    vl1 = temp[1];
                    if (mat == "Obleceni/Sperk")
                    {
                        vl1 = temp[1];
                        sort1 = "Ostatni";

                    }
                }
                if (temp.Count == 3)
                {
                    vl1 = temp[2];
                    dur = temp[1];
                    sort1 = "Ostatni";

                    if (mat == "Obleceni/Sperk")
                    {
                        vl1 = temp[1];
                        vl2 = temp[2];
                        sort2 = "Ostatni";
                        dur = "";
                    }
                }
                if (temp.Count == 4)
                {
                    vl1 = temp[2];
                    vl2 = temp[3];
                    sort1 = "Ostatni";
                    sort2 = "Ostatni";
                    dur = temp[1];
                }
                foreach (string st in prop.Keys)
                {
                    if (vl1.Contains(st)) sort1 = prop[st];
                    if (vl2.Contains(st)) sort2 = prop[st];
                }
                Notepad.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", temp[0], mat, cast, typp, vl1, vl2, sort1, sort2, dur, it.Serial.ToString());

                temp.Clear();
                mat = "";
                cast = "";
                typp = "";
                vl1 = "";
                vl2 = "";
                sort1 = "";
                sort2 = "";
                //Notepad.WriteLine("");

            }
        }
        CallbackResult onItemClick(byte[] data, CallbackResult prevResult)
        {
            AsciiSpeech packet = new AsciiSpeech(data);
            //Notepad.Write(packet.Text);
            temp.Add(packet.Text);
            return CallbackResult.Normal;
        }


    }
}