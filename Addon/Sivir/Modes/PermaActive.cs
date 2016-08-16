using Settings = Sivir.Config.Auto;

namespace Sivir.Modes
{
    internal sealed class PermaActive : ModeBase
    {
        internal override bool ShouldBeExecuted()
        {
            return true;
        }

        internal override void Execute() { }
    }
}
