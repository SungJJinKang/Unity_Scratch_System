[BlockTitle("Variable")]
[System.Serializable]
public sealed class VariableBlock : ReporterBlock, VariableBlockType
{
    /// <summary>
    /// VariableValue는 각 로봇마다 다른 값을 가질 수 있다.
    /// </summary>
    public string VariableName;

    sealed public override string GetReporterStringValue()
    {//Think How To Robotbase.StoredVariableBlock
        return "fd";
    }
}
