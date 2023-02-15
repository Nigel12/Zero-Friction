﻿using InvoiceApp.Domain.Aggregates.InvoiceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Interfaces;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetMultipleAsync(string query);

    Task<List<Invoice>> GetAllAsync();
    Task<Invoice> GetAsync(string id);
    Task<Invoice> CreateAsync(Invoice item);
    Task UpdateAsync(Invoice item);
    Task DeleteAsync(string id);
}
