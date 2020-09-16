/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallThreeParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock, ReporterBlock>
{

    private DefinitionThreeParameterCustomBlock definitionThreeParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionThreeParameterCustomBlock as DefinitionCustomBlock; }

    public CallThreeParameterCustomBlock(DefinitionThreeParameterCustomBlock definitionThreeParameterCustomBlock)
    {
        this.definitionThreeParameterCustomBlock = definitionThreeParameterCustomBlock;
    }

    /// <summary>
    /// Passed Paramter 1
    /// </summary>
    public ReporterBlock Input1 { get; set; }
    /// <summary>
    /// Passed Paramter 2
    /// </summary>
    public ReporterBlock Input2 { get; set; }
    /// <summary>
    /// Passed Paramter 3
    /// </summary>
    public ReporterBlock Input3 { get; set; }


    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        //Set RobotBase.CustomBlockLocalVariables with Input String Value 
        if (this.Input1 != null)
            operatingRobotBase.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionThreeParameterCustomBlock.Input1Name, this.Input1.GetReporterStringValue(operatingRobotBase));

        if (this.Input2 != null)
            operatingRobotBase.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionThreeParameterCustomBlock.Input2Name, this.Input2.GetReporterStringValue(operatingRobotBase));

        if (this.Input3 != null)
            operatingRobotBase.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionThreeParameterCustomBlock.Input3Name, this.Input3.GetReporterStringValue(operatingRobotBase));

      
        base.Operation(operatingRobotBase);
    }

   
}
