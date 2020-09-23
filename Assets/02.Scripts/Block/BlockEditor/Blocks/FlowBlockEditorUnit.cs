using UnityEngine;

public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    protected FlowBlock targetFlowBlock { get => base.TargetBlock as FlowBlock; }


    [SerializeField]
    private FlowBlockEditorUnit previousFlowBlockEditorUnit;
    public bool IsHavePreviousBlockEditorUnit => typeof(IUpNotchBlock).IsAssignableFrom(base.TargetBlockType);
    public FlowBlockEditorUnit PreviousFlowBlockEditorUnit
    {
        get
        {
            if (this.IsHavePreviousBlockEditorUnit)
            {
                return this.previousFlowBlockEditorUnit;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (this.IsHavePreviousBlockEditorUnit)
            {
                //Don't Set this.previousFlowBlockEditorUnit.NextFlowBlockEditorUnit ~~~
                if (this.PreviousFlowBlockEditorUnit != null)
                {
                    this.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
                }

                this.previousFlowBlockEditorUnit = value;
                this.targetFlowBlock.PreviousBlock = value.targetFlowBlock; // Set To FlowBlock.NextBlock

                if (this.PreviousFlowBlockEditorUnit != null)
                {
                    this.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = this;
                }
            }
        }

    }

    [SerializeField]
    private FlowBlockEditorUnit nextFlowBlockEditorUnit;
    public bool IsHaveNextBlockEditorUnit => typeof(IDownBumpBlock).IsAssignableFrom(base.TargetBlockType);
    public FlowBlockEditorUnit NextFlowBlockEditorUnit
    {
        get
        {
            if (this.IsHaveNextBlockEditorUnit)
            {
                return this.nextFlowBlockEditorUnit;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (IsHaveNextBlockEditorUnit == true)
            {
                //Never Set Value Of BlockEditorUnit From This Class
                // FlowBlock -> BlockEditorUnit XXXXXX
                // BlockEditorUnit -> FLowBlock OOOOOOO
                if (this.NextFlowBlockEditorUnit != null)
                {
                    this.NextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = null;
                }

                this.nextFlowBlockEditorUnit = value;
                this.targetFlowBlock.NextBlock = value.targetFlowBlock; // Set To FlowBlock.NextBlock

                if (this.nextFlowBlockEditorUnit != null)
                {
                    this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = this;
                }
            }

        }
    }

   
}

