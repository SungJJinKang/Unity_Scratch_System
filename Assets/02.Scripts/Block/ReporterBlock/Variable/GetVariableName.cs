/// <summary>
/// Get Variable Name Of VariableBlock(RobotBase.MemoryVariable)
/// </summary>
public sealed class GetVariableName : ReporterBlock, IContainingParameter<VariableBlock>, IVariableBlockType
{
    public VariableBlock Input1 { get; set; }

    public override string GetReporterStringValue()
    {
        return this.Input1.VariableName;
    }
}
