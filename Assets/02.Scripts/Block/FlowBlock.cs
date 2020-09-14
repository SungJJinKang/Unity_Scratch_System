[System.Serializable]
public abstract class FlowBlock : Block, FlowBlockType
{
   

    public void StartFlowBlock(RobotBase operatingRobotBase)
    {
        base.OperatingRobotBase = operatingRobotBase;
        this.Operation(); // Operate Block Work
        this.EndFlowBlock(); // End This Flow Block, Maybe Next Block Called
    }

    public abstract void EndFlowBlock();

    public abstract void Operation();
}
