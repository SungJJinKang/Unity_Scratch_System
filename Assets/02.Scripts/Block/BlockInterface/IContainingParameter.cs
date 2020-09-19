using System;
/// <summary>
/// Thie interface attached to Block that can be parameter
/// </summary>
public interface ICanBeParameter : ICloneable
{
}

/// Why Make Seperately Each Class Have multipleParameter
/// -> For Checking Type Check 
/// If TwoParameter inherit OneParameter, 
/// (TwoParameter Is Onparameter) return True . This cause bugs
/// //
public interface IContainingParameter 
{
  
}

public interface IContainingParameter<T0> : IContainingParameter where T0 : ICanBeParameter 
{
    T0 Input1 { get; set; }

}

public interface IContainingParameter<T0, T1> : IContainingParameter where T0 : ICanBeParameter where T1 : ICanBeParameter
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }

}

public interface IContainingParameter<T0, T1, T2> : IContainingParameter where T0 : ICanBeParameter where T1 : ICanBeParameter where T2 : ICanBeParameter
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }
    T2 Input3 { get; set; }
}

public interface IContainingParameter<T0, T1, T2, T3> : IContainingParameter where T0 : ICanBeParameter where T1 : ICanBeParameter where T2 : ICanBeParameter where T3 : ICanBeParameter
{
    T0 Input1 { get; set; }
    T1 Input2 { get; set; }
    T2 Input3 { get; set; }
    T3 Input4 { get; set; }
}