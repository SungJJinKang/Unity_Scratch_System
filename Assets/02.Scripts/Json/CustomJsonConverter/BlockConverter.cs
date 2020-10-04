using Newtonsoft.Json.Converters;
using System;

public class BlockConverter : CustomCreationConverter<Block>
{
    public override Block Create(Type objectType)
    {
        return Block.CreatBlock(objectType);
    }
}