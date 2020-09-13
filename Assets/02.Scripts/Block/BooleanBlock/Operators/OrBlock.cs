[BlockTitle("OrBlock")]
public sealed class OrBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue()
    {
        return base.Input1.GetBooleanValue() || base.Input2.GetBooleanValue();
    }
}
