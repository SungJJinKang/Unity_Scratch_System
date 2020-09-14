/// <summary>
/// Definaton Block Of Custom Block
/// Why Make Seperately Each Class Have multipleParameter
/// -> For Checking Type Check 
/// If TwoParameter inherit OneParameter, 
/// (TwoParameter Is Onparameter) return True . This cause bugs
/// </summary>
[System.Serializable]
public sealed class CallNoParameterCustomBlock : CallCustomBlock
{
    private DefinitionNoParameterCustomBlock definitionNoParameterCustomBlock;

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        base.Operation(operatingRobotBase);
    }

    sealed protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionNoParameterCustomBlock = customBlockDefinitionBlock as DefinitionNoParameterCustomBlock;
    }
}
