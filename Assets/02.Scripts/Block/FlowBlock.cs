using System.Runtime.InteropServices.WindowsRuntime;

[System.Serializable]
public abstract class FlowBlock : Block, FlowBlockType
{
   

    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return RobotSystem.ExecuteRobotsWaitingBlockRate;
    }

    public enum FlowBlockState
    {
        WaitDurationTime,
        StartNextBlockAfterOperation,

        /// <summary>
        /// There is no Next Block
        /// </summary>
        ExitFlowAfterOperation

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
            return FlowBlock.FlowBlockState.ExitFlowAfterOperation;
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


    private FlowBlock GetRootBlock(FlowBlock flowBlock)
    {
        UpNotchBlock upNotchBlock = this as UpNotchBlock;
        if (upNotchBlock == null || upNotchBlock.PreviousBlock == null)
        {
            return this;
        }
        else
        {
            return this.GetRootBlock(flowBlock);
        }
    }

    public FlowBlock GetRootBlock()
    {
        return GetRootBlock(this);

    }

}
