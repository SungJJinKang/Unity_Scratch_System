/// <summary>
/// Hat Block
/// This Can be used as InitBlock, LoopBlock
/// </summary>
[System.Serializable]
public abstract class HatBlock : FlowBlock, DownBumpBlock
{
    public UpNotchBlock NextBlock { get; set; }


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
