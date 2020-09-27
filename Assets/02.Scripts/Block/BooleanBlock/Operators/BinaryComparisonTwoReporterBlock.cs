public abstract class BinaryComparisonTwoReporterBlock : BinaryComparisonBlock, IContainingParameter<ReporterBlock, ReporterBlock>
{
    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }
}