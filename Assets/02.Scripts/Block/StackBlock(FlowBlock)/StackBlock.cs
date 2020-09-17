[System.Serializable]
public abstract class StackBlock : FlowBlock, UpNotchBlock, DownBumpBlock
{
    public FlowBlock PreviousBlock { get ; set; }

    /// <summary>
    /// Called After ExecuteCommand
    /// </summary>
    /// <value>The next block.</value>
    public FlowBlock NextBlock { get ; set ; }


   

}
