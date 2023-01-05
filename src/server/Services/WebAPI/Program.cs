var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();
services.AddDbContext(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();