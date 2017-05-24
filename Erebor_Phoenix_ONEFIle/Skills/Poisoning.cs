using Phoenix;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phoenix.EreborPlugin.Skills
{
    public class Poisoning
    {
        public UOItem PoisonBottle { get; set; }
        public Poisoning()
        {
            PoisonBottle = null;
        }
        public void pois()
        {
            if (PoisonBottle == null) UO.PrintError("Nemas nastaveny poison");
            UOItem zbran = World.Player.Layers[Layer.RightHand];

            UOItem pois = World.Player.Backpack.Items.FindType(PoisonBottle.Graphic, PoisonBottle.Color);

            zbran.WaitTarget();
            pois.Use();
        }
    }

}
