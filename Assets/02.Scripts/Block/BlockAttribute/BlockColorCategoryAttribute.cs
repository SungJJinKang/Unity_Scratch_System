using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
//[fsObject(Converter = typeof(UnitCategoryConverter))]
public class BlockColorCategoryAttribute : Attribute
{
    public BlockColorCategoryAttribute(float r, float g, float b)
    {
        this.Color = new Color(r, g, b);
    }

    public Color Color;


    public override bool Equals(object obj)
    {
        return obj is BlockColorCategoryAttribute && ((BlockColorCategoryAttribute)obj).Color == Color;
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode();
    }

    public override string ToString()
    {
        return Color.ToString();
    }

    /*
    public static bool operator ==(BlockColorCategoryAttribute a, BlockColorCategoryAttribute b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a == null || b == null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(BlockColorCategoryAttribute a, BlockColorCategoryAttribute b)
    {
        return !(a == b);
    }
    */
}