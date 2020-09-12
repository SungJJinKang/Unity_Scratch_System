[System.Serializable]
public abstract class CapBlock : Block, UpNotchBlock
{
    public DownBumpBlock PreviousBlock { get; set; }
}
