[System.Serializable]
public abstract class ReporterBlock : Block, ICanBeParameter
{
    /*
    public virtual string GetReporterStringValue()
    {
        return DefaultStringValue;
    }
    */
    public abstract string GetReporterStringValue();

    public float GetReporterNumberValue()
    {
        return BlockUtility.ConvertStringToFloat(this.GetReporterStringValue());

    }

}
