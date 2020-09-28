[BlockTitle("LiteralTrue")]
[BlockDefinitionAttribute("True")]
public sealed class LiteralTrueBooleanBlock : BooleanBlock, IOperatorBlockType
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return true;
    }


}
