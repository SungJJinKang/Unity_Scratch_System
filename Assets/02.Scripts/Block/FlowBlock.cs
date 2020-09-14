[System.Serializable]
public abstract class FlowBlock : Block, FlowBlockType
{
   

    public void StartFlowBlock(RobotBase operatingRobotBase)
    {
        this.Operation(operatingRobotBase); // Operate Block Work
        this.EndFlowBlock(operatingRobotBase); // End This Flow Block, Maybe Next Block Called
    }

    public abstract void EndFlowBlock(RobotBase operatingRobotBase);

    public abstract void Operation(RobotBase operatingRobotBase);
}
