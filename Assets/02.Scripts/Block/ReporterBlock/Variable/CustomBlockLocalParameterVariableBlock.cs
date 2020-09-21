/// <summary>
/// This works like Memory_GetValue
/// But Memory_GetValue doesn't exist. This replace that
/// </summary>
[BlockTitle("CustomBlockLocalVariable")]
[System.Serializable]
[NotAutomaticallyMadeOnBlockShop]
public sealed class CustomBlockLocalParameterVariableBlock : ReporterBlock
{
    public DefinitionCustomBlock DefinitionCustomBlock;
    /// <summary>
    /// VariableValue는 각 로봇마다 다른 값을 가질 수 있다.
    /// Sync this value to Key of RobotBase.MemoryVariable Dictionary
    /// </summary>
    public string LocalVariableName;

    public CustomBlockLocalParameterVariableBlock(DefinitionCustomBlock definitionCustomBlock, string localVariableName)
    {
        this.DefinitionCustomBlock = definitionCustomBlock;
        this.LocalVariableName = localVariableName;
    }

    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {//Think How To Robotbase.StoredVariableBlock
        if (operatingRobotBase != null)
        {
            return operatingRobotBase.GetCustomBlockParameterVariables(DefinitionCustomBlock, this.LocalVariableName);
        }
        else
        {
            return System.String.Empty;
        }
    }

    public override Block CloneDeepCopy()
    {
        var block = (CustomBlockLocalParameterVariableBlock)base.CloneDeepCopy();
        block.LocalVariableName = this.LocalVariableName;

        return block;
    }

}
