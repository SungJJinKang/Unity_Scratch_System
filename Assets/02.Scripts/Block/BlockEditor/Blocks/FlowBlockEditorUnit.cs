using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    public FlowBlock TargetFlowBlock => base.TargetBlock as FlowBlock;

    sealed public override BlockEditorElement ParentBlockEditorElement => this.PreviousFlowBlockEditorUnit;

    public override void OnStartControllingByPlayer()
    {
        base.OnStartControllingByPlayer();
        ConnectFlowBlockEditorUnit(null, this);

    }

    sealed public override void Release()
    {
        if (this.NextFlowBlockEditorUnit != null)
        {
            this.NextFlowBlockEditorUnit.Release();
        }

        this.previousFlowBlockEditorUnit = null;
        this.nextFlowBlockEditorUnit = null;


        base.Release();


    }

    /// <summary>
    /// Create MockUp Under IAttachableEditorElement
    /// </summary>
    /// <param name="flowBlockConnector"></param>
    public void CreateFlowBlockMockUp(FlowBlockConnector flowBlockConnector, int siblingIndex = 0)
    {
        if (flowBlockConnector == null)
            return;

        BlockMockupHelper spawnedMockUp = PoolManager.SpawnObject(BlockEditorController.instance.BlockMockUpPrefab).GetComponent<BlockMockupHelper>();
        BlockEditorController.instance.AddToSpawnedBlockMockUp(spawnedMockUp);


        spawnedMockUp.CopyTargetBlock(this);

        Transform attachPointRectTransform = flowBlockConnector.AttachPointRectTransform;


        spawnedMockUp._RectTransform.SetParent(attachPointRectTransform);
        spawnedMockUp._RectTransform.SetAsFirstSibling();
        spawnedMockUp._RectTransform.SetSiblingIndex(siblingIndex);

        spawnedMockUp._RectTransform.localScale = Vector3.one;
        //if copyBlockEditorUnit is FlowBlockEditorUnit
        //SetBlockMockUp nextblock of FlowBlockEditorUnit
        if (this.NextFlowBlockEditorUnit != null)
        {
            this.NextFlowBlockEditorUnit.CreateFlowBlockMockUp(flowBlockConnector, siblingIndex + 1);
        }


    }



    [SerializeField]
    private FlowBlockConnector PreviousBlockConnector;

    [SerializeField]
    public FlowBlockConnector NextBlockConnector;

    public bool IsPreviousBlockEditorUnitAssignable => base.TargetBlock is IUpNotchBlock;


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



    public FlowBlockEditorUnit LowestDescendantBlockUnit
    {
        get
        {
            if (this.NextFlowBlockEditorUnit != null)
            {
                return this.NextFlowBlockEditorUnit.LowestDescendantBlockUnit;
            }
            else
            {
                return this;
            }
        }
    }

    private List<FlowBlockEditorUnit> descendantBlockList;
    private List<FlowBlockEditorUnit> DescendantBlockList
    {
        get
        {
            if (this.descendantBlockList == null)
                this.descendantBlockList = new List<FlowBlockEditorUnit>();
            else
                this.descendantBlockList.Clear();

            this.GetDescendantBlock(ref this.descendantBlockList);
            return this.descendantBlockList;
        }
    }

    private void GetDescendantBlock(ref List<FlowBlockEditorUnit> descendantBlocks)
    {
        if (this.NextFlowBlockEditorUnit != null)
        {
            descendantBlocks.Add(NextFlowBlockEditorUnit);
            this.NextFlowBlockEditorUnit.GetDescendantBlock(ref descendantBlocks);
        }
    }

    /// <summary>
    /// Return DescendantBlock Count
    /// this doesn't include this block
    /// </summary>
    public int DescendantBlockCount
    {
        get
        {
            int count = 0;
            FlowBlockEditorUnit flowBlockEditorUnit = this.NextFlowBlockEditorUnit;
            while(flowBlockEditorUnit != null)
            {
                count++;
                flowBlockEditorUnit = flowBlockEditorUnit.NextFlowBlockEditorUnit;
            }
            return count;
        }
    }


    #region AttachBlock



    public static void ConnectFlowBlockEditorUnit(FlowBlockEditorUnit parentBlock, FlowBlockEditorUnit newChildBlock)
    {
        if (parentBlock == null && newChildBlock != null)
        {
            //disconnect block with parentblock

            if (newChildBlock.PreviousFlowBlockEditorUnit != null)
            {
                newChildBlock.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
                newChildBlock.PreviousFlowBlockEditorUnit = null;
            }

            BlockEditorController.instance.SetBlockHoverOnBlockWorkSpaceContentBody(newChildBlock);
            return;
        }

        if (parentBlock != null && newChildBlock == null)
        {
            if (parentBlock.NextFlowBlockEditorUnit != null)
            {
                parentBlock.NextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = null;
                parentBlock.NextFlowBlockEditorUnit = null;
            }
            return;
        }

        if (parentBlock != null && newChildBlock != null)
        {
            if (parentBlock.IsNextBlockEditorUnitAssignable && newChildBlock.IsPreviousBlockEditorUnitAssignable)
            {
                parentBlock.NextFlowBlockEditorUnit = newChildBlock;
                newChildBlock.PreviousFlowBlockEditorUnit = parentBlock;
                newChildBlock.transform.SetParent(parentBlock.NextBlockConnector.AttachPointRectTransform);
                newChildBlock._RectTransform.anchoredPosition = Vector2.zero;
            }
            else
            {
                parentBlock.NextFlowBlockEditorUnit = null;
                newChildBlock.PreviousFlowBlockEditorUnit = null;
            }

            return;
        }
    }


    /// <summary>
    /// Don call this every tick, update
    /// </summary>
    /// <returns></returns>
    sealed public override bool IsAttatchable()
    {
        FlowBlockConnector.ConnectorType expectedConnectorTypeFlag = FlowBlockConnector.ConnectorType.None;

        if (IsPreviousBlockEditorUnitAssignable == true && PreviousFlowBlockEditorUnit == null)
        {
            expectedConnectorTypeFlag |= FlowBlockConnector.ConnectorType.DownBump;
        }
        if (IsNextBlockEditorUnitAssignable == true && NextFlowBlockEditorUnit == null)
        {
            expectedConnectorTypeFlag |= FlowBlockConnector.ConnectorType.UpNotch;
        }


        FlowBlockConnector flowBlockConnector = this.GetTopFlowBlockConnector(transform.position, expectedConnectorTypeFlag);
        if (flowBlockConnector == null && this.NextFlowBlockEditorUnit != null)
        {//if Fail to find flowblockconnector

            //Find Top Block Connector at LowestDescendantBlock Postion
            FlowBlockEditorUnit lowestDescendantBlockUnit = this.LowestDescendantBlockUnit; 
            flowBlockConnector = this.GetTopFlowBlockConnector(lowestDescendantBlockUnit.transform.position, FlowBlockConnector.ConnectorType.UpNotch); 
        }


        if (flowBlockConnector == null || flowBlockConnector.OwnerFlowBlockEditorUnit == this || flowBlockConnector.OwnerBlockEditorUnit.IsShopBlock == true)
        {
            base.AttachableEditorElement = null;
            return false;
        }
        else
        {
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                if (this.IsNextBlockEditorUnitAssignable)
                {
                    base.AttachableEditorElement = flowBlockConnector;
                    return true;
                }
                else
                {
                    base.AttachableEditorElement = null;
                    return false;
                }
            }
            else
            {//if hit connector is down bump type
                if (this.IsPreviousBlockEditorUnitAssignable)
                {
                    base.AttachableEditorElement = flowBlockConnector;
                    return true;
                }
                else
                {
                    base.AttachableEditorElement = null;
                    return false;
                }
            }
        }

    }

   

    sealed public override bool AttachBlock()
    {
        if (this.AttachableEditorElement != null)
        {
            FlowBlockConnector flowBlockConnector = this.AttachableEditorElement as FlowBlockConnector;
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                ConnectFlowBlockEditorUnit(flowBlockConnector.OwnerFlowBlockEditorUnit.PreviousFlowBlockEditorUnit, this);
                ConnectFlowBlockEditorUnit(this.LowestDescendantBlockUnit ?? this, flowBlockConnector.OwnerFlowBlockEditorUnit);

            }
            else
            {
                FlowBlockEditorUnit originalNextBlock = flowBlockConnector.OwnerFlowBlockEditorUnit.NextFlowBlockEditorUnit;
                ConnectFlowBlockEditorUnit(flowBlockConnector.OwnerFlowBlockEditorUnit, this);
                ConnectFlowBlockEditorUnit(this.LowestDescendantBlockUnit ?? this, originalNextBlock);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the top flow block connector.
    /// </summary>
    /// <returns>The top flow block connector.</returns>
    /// <param name="worldPoint">World position.</param>
    /// <param name="exceptedUnitList">Excepted unit list.</param>
    /// <param name="expectedConnectorType">Expected connector type. if this value is 2 , UpNotch, DownBump is ok</param>
    private FlowBlockConnector GetTopFlowBlockConnector(Vector2 worldPoint, FlowBlockConnector.ConnectorType expectedConnectorTypeFlag)
    {
        return UiUtility.GetTopBlockEditorElementWithWorldPoint<FlowBlockConnector>(worldPoint, FlowBlockConnector.FlowBlockConnectorTag, x => expectedConnectorTypeFlag.HasFlag(x._ConnectorType) == true);
    }

    #endregion



#if UNITY_EDITOR

    private string debugStr;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(_RectTransform.rect, this.debugStr, BlockEditorController.instance._GUIStyle);

    }

   

#endif
}

#region EDITOR
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
            if (flowBlockEditorUnit.PreviousFlowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.PreviousFlowBlockEditorUnit.name);
            }

            stringBuilder.Append("   ---   ");

            if (flowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.name);
            }

            stringBuilder.Append("   ---   ");

            if (flowBlockEditorUnit.NextFlowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.NextFlowBlockEditorUnit.name);
            }

            stringBuilder.Append("\n");

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
#endregion