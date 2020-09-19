/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionThreeParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralReporterBlock, LiteralReporterBlock, LiteralReporterBlock>
{

    public DefinitionThreeParameterCustomBlock(string customBlockName, string inputName1, string inputName2, string inputName3) : base(customBlockName)
    {
        this.Input1Name = inputName1;
        this.Input2Name = inputName2;
        this.Input3Name = inputName3;

        ParameterNames = new string[] { this.Input1Name, this.Input2Name, this.Input3Name };
    }

    public LiteralReporterBlock Input1 { get; set; }
    public string Input1Name;

    public LiteralReporterBlock Input2 { get; set; }
    public string Input2Name;

    public LiteralReporterBlock Input3 { get; set; }
    public string Input3Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock1">Passed reporter block1.</param>
    /// <param name="passedReporterBlock2">Passed reporter block2.</param>
    /// <param name="passedReporterBlock3">Passed reporter block3.</param>
    public void CopyParamter(RobotBase operatingRobotBase, ReporterBlock passedReporterBlock1, ReporterBlock passedReporterBlock2, ReporterBlock passedReporterBlock3)
    {
        this.Input1 = new LiteralReporterBlock(passedReporterBlock1.GetReporterStringValue(operatingRobotBase)); // 
        this.Input2 = new LiteralReporterBlock(passedReporterBlock2.GetReporterStringValue(operatingRobotBase)); // 
        this.Input3 = new LiteralReporterBlock(passedReporterBlock3.GetReporterStringValue(operatingRobotBase)); // 
    }

    public override void Operation(RobotBase operatingRobotBase)
    {
    }
}
