using MapEditorReborn.API.Features;

namespace Gala.Plugin
{
    public class EventHandlers
    {
        public void OnWaitingForPlayers()
        {
            // MapEditorReborn: carga el mapa con el nombre especificado ubicado en EXILED/Configs/MapEditorReborn/Maps
            MapUtils.LoadMap(MapUtils.GetMapByName("cambiar_esto_al_nombre_del_mapa"));
        }
    }
}
