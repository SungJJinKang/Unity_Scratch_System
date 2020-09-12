using System.Collections;

[System.Serializable]
public abstract class HatBlock : Block, DownBumpBlock
{
    public UpNotchBlock NextBlock { get; set; }

   
}
