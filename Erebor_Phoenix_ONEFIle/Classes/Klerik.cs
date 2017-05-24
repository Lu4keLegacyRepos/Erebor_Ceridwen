using Phoenix.WorldData;

namespace Phoenix.EreborPlugin.Classes
{
    public class Klerik : IUOClass
    {
        public Klerik(uint critHP, string healComm, uint hidDel,uint minHP2Bandage,ushort bandageType)
        {
            criticalHits = critHP;
            CrystalCmd = ".enlightment";
            HealCmd = healComm;
            hidDelay = hidDel;
            minHP = minHP2Bandage;
            lotbag = World.Player.Backpack;
        }
        public bool arrowSelfProgress
        {
            get; set;
        }

        public bool musicProgress
        {
            get; set;
        }

        public bool BandageDone
        {
            get; set;
        }
        public uint criticalHits
        {
            get;set;

        }

        public string CrystalCmd
        {
            get; set;

        }
        public UOItem lotbag { get; set; }
        public UOItem carvTool { get; set; }

        public bool crystalState
        {
            get; set;

        }

        public string HealCmd
        {
            get;

        }

        public uint hidDelay
         {
                get; set;

            
        }

        public uint minHP
        {
        
                get; set;

            
        }


    }
}
