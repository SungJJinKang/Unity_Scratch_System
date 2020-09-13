[BlockTitle("GetEnginePower")]
public sealed class JetEngine_GetEnginePower : ReporterBlock, IJetEngineBlockType
{
    public override string GetReporterStringValue()
    {
        JetEngine jetEngine = base.GetOperatingRobotPart<JetEngine>();
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
