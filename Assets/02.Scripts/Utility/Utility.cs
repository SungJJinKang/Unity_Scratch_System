using System.Text;

public static class Utility 
{
    public static bool IsNumeric(this string str)
    {
        float f;
        return float.TryParse(str, out f);
    }

    public static bool IsNumeric(this string str, out float f)
    {
        return float.TryParse(str, out f);
    }

   

    public static StringBuilder stringBuilderCache = new StringBuilder();
  
}


