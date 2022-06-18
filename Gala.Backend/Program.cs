using Discord.WebSocket;
using Gala.Backend.Bot;
using Gala.Backend;
using Gala.Shared.Interfaces;
using LiteDB;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton(logger);
builder.Services.AddSingleton<Static>();
var litedb = new LiteDatabase("sync.db");
litedb.GetCollection<SynchronizedUser>().EnsureIndex(x => x.DiscordId);
litedb.GetCollection<SyncChallenge>().EnsureIndex(x => x.DiscordId);
builder.Services.AddSingleton(litedb);
builder.Services.AddSingleton<Database>();
builder.Services.AddSingleton<DiscordSocketClient>();
builder.Services.AddHostedService<DiscordClientService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();