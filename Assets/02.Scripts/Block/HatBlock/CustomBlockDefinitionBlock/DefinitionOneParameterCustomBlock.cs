/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionOneParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralBlock>
{
    public DefinitionOneParameterCustomBlock(string input1Name)
    {
        this.Input1Name = input1Name;
    }


    public LiteralBlock Input1 { get ; set ; }
    public string Input1Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock">Passed reporter block.</param>
    public void CopyParamter(ReporterBlock passedReporterBlock)
    {
        this.Input1 = new LiteralBlock(passedReporterBlock.GetReporterStringValue()); // 
    }

    public override void Operation()
    {
    }
}
