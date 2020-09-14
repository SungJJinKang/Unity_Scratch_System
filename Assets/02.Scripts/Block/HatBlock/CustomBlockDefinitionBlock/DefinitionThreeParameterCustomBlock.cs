/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionThreeParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralBlock, LiteralBlock, LiteralBlock>
{

    public DefinitionThreeParameterCustomBlock(string inputName1, string inputName2, string inputName3)
    {
        this.Input1Name = inputName1;
        this.Input2Name = inputName2;
        this.Input3Name = inputName3;
    }

    public LiteralBlock Input1 { get; set; }
    public string Input1Name;

    public LiteralBlock Input2 { get; set; }
    public string Input2Name;

    public LiteralBlock Input3 { get; set; }
    public string Input3Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock1">Passed reporter block1.</param>
    /// <param name="passedReporterBlock2">Passed reporter block2.</param>
    /// <param name="passedReporterBlock3">Passed reporter block3.</param>
    public void CopyParamter(ReporterBlock passedReporterBlock1, ReporterBlock passedReporterBlock2, ReporterBlock passedReporterBlock3)
    {
        this.Input1 = new LiteralBlock(passedReporterBlock1.GetReporterStringValue()); // 
        this.Input2 = new LiteralBlock(passedReporterBlock2.GetReporterStringValue()); // 
        this.Input3 = new LiteralBlock(passedReporterBlock3.GetReporterStringValue()); // 
    }

    public override void Operation()
    {
    }
}
