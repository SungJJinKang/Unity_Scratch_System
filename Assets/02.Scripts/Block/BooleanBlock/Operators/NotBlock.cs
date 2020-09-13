﻿[BlockTitle("NotBlock")]
public sealed class NotBlock : BooleanBlock, IContainingParameter<BooleanBlock>
{
    public BooleanBlock Input1 { get ; set ; }

    sealed public override bool GetBooleanValue()
    {
        return !this.Input1.GetBooleanValue();
    }
}
