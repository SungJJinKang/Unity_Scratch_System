/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallOneParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock>
{
    private DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionOneParameterCustomBlock as DefinitionCustomBlock; }

    public CallOneParameterCustomBlock(string customBlockName, DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock, ReporterBlock input1) : base(customBlockName)
    {
        this.definitionOneParameterCustomBlock = definitionOneParameterCustomBlock;
        Input1 = input1;
    }

    public ReporterBlock Input1 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        this.definitionOneParameterCustomBlock.CopyParamter(operatingRobotBase, this.Input1);
        base.Operation(operatingRobotBase);
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionOneParameterCustomBlock = customBlockDefinitionBlock as DefinitionOneParameterCustomBlock;
    }
}
