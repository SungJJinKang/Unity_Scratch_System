using UnityEngine;

[BlockDefinitionAttribute("Random Number Min", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "Max", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class GetRandomNumberBlock : ReporterBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IOperatorBlockType
{
    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }

    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        return Random.Range(this.Input1.GetReporterNumberValue(operatingRobotBase), this.Input2.GetReporterNumberValue(operatingRobotBase)).ToString();
    }
}
