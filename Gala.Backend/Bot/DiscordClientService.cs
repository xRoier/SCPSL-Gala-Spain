using Discord;
using Discord.WebSocket;

namespace Gala.Backend.Bot;

public class DiscordClientService : BackgroundService
{
    private DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DiscordClientService> _logger;

    public DiscordClientService(DiscordSocketClient client, IConfiguration configuration, ILogger<DiscordClientService> logger)
    {
        _client = client;
        // Si no esta inicializado, lo inicializa.
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info
        });
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Ready += ClientOnReady;
        
        await _client.LoginAsync(TokenType.Bot, _configuration["Auth:Discord"]);
        await _client.StartAsync();
        await Task.Delay(-1, stoppingToken);
    }

    private async Task ClientOnReady()
    {
        await _client.SetStatusAsync(UserStatus.Online);
        _logger.LogInformation("Cliente listo");
    }
}