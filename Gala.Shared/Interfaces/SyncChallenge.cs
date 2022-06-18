using System;
using System.Linq;

namespace Gala.Shared.Interfaces;

public class SyncChallenge
{
    public SyncChallenge(ulong discordId, string chars, Random random)
    {
        DiscordId = discordId;
        TimeToLive = DateTime.Now.AddMinutes(5);
        Hash = new string(Enumerable.Repeat(chars, 9).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public ulong DiscordId { get; }
    public DateTime TimeToLive { get; }
    public string Hash { get; }
}