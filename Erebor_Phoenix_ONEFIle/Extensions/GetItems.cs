using Phoenix;
using Phoenix.Communication.Packets;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin.Extensions
{
    public class GetItems
    {

        public UOItem Box { get; set; }

        static  Dictionary<Graphic, string> typ;
        static  Dictionary<ushort, string> materials;
        static  List<Graphic> boxes;
        public  List<Item> Items;
        public GetItems()
        {
            typ = new Dictionary<Graphic, string>();
            typ.Add(0x13F9, "staff,  zbran");
            typ.Add(0x13F8, "staff,  zbran");
            typ.Add(0x13FF, " Katana,  zbran");
            typ.Add(0x13FE, " Katana,  zbran");
            typ.Add(0x143F, " Hala,  zbran");
            typ.Add(0x143E, " Hala,  zbran");
            typ.Add(0x13FB, " LBA,  zbran");
            typ.Add(0x13FA, " LBA,  zbran");
            typ.Add(0x1438, " Kladivo,  zbran");
            typ.Add(0x1439, " Kladivo,  zbran");
            typ.Add(0x13BA, " Vik,  zbran");
            typ.Add(0x13B9, " Vik,  zbran");
            typ.Add(0x13B1, " Luk,  zbran");
            typ.Add(0x13B2, " Luk,  zbran");
            typ.Add(0x13B6, " Scimitar,  zbran");
            typ.Add(0x13B5, " Scimitar,  zbran");
            typ.Add(0x1401, " Kryss,  zbran");
            typ.Add(0x1400, " Kryss,  zbran");
            typ.Add(0x1416, "Plate,  hrud");
            typ.Add(0x1415, "Plate,  hrud");
            typ.Add(0x1B77, "Plate,  stit");
            typ.Add(0x1B76, "Plate,  stit");
            typ.Add(0x1411, "Plate,  nohy");
            typ.Add(0x141A, "Plate,  nohy");
            typ.Add(0x1410, "Plate,  rukavy");
            typ.Add(0x1417, "Plate,  rukavy");
            typ.Add(0x1419, "Plate,  helma");
            typ.Add(0x1412, "Plate,  helma");
            typ.Add(0x140D, "Plate,  helma");
            typ.Add(0x140C, "Plate,  helma");
            typ.Add(0x140F, "Plate,  helma");
            typ.Add(0x140E, "Plate,  helma");
            typ.Add(0x1414, "Plate,  rukavice");
            typ.Add(0x1418, "Plate,  rukavice");
            typ.Add(0x1413, "Plate,  gorget");
            typ.Add(0x13BF, "Chain,  hrud");
            typ.Add(0x1B7B, "Chain,  stit");
            typ.Add(0x13BE, "Chain,  nohy");
            typ.Add(0x13CD, "Chain,  rukavy");
            typ.Add(0x13BB, "Chain,  helma");
            typ.Add(0x13C6, "Chain,  rukavice");
            typ.Add(0x1454, "Bone,  hrud");
            typ.Add(0x144F, "Bone,  hrud");
            typ.Add(0x1452, "Bone,  nohy");
            typ.Add(0x1457, "Bone,  nohy");
            typ.Add(0x1456, "Bone,  helma");
            typ.Add(0x1451, "Bone,  helma");
            typ.Add(0x1453, "Bone,  rukavy");
            typ.Add(0x144E, "Bone,  rukavy");
            typ.Add(0x1450, "Bone,  rukavice");
            typ.Add(0x1455, "Bone,  rukavice");
            typ.Add(0x13EC, "Ring,  hrud");
            typ.Add(0x13ED, "Ring,  hrud");
            typ.Add(0x13F0, "Ring,  nohy");
            typ.Add(0x13F1, "Ring,  nohy");
            typ.Add(0x13EE, "Ring,  rukavy");
            typ.Add(0x13EF, "Ring,  rukavy");
            typ.Add(0x13EB, "Ring,  rukavice");
            typ.Add(0x13F2, "Ring,  rukavice");
            typ.Add(0x140A, "Ring,  helma");
            typ.Add(0x140B, "Ring,  helma");
            typ.Add(0x13C7, "Ring/Chain,  gorget");
            typ.Add(0x13E2, "Studdent,  hrud");
            typ.Add(0x13DB, "Studdent,  hrud");
            typ.Add(0x13E1, "Studdent,  nohy");
            typ.Add(0x13DA, "Studdent,  nohy");
            typ.Add(0x13D4, "Studdent,  ramena");
            typ.Add(0x13DC, "Studdent,  ramena");
            typ.Add(0x13DD, "Studdent,  rukavice");
            typ.Add(0x13D5, "Studdent,  rukavice");
            typ.Add(0x1DBA, "Studdent,  helma");
            typ.Add(0x1DB9, "Studdent,  helma");
            typ.Add(0x13D6, "Studdent,  gorget");

            typ.Add(0x1515, "Plast");
            typ.Add(0x1530, "Plast");
            typ.Add(0x1F04, "Roba");
            typ.Add(0x1F03, "Roba");
            typ.Add(0x1087, "Nausnice");
            typ.Add(0x1F07, "Nausnice");
            typ.Add(0x1086, "Bracelet");
            typ.Add(0x1F06, "Bracelet");
            typ.Add(0x108A, "Prsten");
            typ.Add(0x1F09, "Prsten");
            typ.Add(0x1F08, "Necklace");
            typ.Add(0x1088, "Necklace");
            typ.Add(0x1718, "Klobouk");
            typ.Add(0x1713, "Klobouk");
            typ.Add(0x1543, "Klobouk");
            typ.Add(0x171A, "Klobouk");
            typ.Add(0x1542, "Serpa");
            typ.Add(0x1541, "Serpa");

            materials = new Dictionary<ushort, string>();
            materials.Add(0x099A, "Copper");
            materials.Add(0x0763, "Iron");
            materials.Add(0x097F, "Verite");
            materials.Add(0x0985, "Valorite");
            materials.Add(0x0989, "Obsidian");
            materials.Add(0x0999, "Adamantium");
            Items = new List<Item>();

            boxes = new List<Graphic>();
            boxes.Add(new Graphic(0x09A8));
            boxes.Add(new Graphic(0x09A9));
            boxes.Add(new Graphic(0x09AA));
            boxes.Add(new Graphic(0x09B0));
            boxes.Add(new Graphic(0x0E42));
            boxes.Add(new Graphic(0x0E43));
            boxes.Add(new Graphic(0x0E75));
            boxes.Add(new Graphic(0x0E76));
            boxes.Add(new Graphic(0x0E79));
            boxes.Add(new Graphic(0x0E7D));
            boxes.Add(new Graphic(0x0E7E));
            boxes.Add(new Graphic(0x0E80));


        }

        public void GetItem(int index)
        {
           // UO.MoveItem()
        }
        /* public void fillTreeView(TreeView t)
         {
             try
             {
                 t.Nodes.Clear();
                 foreach (Item r in Items)
                 {
                     string[] tmp = r.path_name.ToArray();
                     UOItem[] tmp2 = r.path.ToArray();
                     string[] itemInfo = r.GetInfo();
                     string name = itemInfo[0] + ", " + itemInfo[1] + ", " + itemInfo[2] + ", " + itemInfo[3];
                     switch (tmp.Length)
                     {
                         case 1:
                             t.Nodes.Add(new TreeNode(name) { Name = name, Tag = r.item.Serial });
                             break;
                         case 2:
                             if (t.Nodes[tmp[1]] == null)
                             {
                                 t.Nodes.Add(tmp[1],tmp[1]).Tag=tmp2[1].Serial.ToString();
                             }
                             if(t.Nodes[tmp[1]].Tag.ToString() != tmp2[1].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             t.Nodes[tmp[1]].Nodes.Add(new TreeNode(name) { Name = name, Tag = r.item.Serial });
                             break;
                         case 3:
                             if (t.Nodes[tmp[1]] == null)
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[1]].Tag.ToString() != tmp2[1].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]] == null)
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]].Tag.ToString() != tmp2[2].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             t.Nodes[tmp[1]].Nodes[tmp[2]].Nodes.Add(new TreeNode() { Name = name, Tag = r.item.Serial });
                             break;
                         case 4:
                             if (t.Nodes[tmp[1]] == null)
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[1]].Tag.ToString() != tmp2[1].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]] == null)
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]].Tag.ToString() != tmp2[2].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             if (t.Nodes[tmp[3]] == null)
                             {
                                 t.Nodes.Add(tmp[3], tmp[3]).Tag = tmp2[3].Serial.ToString();
                             }
                             if (t.Nodes[tmp[3]].Tag.ToString() != tmp2[3].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[3], tmp[3]).Tag = tmp2[3].Serial.ToString();
                             }
                             t.Nodes[tmp[1]].Nodes[tmp[2]].Nodes[tmp[3]].Nodes.Add(new TreeNode(name) { Name = name, Tag = r.item.Serial });
                             break;
                         case 5:
                             if (t.Nodes[tmp[1]] == null)
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[1]].Tag.ToString() != tmp2[1].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[1], tmp[1]).Tag = tmp2[1].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]] == null)
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             if (t.Nodes[tmp[2]].Tag.ToString() != tmp2[2].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[2], tmp[2]).Tag = tmp2[2].Serial.ToString();
                             }
                             if (t.Nodes[tmp[3]] == null)
                             {
                                 t.Nodes.Add(tmp[3], tmp[3]).Tag = tmp2[3].Serial.ToString();
                             }
                             if (t.Nodes[tmp[3]].Tag.ToString() != tmp2[3].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[3], tmp[3]).Tag = tmp2[3].Serial.ToString();
                             }
                             if (t.Nodes[tmp[4]] == null)
                             {
                                 t.Nodes.Add(tmp[4], tmp[4]).Tag = tmp2[4].Serial.ToString();
                             }
                             if (t.Nodes[tmp[4]].Tag.ToString() != tmp2[4].Serial.ToString())
                             {
                                 t.Nodes.Add(tmp[4], tmp[4]).Tag = tmp2[4].Serial.ToString();
                             }
                             t.Nodes[tmp[1]].Nodes[tmp[2]].Nodes[tmp[3]].Nodes[tmp[4]].Nodes.Add(new TreeNode(name) { Name = name, Tag = r.item.Serial });
                             break;
                     }
                 }
             }

             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }*/

        public void fillListView(ListView l)
        {//material,typ,cast,vl1,vl2
            l.Items.Clear();
            l.Columns.Clear();
            l.View = View.Details;
            l.ShowGroups = true;
            
            l.Columns.Add(new ColumnHeader() { Text = "Material", Width = 80 });
            l.Columns.Add(new ColumnHeader() { Text = "Type", Width = 100 });
            l.Columns.Add(new ColumnHeader() { Text = "Attr1", Width = 180 });
            l.Columns.Add(new ColumnHeader() { Text = "Attr2", Width = 180 });
            l.Columns.Add(new ColumnHeader() { Text = "Serial", Width = 100 });
            string[] tmp;
            var gouped = Items.GroupBy(item => item.path_name.ToArray().Last()).Select(grp => grp.ToList()).ToList();
            foreach( var i in gouped)
            {

                List<Item> tm = i;
                ListViewGroup lg = new ListViewGroup(tm[0].path_name.Last());
                l.Groups.Add(lg);

                foreach (Item it in i)
                {
                    tmp = it.GetInfo();
                    ListViewItem li = new ListViewItem(tmp) { Group=lg};
                    l.Items.Add(li);

                }

            }

        }

        public void Add()
        {
            Items.Clear();
            Box = new UOItem(UIManager.TargetObject());
            Box.Click();
            UO.Wait(100);
            List<UOItem> tmp;
            tmp = Box.AllItems.Where(x => boxes.Any(y => x.Graphic == y)).ToList();
            int cnt;
            do
            {
                cnt = tmp.Count();
                foreach (UOItem i in tmp)
                {
                    i.Use();
                }
                tmp = Box.AllItems.Where(x => boxes.Any(y => x.Graphic == y)).ToList();
            }
            while (tmp.Count>cnt);
            try
            {
                foreach (UOItem i in Box.AllItems.Where(x => boxes.All(y =>y!=x.Graphic)))//projde itemy
                {
                    Items.Add(new Item(i, Box));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /*string[] s;
            foreach(Item it in Items)
            {
                s = it.GetInfo();
                Notepad.WriteLine("{0},{1},{2},{3}",s[0], s[1], s[2], s[3]);
                foreach (string sd in it.path_name.ToList())
                    Notepad.WriteLine(sd);
                Notepad.WriteLine();
            }*/
        }


        public class Item
        {
            public UOItem item { get;private set; }
            public Stack<UOItem> path;
            public Stack<string> path_name;
            public 
            List<string> temp;
            

            public Item(UOItem item,UOItem box)
            {
                try
                {
                    UOItem cont;
                    temp = new List<string>();
                    path = new Stack<UOItem>();
                    path_name = new Stack<string>();
                    this.item = item;
                    cont = new UOItem(item.Container);
                    cont.Click();
                    UO.Wait(100);
                    if (cont.Serial == box.Serial)
                    {
                        path.Push(cont);
                        path_name.Push(cont.Name);
                    }
                    else
                    {
                        do
                        {
                            path.Push(cont);
                            path_name.Push(cont.Name);
                            cont = new UOItem(cont.Container);
                            cont.Click();
                            UO.Wait(100);
                        }
                        while (cont.Serial != box.Serial);
                        path.Push(cont);
                        path_name.Push(cont.Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public string[] GetInfo()
            {
                string prop1 = "", prop2 = "", prop3 = "", prop4 = ""; //material,typ,cast,vl1,vl2
                temp.Clear();
                if (materials.ContainsKey(item.Color))
                    prop1 = materials[item.Color];
                else prop1 = "UnKnown";
                if (typ.ContainsKey(item.Graphic))
                {
                    prop2 = typ[item.Graphic];
                }
                else prop2 = "UnKnown";
                Core.RegisterServerMessageCallback(0x1C, onItemClick);
                item.Click();
                UO.Wait(100);
                Core.UnregisterServerMessageCallback(0x1C, onItemClick);

                
                switch(temp.Count)
                {
                    case 1:
                        break;
                    case 2:
                        prop3 = temp[1];
                        break;
                    case 3:
                        prop3 = temp[1].Contains("Durabilita:")?"": temp[1];
                        prop4 = temp[2];
                        break;
                    case 4:
                        prop3 = temp[2];
                        prop4 = temp[3];
                        break;
                }
                return new string[] { prop1, prop2, prop3, prop4, item.Serial.ToString()};
            }


            CallbackResult onItemClick(byte[] data, CallbackResult prevResult)
            {
                AsciiSpeech packet = new AsciiSpeech(data);
                temp.Add(packet.Text);
                return CallbackResult.Normal;
            }
        }
    }
}
