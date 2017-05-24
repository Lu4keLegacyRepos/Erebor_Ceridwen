﻿using Phoenix.WorldData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.EreborPlugin.Abilites
{
    public class Targeting
    {
        public readonly Notoriety[] Filter = new Notoriety[] { Notoriety.Murderer, Notoriety.Enemy };
        private readonly List<uint> used = new List<uint>();
        [Command, BlockMultipleExecutions]
        public void targetnext()
        {
            targetnext(false);
        }
        [Command, BlockMultipleExecutions]
        public void targetnext(bool closest)
        {
            if (closest)
            {
                List<UOCharacter> redList = new List<UOCharacter>();
                List<UOCharacter> sortedlist;
                redList.Clear();
                foreach (UOCharacter ch in World.Characters)
                {
                    if (ch.Distance > 10 || Array.IndexOf(Filter, ch.Notoriety) < 0 || ch.Serial == World.Player.Serial || ch.Renamable)
                        continue;
                    redList.Add(ch);
                }
                sortedlist = redList.OrderBy(o => o.Distance).ToList();

                if (sortedlist.Count < 1) return;
                Aliases.LastAttack = sortedlist[0].Serial;
                Aliases.SetObject("laststatus", sortedlist[0].Serial);
                byte[] data = new byte[5];
                data[0] = 0xAA;
                ByteConverter.BigEndian.ToBytes((uint)sortedlist[0].Serial, data, 1);
                Core.SendToClient(data, false);
                return;
            }

            bool first = true;
            tryagain:
            foreach (UOCharacter mob in World.Characters)
            {
                if (mob.Distance > 18 || Array.IndexOf(Filter, mob.Notoriety) < 0 || mob.Renamable) continue;
                if (used.Contains(mob.Serial)) continue;
                used.Add(mob.Serial);
                Aliases.LastAttack = mob.Serial;
                Aliases.SetObject("laststatus", mob.Serial);
                byte[] data = new byte[5];
                data[0] = 0xAA;
                ByteConverter.BigEndian.ToBytes((uint)mob.Serial, data, 1);
                Core.SendToClient(data, false);
                return;
            }
            used.Clear();
            if (first)
            {
                first = false;
                goto tryagain;
            }
        }
    }
}
