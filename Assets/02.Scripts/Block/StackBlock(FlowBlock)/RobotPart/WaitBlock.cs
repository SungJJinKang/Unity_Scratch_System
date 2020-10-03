[BlockDefinitionAttribute("Wait",BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "second")]
public sealed class WaitBlock : StackBlock, IContainingParameter<ReporterBlock>, IVariableBlockType
{
    public override float GetDurationTime(RobotBase operatingRobotBase)
    {
        return this.Input1.GetReporterNumberValue(operatingRobotBase);
    }


    /// <summary>
    /// Variable Key
    /// </summary>
    /// <value>The input1.</value>
    public ReporterBlock Input1 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
    }



}
