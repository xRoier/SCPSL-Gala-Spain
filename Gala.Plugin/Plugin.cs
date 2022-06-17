using Exiled.API.Features;
using ServerHandler = Exiled.Events.Handlers.Server;
using PlayerHandler = Exiled.Events.Handlers.Player;

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
            PlayerHandler.VoiceChatting += EventHandlers.OnVoiceChatting;
            PlayerHandler.Transmitting += EventHandlers.OnTransmitting;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerHandler.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            PlayerHandler.VoiceChatting -= EventHandlers.OnVoiceChatting;
            PlayerHandler.Transmitting -= EventHandlers.OnTransmitting;

            EventHandlers = null;
            base.OnDisabled();
        }
    }
}
