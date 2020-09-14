[BlockTitle("GetRobotUniqueId")]
public sealed class RobotBase_GetRobotUniqueId : ReporterBlock, IJetEngineBlockType
{
    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        if (operatingRobotBase == null)
            return "";
        else
            return operatingRobotBase.UniqueRobotId;
    }
}
