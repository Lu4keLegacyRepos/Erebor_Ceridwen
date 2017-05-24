using Phoenix.Communication;
using Phoenix.WorldData;

namespace Phoenix.EreborPlugin.Extensions

{
    public class Massmove
    {
        int x;
        UOItem presunovanaVec;
        UOItem zdroj;
        ushort Color;
        int Pause;
        int MaxPocet;
        int typ;
        bool massmoveRunning = false;

        public CallbackResult DropItem(byte[] data, CallbackResult prevResult)
        {
            PacketReader reader = new PacketReader(data);
            byte id = reader.ReadByte();
            presunovanaVec = World.GetItem(reader.ReadUInt32());
            zdroj = World.GetItem(presunovanaVec.Container);
            x = 0;
            presunovanaVec.Changed += new ObjectChangedEventHandler(presunovanaVec_Changed);
            return CallbackResult.Normal;
        }

        void presunovanaVec_Changed(object sender, ObjectChangedEventArgs e)
        {
            if ((presunovanaVec.Container == 0x00000000) && (x == 1))
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                presunovanaVec.Changed -= presunovanaVec_Changed;
            }
            x++;
            if (presunovanaVec.Container != 0x00000000)
            {
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                presouvani(typ);
            }
        }

        [Command("massmove")]
        public void informace()
        {
            UO.PrintInformation(",massmove pause");
            UO.PrintInformation(",massmove pause barva");
            UO.PrintInformation(",massmove pause maxPocet barva");
            UO.PrintInformation(",massmove pause stejnaBarva");
            UO.PrintInformation(",massmove pause maxPocet stejnaBarva");
            UO.PrintInformation("stejnaBarva = true/false, je jedno co pouzijete");
        }

        [Command("massmove")]
        public void presun(int pause)
        {
            if (massmoveRunning == true)
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                UO.Print("Massmove vypnut");
                return;
            }
            UO.Print("Pohni s itemem.");
            Core.RegisterClientMessageCallback(0x07, new MessageCallback(DropItem));
            massmoveRunning = true;
            typ = 1;
            Pause = pause;
        }

        [Command("massmove")]
        public void presun(int pause, ushort color)
        {
            if (massmoveRunning == true)
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                UO.Print("Massmove vypnut");
                return;
            }
            UO.Print("Pohni s itemem.");
            Core.RegisterClientMessageCallback(0x07, new MessageCallback(DropItem));
            massmoveRunning = true;
            typ = 2;
            Pause = pause;
            Color = color;
        }

        [Command("massmove")]
        public void presun(int pause, int pocet, ushort color)
        {
            if (massmoveRunning == true)
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                UO.Print("Massmove vypnut");
                return;
            }
            UO.Print("Pohni s itemem.");
            Core.RegisterClientMessageCallback(0x07, new MessageCallback(DropItem));
            massmoveRunning = true;
            typ = 3;
            Pause = pause;
            Color = color;
            MaxPocet = pocet;
        }


        [Command("massmove")]
        public void presun(int pause, bool stejnaBarva)
        {
            if (massmoveRunning == true)
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                UO.Print("Massmove vypnut");
                return;
            }
            UO.Print("Pohni s itemem.");
            Core.RegisterClientMessageCallback(0x07, new MessageCallback(DropItem));
            massmoveRunning = true;
            typ = 4;
            Pause = pause;
        }


        [Command("massmove")]
        public void presun(int pause, int pocet, bool stejnaBarva)
        {
            if (massmoveRunning == true)
            {
                massmoveRunning = false;
                Core.UnregisterClientMessageCallback(0x07, DropItem);
                UO.Print("Massmove vypnut");
                return;
            }
            UO.Print("Pohni s itemem.");
            Core.RegisterClientMessageCallback(0x07, new MessageCallback(DropItem));
            massmoveRunning = true;
            typ = 5;
            Pause = pause;
            MaxPocet = pocet;
        }

        public void presouvani(int id)
        {
            int pocitadlo;
            presunovanaVec.Changed -= presunovanaVec_Changed;
            switch (id)
            {
                case 1:
                    {
                        pocitadlo = zdroj.Items.Count(presunovanaVec.Graphic);
                        foreach (UOItem item in zdroj.Items)
                        {
                            if (item.Graphic == presunovanaVec.Graphic)
                            {
                                UO.Print("Zbyva " + pocitadlo + " itemu");
                                UO.MoveItem(item, item.Amount, presunovanaVec.Container, presunovanaVec.X, presunovanaVec.Y);
                                pocitadlo--;
                                UO.Wait(Pause);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        pocitadlo = zdroj.Items.Count(presunovanaVec.Graphic, Color);
                        foreach (UOItem item in zdroj.Items)
                        {
                            if (item.Graphic == presunovanaVec.Graphic && item.Color == Color)
                            {
                                UO.Print("Zbyva " + pocitadlo + " itemu");
                                UO.MoveItem(item, item.Amount, presunovanaVec.Container, presunovanaVec.X, presunovanaVec.Y);
                                pocitadlo--;
                                UO.Wait(Pause);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        pocitadlo = zdroj.Items.Count(presunovanaVec.Graphic, Color);
                        if (pocitadlo > MaxPocet)
                        {
                            pocitadlo = MaxPocet;
                        }
                        foreach (UOItem item in zdroj.Items)
                        {
                            if (item.Graphic == presunovanaVec.Graphic && item.Color == Color && MaxPocet > 0)
                            {
                                UO.Print("Zbyva " + pocitadlo + " itemu");
                                UO.MoveItem(item, item.Amount, presunovanaVec.Container, presunovanaVec.X, presunovanaVec.Y);
                                pocitadlo--;
                                MaxPocet--;
                                UO.Wait(Pause);
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        pocitadlo = zdroj.Items.Count(presunovanaVec.Graphic, presunovanaVec.Color);
                        foreach (UOItem item in zdroj.Items)
                        {
                            if (item.Graphic == presunovanaVec.Graphic && item.Color == presunovanaVec.Color)
                            {
                                UO.Print("Zbyva " + pocitadlo + " itemu");
                                UO.MoveItem(item, item.Amount, presunovanaVec.Container, presunovanaVec.X, presunovanaVec.Y);
                                pocitadlo--;
                                UO.Wait(Pause);
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        pocitadlo = zdroj.Items.Count(presunovanaVec.Graphic, presunovanaVec.Color);
                        if (pocitadlo > MaxPocet)
                        {
                            pocitadlo = MaxPocet;
                        }
                        foreach (UOItem item in zdroj.Items)
                        {
                            if (item.Graphic == presunovanaVec.Graphic && item.Color == presunovanaVec.Color && MaxPocet > 0)
                            {
                                UO.Print("Zbyva " + pocitadlo + " itemu");
                                UO.MoveItem(item, item.Amount, presunovanaVec.Container, presunovanaVec.X, presunovanaVec.Y);
                                pocitadlo--;
                                MaxPocet--;
                                UO.Wait(Pause);
                            }
                        }
                        break;
                    }
            }
            UO.Print("Dokonceno");
            massmoveRunning = false;
        }
    }
}