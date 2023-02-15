using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using InvoiceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Services;

public class InvoiceService : IInvoiceService
{
    private ICosmosConnection _connection;

    public InvoiceService(ICosmosConnection connection)
    {
        _connection = connection;
    }

    public async Task<Invoice> CreateAsync(Invoice item)
    {
        var added = await _connection.AddAsync(item);
        return item;
    }

    public async Task<List<Invoice>> GetAllAsync()
    {
        return await _connection.GetAllAsync();
    }

    public Task<Invoice> GetAsync(string id)
    {
        return _connection.GetAsync(id);
    }

    public Task<IEnumerable<Invoice>> GetMultipleAsync(string query)
    {
        return _connection.GetMultipleAsync(query);
    }

    public async Task UpdateAsync(Invoice item)
    {
        var invoiceToUpdate = await GetAsync(item.Id.Value);
        invoiceToUpdate.Description = item.Description;
        invoiceToUpdate.TotalAmount = item.TotalAmount;
        invoiceToUpdate.Items = item.Items;
        await _connection.UpdateAsync(invoiceToUpdate);
    }

    public Task DeleteAsync(string id)
    {
        _connection.DeleteAsync(id);
        return Task.CompletedTask;
    }
}
