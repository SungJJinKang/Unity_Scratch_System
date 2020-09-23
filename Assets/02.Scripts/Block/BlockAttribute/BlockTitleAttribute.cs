using System;

/// <summary>
/// reference UnitTitleAttribute class in "Bolt" source code
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class BlockTitleAttribute : Attribute
{
    public BlockTitleAttribute(string title)
    {
        this.title = title;
    }

    public string title { get; private set; }
}