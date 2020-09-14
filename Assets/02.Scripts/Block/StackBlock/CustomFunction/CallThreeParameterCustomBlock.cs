/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallThreeParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock, ReporterBlock>
{

    private DefinitionThreeParameterCustomBlock definitionThreeParameterCustomBlock;

    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }
    public ReporterBlock Input3 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        this.definitionThreeParameterCustomBlock.CopyParamter(operatingRobotBase, this.Input1, this.Input2, this.Input3);
        base.Operation(operatingRobotBase);
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionThreeParameterCustomBlock = customBlockDefinitionBlock as DefinitionThreeParameterCustomBlock;
    }
}
