/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallOneParameterCustomBlock : CallCustomFunctionBlock, IContainingParameter<ReporterBlock>
{
    private DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock;

    public ReporterBlock Input1 { get; set; }

    sealed public override void Operation()
    {
        this.definitionOneParameterCustomBlock.CopyParamter(this.Input1);
        base.Operation();
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionOneParameterCustomBlock = customBlockDefinitionBlock as DefinitionOneParameterCustomBlock;
    }
}
