using Gala.Shared.Interfaces;
using LiteDB;

namespace Gala.Backend;

public class Database
{
    private readonly LiteDatabase _liteDatabase;

    public Database(LiteDatabase liteDatabase)
    {
        _liteDatabase = liteDatabase;
    }

    public SynchronizedUser? GetSyncUser(ulong id)
        => _liteDatabase.GetCollection<SynchronizedUser>().FindOne(x => x.DiscordId == id || x.SteamId == id);

    public SynchronizedUser? GetSyncUser(string id)
        => GetSyncUser(ulong.Parse(id));

    public bool TryCreateSyncUser(SynchronizedUser user)
    {
        var collection = _liteDatabase.GetCollection<SynchronizedUser>();
        if(collection.Exists(x => x.DiscordId == user.DiscordId || x.SteamId == user.SteamId))
            return false;
        return true;
    }

    public SyncChallenge? GetSyncChallenge(string hash)
    {
        var collection = _liteDatabase.GetCollection<SyncChallenge>();
        var challenge = collection.FindOne(x => x.Hash == hash);
        if(challenge.TimeToLive > DateTime.Now)
            collection.DeleteMany(x => x.Hash == challenge.Hash);
        return challenge;
    }
    
    public SyncChallenge? GetSyncChallenge(ulong discordId)
    {
        var collection = _liteDatabase.GetCollection<SyncChallenge>();
        var challenge = collection.FindOne(x => x.DiscordId == discordId);
        if(challenge.TimeToLive > DateTime.Now)
            collection.DeleteMany(x => x.Hash == challenge.Hash);
        return challenge;
    }

    public void CreateSyncChallenge(SyncChallenge challenge)
        => _liteDatabase.GetCollection<SyncChallenge>().Insert(challenge);
}