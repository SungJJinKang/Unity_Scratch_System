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
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        if (NextBlock != null)
        {
            NextBlock.StartFlowBlock(operatingRobotBase);
            return true;
        }
        else
        {
            return false;
        }
            
    }

}
