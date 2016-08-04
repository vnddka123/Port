using EloBuddy;
using LeagueSharp.SDK;

using Settings = Sivir.Config.Modes.Flee;

namespace Sivir.Modes
{
    internal sealed class Flee : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.FleeActive;
        }

        internal override void Execute()
        {
            Variables.Orbwalker.Move(Game.CursorPos);

            if(Settings.UseR)
            {
                R.Cast();
            }            
        }
    }
}
