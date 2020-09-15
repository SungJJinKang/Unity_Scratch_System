public static class BlockUtility
{
    static float cachedFloatVariable;
    public static float ConvertStringToFloat(this string str)
    {

        if (float.TryParse(str, out cachedFloatVariable) == true)
        {
            return cachedFloatVariable;
        }
        else
        {//In Scratch, if convert string to number, return 0
            return 0;
        }
    }
}