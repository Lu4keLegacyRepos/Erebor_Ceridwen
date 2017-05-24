using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin
{
    public class RuneTree
    {
        public List<Rune> Runes;
        public static RuneTree instance;
        public RuneTree()
        {
            instance = this;
            Runes = new List<Rune>();
        }


        public void GetRunes()
        {
            UOItem a,aa;
            List<uint> runeTypes = new List<uint>() { 0x1F14, 0x1F15, 0x1F16, 0x1F17 };
            UO.Print("Zamer bednu s runama");
            UOItem runeBox = new UOItem(UIManager.TargetObject());
            runeBox.Click();
            UO.Wait(200);
            runeBox.Use();
            Runes.Clear();
            openBoxes(runeBox, 200);
            foreach (UOItem it in runeBox.AllItems.Where(item=>runeTypes.Any(typ=>item.Graphic==typ)))
            {
                string tmps="";
                Serial[] tmp = { 0x0, 0x0, 0x0 };
                string[] tmp2 = { "null", "null"};

                    it.Click();
                    UO.Wait(100);
                    if (it.Container != runeBox)
                    {
                        tmp[0] = it.Container;
                        a = new UOItem(it.Container);
                        a.Click();
                        UO.Wait(100);
                        tmp2[0] = a.Name;
                        if (a.Container != runeBox)
                        {
                            tmp[1] = a.Container;
                            aa = new UOItem(a.Container);
                            aa.Click();
                            UO.Wait(100);

                            tmp2[1] = aa.Name;
                        }

                    if (tmp2[1] != "null" && tmp2[0] != "null")
                    {
                        tmps = tmp2[0];
                        tmp2[0]= tmp2[1];
                        tmp2[1] = tmps;
                    }

                }
                tmp[2] = runeBox;
                Runes.Add(new Rune(it.Name, it.Serial, tmp, tmp2));
            }

            List<string> runes_String = new List<string>();
            foreach (Rune r in Runes)
            {
                runes_String.Add(r.ToString());
            }
            UO.PrintInformation("Nacteno");
        }

        private void openBoxes(UOItem mainBox, int delay)
        {
            List<uint> containerType = new List<uint>() { 0x09AA, 0x0E7D, 0x0E75, 0x0E79, 0x09B0, 0x0E76 };
            foreach (UOItem it in mainBox.AllItems)
            {
                if (containerType.Contains(it.Graphic))
                {
                    it.Use();
                    UO.Wait(delay);
                    foreach (UOItem it2 in it.AllItems)
                    {
                        if (containerType.Contains(it2.Graphic))
                        {
                            it2.Use();
                            UO.Wait(delay);
                        }
                    }
                }
                UO.Wait(100);
            }
        }

        public void findRune(Rune r)
        {
            if (new UOItem(r.containers[2]).Distance < 5)
            {
                if (r.containers[2] > 0x0) new UOItem(r.containers[2]).Use();
                UO.Wait(100);
                if (r.containers[1] > 0x0) new UOItem(r.containers[1]).Use();
                UO.Wait(100);
                if (r.containers[0] > 0x0) new UOItem(r.containers[0]).Use();
                UO.Wait(100);
            }
            else UO.PrintError("Nedosahnes na bednu");
        }

        public void FillTreeView(TreeView t)
        {
            try
            {
                t.Nodes.Clear();
                foreach (Rune r in Runes)
                {
                    //Notepad.WriteLine(r.ToString());
                    if (r.containersName[0] != "null")
                    {
                        if (t.Nodes[r.containersName[0]] == null)
                            t.Nodes.Add(new TreeNode(r.containersName[0]) { Name = r.containersName[0] });
                        if (r.containersName[1] != "null")
                        {
                            if (t.Nodes[r.containersName[0]].Nodes[r.containersName[1]] == null)
                                t.Nodes[r.containersName[0]].Nodes.Add(new TreeNode(r.containersName[1]) { Name = r.containersName[1] });
                            t.Nodes[r.containersName[0]].Nodes[r.containersName[1]].Nodes.Add(new TreeNode(r.Name) { Name = r.Name, Tag = r.id });
                        }
                        else
                        {
                            t.Nodes[r.containersName[0]].Nodes.Add(new TreeNode(r.Name) { Name = r.Name, Tag = r.id });
                        }
                    }
                    else
                    {
                        t.Nodes.Add(new TreeNode(r.Name) { Name = r.Name, Tag = r.id });
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Add(string Code)
        {
            if (Code == "NULL") return;
            string[] tmp = Code.Split('%');

            foreach (string s in tmp)
            {
                Runes.Add(new Rune(s));
            }

        }
        public override string ToString()
        {
            string tmp = "";
            if (Runes.Count < 1) return "NULL";
            foreach (Rune e in Runes)
            {
                tmp += e.ToString() + "%";
            }
            tmp = tmp.Remove(tmp.Length - 1);
            return tmp;
        }
    }
}
