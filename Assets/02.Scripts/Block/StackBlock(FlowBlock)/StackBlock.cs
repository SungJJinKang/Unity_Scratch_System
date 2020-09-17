[System.Serializable]
public abstract class StackBlock : FlowBlock, UpNotchBlock, DownBumpBlock
{
    public DownBumpBlock PreviousBlock { get ; set; }

    /// <summary>
    /// Called After ExecuteCommand
    /// </summary>
    /// <value>The next block.</value>
    public UpNotchBlock NextBlock { get ; set ; }


    /// <summary>
    /// EndFlowBlock
    /// And Start NextBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    public override FlowBlock EndFlowBlock(RobotBase operatingRobotBase)
    {
        return this.NextBlock as FlowBlock;
    }

}
