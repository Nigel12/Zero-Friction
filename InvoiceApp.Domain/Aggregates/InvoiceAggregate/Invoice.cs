using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Entities;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate;

public class Invoice : AggregateRoot<InvoiceId>
{
    [JsonProperty(PropertyName = "date")]
    public DateTime Date { get; set; }

    [JsonProperty(PropertyName = "description")]
    public Description Description { get; set; }

    [JsonProperty(PropertyName = "totalAmount")]
    public TotalAmount TotalAmount { get; set; }

    [JsonProperty(PropertyName = "items")]
    public List<InvoiceItem> Items { get; set; }

    public Invoice(InvoiceId invoiceId, string description, double totalAmount, List<InvoiceItem> items) : base(invoiceId)
    {
        Date = DateTime.Now;
        Description = new Description(description);
        TotalAmount = new TotalAmount(totalAmount);
        Items = items;
    }
}
