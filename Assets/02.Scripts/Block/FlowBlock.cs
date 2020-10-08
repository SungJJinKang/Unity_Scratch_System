using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public abstract class FlowBlock : Block
{

    public virtual float GetDurationTime(RobotBase operatingRobotBase)
    {
        return 1;
    }


    public bool IsHavePreviousBlock => this is IUpNotchBlock;

    /*
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
    */

    /// <summary>
    /// NextBlock
    /// </summary>
    private FlowBlock nextBlock;

    [JsonProperty]
    public int IndexInRobotSourceCode;
    
    public bool IsHaveNextBlock => this is IDownBumpBlock;
    [JsonProperty]
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

    public bool IsOperatable(RobotBase operatingRobotBase)
    {
        float durationTime = this.GetDurationTime(operatingRobotBase);
        return operatingRobotBase.WaitingTime >= durationTime - Mathf.Epsilon;
    }

    private bool IsOperatable(RobotBase operatingRobotBase, float durationTime)
    {
        return operatingRobotBase.WaitingTime >= durationTime - Mathf.Epsilon;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If Robot should wait more, return false
    /// otherwise, return true
    /// </returns>
    public FlowBlock StartFlowBlock(RobotBase operatingRobotBase)
    {
        float durationTime = this.GetDurationTime(operatingRobotBase);
        if (this.IsOperatable(operatingRobotBase))
        {
            //RobotBase wait more than DurationTime
            //Can operate Block!!!!
            operatingRobotBase.WaitingTime -= durationTime; // Decrease WaitingTime Of RobotBase
        }
        else
        {
            //RobotBase should wait more
            return null;
        }

        Debug.Log("Start Flow Block : " + GetType().Name);

        this.Operation(operatingRobotBase); // Operate Block Work
        return this.EndFlowBlock(operatingRobotBase);

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
