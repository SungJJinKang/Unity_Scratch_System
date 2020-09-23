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


    /// <summary>
    /// PreviousBlock
    /// </summary>
    private FlowBlock previousBlock;
    public bool IsHavePreviousBlock => this is IUpNotchBlock;
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
            //Don't Set this.previousBlock.NextBlock ~~~
            if (this.IsHavePreviousBlock == true)
            {
                if (this.PreviousBlock != null)
                    this.PreviousBlock.NextBlock = null;

                this.previousBlock = value;

                if (this.PreviousBlock != null)
                {
                    this.PreviousBlock.NextBlock = this;
                }
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
            //Never Set Value Of BlockEditorUnit From This Class
            // FlowBlock -> BlockEditorUnit XXXXXX
            // BlockEditorUnit -> FLowBlock OOOOOOO
            if (this.IsHaveNextBlock == true)
            {
                if (this.NextBlock != null)
                    this.NextBlock.PreviousBlock = null;

                this.nextBlock = value;

                if (this.NextBlock != null)
                {
                    this.NextBlock.PreviousBlock = this;
                }
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
        return this.NextBlock;
    }

    public abstract void Operation(RobotBase operatingRobotBase);


    private FlowBlock GetRootBlock(FlowBlock flowBlock)
    {
        if (this.PreviousBlock == null)
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
