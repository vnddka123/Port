#region

using System;
using LeagueSharp.Common;
using NechritoRiven.Menus;

#endregion

namespace NechritoRiven.Event
{
    internal class Skinchanger : Core.Core
    {
        public static void Update(EventArgs args)
        {
            if (CheckSkin())
            {
                Player.SetSkinId(SkinId());
            }
            Player.SetSkinId(MenuConfig.Config.Item("Skin").GetValue<StringList>().SelectedIndex);
        }
        public static int SkinId()
        {
            return MenuConfig.Config.Item("Skin").GetValue<StringList>().SelectedIndex;
        }

        public static bool CheckSkin()
        {
            return MenuConfig.UseSkin;
        }
    }
}
