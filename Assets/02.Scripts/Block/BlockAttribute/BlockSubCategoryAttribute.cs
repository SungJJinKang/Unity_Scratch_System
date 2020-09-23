using System;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
//[fsObject(Converter = typeof(UnitCategoryConverter))]
public class BlockSubCategoryAttribute : Attribute
{
    public BlockSubCategoryAttribute(string subCategory)
    {
        this._SubCategory = subCategory;
    }

    public string _SubCategory;


    public override bool Equals(object obj)
    {
        return obj is BlockSubCategoryAttribute && ((BlockSubCategoryAttribute)obj)._SubCategory == _SubCategory;
    }

    public override int GetHashCode()
    {
        return _SubCategory.GetHashCode();
    }

    public override string ToString()
    {
        return _SubCategory;
    }

    public static bool operator ==(BlockSubCategoryAttribute a, BlockSubCategoryAttribute b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if ((object)a == null || (object)b == null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(BlockSubCategoryAttribute a, BlockSubCategoryAttribute b)
    {
        return !(a == b);
    }
}