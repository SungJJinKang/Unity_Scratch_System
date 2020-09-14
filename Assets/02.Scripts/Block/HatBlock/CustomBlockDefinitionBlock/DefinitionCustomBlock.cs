/// <summary>
/// Definaton Block Of Custom Block
/// Can Containing Parameter but only ReporterBlock Type
/// 
/// Thie Class instance Sould exist Just One Intance For Each CustomBlockDefinitionBlock
/// </summary>
[System.Serializable]
public abstract class DefinitionCustomBlock : HatBlock, IDefinitionCustomBlockType
{
    public string CustomBlockName;


    //Parameter Of Custom Block is Just LiteralBlock
    //Parameter Of Custom Block is set to passed String Value of ReporterBlock
}
