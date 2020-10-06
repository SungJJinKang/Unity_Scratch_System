using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

/*
public class BlockConverter : CustomCreationConverter<Block> 
{
    public override Block Create(Type objectType)
    {
        if(objectType == null)
        {
            Debug.LogError("objectType is null");
            return null;
        }

        Block block = Block.CreatBlock(objectType);
        if (block == null)
            Debug.LogError("Fail to create Block : " + objectType.FullName);

        return block;
    }

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>The object value.</returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }


        
        var jObj = JObject.Load(reader);
        var type = jObj.Value<string>("$type");

       


        Block value = this.Create(Type.GetType(type));
        if (value == null)
        {
            throw new JsonSerializationException("No object created.");
        }

        serializer.Populate(reader, value);
        return value;

    }

    public override bool CanWrite => false;

}
*/

public class BlockConverter : JsonConverter<Block>
{

    public override Block ReadJson(JsonReader reader, Type objectType, Block existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }


        JObject jObject = JObject.Load(reader);
        var type = jObject.Value<string>("$type");

        Block block = Block.CreatBlock(Type.GetType(type));

        serializer.Populate(jObject.CreateReader(), block);

        return block;
    }



    public override void WriteJson(JsonWriter writer, Block value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }



    public override bool CanWrite => false;

    
}
