using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceApp.Domain.Aggregates.InvoiceAggregate.ValueObjects;

namespace InvoiceApp.Domain.Aggregates.InvoiceAggregate.Converters;

public class InvoiceIdConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(InvoiceId));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return InvoiceId.FromString(JToken.Load(reader).ToString());
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JToken.FromObject(value.ToString()).WriteTo(writer);
    }
}
