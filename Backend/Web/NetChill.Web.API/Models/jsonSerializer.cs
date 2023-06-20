using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

public class IHeaderDictionaryConverter : JsonConverter<IHeaderDictionary>
{
    public override IHeaderDictionary Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Deserialization of IHeaderDictionary is not supported.");
    }

    public override void Write(Utf8JsonWriter writer, IHeaderDictionary value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (KeyValuePair<string, StringValues> header in value)
        {
            writer.WritePropertyName(header.Key);
            writer.WriteStringValue(header.Value);
        }

        writer.WriteEndObject();
    }
}

