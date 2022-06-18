using Exiled.API.Features;
using MapEditorReborn.API.Features;
using Exiled.Events.EventArgs;

namespace Gala.Plugin
{
    public class EventHandlers
    {
        public void OnWaitingForPlayers()
        {
            // Bloquear el lobby para el servidor de sincronización.
            if (Server.Port == 7778)
            {
                Round.IsLobbyLocked = true;
                return;
            }
            
            // MapEditorReborn: carga el mapa con el nombre especificado ubicado en EXILED/Configs/MapEditorReborn/Maps
            MapUtils.LoadMap(MapUtils.GetMapByName("cambiar_esto_al_nombre_del_mapa"));
        }

        public void OnTransmitting(TransmittingEventArgs ev) // Transmitting = Chat de la V
        {
            ev.IsAllowed = false;
        }

        public void OnVoiceChatting(VoiceChattingEventArgs ev) // VoiceChatting = Chat de la Q
        {
            ev.IsAllowed = false;
        }
    }
}
