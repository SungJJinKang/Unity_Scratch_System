[BlockTitle("OrBlock")]
public sealed class OrBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return base.Input1.GetBooleanValue(operatingRobotBase) || base.Input2.GetBooleanValue(operatingRobotBase);
    }
}
