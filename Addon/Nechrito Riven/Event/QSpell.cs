using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace NechritoRiven.Event
{
    class QSpell
    {
        public static void OnSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (args.Slot == SpellSlot.Q)
            {
                LeagueSharp.Common.Orbwalking.LastAATick = 0;
            }
        }
    }
}
