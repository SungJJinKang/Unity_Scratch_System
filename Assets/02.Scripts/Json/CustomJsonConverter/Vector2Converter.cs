using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class Vector2Converter : JsonConverter<Vector2>
{
    public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        reader.SupportMultipleContent = true;

        try
        {
            reader.Read();
            double x = (double)reader.Value;
            reader.Read();
            double y = (double)reader.Value;
            reader.Read();
            
            
            return new Vector2(Convert.ToSingle(x), Convert.ToSingle(y));
        }
        catch
        {
            Debug.LogError("EXEPTIONNNN!");
            return Vector2.zero;
        }
    }

    public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
    {
        writer.Formatting = Formatting.Indented;

        writer.WriteStartArray();
        writer.WriteValue(value.x);
        writer.WriteValue(value.y);
        writer.WriteEndArray();
    }
}