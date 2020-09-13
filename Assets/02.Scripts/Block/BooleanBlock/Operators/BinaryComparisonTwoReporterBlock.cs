public abstract class BinaryComparisonTwoReporterBlock : BooleanBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IOperatorBlockType
{
    public ReporterBlock Input1 { get ; set ; }
    public ReporterBlock Input2 { get ; set ; }
}