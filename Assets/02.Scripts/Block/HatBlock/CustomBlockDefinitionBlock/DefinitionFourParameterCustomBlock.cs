/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionFourParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralBlock, LiteralBlock, LiteralBlock, LiteralBlock>
{
    public DefinitionFourParameterCustomBlock(string inputName1, string inputName2, string inputName3, string inputName4)
    {
        this.Input1Name = inputName1;
        this.Input2Name = inputName2;
        this.Input3Name = inputName3;
        this.Input4Name = inputName4;
    }

    public LiteralBlock Input1 { get; set; }
    public string Input1Name;

    public LiteralBlock Input2 { get; set; }
    public string Input2Name;

    public LiteralBlock Input3 { get; set; }
    public string Input3Name;

    public LiteralBlock Input4 { get; set; }
    public string Input4Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock1">Passed reporter block1.</param>
    /// <param name="passedReporterBlock2">Passed reporter block2.</param>
    /// <param name="passedReporterBlock3">Passed reporter block3.</param>
    /// <param name="passedReporterBlock4">Passed reporter block4.</param>
    public void CopyParamter(RobotBase operatingRobotBase, ReporterBlock passedReporterBlock1, ReporterBlock passedReporterBlock2, ReporterBlock passedReporterBlock3, ReporterBlock passedReporterBlock4)
    {
        this.Input1 = new LiteralBlock(passedReporterBlock1.GetReporterStringValue(operatingRobotBase)); // 
        this.Input2 = new LiteralBlock(passedReporterBlock2.GetReporterStringValue(operatingRobotBase)); // 
        this.Input3 = new LiteralBlock(passedReporterBlock3.GetReporterStringValue(operatingRobotBase)); // 
        this.Input4 = new LiteralBlock(passedReporterBlock4.GetReporterStringValue(operatingRobotBase)); // 
    }

    public override void Operation(RobotBase operatingRobotBase)
    {
    }
}
