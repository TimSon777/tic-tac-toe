using MassTransit;
using MinimalApi.Endpoint.Extensions;
using IEventBus = Application.Events.IEventBus;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddEndpoints();
services.AddSwaggerGen(options => options.CustomSchemaIds(t => t.ToString()));
services.AddDbContext(configuration);
services.AddCommands(configurator => configurator.UsingInMemory());
services.AddQueries(configurator => configurator.UsingInMemory());
services.AddMassTransit<IEventBus>(configurator => configurator.UsingRabbitMq(configuration));
services.AddApplicationMediator();
services.AddIdentity();
services.AddJwt(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapEndpoints();

app.Run();