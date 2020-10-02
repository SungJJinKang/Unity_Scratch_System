
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "<", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class LessBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return base.Input1.GetReporterNumberValue(operatingRobotBase) < base.Input2.GetReporterNumberValue(operatingRobotBase);
    }
}
