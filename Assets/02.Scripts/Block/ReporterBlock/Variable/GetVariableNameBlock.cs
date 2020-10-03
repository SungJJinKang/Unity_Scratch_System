/// <summary>
/// Get Variable Name Of VariableBlock(RobotBase.MemoryVariable)
/// </summary>
[BlockDefinitionAttribute("Variable Name of", BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector)]
public sealed class GetVariableNameBlock : ReporterBlock, IContainingParameter<ReporterBlock>, IVariableBlockType
{
    public ReporterBlock Input1 { get; set; }

    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        return this.Input1.GetReporterStringValue(operatingRobotBase);
    }
}
