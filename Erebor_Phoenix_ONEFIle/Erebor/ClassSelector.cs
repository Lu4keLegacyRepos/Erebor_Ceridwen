using Phoenix.Communication.Packets;
using Phoenix.EreborPlugin.Classes;
using Phoenix.WorldData;
using System;
using System.Collections.Generic;

namespace Phoenix.EreborPlugin
{
    public delegate void GetClass(out IUOClass Character, out bool done);
    public class ClassSelector
    {
        public event EventHandler ClassGetted;
        int classType;
        private List<string> classes = new List<string>() { "priest", "shaman", "mag", "necro", "thief", "craft", "war", "ranger" };

        public void GetClass(out IUOClass Character, out bool done)
        {
            Character = null;
            Core.RegisterServerMessageCallback(0x1C, onWhoami);
            int x = World.Player.X;
            int y = World.Player.Y;
            while (x == World.Player.X && y == World.Player.Y) UO.Wait(100);
            UO.Wait(200);
            UO.Say(".whoami");
            UO.Wait(500);
            Core.UnregisterServerMessageCallback(0x1C, onWhoami);

            if (classType == 0) Character = new Klerik(80, ".heal", 2800, 120, 0x0E21);
            if (classType == 1) Character = new Shaman(40, ".samheal", 2800, 90, 0x0E20);
            if (classType == 2) Character = new Mage(50, 3500);
            if (classType == 3) Character = new Nekromant(50, 3000);
            if (classType == 4) Character = new Thief(60, 1500);
            if (classType == 5) Character = new Klerik(80, ".heal", 2800, 120, 0x0E21);
            if (classType == 6) Character = new Klerik(80, ".heal", 2800, 120, 0x0E21);
            if (classType == 7) Character = new Ranger(80, 2700);
            Main.instance.ActualClass = Character;

            done = true;
            if(ClassGetted!=null)
            {
                ClassGetted(this,new EventArgs());
            }
        }


        CallbackResult onWhoami(byte[] data, CallbackResult prevResult)//0x1C
        {
            AsciiSpeech packet = new AsciiSpeech(data);

            foreach (string s in classes)
            {
                if (packet.Text.Contains(s))
                {
                    classType = classes.IndexOf(s);
                    return CallbackResult.Eat;
                }
            }
            return CallbackResult.Normal;
        }
    }
}
