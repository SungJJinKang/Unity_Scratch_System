
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput, "and", BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput)]
public sealed class AndBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return base.Input1.GetBooleanValue(operatingRobotBase) && base.Input2.GetBooleanValue(operatingRobotBase);
    }
}
