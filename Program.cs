using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "Host=127.0.0.1;Database=squidy;Username=smeechward;Password=";
builder.Services.AddDbContext<SomethingDbCtx>(opt => opt.UseNpgsql(connectionString));

var app = builder.Build();

var squidMapGroup = app.MapGroup("/api/squids");
var squidEndpoints = new SquidEndpoints();
squidEndpoints.Configure(squidMapGroup);

var instrumentMapGroup = app.MapGroup("/api/instruments");
var instrumentEndpoints = new InstrumentEndpoint();
instrumentEndpoints.Configure(instrumentMapGroup);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
