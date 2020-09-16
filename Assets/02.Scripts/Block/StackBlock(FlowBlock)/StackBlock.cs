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
    public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        if (this.NextBlock != null && this.NextBlock is FlowBlock)
        {
            operatingRobotBase.SetWaitingBlock(this.NextBlock as FlowBlock);
            return true;
        }
        else
        {
            return false;
        }

    }

}
