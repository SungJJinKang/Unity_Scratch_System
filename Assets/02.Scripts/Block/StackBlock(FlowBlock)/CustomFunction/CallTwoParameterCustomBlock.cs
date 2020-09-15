/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallTwoParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock>
{
    private DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionTwoParameterCustomBlock as DefinitionCustomBlock; }

    public CallTwoParameterCustomBlock(string customBlockName, DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock, ReporterBlock input1, ReporterBlock input2) : base(customBlockName)
    {
        this.definitionTwoParameterCustomBlock = definitionTwoParameterCustomBlock;
        Input1 = input1;
        Input2 = input2;
    }

    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        this.definitionTwoParameterCustomBlock.CopyParamter(operatingRobotBase, this.Input1, this.Input2);
        base.Operation(operatingRobotBase);
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionTwoParameterCustomBlock = customBlockDefinitionBlock as DefinitionTwoParameterCustomBlock;
    }
}
