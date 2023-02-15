using InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.Entities;

public class InvoiceItem : Entity<InvoiceItemId>
{
    [JsonProperty(PropertyName = "amount")]
    public double Amount { get; set; }

    [JsonProperty(PropertyName = "quantity")]
    public int Quantity { get; set; }

    [JsonProperty(PropertyName = "unitPrice")]
    public double UnitPrice { get; set; }

    public InvoiceItem(InvoiceItemId invoiceItemId, int quantity, double unitPrice) : base(invoiceItemId)
    {
        Amount = quantity * unitPrice;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
