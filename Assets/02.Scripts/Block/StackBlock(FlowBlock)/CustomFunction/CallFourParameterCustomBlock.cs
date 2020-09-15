/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallFourParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock, ReporterBlock, ReporterBlock>
{

    private DefinitionFourParameterCustomBlock definitionFourParameterCustomBlock;

    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionFourParameterCustomBlock as DefinitionCustomBlock; }

    public CallFourParameterCustomBlock(string customBlockName, DefinitionFourParameterCustomBlock definitionFourParameterCustomBlock, ReporterBlock input1, ReporterBlock input2, ReporterBlock input3, ReporterBlock input4) : base(customBlockName)
    {
        this.definitionFourParameterCustomBlock = definitionFourParameterCustomBlock;
        Input1 = input1;
        Input2 = input2;
        Input3 = input3;
        Input4 = input4;
    }

    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }
    public ReporterBlock Input3 { get; set; }
    public ReporterBlock Input4 { get; set; }
   
    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        this.definitionFourParameterCustomBlock.CopyParamter(operatingRobotBase, this.Input1, this.Input2, this.Input3, this.Input4);
        base.Operation(operatingRobotBase);
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionFourParameterCustomBlock = customBlockDefinitionBlock as DefinitionFourParameterCustomBlock;
    }
}
