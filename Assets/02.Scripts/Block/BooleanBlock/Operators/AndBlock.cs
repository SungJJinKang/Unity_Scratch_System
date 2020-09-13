[BlockTitle("AndBlock")]
public sealed class AndBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue()
    {
        return base.Input1.GetBooleanValue() && base.Input2.GetBooleanValue();
    }
}
