using LeagueSharp.SDK;

using Settings = Sivir.Config.Modes.Combo;

namespace Sivir.Modes
{
    internal sealed class Combo : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.ComboActive;
        }

        internal override void Execute()
        {
            if (!Variables.Orbwalker.CanMove)
            {
                return;
            }

            if(Settings.UseQ && Q.IsReady())
            {
                Q.CastOnBestTarget(0, true);
            }
        }
    }
}
