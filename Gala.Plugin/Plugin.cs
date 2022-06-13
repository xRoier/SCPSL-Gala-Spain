using Exiled.API.Features;

namespace Gala.Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "SCP:SL ESP";
        public override string Name => typeof(Plugin).Namespace;

        public override void OnEnabled()
        {
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}