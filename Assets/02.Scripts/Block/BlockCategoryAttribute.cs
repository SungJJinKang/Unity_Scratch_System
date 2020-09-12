using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Interface, Inherited = true)]
//[fsObject(Converter = typeof(UnitCategoryConverter))]
public class BlockCategoryAttribute : Attribute
{
	public BlockCategoryAttribute(string fullName)
	{
		fullName = fullName.Replace('\\', '/');

		this.fullName = fullName;

		var parts = fullName.Split('/');

		name = parts[parts.Length - 1];

		if (parts.Length > 1)
		{
			root = new BlockCategoryAttribute(parts[0]);
			parent = new BlockCategoryAttribute(fullName.Substring(0, fullName.LastIndexOf('/')));
		}
		else
		{
			root = this;
			isRoot = true;
		}
	}

	public BlockCategoryAttribute root { get; }
	public BlockCategoryAttribute parent { get; }
	public string fullName { get; }
	public string name { get; }
	public bool isRoot { get; }

	public IEnumerable<BlockCategoryAttribute> ancestors
	{
		get
		{
			var ancestor = parent;

			while (ancestor != null)
			{
				yield return ancestor;
				ancestor = ancestor.parent;
			}
		}
	}

	public IEnumerable<BlockCategoryAttribute> AndAncestors()
	{
		yield return this;

		foreach (var ancestor in ancestors)
		{
			yield return ancestor;
		}
	}

	public override bool Equals(object obj)
	{
		return obj is BlockCategoryAttribute && ((BlockCategoryAttribute)obj).fullName == fullName;
	}

	public override int GetHashCode()
	{
		return fullName.GetHashCode();
	}

	public override string ToString()
	{
		return fullName;
	}

	public static bool operator ==(BlockCategoryAttribute a, BlockCategoryAttribute b)
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

	public static bool operator !=(BlockCategoryAttribute a, BlockCategoryAttribute b)
	{
		return !(a == b);
	}
}