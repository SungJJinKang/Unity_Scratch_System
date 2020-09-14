[System.Serializable]
public abstract class StackBlock : FlowBlock, UpNotchBlock, DownBumpBlock
{
    public DownBumpBlock PreviousBlock { get ; set; }

    /// <summary>
    /// Called After ExecuteCommand
    /// </summary>
    /// <value>The next block.</value>
    public UpNotchBlock NextBlock { get ; set ; }

    sealed public override void EndFlowBlock(RobotBase operatingRobotBase)
    {
        if (NextBlock != null)
            NextBlock.StartFlowBlock(operatingRobotBase);
    }

}
