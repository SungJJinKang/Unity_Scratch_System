[System.Serializable]
public abstract class ReporterBlock : ValueBlock
{
    /*
    public virtual string GetReporterStringValue()
    {
        return DefaultStringValue;
    }
    */
    public abstract string GetReporterStringValue(RobotBase operatingRobotBase);

    public float GetReporterNumberValue(RobotBase operatingRobotBase)
    {
        return BlockUtility.ConvertStringToFloat(this.GetReporterStringValue(operatingRobotBase));

    }

}
