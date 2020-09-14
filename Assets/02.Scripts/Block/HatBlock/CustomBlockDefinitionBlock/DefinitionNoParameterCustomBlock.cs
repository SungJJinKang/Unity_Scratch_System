/// <summary>
/// Definaton Block Of Custom Block
/// Why Make Seperately Each Class Have multipleParameter
/// -> For Checking Type Check 
/// If TwoParameter inherit OneParameter, 
/// (TwoParameter Is Onparameter) return True . This cause bugs
/// </summary>
[System.Serializable]
public sealed class DefinitionNoParameterCustomBlock : DefinitionCustomBlock
{
    public override void Operation(RobotBase operatingRobotBase)
    {
    }
}
