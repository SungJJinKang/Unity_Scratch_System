/// <summary>
/// This works like Memory_GetValue
/// But Memory_GetValue doesn't exist. This replace that
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShop]
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector)]
public sealed class GetVariableValueBlock : ReporterBlock, IContainingParameter<ReporterBlock>, IVariableBlockType
{
    public ReporterBlock Input1 { get; set; }

    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {//Think How To Robotbase.StoredVariableBlock
        if (operatingRobotBase != null)
        {
            return operatingRobotBase.GetRobotGlobalVariable(Input1.GetReporterStringValue(operatingRobotBase));
        }
        else
        {
            return System.String.Empty;
        }
    }
}
