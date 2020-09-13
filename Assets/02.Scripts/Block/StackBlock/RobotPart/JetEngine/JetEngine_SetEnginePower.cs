[BlockTitle("SetEnginePower")]
public sealed class JetEngine_SetEnginePower : StackBlock, IContainingParameter<ReporterBlock>,  IJetEngineBlockType
{
    public ReporterBlock Input1 { get ; set ; }

    sealed public override void ExecuteCommand()
    {
        JetEngine jetEngine = base.GetOperatingRobotPart<JetEngine>();
        if(jetEngine != null)
        {
            jetEngine.EnginePower = Input1.GetReporterNumberValue();
        }
       
    }
}
