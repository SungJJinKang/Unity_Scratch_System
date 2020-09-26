using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    public FlowBlock TargetFlowBlock => base.TargetBlock as FlowBlock;


    sealed public override void Release()
    {
        base.Release();

        if(this.NextFlowBlockEditorUnit != null)
        {
            this.NextFlowBlockEditorUnit.Release();
        }
    }



    [SerializeField]
    private BlockConnector PreviousBlockConnector;

    [SerializeField]
    private BlockConnector NextBlockConnector;

    public bool IsPreviousBlockEditorUnitAssignable => base.TargetBlock is IUpNotchBlock;

  

    [SerializeField]
    private FlowBlockEditorUnit previousFlowBlockEditorUnit;
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
                this.previousFlowBlockEditorUnit = value;
                this.TargetFlowBlock.PreviousBlock = value?.TargetFlowBlock;
            }
        }
    }

    [SerializeField]
    private FlowBlockEditorUnit nextFlowBlockEditorUnit;
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
            if (this.IsNextBlockEditorUnitAssignable)
            {
                this.nextFlowBlockEditorUnit = value;
                this.TargetFlowBlock.NextBlock = value?.TargetFlowBlock;
            }
        }
    }

    public override void MakeRootBlock()
    {
        base.MakeRootBlock();
        ConnectTwoBlock(null, this);
       
    }


    public static void ConnectTwoBlock(FlowBlockEditorUnit parentBlock, FlowBlockEditorUnit newChildBlock)
    {
        if(parentBlock == null && newChildBlock != null)
        {
            //disconnect block with parentblock

            if(newChildBlock.PreviousFlowBlockEditorUnit != null)
            {
                newChildBlock.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
                newChildBlock.PreviousFlowBlockEditorUnit = null;
            }
           

            BlockEditorController.instance.SetBlockRoot(newChildBlock);
            return;
        }

        if(parentBlock != null && newChildBlock == null)
        {
            //disconnect block with childblock

            if(parentBlock.NextFlowBlockEditorUnit != null)
            {
                parentBlock.NextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = null;
                parentBlock.NextFlowBlockEditorUnit = null;
            }
            return;
        }

        if (parentBlock != null && newChildBlock != null)
        {
            //FlowBlockEditorUnit OriginalChildBlockOfParentBlock = parentBlock.NextFlowBlockEditorUnit;

            //ConnectTwoBlock(parentBlock, null);
            //ConnectTwoBlock(null, newChildBlock);

            parentBlock.NextFlowBlockEditorUnit = newChildBlock;
            newChildBlock.PreviousFlowBlockEditorUnit = parentBlock;
            newChildBlock.transform.SetParent(parentBlock.NextBlockConnector.ConnectionPoint);
            newChildBlock._RectTransform.anchoredPosition = Vector2.zero;

            return;
            //ConnectTwoBlock(newChildBlock.DescendantBlockUnit, OriginalChildBlockOfParentBlock);
        }


        /*
            FlowBlockEditorUnit OriginalChildBlockOfParentBlock = parentBlock.NextFlowBlockEditorUnit;

        if(OriginalChildBlockOfParentBlock != null)
        {
            //Disconnect ParentBlock with Original ChildBlock Of ParentBlock
            OriginalChildBlockOfParentBlock.DetachFromParentBlock();
        }

        if(newChildBlock != null)
        { 
            //Disconnect newChildBlock with original parent blcck of newChildBlock
            newChildBlock.DetachFromParentBlock();

            parentBlock.nextFlowBlockEditorUnit = newChildBlock;
            newChildBlock.previousFlowBlockEditorUnit = parentBlock;

            newChildBlock.transform.SetParent(parentBlock.NextBlockConnector.ConnectionPoint);
            newChildBlock._RectTransform.anchoredPosition = Vector2.zero;

            //connect OriginalChildBlockOfParentBlock with Descendant of newChildBlock
            ConnectTwoBlock(newChildBlock.DescendantBlockUnit, OriginalChildBlockOfParentBlock);
        }
        */
    }

    public FlowBlockEditorUnit DescendantBlockUnit
    {
        get
        {
            if (this.NextFlowBlockEditorUnit != null)
            {
                return this.NextFlowBlockEditorUnit.DescendantBlockUnit;
            }
            else
            {
                return this;
            }
        }
    }

    private List<FlowBlockEditorUnit> childBlockList;
    private List<FlowBlockEditorUnit> ChildBlockList
    { 
        get
        {
            if (this.childBlockList == null)
                this.childBlockList = new List<FlowBlockEditorUnit>();
            else
                this.childBlockList.Clear();

            this.GetDescendantBlockList(ref this.childBlockList);
            return this.childBlockList;
        }
    }

    private void GetDescendantBlockList(ref List<FlowBlockEditorUnit> descendantBlocks)
    {
        if(this.NextFlowBlockEditorUnit != null)
        {
            descendantBlocks.Add(NextFlowBlockEditorUnit);
            this.NextFlowBlockEditorUnit.GetDescendantBlockList(ref descendantBlocks);
        }
    }
    


    /// <summary>
    /// Don call this every tick, update
    /// </summary>
    /// <returns></returns>
    sealed public override bool IsAttatchable()
    {
        List<FlowBlockEditorUnit> exceptedCheckBlockList = this.ChildBlockList;
        exceptedCheckBlockList.Add(this);

        FlowBlockConnector flowBlockConnector = BlockEditorController.instance.GetTopBlockConnector<FlowBlockConnector>(transform.position, exceptedCheckBlockList);

        if(flowBlockConnector != null)
            //Debug.Log(flowBlockConnector.OwnerFlowBlockEditorUnit.name);

        base.AttachableBlockConnector = null;

        if (flowBlockConnector == null || flowBlockConnector.OwnerFlowBlockEditorUnit == this)
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
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                ConnectTwoBlock(flowBlockConnector.OwnerFlowBlockEditorUnit.PreviousFlowBlockEditorUnit, this);
                ConnectTwoBlock(this.DescendantBlockUnit, flowBlockConnector.OwnerFlowBlockEditorUnit);

            }
            else
            {
                FlowBlockEditorUnit originalNextBlock = flowBlockConnector.OwnerFlowBlockEditorUnit.NextFlowBlockEditorUnit;
                ConnectTwoBlock(flowBlockConnector.OwnerFlowBlockEditorUnit, this);
                ConnectTwoBlock(this.DescendantBlockUnit, originalNextBlock);
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

   

#endif
}

#if UNITY_EDITOR

[CustomEditor(typeof(FlowBlockEditorUnit), true)]
public class FlowBlockEditorUnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        FlowBlockEditorUnit targetFlowBlockEditorUnit = base.target as FlowBlockEditorUnit;

        if (GUILayout.Button("Debug Block Unit Flow"))
        {
            StringBuilder stringBuilder = Utility.stringBuilderCache;
            stringBuilder.Clear();

            DebugBlockUnitFlowRecursive(targetFlowBlockEditorUnit, ref stringBuilder);
            Debug.Log(stringBuilder.ToString());
        }

        if (GUILayout.Button("Debug Block Flow"))
        {
            StringBuilder stringBuilder = Utility.stringBuilderCache;
            stringBuilder.Clear();

            DebugBlockFlowRecursive(targetFlowBlockEditorUnit.TargetFlowBlock, ref stringBuilder);
            Debug.Log(stringBuilder.ToString());
        }


    }

    private void DebugBlockUnitFlowRecursive(FlowBlockEditorUnit flowBlockEditorUnit, ref StringBuilder stringBuilder)
    {
        if (flowBlockEditorUnit != null)
        {
            if (flowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.name);
            }

            stringBuilder.Append("\n|\n|\n");

            this.DebugBlockUnitFlowRecursive(flowBlockEditorUnit.NextFlowBlockEditorUnit, ref stringBuilder);
        }
    }

    private void DebugBlockFlowRecursive(FlowBlock flowBlock, ref StringBuilder stringBuilder)
    {
        if (flowBlock != null)
        { 
            if (flowBlock != null)
            {
                stringBuilder.Append(flowBlock.GetType().Name);
            }

            stringBuilder.Append("\n|\n|\n");


            this.DebugBlockFlowRecursive(flowBlock.NextBlock, ref stringBuilder);
        }
    }
}


#endif