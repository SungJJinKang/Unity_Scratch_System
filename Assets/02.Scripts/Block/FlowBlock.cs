using UnityEngine;

[System.Serializable]
public abstract class FlowBlock : Block
{

    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return 1;
    }


    public bool IsHavePreviousBlock => this is IUpNotchBlock;


    private FlowBlock previousBlock;
    public FlowBlock PreviousBlock
    {
        get
        {
            if (this.IsHavePreviousBlock == true)
            {
                return this.previousBlock;
            }
            else
            {
                return null;
            }

        }
        set
        {
            if (this.IsHavePreviousBlock == true)
            {
                this.previousBlock = value;
            }
        }
    }


    /// <summary>
    /// NextBlock
    /// </summary>
    private FlowBlock nextBlock;
    public bool IsHaveNextBlock => this is IDownBumpBlock;
    public FlowBlock NextBlock
    {
        get
        {
            if (this.IsHaveNextBlock == true)
            {
                return this.nextBlock;
            }
            else
            {
                return null;
            }

        }
        set
        {
            if (this.IsHaveNextBlock == true)
            {
                this.nextBlock = value;
            }
        }
    }



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
        float durationTime = this.GetDurationTime(operatingRobotBase);
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

        Debug.Log("Start Flow Block : " + GetType().Name);

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
        return this.NextBlock;
    }

    public abstract void Operation(RobotBase operatingRobotBase);


    /// <summary>
    /// return last DescendantBlock
    /// </summary>
    public FlowBlock LastDescendantBlock
    {
        get
        {
            if (this.NextBlock != null)
            {
                return this.NextBlock;
            }
            else
            {
                return this;
            }
        }
    }




}


/// <summary>
/// This Block can have PreviousBlock
/// StackBlock, C Block, CapBlock
/// </summary>
public interface IUpNotchBlock
{

}

/// <summary>
/// This Block can have NextBlock
/// Hat Block, StackBlock, C Block
/// </summary>
public interface IDownBumpBlock
{


}
