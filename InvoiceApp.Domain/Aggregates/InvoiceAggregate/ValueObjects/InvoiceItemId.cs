using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Converters;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

[JsonConverter(typeof(InvoiceItemIdConverter))]
public sealed class InvoiceItemId : ValueObject
{
    public string Value { get; }

    public InvoiceItemId(string value)
    {
        Value = value;
    }

    public static InvoiceItemId CreateUnique()
    {
        return new(Guid.NewGuid().ToString());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static InvoiceItemId FromString(string invoiceItemId)
    {
        CheckValidity(invoiceItemId);
        return new InvoiceItemId(invoiceItemId);
    }

    public static implicit operator string(InvoiceItemId invoiceItemId) => invoiceItemId.Value;

    public override string ToString()
    {
        return Value;
    }
    private static void CheckValidity(string value)
    {
        if (!Guid.TryParse(value, out _))
        {
            throw new ArgumentException(nameof(value), "Invoice Item Id is not a GUID.");
        }
    }
}
