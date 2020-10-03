using UnityEngine;

[BlockDefinitionAttribute("Set Destination X : ", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, " Y : ", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class JetEngine_SetDestination : StackBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IJetEngineBlockType
{
    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }
    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        JetEngine jetEngine = operatingRobotBase.GetRobotPart<JetEngine>();
        if (jetEngine != null)
        {
            jetEngine.Destination = new Vector2(Input1.GetReporterNumberValue(operatingRobotBase), Input2.GetReporterNumberValue(operatingRobotBase));
        }

    }
}
