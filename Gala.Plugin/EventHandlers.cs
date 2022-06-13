using MapEditorReborn.API.Features;

namespace Gala.Plugin
{
    public class EventHandlers
    {
        // MapEditorReborn: carga el mapa con el nombre especificado ubicado en EXILED/Configs/MapEditorReborn/Maps
        public void OnWaitingForPlayers()
        {
            MapUtils.LoadMap(MapUtils.GetMapByName("cambiar_esto_al_nombre_del_mapa"));
        }
    }
}
