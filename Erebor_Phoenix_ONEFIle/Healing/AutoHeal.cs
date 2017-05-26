using Phoenix.EreborPlugin.Classes;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Phoenix.EreborPlugin.Healing
{
    public delegate void bandage();
    public class AutoHeal
    {
        private Timer checker;
        public DateTime startBandage;
        public event EventHandler<HurtedPatient> PatientHurted;
        private bool onOff;
        public bool OnOff
        {
            get { return onOff; }
            set
            {
                if (value)
                {
                    ch.BandageDone = true;
                    checker.Start();
                    GetStatuses();
                    UO.PrintInformation("Heal ON");
                }
                else
                {
                    checker.Stop();
                    UO.PrintInformation("Heal OFF");
                }
                onOff = value;
            }
        }
        IUOClass ch;

        public List<Patient> HealedPlayers { get; set; }

        public AutoHeal(IUOClass ch)
        {
            HealedPlayers = new List<Patient>();
            this.ch = ch;
            checker = new Timer(300);
            checker.Elapsed += Checker_Elapsed;

        }

        public void GetStatuses()
        {
            Serial tmp=Aliases.GetObject("laststatus");
            foreach(Patient p in HealedPlayers)
            {
                p.chara.RequestStatus(100);
            }
            Aliases.SetObject("laststatus",tmp);
        }
        private void Checker_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<Patient> tmp = HealedPlayers.Where(pat => pat.chara.Distance < 7 && pat.chara.Hits > 0 && pat.chara.Hits < 87).ToList();
            if (tmp.Count == 0)
            {
                PatientHurted?.Invoke(this, new HurtedPatient() { crystalOff = true });
                return;
            }
            tmp.Sort((x, y) => (x.chara.Hits.CompareTo(y.chara.Hits)));
            if (ch.arrowSelfProgress || World.Player.Hidden) return;
            if (World.Player.Hits < ch.minHP)
                PatientHurted?.Invoke(this, new HurtedPatient() { selfHurted = true });
            if (tmp[0].chara.Hits > 60 && ch.musicProgress) return;
            PatientHurted?.Invoke(this, new HurtedPatient() { pati = tmp[0] });
        }

        public void bandage()
        {
            if (startBandage == null) startBandage = DateTime.Now;
            if ((DateTime.Now - startBandage) > TimeSpan.FromSeconds(7)) ch.BandageDone = true;
            if (!ch.BandageDone ) return;
            startBandage = DateTime.Now;
            ch.BandageDone = false;
            if(ch is Shaman)UO.Say(".samheal15");
            else UO.Say(".heal15");

        }

        public void bandage(Patient p)
        {
            if (startBandage == null) startBandage = DateTime.Now;
            if ((DateTime.Now - startBandage) > TimeSpan.FromSeconds(7)) ch.BandageDone = true;
            if (!ch.BandageDone) return;
            startBandage = DateTime.Now;
            ch.BandageDone = false;
            if (!ch.crystalState) UO.Say(ch.CrystalCmd);
            UO.Say(ch.HealCmd + p.equip);
            UO.Wait(100);
            if (ch is Shaman && ch.crystalState) UO.Say(ch.CrystalCmd);
        }



        public void Add()
        {
            UO.PrintInformation("Zamer hrace");
            UOCharacter tmp = new UOCharacter(UIManager.TargetObject());
            if (HealedPlayers.Any(ch => ch.chara.Serial == tmp.Serial))
            {
                UO.PrintError("Uz je v seznamu");
                return;
            }
            if (HealedPlayers.Count >= 14)
            {
                UO.PrintError("Plny seznam");
                return;
            }
            UO.WaitTargetObject(tmp);
            UO.Say(".setequip" + HealedPlayers.Count);
            HealedPlayers.Add(new Patient(tmp, HealedPlayers.Count));

        }
        public void Add(string Code)
        {
            if (Code == "NULL") return;
            string[] tmp = Code.Split('_');
            foreach (string s in tmp)
            {
                HealedPlayers.Add(new Patient(s));
            }
        }
        public void fillListBox(System.Windows.Forms.ListBox lb)
        {
            lb.Items.Clear();
            foreach (Patient e in HealedPlayers)
            {
                lb.Items.Add(e.chara.Name == null ? e.chara.ToString() : e.chara.Name.Length < 2 ? e.chara.ToString() : e.chara.Name + ", equip: " + e.equip.ToString());
            }

        }
        public override string ToString()
        {
            string tmp = "";
            if (HealedPlayers.Count < 1) return "NULL";
            foreach (Patient e in HealedPlayers)
            {
                tmp += e.ToString() + "_";
            }
            tmp = tmp.Remove(tmp.Length - 1);
            return tmp;
        }

        public void Remove(int index)
        {
            if (index >= 0 && index >= HealedPlayers.Count) return;
            HealedPlayers.RemoveAt(index);

        }
    }
}
