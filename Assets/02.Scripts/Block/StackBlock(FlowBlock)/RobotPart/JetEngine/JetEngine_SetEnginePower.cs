
[BlockDefinitionAttribute("Set EnginePower", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class JetEngine_SetEnginePower : StackBlock, IContainingParameter<ReporterBlock>, IJetEngineBlockType
{
    public ReporterBlock Input1 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        JetEngine jetEngine = operatingRobotBase.GetRobotPart<JetEngine>();
        if (jetEngine != null)
        {
            jetEngine.EnginePower = Input1.GetReporterNumberValue(operatingRobotBase);
        }

    }
}
