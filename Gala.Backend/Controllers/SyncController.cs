using Discord.WebSocket;
using Gala.Shared.Enums;
using Gala.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Gala.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Database _database;
    private readonly DiscordSocketClient _client;

    public SyncController(IConfiguration configuration, Database database, DiscordSocketClient client)
    {
        _configuration = configuration;
        _database = database;
        _client = client;
    }

    [HttpGet("{id}")]
    public ActionResult<SynchronizedUser> GetUser(string id)
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var token) || token != _configuration["Auth:Api"])
            return Forbid();
        if (!ulong.TryParse(id, out var userId))
            return BadRequest();
        return Ok(_database.GetSyncUser(userId));
    }

    [HttpPut("{hash}/{steamid}")]
    public ActionResult CreateUser(string hash, string steamid)
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var token) || token != _configuration["Auth:Api"])
            return Forbid();
        if (!ulong.TryParse(steamid.Remove(steamid.IndexOf('@')), out var steamId))
            return BadRequest();
        var challenge = _database.GetSyncChallenge(hash);
        if (challenge == null)
            return BadRequest();
        var syncUser = new SynchronizedUser
        {
            DiscordId = challenge.DiscordId,
            Role = (Role)(_client.GetGuild(985788875089793104)?.GetUser(challenge.DiscordId)?.Roles?.Where(x => x.Id != 987357279521239091).MaxBy(x => x.Position)?.Id ?? 0),
            SteamId = steamId
        };
        if (!_database.TryCreateSyncUser(syncUser))
            return Conflict();
        return Ok();
    }
}