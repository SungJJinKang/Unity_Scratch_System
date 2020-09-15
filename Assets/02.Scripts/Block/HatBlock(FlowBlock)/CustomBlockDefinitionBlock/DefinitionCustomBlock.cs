/// <summary>
/// Definaton Block Of Custom Block
/// Can Containing Parameter but only ReporterBlock Type
/// 
/// Thie Class instance Sould exist Just One Intance For Each CustomBlockDefinitionBlock
/// </summary>
[System.Serializable]
public abstract class DefinitionCustomBlock : HatBlock, IDefinitionCustomBlockType
{
    public readonly string CustomBlockName;

    public DefinitionCustomBlock(string customBlockName)
    {
        this.CustomBlockName = customBlockName;
    }

    //Parameter Of Custom Block is Just LiteralBlock
    //Parameter Of Custom Block is set to passed String Value of ReporterBlock
}
