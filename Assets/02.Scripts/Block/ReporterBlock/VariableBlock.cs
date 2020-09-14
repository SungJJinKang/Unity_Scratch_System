[BlockTitle("Variable")]
[System.Serializable]
public sealed class VariableBlock : ReporterBlock, IVariableBlockType
{
    /// <summary>
    /// VariableValue는 각 로봇마다 다른 값을 가질 수 있다.
    /// Sync this value to Key of RobotBase.MemoryVariable Dictionary
    /// </summary>
    public string VariableName;

    sealed public override string GetReporterStringValue()
    {//Think How To Robotbase.StoredVariableBlock
        RobotBase robotBase = base.GetOperatingRobotBase();
        if (robotBase != null)
        {
            return robotBase.GetMemoryVariable(this.VariableName);
        }
        else
        {
            return "";
        }
    }
}
