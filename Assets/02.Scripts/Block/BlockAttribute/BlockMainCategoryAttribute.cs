using System;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
//[fsObject(Converter = typeof(UnitCategoryConverter))]
public class BlockMainCategoryAttribute : Attribute
{
    public BlockMainCategoryAttribute(string mainCategory)
    {
        this._MainCategory = mainCategory;
    }

    public string _MainCategory;


    public override bool Equals(object obj)
    {
        return obj is BlockMainCategoryAttribute && ((BlockMainCategoryAttribute)obj)._MainCategory == _MainCategory;
    }

    public override int GetHashCode()
    {
        return _MainCategory.GetHashCode();
    }

    public override string ToString()
    {
        return _MainCategory;
    }

    public static bool operator ==(BlockMainCategoryAttribute a, BlockMainCategoryAttribute b)
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

    public static bool operator !=(BlockMainCategoryAttribute a, BlockMainCategoryAttribute b)
    {
        return !(a == b);
    }
}