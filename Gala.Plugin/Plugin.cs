using Exiled.API.Features;
using ServerHandler = Exiled.Events.Handlers.Server;

namespace Gala.Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "SCP:SL ESP";
        public override string Name => typeof(Plugin).Namespace;
        
        public EventHandlers ev { get; private set; }

        public override void OnEnabled()
        {
            ev = new EventHandlers();
            ServerHandler.WaitingForPlayers += ev.OnWaitingForPlayers;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerHandler.WaitingForPlayers -= ev.OnWaitingForPlayers;

            ev = null;
            base.OnDisabled();
        }
    }
}