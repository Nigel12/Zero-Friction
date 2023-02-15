using InvoiceApp.Application;
using InvoiceApp.Domain.Interfaces;
using InvoiceApp.Domain.Services;
using InvoiceApp.Infrastructure.DataAccess;
using MediatR;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<ICosmosConnection>(await InitializeCosmosConnectionAsync(builder.Configuration.GetSection("CosmosDb")));
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



async Task<ICosmosConnection> InitializeCosmosConnectionAsync(IConfigurationSection configurationSection)
{
    var databaseName = configurationSection["DatabaseId"];
    var containerName = configurationSection["ContainerId"];
    var endpointUri = configurationSection["EndpointUri"];
    var primaryKey = configurationSection["PrimaryKey"];
    var client = new Microsoft.Azure.Cosmos.CosmosClient(endpointUri, primaryKey);
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id", 500);
    var cosmosConnection = new CosmosConnection(client, databaseName, containerName);
    return cosmosConnection;
}
