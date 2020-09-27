public abstract class BinaryComparisonTwoBooleanBlock : BinaryComparisonBlock, IContainingParameter<BooleanBlock, BooleanBlock>
{
    public BooleanBlock Input1 { get; set; }
    public BooleanBlock Input2 { get; set; }
}