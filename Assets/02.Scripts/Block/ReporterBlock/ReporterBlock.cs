[System.Serializable]
public abstract class ReporterBlock : Block, ICanBeParameter
{
    public readonly string DefaultStringValue = "";
    public virtual string GetReporterStringValue()
    {
        return DefaultStringValue;
    }

    public float GetReporterNumberValue()
    {
        return BlockUtility.ConvertStringToFloat(this.GetReporterStringValue());
    }

}
