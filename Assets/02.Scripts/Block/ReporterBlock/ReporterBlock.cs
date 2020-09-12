[System.Serializable]
public abstract class ReporterBlock : Block, CanBeParameterBlockInterface
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
