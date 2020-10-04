using Newtonsoft.Json;
/// Why Make Seperately Each Class Have multipleParameter
/// -> For Checking Type Check 
/// If TwoParameter inherit OneParameter, 
/// (TwoParameter Is Onparameter) return True . This cause bugs
/// //
[JsonObjectAttribute(MemberSerialization.OptIn)]
public interface IContainingParameter
{

}

public interface IContainingParameter<T0> : IContainingParameter where T0 : ValueBlock
{
    [JsonProperty]
    T0 Input1 { get; set; }

}

public interface IContainingParameter<T0, T1> : IContainingParameter where T0 : ValueBlock where T1 : ValueBlock
{
    [JsonProperty]
    T0 Input1 { get; set; }
    [JsonProperty]
    T1 Input2 { get; set; }

}

public interface IContainingParameter<T0, T1, T2> : IContainingParameter where T0 : ValueBlock where T1 : ValueBlock where T2 : ValueBlock
{
    [JsonProperty]
    T0 Input1 { get; set; }
    [JsonProperty]
    T1 Input2 { get; set; }
    [JsonProperty]
    T2 Input3 { get; set; }
}

public interface IContainingParameter<T0, T1, T2, T3> : IContainingParameter where T0 : ValueBlock where T1 : ValueBlock where T2 : ValueBlock where T3 : ValueBlock
{
    [JsonProperty]
    T0 Input1 { get; set; }
    [JsonProperty]
    T1 Input2 { get; set; }
    [JsonProperty]
    T2 Input3 { get; set; }
    [JsonProperty]
    T3 Input4 { get; set; }
}