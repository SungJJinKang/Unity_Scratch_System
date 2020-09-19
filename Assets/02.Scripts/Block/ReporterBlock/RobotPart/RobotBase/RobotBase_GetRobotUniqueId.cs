[BlockTitle("GetRobotUniqueId")]
public sealed class RobotBase_GetRobotUniqueId : ReporterBlock, IJetEngineBlockType
{
    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        if (operatingRobotBase == null)
            return System.String.Empty;
        else
            return operatingRobotBase.RobotUniqueId;
    }
}
