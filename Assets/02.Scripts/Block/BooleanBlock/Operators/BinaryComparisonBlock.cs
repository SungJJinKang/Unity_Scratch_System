public abstract class BinaryComparisonBlock : BooleanBlock, BlockParameter<ReporterBlock, ReporterBlock>, OperatorBlockType
{
    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }
}