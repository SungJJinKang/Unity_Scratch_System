using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBlockEditorUnit : BlockEditorUnit
{
    protected FlowBlock targetFlowBlock { get => base.TargetBlock as FlowBlock; }


    [SerializeField]
    private FlowBlockEditorUnit previousFlowBlockEditorUnit;
    public bool IsHavePreviousBlockEditorUnit => typeof(IUpNotchBlock).IsAssignableFrom(base.BlockEditorUnitAttribute.BlockEditorUnitType);
    public FlowBlockEditorUnit PreviousFlowBlockEditorUnit
    {
        get
        {
            if(this.IsHavePreviousBlockEditorUnit)
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
                this.previousFlowBlockEditorUnit = value;
            }
        }

    }

    [SerializeField]
    private FlowBlockEditorUnit nextFlowBlockEditorUnit;
    public bool IsHaveNextBlockEditorUnit => typeof(IDownBumpBlock).IsAssignableFrom(base.BlockEditorUnitAttribute.BlockEditorUnitType);
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
             //Never Set Value Of BlockEditorUnit From This Class
            // FlowBlock -> BlockEditorUnit XXXXXX
            // BlockEditorUnit -> FLowBlock OOOOOOO
            if (this.nextFlowBlockEditorUnit != null)
                this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = null;

            this.nextFlowBlockEditorUnit = value;
            this.targetFlowBlock.NextBlock = value.targetFlowBlock; // Set To FlowBlock.NextBlock

            if (this.nextFlowBlockEditorUnit != null)
            {
                this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = this;
            }
        }
    }

    protected override void CleanBlockEditorUnit()
    {
        base.CleanBlockEditorUnit();

        //Clean Previous, Next FlowBlock Editor Unit(!!!) 
        //Please Use Object Pool
    }
}

