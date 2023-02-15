using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Converters;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

[JsonConverter(typeof(InvoiceDescriptionConverter))]
public sealed class Description : ValueObject
{
    public string Value { get; set; }

    public Description(string value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Description FromString(string invoiceId)
    {
        CheckValidity(invoiceId);
        return new Description(invoiceId);
    }

    public static implicit operator string(Description invoiceDescription) => invoiceDescription.Value;

    public override string ToString()
    {
        return Value;
    }
    private static void CheckValidity(string value)
    {
        if (!Guid.TryParse(value, out _))
        {
            throw new ArgumentException(nameof(value), "Invoice Description is not a valid.");
        }
    }
}
