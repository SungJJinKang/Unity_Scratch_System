/// <summary>
/// Hat Block
/// This Can be used as InitBlock, LoopBlock
/// </summary>
[System.Serializable]
public abstract class HatBlock : FlowBlock, DownBumpBlock
{
    public UpNotchBlock NextBlock { get; set; }


  

}
