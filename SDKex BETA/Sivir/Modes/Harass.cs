using LeagueSharp.SDK;

using Settings = Sivir.Config.Modes.Harass;

namespace Sivir.Modes
{
    internal sealed class Harass : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return Config.Keys.HarassActive;
        }

        internal override void Execute()
        {
            if (!Variables.Orbwalker.CanMove)
            {
                return;
            }

            if(Settings.UseQ && Q.IsReady() && GameObjects.Player.ManaPercent > Settings.MinMana)
            {
                Q.CastOnBestTarget(0, true);
            }
        }
    }
}
