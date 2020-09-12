/// <summary>
/// Thie interface attached to Block that can be parameter
/// </summary>
public interface CanBeParameterBlockInterface
{
}



public interface BlockParameter
{
  
}

public interface BlockParameter<T0> : BlockParameter where T0 : CanBeParameterBlockInterface 
{
    T0 Input1 { get; set; }

}

public interface BlockParameter<T0, T1> : BlockParameter where T0 : CanBeParameterBlockInterface where T1 : CanBeParameterBlockInterface
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }

}

public interface BlockParameter<T0, T1, T2> : BlockParameter where T0 : CanBeParameterBlockInterface where T1 : CanBeParameterBlockInterface where T2 : CanBeParameterBlockInterface
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }
    T2 Input3 { get; set; }
}

public interface BlockParameter<T0, T1, T2, T3> : BlockParameter where T0 : CanBeParameterBlockInterface where T1 : CanBeParameterBlockInterface where T2 : CanBeParameterBlockInterface where T3 : CanBeParameterBlockInterface
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }
    T2 Input3 { get; set; }
    T3 Input4 { get; set; }
}