using UnityEngine;

[System.Serializable]
public abstract class FlowBlock : Block
{

    /// <summary>
    /// Should greater than 0
    /// </summary>
    [SerializeField]
    private float durationTime;
    public float DurationTime
    {
        get
        {
            return this.durationTime;
        }
    }
    

    /*
    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return RobotSystem.ExecuteRobotsWaitingBlockRate;
    }
    */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If Robot should wait more, return false
    /// otherwise, return true
    /// </returns>
    public bool StartFlowBlock(RobotBase operatingRobotBase, out FlowBlock NextBlock)
    {
        float durationTime = DurationTime;
        if (operatingRobotBase.WaitingTime >= durationTime - Mathf.Epsilon)
        {
            //RobotBase wait more than DurationTime
            //Can operate Block!!!!
            operatingRobotBase.WaitingTime -= durationTime; // Decrease WaitingTime Of RobotBase
        }
        else
        {
            //RobotBase should wait more
            NextBlock = null;
            return false;
        }

        this.Operation(operatingRobotBase); // Operate Block Work
        NextBlock = this.EndFlowBlock(operatingRobotBase);
        return true;
       
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
    public virtual FlowBlock EndFlowBlock(RobotBase operatingRobotBase)
    {
        DownBumpBlock downBumpBlock = this as DownBumpBlock;
        if (downBumpBlock == null)
        {
            return null;
        }
        else
        {
            return downBumpBlock?.NextBlock as FlowBlock;
        }
    }

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
