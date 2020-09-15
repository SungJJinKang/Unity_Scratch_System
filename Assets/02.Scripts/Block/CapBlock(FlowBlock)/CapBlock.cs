[System.Serializable]
public abstract class CapBlock : FlowBlock, UpNotchBlock
{
    public DownBumpBlock PreviousBlock { get; set; }



    /// <summary>
    /// EndFlowBlock
    /// And Start NextBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    sealed public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        return false;

    }

}
