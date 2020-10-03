[BlockDefinitionAttribute("set", BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector, "to", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class Memory_SetValue : StackBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IVariableBlockType
{
    /// <summary>
    /// Variable Key
    /// </summary>
    /// <value>The input1.</value>
    public ReporterBlock Input1 { get; set; }

    /// <summary>
    /// Data set
    /// </summary>
    /// <value>The input2.</value>
    public ReporterBlock Input2 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        if (operatingRobotBase != null)
        {
            operatingRobotBase.SetRobotGlobalVariable(Input1.GetReporterStringValue(operatingRobotBase), Input2.GetReporterStringValue(operatingRobotBase));
        }
    }



}
