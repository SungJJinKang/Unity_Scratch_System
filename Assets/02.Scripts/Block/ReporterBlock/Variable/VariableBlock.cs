/// <summary>
/// This works like Memory_GetValue
/// But Memory_GetValue doesn't exist. This replace that
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShop]
public sealed class VariableBlock : ReporterBlock, IVariableBlockType
{
    /// <summary>
    /// VariableValue는 각 로봇마다 다른 값을 가질 수 있다.
    /// Sync this value to Key of RobotBase.MemoryVariable Dictionary
    /// </summary>
    public string VariableName;

    public VariableBlock(string variableName)
    {
        this.VariableName = variableName;
    }

    public override Block Clone()
    {
        var block = (VariableBlock)base.Clone();
        block.VariableName = this.VariableName;

        return block;
    }


    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {//Think How To Robotbase.StoredVariableBlock
        if (operatingRobotBase != null)
        {
            return operatingRobotBase.GetRobotGlobalVariable(this.VariableName);
        }
        else
        {
            return System.String.Empty;
        }
    }
}
