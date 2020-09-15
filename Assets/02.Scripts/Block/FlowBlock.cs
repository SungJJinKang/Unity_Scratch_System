[System.Serializable]
public abstract class FlowBlock : Block, FlowBlockType
{
    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return 0;
    }

    public void StartFlowBlock(RobotBase operatingRobotBase)
    {
        operatingRobotBase.SetWaitingBlock(this);

        float durationTime = this.GetDurationTime(operatingRobotBase);
        if (operatingRobotBase.WaitingTime > durationTime)
        {
            //RobotBase wait more than DurationTime
            //Can operate Block!!!!
            operatingRobotBase.WaitingTime -= durationTime; // Decrease WaitingTime Of RobotBase
        }
        else
        {
            //RobotBase should wait more
            return;
        }

        this.Operation(operatingRobotBase); // Operate Block Work

        //OnEndOperation
        this.EndFlowBlock(operatingRobotBase); // End This Flow Block, Maybe Next Block Called
    }

    public abstract void EndFlowBlock(RobotBase operatingRobotBase);

    public abstract void Operation(RobotBase operatingRobotBase);
}
