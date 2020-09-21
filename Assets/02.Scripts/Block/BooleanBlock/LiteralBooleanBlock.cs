using System;
[NotAutomaticallyMadeOnBlockShop]
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

    public override Block CloneDeepCopy()
    {
        var block = (LiteralBooleanBlock)base.CloneDeepCopy();
        block.BooleanValue = this.BooleanValue;

        return block;
    }


}
