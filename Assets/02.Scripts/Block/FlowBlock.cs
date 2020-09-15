[System.Serializable]
public abstract class FlowBlock : Block, FlowBlockType
{
   

    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return 0;
    }

    public enum FlowBlockState
    {
        WaitDurationTime,
        StartNextBlockAfterOperation,
        EndFlowAfterOperation
    
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// After Operating This Block, If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    public FlowBlockState StartFlowBlock(RobotBase operatingRobotBase)
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
            return  FlowBlock.FlowBlockState.WaitDurationTime;
        }

        this.Operation(operatingRobotBase); // Operate Block Work

        //OnEndOperation
        if (this.EndFlowBlock(operatingRobotBase) == true)
        {// End Operation. Start Next Block
            return FlowBlock.FlowBlockState.StartNextBlockAfterOperation;
        }
        else
        {// End Operation. End FlowBlock, Because There is no Next Block
            return FlowBlock.FlowBlockState.EndFlowAfterOperation;
        }
        
    }




    /// <summary>
    /// EndFlowBlock
    /// And Start NextBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    public abstract bool EndFlowBlock(RobotBase operatingRobotBase);

    public abstract void Operation(RobotBase operatingRobotBase);
}
