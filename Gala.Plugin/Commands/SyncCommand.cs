using System;
using CommandSystem;
using Exiled.API.Features;

namespace Gala.Plugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SyncCommand : ICommand
    {
        public string Command => "sync";
        public string[] Aliases => new[] { "sinc", "sincronizar" };
        public string Description => "Comando para sincronizar tu cuenta de Steam con la de Discord.";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Server.Port != 7778)
            {
                response = "No puedes ejecutar el comando en este servidor.";
                return false;
            }

            // TODO: Implementar ApiWrapper para manejar los SyncUsers y SyncChallenges en este comando.
            
            response = "";
            return true;
        }
    }
}