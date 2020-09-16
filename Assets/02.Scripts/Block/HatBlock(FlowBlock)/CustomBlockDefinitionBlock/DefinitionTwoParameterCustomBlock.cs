/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionTwoParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralBlock, LiteralBlock>
{
    public DefinitionTwoParameterCustomBlock(string customBlockName, string inputName1, string inputName2) : base(customBlockName)
    {
        this.Input1Name = inputName1;
        this.Input2Name = inputName2;

        ParameterNames = new string[] { this.Input1Name, this.Input2Name };
    }

    public LiteralBlock Input1 { get ; set ; }
    public string Input1Name;

    public LiteralBlock Input2 { get; set; }
    public string Input2Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock1">Passed reporter block1.</param>
    /// <param name="passedReporterBlock2">Passed reporter block2.</param>
    public void CopyParamter(RobotBase operatingRobotBase, ReporterBlock passedReporterBlock1, ReporterBlock passedReporterBlock2)
    {
        this.Input1 = new LiteralBlock(passedReporterBlock1.GetReporterStringValue(operatingRobotBase)); // 
        this.Input2 = new LiteralBlock(passedReporterBlock2.GetReporterStringValue(operatingRobotBase)); // 
    }

    public override void Operation(RobotBase operatingRobotBase)
    {
    }
}
