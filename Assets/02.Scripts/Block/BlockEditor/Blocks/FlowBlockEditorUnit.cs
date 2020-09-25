using UnityEngine;

public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    protected FlowBlock targetFlowBlock { get => base.TargetBlock as FlowBlock; }


    [SerializeField]
    private FlowBlockEditorUnit previousFlowBlockEditorUnit;
    public bool IsPreviousBlockEditorUnitAssignable => typeof(IUpNotchBlock).IsAssignableFrom(base.TargetBlockType);
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
    public bool IsNextBlockEditorUnitAssignable => typeof(IDownBumpBlock).IsAssignableFrom(base.TargetBlockType);
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
        FlowBlockConnector flowBlockConnector = BlockEditorController.instance.GetTopFlowBlockConnector(RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position + Vector3.right * OffsetX));

        if (flowBlockConnector == null || flowBlockConnector._FlowBlockEditorUnit == this)
        {
            return false;
        }
        else
        {
            if(flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {
                if(this.IsNextBlockEditorUnitAssignable)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (this.IsPreviousBlockEditorUnitAssignable)
                {
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
        return true;
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

