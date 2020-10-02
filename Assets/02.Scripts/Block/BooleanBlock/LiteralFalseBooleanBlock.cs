[BlockDefinitionAttribute("False")]
public sealed class LiteralFalseBooleanBlock : BooleanBlock, IOperatorBlockType
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return false;
    }


}
