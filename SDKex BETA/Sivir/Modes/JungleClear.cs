using EloBuddy;
using LeagueSharp.SDK;

using Settings = Sivir.Config.Modes.JungleClear;

namespace Sivir.Modes
{
    internal sealed class JungleClear : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.JungleClearActive;
        }

        internal override void Execute()
        {
            //if (!Variables.Orbwalker.CanMove)
            //{
            //    return;
            //}
        }
    }
}
