using Phoenix;
using Phoenix.Communication;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Phoenix.EreborPlugin.Extensions
{
    [RuntimeObject]
    public class RangerCircle
    {
        List<Item> circleItems;
        uint id = 1111111110;
        const int dist = 12;
         int[] coords={-12,3,-12,-1,-11,5,-10,7,-9,8,-8,9,-6,-11,-4,-12,-2,-12,0,-12,2,-12,4,-12,6,-11,8,-10,9,-9,10,-8,11,-6,12,4,12,0,12,-4
 };
        Timer t;
        public RangerCircle()
        {
            circleItems = new List<Item>();
        }

        public void MakeCircle()
        {
            Item tmp;
           /* for(int x=-dist;x<= dist; x++)
            {
                for(int y = -dist; y <= dist; y++)
                {
                    tmp = new Item((int)(id + circleItems.Count), (ushort)(World.Player.X + x), (ushort)(World.Player.Y + y));
                    if (tmp.Distance == dist)
                    {
                        circleItems.Add(tmp);
                    }
                    else tmp.Dispose();
                }
            }*/
            for(int k=0;k<coords.Length;k=k+2)
            {
                tmp = new Item((int)(id + circleItems.Count), (ushort)(World.Player.X + coords[k]), (ushort)(World.Player.Y + coords[k+1]));
                circleItems.Add(tmp);

            }
            foreach(Item it in circleItems)
            {
                if (it.Distance == dist)
                {
                    it.MakeItem();
                    //Notepad.WriteLine(it.X + ";" + it.Y);
                }
                else it.Dispose();
            }
        }
        [Command]
        public void circl()
        {
            t = new Timer(200);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (World.Player.Warmode)
            {
                MakeCircle();
            }
            else
            {
                foreach(Item it in circleItems)
                {
                    it.Dispose();
                }
                circleItems.Clear();
            }
           // t.Stop();
        }

        public class Item : IDisposable
        {
            int Serial { get; set; }
            public int Distance
            {
                get
                {
                    return (int)Math.Abs(Math.Sqrt(Math.Pow(World.Player.X - X, 2) + Math.Pow(World.Player.Y - Y, 2)));
                }
            }

            public ushort X { get; private set; }
            public ushort Y { get; private set; }

            public Item(int serial, ushort x, ushort y)
            {
                Serial = serial;
                X = x;
                Y = y;

            }
            public void MakeItem()
            {
                PacketWriter pw = new PacketWriter(0x78);
                pw.Write((ushort)23);
                pw.Write((int)Serial);
                pw.Write((ushort)6886);
                pw.Write((ushort)X);
                pw.Write((ushort)Y);
                pw.Write((byte)0);
                pw.Write((byte)1);
                pw.Write((ushort)0);
                pw.Write((byte)8);
                pw.Write((byte)7);
                pw.Write((int)0);


                Core.SendToClient(pw.GetBytes());
            }

            public void Dispose()
            {
                UO.Hide((Serial)Serial);
            }
        }

    } 
}
