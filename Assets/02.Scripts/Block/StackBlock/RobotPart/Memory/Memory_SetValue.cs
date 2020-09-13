[BlockTitle("SetEnginePower")]
public sealed class Memory_SetValue : StackBlock, IContainingParameter<VariableBlock, ReporterBlock>, IVariableBlockType
{
    public VariableBlock Input1 { get ; set; }
    public ReporterBlock Input2 { get ; set; }

    sealed public override void ExecuteCommand()
    {
        RobotBase robotBase = base.GetOperatingRobotBase();
        if (robotBase != null)
        {
            robotBase.SetMemoryVariable(Input1.VariableName, Input2.GetReporterStringValue());
        }
    }
}
