using Discord;
using Discord.WebSocket;
using Gala.Shared.Interfaces;

namespace Gala.Backend.Bot;

public class DiscordClientService : BackgroundService
{
    private DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DiscordClientService> _logger;
    private readonly Database _database;
    private readonly Static _static;

    public DiscordClientService(DiscordSocketClient client, IConfiguration configuration, ILogger<DiscordClientService> logger, Database database, Static @static)
    {
        _client = client;
        // Si no esta inicializado, lo inicializa.
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info
        });
        _configuration = configuration;
        _logger = logger;
        _database = database;
        _static = @static;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Ready += ClientOnReady;
        _client.SlashCommandExecuted += ClientOnSlashCommandExecuted;
        
        await _client.LoginAsync(TokenType.Bot, _configuration["Auth:Discord"]);
        await _client.StartAsync();
        await Task.Delay(-1, stoppingToken);
    }

    private async Task ClientOnReady()
    {
        await _client.SetStatusAsync(UserStatus.Online);
        var syncCmd = new SlashCommandBuilder()
            .WithName("sync")
            .WithDescription("Sincroniza tu cuenta de Discord con tu cuenta de Steam")
            .Build();
        await _client.Rest.CreateGlobalCommand(syncCmd);
        _logger.LogInformation("Cliente listo");
    }
    
    private async Task ClientOnSlashCommandExecuted(SocketSlashCommand ev)
    {
        if (ev.CommandName == "sync")
        {
            if (_database.GetSyncUser(ev.User.Id) != null)
            {
                await ev.RespondAsync("Tu cuenta ya estaba sincronizada.", ephemeral: true);
                return;
            }

            var challenge = _database.GetSyncChallenge(ev.User.Id);
            if (challenge != null)
            {
                await ev.RespondAsync($"Ya estás en un proceso de sincronización, el código es: ||{challenge.Hash}||, caduca en {(int)(challenge.TimeToLive - DateTime.Now).TotalMinutes} minutos.", ephemeral: true);
                return;
            }

            challenge = new SyncChallenge(ev.User.Id, _static.Chars, _static.Random);
            _database.CreateSyncChallenge(challenge);
            await ev.RespondAsync("**Has comenzado el proceso de sincronización**\n" +
                                  $"> Tienes **5 minutos** para unirte al servidor de SCP:SL [{_configuration["Info:SyncServer"]}] e introducir el comando ||``.sync {challenge.Hash}``|| en la consola del juego (Ñ) para sincronizar tu cuenta de Discord y Steam.\n" +
                                  "> ¡Ten en cuenta que si tienes el **Do Not Track** activado no podrás sincronizar tu cuenta de Steam!", ephemeral: true);
        }
    }
}