#region

using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using LeagueSharp.Common;

#endregion

namespace NechritoRiven
{
    public class Program
    {
        private static void Main()
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName != "Riven")
            {
                Chat.Print("Could not load Riven");
                return;
            }
           Load.Load.LoadAssembly();
        }
    }
}