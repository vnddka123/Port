﻿using System;
using EloBuddy;
namespace LeagueSharp.Common
{
    /// <summary>
    /// Adds hacks to the menu.
    /// </summary>
    internal class Hacks
    {
        private static Menu menu;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            CustomEvents.Game.OnGameLoad += eventArgs =>
            {
                menu = new Menu("Hacks", "Hacks");

                var draw = menu.AddItem(new MenuItem("DrawingHack", "Disable Drawing").SetValue(false));
                draw.SetValue(EloBuddy.Hacks.DisableDrawings);
                draw.ValueChanged +=
                    delegate(object sender, OnValueChangeEventArgs args)
                    {
                        EloBuddy.Hacks.DisableDrawings = args.GetNewValue<bool>();
                    };

                var say = menu.AddItem(new MenuItem("OrbElo", "Disable Orbwalk of Elo").SetValue(false)
                    .SetTooltip("Disable Orbwalker of EloBuddy"));
                say.ValueChanged +=
                    delegate(object sender, OnValueChangeEventArgs args)
                    {
                        EloBuddy.SDK.Orbwalker.DisableAttacking = args.GetNewValue<bool>();
                        EloBuddy.SDK.Orbwalker.DisableMovement = args.GetNewValue<bool>();
                    };

                /*  var zoom = menu.AddItem(new MenuItem("ZoomHack", "Extended Zoom").SetValue(false));
                zoom.SetValue(LeagueSharp.Hacks.ZoomHack);
                zoom.ValueChanged +=
                    delegate (object sender, OnValueChangeEventArgs args)
                    {
                        LeagueSharp.Hacks.ZoomHack = args.GetNewValue<bool>();
                    };

                menu.AddItem(
                    new MenuItem("ZoomHackInfo", "Note: ZoomHack may be unsafe!", false, FontStyle.Regular, Color.Red));
                */

                var tower = menu.AddItem(new MenuItem("TowerHack", "Show Tower Ranges").SetValue(false));
                tower.SetValue(EloBuddy.Hacks.TowerRanges);
                tower.ValueChanged +=
                    delegate(object sender, OnValueChangeEventArgs args)
                    {
                        EloBuddy.Hacks.TowerRanges = args.GetNewValue<bool>();
                    };

                CommonMenu.Instance.AddSubMenu(menu);
            };
        }

        public static void Shutdown()
        {
            Menu.Remove(menu);
        }
    }
}