using MassTransit;
using MinimalApi.Endpoint.Extensions;
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

var app = builder.Build();

app.UseCors((options) =>
{
    options.AllowCredentials()
        .WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
});
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.Run();