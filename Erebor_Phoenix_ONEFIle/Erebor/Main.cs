using Phoenix.Communication;
using Phoenix.Communication.Packets;
using Phoenix.EreborPlugin.Classes;
using Phoenix.EreborPlugin.Extensions;
using Phoenix.EreborPlugin.Healing;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Phoenix.EreborPlugin
{
    [RuntimeObject]
    public class Main
    {
        public static Main instance;
        private string[] crystalOnCalls = new string[] { "Nyni jsi schopen rychleji lecit bandazemi.", "Nyni jsi schopen lecit lepe.", "Nyni ti muze byt navracena spotrebovana magenergie.", "Nyni jsi schopen kouzlit za mene many." };

        private string[] bandageDoneCalls = new string[] { " byl uspesne osetren", "Leceni se ti nepovedlo.", "prestal krvacet", " neni zranen.", "Nevidis na cil.", "Nevidis cil." };
        private List<string> onHitCalls = new List<string>() { "Tvuj cil krvaci.","Skvely zasah!", "Kriticky zasah!", "Vysavas staminu", "Vysavas zivoty!" };
        public IUOClass ActualClass;


        public int exp { get; private set; }
        private DateTime HiddenTime;
        private bool Done;
        private bool hitBandage;
        private bool hitTrack;
        private bool corpseHide;
        private bool doLot;
        private bool printAnim;


        public bool PrintAnim
        {
            get
            {
                return printAnim;
            }
            set
            {
                if (value)
                    Core.RegisterServerMessageCallback(0x6E, onNaprah);
                else
                    Core.UnregisterServerMessageCallback(0x6E, onNaprah);
                printAnim = value;
            }
        }

        public bool CorpseHide
        {
            get
            {
                return corpseHide;
            }
            set
            {
                corpseHide = value;
                Lot.HideCorpses = value;
            }
        }
        public bool DoLot
        {
            get
            {
                return doLot;
            }
            set
            {
                doLot = value;
                Lot.DoLot = value;
            }
        }
        public UOItem lotBagl;
        public ushort GoldLimit;
        public bool HitBandage
        {
            get { return hitBandage; }
            set
            {
                if (value)
                {
                    Core.RegisterServerMessageCallback(0x1C, OnHitBandage);
                }
                else
                {
                    Core.UnregisterServerMessageCallback(0x1C, OnHitBandage);
                }
                hitBandage = value;
            }
        }
        public bool HitTrack
        {
            get { return hitTrack; }
            set
            {
                if (value)
                {
                    Core.RegisterServerMessageCallback(0x1C, OnHitTrack);
                }
                else
                {
                    Core.UnregisterServerMessageCallback(0x1C, OnHitTrack);
                }
                hitTrack = value;
            }
        }
        private bool autoDrink;
        public bool AutoDrink { get { return autoDrink; } set
            {
                autoDrink = value;
                //Erebor.instance.Autodrink(value);
            }
        }



        //Libs
        public GetItems Items;
        public AutoHeal AHeal;
        public EquipSet.EquipSet EqipSet;
        public ClassSelector cs;
        public Events.Events ev;
        public Extensions.ScreenCapture snap;
        public Abilites.AutoMorf Amorf;
        public Abilites.Other OtherAbilites;
        public Autolot.Lot Lot;
        public Magic.Spells Spells;
        public WallCounter wallCnt;
        public Magic.Casting Casting;
        public Skills.Hiding Hiding;
        public Skills.Peacemaking_Enticement Peace_Entic;
        public Skills.Poisoning Poisoning;
        public Skills.Provocation Provo;
        public Skills.Veterinary Vete;
        public Weapons.WeaponsHandle Weapons;
        public SwitchabeHotkeys ExHotKeys;
        public Skills.Tracking Track;
        public GameWindowSize patch;
        public Abilites.Voodoo Voodoo;
        private int CrystallCnt;
        public int GWWidth;
        public int GWHeight;
        private bool SpellFizz;

        public Main()
        {
            try {
                instance = this;
                cs = new ClassSelector();
                Done = false;
                GWWidth = 1000;
                GWHeight = 600;

                Core.LoginComplete += Core_LoginComplete;


            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Core_LoginComplete(object sender, EventArgs e)
        {
            Save_Load.LoadWindowSize();
            patch = new GameWindowSize(GWWidth, GWHeight);
            Core.UnregisterServerMessageCallback(0x1C, onBandageDone);
            Core.UnregisterServerMessageCallback(0x1C, onCrystal);
            Core.UnregisterServerMessageCallback(0x1C, onExp);
            Core.UnregisterServerMessageCallback(0xa1, onHpChanged);
            Core.UnregisterServerMessageCallback(0x11, OnNextTarget);
            UO.Wait(10);
            Core.RegisterServerMessageCallback(0x1C, onBandageDone);
            Core.RegisterServerMessageCallback(0x1C, onCrystal);
            Core.RegisterServerMessageCallback(0x1C, onExp);
            Core.RegisterServerMessageCallback(0xa1, onHpChanged);
            Core.RegisterServerMessageCallback(0x11, OnNextTarget);


            GetClass gc = new GetClass(cs.GetClass);
            gc.BeginInvoke(out ActualClass, out Done, null, null);
            cs.ClassGetted += Cs_ClassGetted;
            Core.LoginComplete -= Core_LoginComplete;
            Core.Disconnected += Core_Disconnected;


        }

        private void Cs_ClassGetted(object sender, EventArgs e)
        {

            ev = new Events.Events();
            ExHotKeys = new SwitchabeHotkeys();
            EqipSet = new EquipSet.EquipSet();
            Hiding = new Skills.Hiding(ActualClass);
            Weapons = new Weapons.WeaponsHandle();
            Amorf = new Abilites.AutoMorf();
            Lot = new Autolot.Lot();
            Track = new Skills.Tracking();
            Spells = new Magic.Spells(ActualClass);
            Casting = new Magic.Casting();
            Peace_Entic = new Skills.Peacemaking_Enticement(ActualClass);
            Poisoning = new Skills.Poisoning();
            Provo = new Skills.Provocation();
            OtherAbilites = new Abilites.Other(Hiding.hidoff, Weapons);
            Vete = new Skills.Veterinary(Weapons);
            Voodoo = new Abilites.Voodoo();
            Items = new GetItems();
            snap = new ScreenCapture();
            wallCnt = new WallCounter();
            ev.hiddenChange += Ev_hiddenChange;
            ev.hitsChanged += Ev_hitsChanged;
            World.Player.Changed += Player_Changed;

            AHeal = new AutoHeal(ActualClass);
            if (ActualClass is Klerik || ActualClass is Shaman) AHeal.PatientHurted += AHeal_PatientHurted;
            ActualClass.BandageDone = true;
            Save_Load.Load(World.Player.Name.ToString() + ".xml");

            UO.Print("Done");
        }

        private void AHeal_PatientHurted(object sender, HurtedPatient e)
        {
            if (e.pati != null)
            {
                AHeal.bandage(e.pati);
            }
            if (e.selfHurted)
            {
                AHeal.bandage();
            }
            if (e.crystalOff)
            {
                CrystallCnt++;
                if (CrystallCnt > 4 && ActualClass.crystalState)
                {
                    CrystallCnt = 0;
                    UO.Say(ActualClass.CrystalCmd);
                    AHeal.GetStatuses();
                }
            }
        }
    

        private void Player_Changed(object sender, ObjectChangedEventArgs e)
        {
            if (World.Player.Hits <= ActualClass.criticalHits && World.Player.Hits >= 1 && !OtherAbilites.potionDelay && AutoDrink) UO.Say(",potion heal");
            string tmp = ActualClass.printState();
            if (tmp != Client.Text)
                Client.Text = tmp;
            if (Lot.LotBag.AllItems.FindType(0x0eed).Amount > GoldLimit && GoldLimit != 0)
            {
                UO.Say(".mesec");
            }
        }

        private void Ev_hitsChanged(object sender, Events.HitsChangedArgs e)
        {
            if (!e.gain) Hiding.getHit = true;
            ushort[] color = new ushort[4];
            color[0] = 0x0026;//red
            color[2] = 0x0175;//green
            color[1] = 0x099;//yellow
            color[3] = 0x0FAB;//fialova - enemy;
            if (e.amount > 3)
            {
                World.Player.Print(e.poison ? color[2] : e.gain ? color[1] : color[0], "[{0} HP]  {1}{2}", World.Player.Hits, e.gain ? "+" : "-", e.amount);
            }
        }

        private void Ev_hiddenChange(object sender, Events.HiddenChangeArgs e)
        {
            if (!e.hiddenState)
            {
                Hiding.hidoff();
                UO.Say(".resync");
                UO.Print("resync");
                UO.Wait(200);
            }
            else HiddenTime = DateTime.Now;
        }

        private void Core_Disconnected(object sender, EventArgs e)
        {
            patch = null;
            Core.UnregisterServerMessageCallback(0x1C, onBandageDone);
            Core.UnregisterServerMessageCallback(0x1C, onCrystal);
            Core.UnregisterServerMessageCallback(0x1C, onExp);
            Core.UnregisterServerMessageCallback(0xa1, onHpChanged);
            Core.UnregisterServerMessageCallback(0x11, OnNextTarget);
            Core.Disconnected -= Core_Disconnected;



            cs.ClassGetted -= Cs_ClassGetted;

            ev = null;
            ExHotKeys = null;
            EqipSet = null;
            Hiding = null;
            Weapons = null;
            Amorf = null;
            Lot = null; 
            Track = null;
            Spells = null;
            Casting = null; 
            Peace_Entic = null;
            Poisoning = null;
            Provo = null;
            OtherAbilites = null;
            Vete = null;
            Voodoo = null;
            Items = null;
            snap = null;
            wallCnt = null;
            ev.hiddenChange -= Ev_hiddenChange;
            ev.hitsChanged -= Ev_hitsChanged;
            World.Player.Changed -= Player_Changed;


            AHeal.PatientHurted -= AHeal_PatientHurted;
            AHeal = null;
            ActualClass.BandageDone = true;

        }

        [Command]
        public void wall(bool energy)
        {
            int t = 10;
            Core.UnregisterServerMessageCallback(0x1C, onSpellFizz);
            Core.RegisterServerMessageCallback(0x1C, onSpellFizz);
            StaticTarget st = UIManager.Target();
            UO.WaitTargetTile(st.X, st.Y, st.Z, st.Graphic);
            if (energy)
            {
                UO.Cast(StandardSpell.EnergyField);
                while(t>0)
                {
                    UO.Wait(500);
                    if(SpellFizz)
                    {
                        SpellFizz = false;
                        Core.UnregisterServerMessageCallback(0x1C, onSpellFizz);
                        return;
                    }
                    t--;
                }
                wallCnt.Add(st.X, st.Y, WallCounter.WallTime.Energy);
            }
            else
            {
                UO.Cast(StandardSpell.WallofStone);
                while (t > 0)
                {
                    UO.Wait(500);
                    if (SpellFizz)
                    {
                        SpellFizz = false;
                        Core.UnregisterServerMessageCallback(0x1C, onSpellFizz);
                        return;
                    }
                    t--;
                }
                wallCnt.Add(st.X, st.Y, WallCounter.WallTime.Stone);
            }
            Core.UnregisterServerMessageCallback(0x1C, onSpellFizz);

        }

        [Command, BlockMultipleExecutions]
        public void kill()
        {
            OtherAbilites.kill();
        }
        [Command, BlockMultipleExecutions]
        public void friend()
        {
            OtherAbilites.frnd();
        }
        [Command]
        public void mesure(string command)
        {
            UO.Say(command);
            DateTime tt = DateTime.Now;
            while(World.Player.Hits==World.Player.MaxHits)
            {
                UO.Wait(50);
            }
            UO.Print((DateTime.Now - tt).TotalMilliseconds);
        }
        [Command]
        public void autoSacrafire()
        {
            if(Casting.autoSacrafire)
            {
                UO.PrintInformation("Auto obet OFF");
                Casting.autoSacrafire = false;
            }
            else
            {
                UO.PrintInformation("Auto obet ON");
                Casting.autoSacrafire = true;
            }
        }
        [Command]
        public void obet()
        {
            Voodoo.Sacrafire(AHeal.bandage);
        }
        [Command]
        public void test()
        {
            Items.Add();
        }
        [Command]
        public void voodoo()
        {
            if (Voodoo.VoDo) Voodoo.VoDo = false;
            else Voodoo.VoDo = true;
        }
        [Command]
        public void switchhotkeys()
        {
            ExHotKeys.swHotk();
        }
        [Command]
        public void reactiv()
        {
            Spells.ReactiveArmor(World.Player);
        }
        [Command]
        public void arrowself(bool b)
        {
            Spells.arrowSelf(b);
        }
        [Command]
        public void potion(string type)
        {
            OtherAbilites.potion(type);
        }
        [Command]
        public void potion(string type, int delay)
        {
            OtherAbilites.potion(type, delay);
        }
        [Command]
        public void attacklast()
        {
            OtherAbilites.attackLast();
        }

        [Command]
        public void war()
        {
            OtherAbilites.war();
        }
        [Command,BlockMultipleExecutions]
        public void probo()
        {
            HitBandage = false;
            OtherAbilites.probo(HiddenTime);
            HitBandage = true;
        }
        [Command,BlockMultipleExecutions]
        public void kudla()
        {
            OtherAbilites.kudla();
        }

        [Command]
        public void Morf(ushort to)
        {
            Amorf.MorfTo(to);
        }
        [Command]
        public void OnOff()
        {
            if (AHeal.OnOff) AHeal.OnOff = false;
            else AHeal.OnOff = true;
        }
        [Command]
        public void bandage()
        {
            AHeal.bandage();
            Weapons.ActualWeapon.Equip();
        }


        [Command,BlockMultipleExecutions]
        public void pois()
        {
            Poisoning.pois();
        }

        [Command]
        public void nhcast(string s, Serial t)
        {
            if (ActualClass.arrowSelfProgress) return;
            bool tmp = false;
            if (AHeal.OnOff)
            {
                AHeal.OnOff = false;
                tmp = true;
            }
            UO.Attack(t);
            Casting.ccast(s, t, AHeal.bandage,obet);
            UO.Wait(Casting.SpellsDelays[s]);
            if (tmp == true && AHeal.OnOff == false) AHeal.OnOff = true;
        }
        [Command]
        public void ccast(string s)
        {
            if (ActualClass.arrowSelfProgress) return;
            Casting.ccast(s, AHeal.bandage,obet);
        }
        [Command]
        public void ccast(string s,Serial t)
        {
            if (ActualClass.arrowSelfProgress) return;
            UO.Attack(t);
            Casting.ccast(s,t, AHeal.bandage,obet);
        }
        [Command]
        public void autocast(bool b)
        {
            Casting.autocast(b,AHeal.bandage,obet);
        }
        [Command]
        public void hidoff()
        {
            Hiding.hidoff();
        }
        [Command,BlockMultipleExecutions]
        public void hid()
        {
            Hiding.hiding(true);

        }
        [Command("exp")]
        public void getExp()
        {
            UO.Print(exp);
        }
        [Command]
        public void inv()
        {
            Spells.inv(Casting.ccast, AHeal.bandage, Casting.SpellsDelays["Invis"]);
        }
        [Command]
        public void save()
        {
            Save_Load.Save(World.Player.Name.ToString() + ".xml");
        }
        [Command("switch")]
        public void swit()
        {
            Weapons.SwitchWeapons();
        }
        [Command,BlockMultipleExecutions]
        public void music(bool b)
        {
            Peace_Entic.music(b);
        }

        [Command]
        public void kuch()
        {
            Lot.Carving();
            Weapons.ActualWeapon.Equip();
        }

        [Command]
        public void track()
        {
            Track.Track();
        }
        [Command]
        public void track(int choice)
        {
            Track.Track(choice);
        }
        [Command]
        public void track(string var)
        {
            Track.Track(var);
        }
        [Command]
        public void track(int choice, bool war)
        {
            Track.Track(choice,war);
        }
        [Command]
        public void add(string name)
        {
            Track.Add(name);
            
        }
        [Command, BlockMultipleExecutions]
        public void autopetheal()
        {
            Vete.autoVet();
        }

        [Command, BlockMultipleExecutions]
        public void petheal()
        {
            Vete.Vet();
        }
        [Command]
        public void harm()
        {
            UO.Attack(Aliases.GetObject("laststatus"));
            UO.Cast(StandardSpell.Harm, Aliases.GetObject("laststatus"));
        }

        CallbackResult onCrystal(byte[] data, CallbackResult prevResult)//0x1C
        {
            AsciiSpeech packet = new AsciiSpeech(data);

            if (packet.Text.Contains("Jsi zpatky v normalnim stavu."))
            {
                ActualClass.crystalState = false;

                return CallbackResult.Normal;
            }
            foreach (string s in crystalOnCalls)
            {
                if (packet.Text.Contains(s))
                {
                    ActualClass.crystalState = true;
                    return CallbackResult.Normal;
                }
            }
            return CallbackResult.Normal;
        }


        CallbackResult onExp(byte[] data, CallbackResult prevResult)
        {
            AsciiSpeech packet = new AsciiSpeech(data);
            if (packet.Text.Contains(" zkusenosti."))
            {

                //string[] numbers = Regex.Split(packet.Text, @"\D+");
                string number = Regex.Match(packet.Text, @"-?\d+").Value;
                if (!string.IsNullOrEmpty(number))
                {
                    exp += Int32.Parse(number);

                }
            }
            return CallbackResult.Normal;
        }

        CallbackResult OnNextTarget(byte[] data, CallbackResult prevResult)
        {
            PacketReader reader = new PacketReader(data);
            if (reader.ReadByte() != 0x11) throw new Exception("Invalid packet passed to OnNextTarget method.");
            ushort blockSize = reader.ReadUInt16();
            uint serial = reader.ReadUInt32();
            if (serial == Aliases.Self)//|| inList.Contains(serial))
            {
                return CallbackResult.Normal;
            }
            Aliases.SetObject("laststatus", serial);
            UOCharacter cil = World.GetCharacter(serial);
            if (cil.MaxHits == -1)
            {
                cil.RequestStatus();
                return CallbackResult.Normal;
            }
            else
            {
                ushort color = 0;
                string not = cil.Notoriety.ToString();
                switch (not)
                {

                    case "Criminal":
                        color = 0x0026;
                        break;

                    case "Enemy":
                        color = 0x0031;
                        break;

                    case "Guild":
                        color = 0x0B50;
                        break;

                    case "Innocent":
                        color = 0x0058;
                        break;

                    case "Murderer":
                        color = 0x0026;
                        break;

                    case "Neutral":
                        color = 0x03BC;
                        break;
                    case "Unknown":
                        color = 0x03BC;
                        break;
                    default:
                        color = Phoenix.Env.DefaultInfoColor;
                        break;
                }
                UO.Print(color, "{0} : {1}/{2} ({3})", cil.Name, cil.Hits, cil.MaxHits, cil.Distance);
                return CallbackResult.Normal;
            }
        }


        CallbackResult OnHitBandage(byte[] data, CallbackResult prevResult)
        {
            AsciiSpeech packet = new AsciiSpeech(data);

            foreach (string s in onHitCalls)
            {
                if (packet.Text.Contains(s) && World.Player.Hits < World.Player.MaxHits - 10)
                {
                    Core.UnregisterServerMessageCallback(0x1C, OnHitBandage);
                    bandage asBanage = new bandage(AHeal.bandage);
                    asBanage.BeginInvoke(null, null);
                    Weapons.Equip eq = new Weapons.Equip(Weapons.ActualWeapon.Equip);
                    eq.BeginInvoke(null, null);
                    UO.Wait(100);
                    Core.RegisterServerMessageCallback(0x1C, OnHitBandage);
                    return CallbackResult.Normal;
                }
            }
            return CallbackResult.Normal;
        }
        CallbackResult OnHitTrack(byte[] data, CallbackResult prevResult)
        {
            AsciiSpeech packet = new AsciiSpeech(data);

            foreach (string s in onHitCalls)
            {
                if (packet.Text.Contains(s))
                {
                    UO.Say(",track 2 true");
                    return CallbackResult.Normal;
                }

            }
            return CallbackResult.Normal;
        }


        CallbackResult onNaprah(byte[] data, CallbackResult prev)
        {

            PacketReader p = new PacketReader(data);
            p.Skip(1);
            uint serial = p.ReadUInt32();
            ushort action = p.ReadUInt16();
            if (action == 26 && serial == World.Player.Serial && new UOCharacter(Aliases.GetObject("laststatus")).Distance < 3)
                UO.Print(SpeechFont.Bold, 0x0076, "Naprah na " + new UOCharacter(Aliases.GetObject("laststatus")).Name);
            //UO.PrintWarning("Naprah na " + new UOCharacter(Aliases.GetObject("laststatus")).Name);
            return CallbackResult.Normal;
        }
        CallbackResult onHpChanged(byte[] data, CallbackResult prevResult)//0xa1
        {
            UOCharacter character = new UOCharacter(Phoenix.ByteConverter.BigEndian.ToUInt32(data, 1));
            if (character.Serial == World.Player.Serial) return CallbackResult.Normal;
            ushort maxHits = 100; // Nejvyssi HITS bez nakouzleni
            ushort hits = Phoenix.ByteConverter.BigEndian.ToUInt16(data, 7);
            ushort[] color = new ushort[4];
            color[0] = 0x0026;//red
            color[2] = 0x0175;//green
            color[1] = 0x099;//yellow
            color[3] = 0x0FAB;//fialova - enemy;
            int col = 0;

            if (character.Hits - hits < -4 || character.Hits - hits > 4)
            {
                if (character.Hits > hits)
                {
                    if (character.Poisoned) col = 2;
                    else col = 0;
                }
                else
                {
                    if (character.Poisoned) col = 2;
                    else col = 1;
                }

                if ((character.Model == 0x0190 || character.Model == 0x0191))
                {
                    character.Print(color[col], "{2} [{0} HP] {1}", ((maxHits / 100) * hits), (hits - character.Hits), character.Name);
                }


                if (character.Serial == Aliases.GetObject("laststatus"))
                    character.Print(color[3], "[{0} HP] {1}", ((maxHits / 100) * hits), (hits - character.Hits));

            }
            return CallbackResult.Normal;
        }

        CallbackResult onBandageDone(byte[] data, CallbackResult prevResult)//0x1C
        {
            AsciiSpeech packet = new AsciiSpeech(data);

            foreach (string s in bandageDoneCalls)
            {
                if (packet.Text.Contains(s))
                {
                    UO.Wait(100);
                    ActualClass.BandageDone = true;
                }
            }
            return CallbackResult.Normal;
        }

        CallbackResult onSpellFizz(byte[] data, CallbackResult prev)
        {
            AsciiSpeech asc = new AsciiSpeech(data);
            if (asc.Text.ToLower().Contains("kouzlo se nezdarilo."))SpellFizz = true;
            return CallbackResult.Normal;
        }
    }
}
