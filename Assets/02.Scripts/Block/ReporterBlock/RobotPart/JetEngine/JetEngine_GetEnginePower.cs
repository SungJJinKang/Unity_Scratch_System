[BlockTitle("GetEnginePower")]
public sealed class JetEngine_GetEnginePower : ReporterBlock, IJetEngineBlockType
{
    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        JetEngine jetEngine = operatingRobotBase.GetRobotPart<JetEngine>();
        if(jetEngine != null)
        {
            return jetEngine.EnginePower.ToString();
        }
        else
        {
            return "";
        }   
    }
}
