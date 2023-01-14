using MassTransit;
using MinimalApi.Endpoint.Extensions;
using WebAPI.Features.Gaming;
using IEventBus = Application.Events.IEventBus;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddCors();
services.AddEndpointsApiExplorer();
services.AddEndpoints();
services.AddSwagger();
services.AddDbContext(configuration);
services.AddCommands(configurator => configurator.UsingInMemory());
services.AddQueries(configurator => configurator.UsingInMemory());
services.AddMassTransit<IEventBus>(configurator => configurator.UsingRabbitMq(configuration));
services.AddApplicationMediator();
services.AddIdentity();
services.AddJwt(configuration);
services.AddAuthorization(configuration);
services.AddRepositories();
services.AddSignalR();

var app = builder.Build();

app.UseCors(options => options
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(configuration.GetString("FRONT_ORIGIN")));

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<GamingHub>("/gaming");
app.MapEndpoints();

app.Run();