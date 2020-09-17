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
    public override DefinitionCustomBlock CustomBlockDefinitionBlock { get => definitionNoParameterCustomBlock as DefinitionCustomBlock; }

    public CallNoParameterCustomBlock(DefinitionNoParameterCustomBlock definitionNoParameterCustomBlock) 
    {
        this.definitionNoParameterCustomBlock = definitionNoParameterCustomBlock;
    }
   
   
}
