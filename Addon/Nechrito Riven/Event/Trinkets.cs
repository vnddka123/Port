#region

using System;
using EloBuddy;
using LeagueSharp.Common;
using NechritoRiven.Menus;

#endregion

namespace NechritoRiven.Event
{
    internal class Trinkets : Core.Core
    {
        public static void Update(EventArgs args)
        {
            if (!MenuConfig.Buytrinket || Player.Level < 9 || !Player.InShop() || Items.HasItem(3363) || Items.HasItem(3364)) return;

            switch (MenuConfig.Trinketlist.SelectedIndex)
            {
                case 0:
                    Shop.BuyItem(ItemId.Oracle_Alteration);
                    break;
                case 1:
                    Shop.BuyItem(ItemId.Farsight_Alteration);
                    break;
            }
        }
    }
}
