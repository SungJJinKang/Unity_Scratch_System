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
    public bool BooleanValue;

    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return this.BooleanValue;
    }

    public override object Clone()
    {
        var block = (LiteralBooleanBlock)base.Clone();
        block.BooleanValue = this.BooleanValue;

        return block;
    }


}
