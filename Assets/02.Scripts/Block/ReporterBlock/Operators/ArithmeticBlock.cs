public abstract class ArithmeticBlock : ReporterBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IOperatorBlockType
{
    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }

}
