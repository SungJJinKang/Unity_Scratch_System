[System.Serializable]
public abstract class StackBlock : Block, UpNotchBlock, DownBumpBlock
{
    public DownBumpBlock PreviousBlock { get ; set; }
    public UpNotchBlock NextBlock { get ; set ; }
   
    public virtual void ExecuteCommand()
    {

    }
}
