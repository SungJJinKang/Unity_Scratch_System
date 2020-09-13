public abstract class BinaryComparisonTwoBooleanBlock : BooleanBlock, IContainingParameter<BooleanBlock, BooleanBlock>, IOperatorBlockType
{
    public BooleanBlock Input1 { get; set; }
    public BooleanBlock Input2 { get; set; }
}