using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Converters;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

[JsonConverter(typeof(InvoiceIdConverter))]
public sealed class InvoiceId : ValueObject
{
    public string Value { get; }

    private InvoiceId(string value)
    {
        Value = value;
    }

    public static InvoiceId CreateUnique()
    {
        return new(Guid.NewGuid().ToString());
    }

    public static InvoiceId CreateExisting(string value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static InvoiceId FromString(string invoiceId)
    {
        CheckValidity(invoiceId);
        return new InvoiceId(invoiceId);
    }

    public static implicit operator string(InvoiceId invoiceId) => invoiceId.Value;

    public override string ToString()
    {
        return Value;
    }
    private static void CheckValidity(string value)
    {
        if (!Guid.TryParse(value, out _))
        {
            throw new ArgumentException(nameof(value), "Invoice Id is not a GUID.");
        }
    }
}
