using Exiled.API.Features;
using ServerHandler = Exiled.Events.Handlers.Server;

namespace Gala.Plugin
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "SCP:SL ESP";
        public override string Name => typeof(Plugin).Namespace;
        
        public EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers();
            ServerHandler.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerHandler.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;

            EventHandlers = null;
            base.OnDisabled();
        }
    }
}
