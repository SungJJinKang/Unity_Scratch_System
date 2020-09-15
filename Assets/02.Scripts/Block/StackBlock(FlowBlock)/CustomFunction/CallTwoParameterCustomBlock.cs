/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallTwoParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock>
{
    private DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionTwoParameterCustomBlock as DefinitionCustomBlock; }

    public CallTwoParameterCustomBlock(DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock)
    { 
        this.definitionTwoParameterCustomBlock = definitionTwoParameterCustomBlock;
    }

    /// <summary>
    /// Passed Paramter 1
    /// </summary>
    public ReporterBlock Input1 { get; set; }
    /// <summary>
    /// Passed Paramter 2
    /// </summary>
    public ReporterBlock Input2 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        if (this.Input1 != null)
            operatingRobotBase.SetCustomBlockLocalVariables(definitionTwoParameterCustomBlock.Input1Name, this.Input1.GetReporterStringValue(operatingRobotBase));

        if (this.Input2 != null)
            operatingRobotBase.SetCustomBlockLocalVariables(definitionTwoParameterCustomBlock.Input2Name, this.Input2.GetReporterStringValue(operatingRobotBase));

      
        base.Operation(operatingRobotBase);
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionTwoParameterCustomBlock = customBlockDefinitionBlock as DefinitionTwoParameterCustomBlock;
    }
}
