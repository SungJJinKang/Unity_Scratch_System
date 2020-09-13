[BlockTitle("Equal")]
public sealed class EqualBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue()
    {
        return base.Input1.GetReporterNumberValue().Equals(base.Input2.GetReporterNumberValue());
    }
}
