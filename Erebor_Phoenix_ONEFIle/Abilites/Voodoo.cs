using Phoenix;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;

namespace Phoenix.EreborPlugin.Abilites
{
    public class Voodoo
    {
        Dictionary<string, UOItem> Heads;
        UOItem bagl;
        string[] boostCalls = {"str","dex","int" };
        Dictionary<string, UOItem> boostBottles;
        QueueEx que;
        private bool boosting;
        private bool vodo;
        DateTime timee;

        public Voodoo()
        {
            boosting = false;
            Heads = new Dictionary<string, UOItem>();
            boostBottles = new Dictionary<string, UOItem>();
            que = new QueueEx();
            
        }

        public void Sacrafire(Action bandage)
        {
            if (World.Player.Mana == World.Player.MaxMana)
            {
                UO.PrintInformation("Mas plnou manu!!");
                if (World.Player.Hits < World.Player.MaxHits) bandage();
                return;
            }
            if (World.Player.Hits > 70)
            {
                if (World.Player.Hits < World.Player.MaxHits)
                {
                    bandage();
                    UO.Say(".voodooobet");
                }
                else
                {
                    UO.Say(".voodooobet");
                    UO.Wait(100);
                    bandage();
                }
                UO.Wait(100);
            }
            else UO.PrintWarning("malo HP!!");
        }

        public bool VoDo
        {
            get
            {
                return vodo;


            }
            set
            {
                if (value)
                {
                    getBottles();
                    getHeads();
                    Journal.EntryAdded += Journal_EntryAdded;
                    que.added += Que_added;
                    Journal.ClearAll();
                    timee = DateTime.Now;
                    UO.Print("ON");
                }
                else
                {
                    Journal.EntryAdded -= Journal_EntryAdded;
                    que.added -= Que_added;
                    boostBottles.Clear();
                    Heads.Clear();
                    UO.Print("OFF");
                }
                vodo = value;
            }
        }
        

        

        private void Que_added(object sender, EventArgs e)
        {
            while (boosting) UO.Wait(100);
            boost(que.Deque());
        }

        private void Journal_EntryAdded(object sender, JournalEntryAddedEventArgs e)
        {
           foreach(string s in boostCalls)
            {
                if(e.Entry.Text==s )
                {
                    UO.Print(e.Entry.Name + ":  " + e.Entry.Text);
                    que.Enque(e.Entry.Name + ";" + e.Entry.Text);

                        
                }
            }
            if (DateTime.Now - timee > TimeSpan.FromMinutes(12)) selfFeed();
        }

        private void getBottles()
        {
            UO.PrintInformation("Zamer pytlik s Potiony a Hlavami");
            UOItem head = new UOItem(UIManager.TargetObject());
            bagl = head;
            head.Use();

            boostBottles.Add("str", new UOItem(head.Items.FindType(0x0F0E, 0x0835)));
            boostBottles.Add("dex", new UOItem(head.Items.FindType(0x0F0E, 0x0006)));
            boostBottles.Add("int", new UOItem(head.Items.FindType(0x0F0E, 0x06C2)));
            boostBottles.Add("def", new UOItem(head.Items.FindType(0x0F0E, 0x0999)));
        }
        private void getHeads()
        {
            foreach(UOItem it in bagl.Items)
            {
                if (it.Graphic == 0x0F0E) continue;
                it.Click();
                UO.Wait(200);
                if(!Heads.ContainsKey(it.Name))Heads.Add(it.Name, it);
            }
            
        }

        private void selfFeed()
        {
            boosting = true;
            World.FindDistance = 4;
            World.Ground.FindType(0x097B).Use();
            UO.Wait(100);
            World.Ground.FindType(0x097B).Use();
            UO.Wait(100);
            World.Ground.FindType(0x097B).Use();
            UO.Wait(100);
            World.Ground.FindType(0x097B).Use();
            UO.Wait(100);
            UO.Cast(StandardSpell.NightSight, World.Player);
            UO.Wait(3333);
            timee = DateTime.Now;
            boosting = false;
        }
        private void boost(string[] args)
        {
            boosting = true;
            bagl.Use();
            UO.Wait(100);
            foreach (string it in Heads.Keys)//.Where(x => x.Graphic != 0x0F0E && x.Name==args[0]).ToList())
            {
                if(it==args[0])
                {
                    Heads[it].Move(1, World.Player.Backpack);
                    znovu:
                    UO.Wait(200);
                    boostBottles[args[1]].WaitTarget();
                    Heads[it].Use();
                    if(Journal.WaitForText(false,5000, "Nepovedlo se."))
                    {
                        Journal.SetLineText(Journal.Find("Nepovedlo se."), "lola");
                        UO.Wait(200);
                        goto znovu;
                    }
                    znovu2:
                    UO.Wait(200);
                    boostBottles["def"].WaitTarget();
                    Heads[it].Use();
                    if (Journal.WaitForText(false, 5000, "Nepovedlo se."))
                    {
                        Journal.SetLineText(Journal.Find("Nepovedlo se."), "lola");
                        UO.Wait(200);
                        goto znovu2;
                    }
                    Heads[it].Move(1, bagl);
                    UO.Wait(300);
                    boosting = false;
                    return;
                }
            }
            boosting = false;
        }


    }
}
