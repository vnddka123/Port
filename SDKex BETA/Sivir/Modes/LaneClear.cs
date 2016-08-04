using LeagueSharp.SDK;

using Settings = Sivir.Config.Modes.LaneClear;

namespace Sivir.Modes
{
    internal sealed class LaneClear : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.LaneClearActive;
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
