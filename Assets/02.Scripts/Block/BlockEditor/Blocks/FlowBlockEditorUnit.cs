using UnityEngine;

public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    protected FlowBlock targetFlowBlock { get => base.TargetBlock as FlowBlock; }


    [SerializeField]
    private FlowBlockEditorUnit previousFlowBlockEditorUnit;
    public bool IsPreviousBlockEditorUnitAssignable => base.TargetBlock is IUpNotchBlock;
    public FlowBlockEditorUnit PreviousFlowBlockEditorUnit
    {
        get
        {
            if (this.IsPreviousBlockEditorUnitAssignable)
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
            if (this.IsPreviousBlockEditorUnitAssignable)
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
    private bool isNextBlockEditorUnitAssignableCahe;
    public bool IsNextBlockEditorUnitAssignable => base.TargetBlock is IDownBumpBlock;
    public FlowBlockEditorUnit NextFlowBlockEditorUnit
    {
        get
        {
            if (this.IsNextBlockEditorUnitAssignable)
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
            if (IsNextBlockEditorUnitAssignable == true)
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

    private const float OffsetX = 25f;

    /// <summary>
    /// Don call this every tick, update
    /// </summary>
    /// <returns></returns>
    sealed public override bool IsAttatchable()
    {
        FlowBlockConnector flowBlockConnector = BlockEditorController.instance.GetTopBlockConnector<FlowBlockConnector>(RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position));
        base.AttachableBlockConnector = null;

        if (flowBlockConnector == null || flowBlockConnector.ParentBlockEditorUnit == this)
        {
            return false;
        }
        else
        {
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                if(this.IsNextBlockEditorUnitAssignable)
                {
                    base.AttachableBlockConnector = flowBlockConnector;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {//if hit connector is down bump type
                if (this.IsPreviousBlockEditorUnitAssignable)
                {
                    base.AttachableBlockConnector = flowBlockConnector;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

    sealed public override bool AttachBlock()
    {
        if (base.AttachableBlockConnector != null)
        {
            FlowBlockConnector flowBlockConnector = base.AttachableBlockConnector as FlowBlockConnector;
            FlowBlockEditorUnit targetFlowBlockEditorUnit = flowBlockConnector.ParentBlockEditorUnit as FlowBlockEditorUnit;
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                targetFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = this;
            }
            else
            {

                targetFlowBlockEditorUnit.NextFlowBlockEditorUnit = this;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    sealed public override Vector3 GetAttachPoint()
    {
        return Vector3.zero;
    }

#if UNITY_EDITOR

    private string debugStr;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(_RectTransform.rect, this.debugStr, BlockEditorController.instance._GUIStyle);

    }

    private void OnDrawGizmos()
    {
        //RectTransformUtility.ScreenPointToWorldPointInRectangle()
        Gizmos.DrawSphere(_RectTransform.position + Vector3.right * OffsetX, 10f);
    }

#endif
}

