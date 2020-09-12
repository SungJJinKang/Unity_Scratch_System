public static class BlockUtility
{
    public static float ConvertStringToFloat(this string str)
    {
        float f;
        if (float.TryParse(str, out f) == true)
        {
            return f;
        }
        else
        {//In Scratch, if convert string to number, return 0
            return 0;
        }
    }
}