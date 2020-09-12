[BlockTitle("SetEnginePower")]
public sealed class JetEngine_SetEnginePower : StackBlock, BlockParameter<VariableBlock>,  JetEngineBlockType
{
    public VariableBlock Input1 { get ; set; }

    public bool IsAllPrameterFilled => throw new System.NotImplementedException();

    sealed public override void ExecuteCommand()
    {
        JetEngine jetEngine = base.GetRobotPart<JetEngine>();
        if(jetEngine != null)
        {
            jetEngine.EnginePower = Input1.GetReporterNumberValue();
        }
       
    }
}
