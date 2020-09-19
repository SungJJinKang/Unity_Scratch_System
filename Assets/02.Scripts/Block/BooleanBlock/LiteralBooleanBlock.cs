using System;

public sealed class LiteralBooleanBlock : BooleanBlock
{
    public LiteralBooleanBlock(bool value)
    {

        this.BooleanValue = value;
    }

    /// <summary>
    /// LiteralValue는 RobotSourceCode에서 정해지고 각 로봇에서는 변경안된다!!!!!!
    /// READ ONLY
    /// </summary>
    public readonly bool BooleanValue;

    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return this.BooleanValue;
    }
}
