using Phoenix.WorldData;

namespace Phoenix.EreborPlugin.Classes
{
    public class Mage : IUOClass
    {

        public UOItem carvTool
        {
            get; set;
        }

        public uint criticalHits
        {
            get; set;
        }

        public string CrystalCmd
        {
            get; set;
        }

        public bool crystalState
        {
            get; set;
        }

        public string HealCmd
        {
            get; set;
        }

        public uint hidDelay
        {
            get; set;
        }

        public UOItem lotbag
        {
            get; set;
        }

        public uint minHP
        {
            get; set;
        }

        public bool arrowSelfProgress
        {
            get;set;
        }

        public bool musicProgress
        {
            get; set;
        }

        public bool BandageDone
        {
            get; set;
        }

        public Mage(uint critHP, uint hidDel)
        {
            criticalHits = critHP;
            CrystalCmd = ".concetration";
            HealCmd = ".heal";
            hidDelay = hidDel;
        }
    }
}
