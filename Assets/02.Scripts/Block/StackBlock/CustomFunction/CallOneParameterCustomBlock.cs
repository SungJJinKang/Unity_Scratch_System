/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallOneParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock>
{
    private DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock;

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
