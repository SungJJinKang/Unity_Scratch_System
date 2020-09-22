using System;

/// <summary>
/// reference UnitTitleAttribute class in "Bolt" source code
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class NotAutomaticallyMadeOnBlockShopAttribute : Attribute
{
}