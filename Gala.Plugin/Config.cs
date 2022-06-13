using Exiled.API.Interfaces;

namespace Gala.Plugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}