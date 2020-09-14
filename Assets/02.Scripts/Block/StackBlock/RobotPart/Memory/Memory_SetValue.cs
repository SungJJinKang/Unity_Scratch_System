[BlockTitle("SetEnginePower")]
public sealed class Memory_SetValue : StackBlock, IContainingParameter<VariableBlock, ReporterBlock>, IVariableBlockType
{
    /// <summary>
    /// Variable Key
    /// </summary>
    /// <value>The input1.</value>
    public VariableBlock Input1 { get ; set; }

    /// <summary>
    /// Data set
    /// </summary>
    /// <value>The input2.</value>
    public ReporterBlock Input2 { get ; set; }

    sealed public override void Operation()
    {
        RobotBase robotBase = base.GetOperatingRobotBase();
        if (robotBase != null)
        {
            robotBase.SetMemoryVariable(Input1.VariableName, Input2.GetReporterStringValue());
        }
    }
}
