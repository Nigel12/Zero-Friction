using InvoiceApp.Domain.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate;

namespace InvoiceApp.Infrastructure.DataAccess;

public class CosmosConnection : ICosmosConnection
{
    private Microsoft.Azure.Cosmos.Container _container;
    public CosmosConnection(CosmosClient cosmosDbClient, string databaseName, string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }


    public async Task<Invoice> AddAsync(Invoice item)
    {
        var added = await _container.CreateItemAsync(item, new PartitionKey(item.Id.Value));
        return added;
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<Invoice>(id, new PartitionKey(id));
    }

    public async Task<List<Invoice>> GetAllAsync()
    {
        var q = _container.GetItemLinqQueryable<Invoice>();
        var iterator = q.ToFeedIterator();
        var results = await iterator.ReadNextAsync();
        return results.ToList();
    }

    public async Task<Invoice> GetAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<Invoice>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException c)
        {
            Console.Write(c.Message);
            return null;
        }
    }
    public async Task<IEnumerable<Invoice>> GetMultipleAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<Invoice>(new QueryDefinition(queryString));
        var results = new List<Invoice>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
    public async Task UpdateAsync(Invoice item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(item.Id.Value));
    }
}
