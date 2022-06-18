using Gala.Shared.Enums;

namespace Gala.Shared.Interfaces;

public class SynchronizedUser
{
    public ulong DiscordId { get; set; }
    public ulong SteamId { get; set; }
    public Role Role { get; set; } = Role.None;
}