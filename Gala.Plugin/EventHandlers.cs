using MapEditorReborn.API.Features;

namespace Gala.Plugin
{
    public class EventHandlers
    {
        public void OnWaitingForPlayers()
        {
            MapUtils.LoadMap(MapUtils.GetMapByName("cambiar_esto_al_nombre_del_mapa"));
        }
    }
}