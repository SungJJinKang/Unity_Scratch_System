/// <summary>
/// Hat Block
/// This Can be used as InitBlock, LoopBlock
/// </summary>
[System.Serializable]
public abstract class HatBlock : FlowBlock, DownBumpBlock
{
    public UpNotchBlock NextBlock { get; set; }

    sealed public override void EndFlowBlock()
    {
        if (NextBlock != null)
            NextBlock.StartFlowBlock(base.OperatingRobotBase);
    }



}
