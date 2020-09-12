[BlockTitle("GetEnginePower")]
public sealed class JetEngine_GetEnginePower : ReporterBlock, JetEngineBlockType
{
    public override string GetReporterStringValue()
    {
        return base.GetRobotPart<JetEngine>()?.EnginePower.ToString();
    }
}
