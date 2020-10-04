using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class BlockJsonConverter : JsonConverter<Block>
{

    public override void WriteJson(JsonWriter writer,  Block value, JsonSerializer serializer)
    {
       
    }

    public override Block ReadJson(JsonReader reader, Type objectType, Block existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}