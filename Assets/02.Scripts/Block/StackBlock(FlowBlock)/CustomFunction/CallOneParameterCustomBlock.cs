/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallOneParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock>
{
    private DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionOneParameterCustomBlock as DefinitionCustomBlock; }

    public CallOneParameterCustomBlock(DefinitionOneParameterCustomBlock definitionOneParameterCustomBlock)
    {
        this.definitionOneParameterCustomBlock = definitionOneParameterCustomBlock;
    }

    /// <summary>
    /// Passed Paramter 1
    /// </summary>
    public ReporterBlock Input1 { get; set; }


    sealed protected override void PassParameterToOperatingRobotBase(RobotBase operatingRobotBase)
    {
        //Set RobotBase.CustomBlockLocalVariables with Input String Value 
        if (this.Input1 != null)
            operatingRobotBase.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionOneParameterCustomBlock.Input1Name, this.Input1.GetReporterStringValue(operatingRobotBase));

    }
   
}
