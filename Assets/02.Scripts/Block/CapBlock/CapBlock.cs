[System.Serializable]
public abstract class CapBlock : FlowBlock, UpNotchBlock
{
    public DownBumpBlock PreviousBlock { get; set; }


    sealed public override void EndFlowBlock()
    {
        //DO NOTHING
        //Because CapBlock don't have next block
    }

}
