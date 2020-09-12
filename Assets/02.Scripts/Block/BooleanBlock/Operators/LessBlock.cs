[BlockTitle("Less")]
public sealed class LessBlock : BinaryComparisonBlock
{
    sealed public override bool GetBooleanValue()
    {
        return base.Input1.GetReporterNumberValue() < base.Input2.GetReporterNumberValue();
    }
}
