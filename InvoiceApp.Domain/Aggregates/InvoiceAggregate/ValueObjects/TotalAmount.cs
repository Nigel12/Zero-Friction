using InvoiceApp.Domain.Aggregates.InvoiceAggregate.Converters;
using InvoiceApp.Domain.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

[JsonConverter(typeof(InvoiceTotalAmountConverter))]
public sealed class TotalAmount : ValueObject
{
    public double Value { get; set; }

    public TotalAmount(double value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static TotalAmount FromString(string totalAmount)
    {
        return new TotalAmount(double.Parse(totalAmount));
    }

    public static implicit operator double(TotalAmount totalAmount) => totalAmount.Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
