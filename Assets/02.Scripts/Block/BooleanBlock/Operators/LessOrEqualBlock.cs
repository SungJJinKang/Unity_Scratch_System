[BlockTitle("LessOrEqual")]
public sealed class LessOrEqualBlock : BinaryComparisonBlock
{
    sealed public override bool GetBooleanValue()
    {
        return base.Input1.GetReporterNumberValue() <= base.Input2.GetReporterNumberValue();
    }
}
