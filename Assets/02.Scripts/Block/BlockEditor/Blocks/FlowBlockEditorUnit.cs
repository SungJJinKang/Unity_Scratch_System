using UnityEngine;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public abstract class FlowBlockEditorUnit : BlockEditorUnit
{
    public FlowBlock TargetFlowBlock => base.TargetBlock as FlowBlock; 

    public override void OnStartControlling()
    {
        base.OnStartControlling();

        if(this.PreviousFlowBlockEditorUnit != null)
        {
            //Dettach Controlling Block From PreviousBlock
            this.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
        }
        
    }

   


    [SerializeField]
    private BlockConnector PreviousBlockConnector;

    [SerializeField]
    private BlockConnector NextBlockConnector;

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
                //if (this.PreviousFlowBlockEditorUnit != null)
                //{
                //    this.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
                //}

                this.previousFlowBlockEditorUnit = value;

                if(this.previousFlowBlockEditorUnit != null)
                {
                    this.TargetFlowBlock.PreviousBlock = value.TargetFlowBlock; // Set To FlowBlock.NextBlock
                }
               

                //if (this.PreviousFlowBlockEditorUnit != null)
                //{
                //    this.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = this;
                //}
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
            if (IsNextBlockEditorUnitAssignable == true)
            {
                if (this.NextFlowBlockEditorUnit != null)
                {
                    this.NextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = null;

                    /*
                    if(value != null)
                    {
                        value.NextFlowBlockEditorUnit = this.NextFlowBlockEditorUnit; //set existing nextblock to nextblock of new nextblock
                    }
                    else
                    {
                        BlockEditorController.instance.SetBlockRoot(this);

                    }
                    */

                    this.nextFlowBlockEditorUnit = null;
                }

                this.nextFlowBlockEditorUnit = value;
                this.TargetFlowBlock.NextBlock = value?.TargetFlowBlock;

                if (this.nextFlowBlockEditorUnit != null)
                {
                    //Set Next block child of Previous block
                    value.transform.SetParent(this.NextBlockConnector.ConnectionPoint);
                    value._RectTransform.anchoredPosition = Vector2.zero;


                    if (this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit != null)
                    {
                        //set original nextblock of 
                        this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit.NextFlowBlockEditorUnit = null;
                    }
                    this.nextFlowBlockEditorUnit.PreviousFlowBlockEditorUnit = this;
                }
            }

        }
    }

    private List<FlowBlockEditorUnit> flowBlockEditorUnitListCache;
    /// <summary>
    /// Every ChildBlock
    /// </summary>
    private List<FlowBlockEditorUnit> ChildFlowBlockEditorUnitList
    {
        get
        {
            if (this.flowBlockEditorUnitListCache == null)
                this.flowBlockEditorUnitListCache = new List<FlowBlockEditorUnit>();

            this.flowBlockEditorUnitListCache.Clear();
            this.GetChildFlowBlockRecursive(ref this.flowBlockEditorUnitListCache);
            return this.flowBlockEditorUnitListCache;
        }
    }

    public FlowBlockEditorUnit RootBlock
    { 
        get
        {
            if(this.PreviousFlowBlockEditorUnit != null)
            {
                return this.PreviousFlowBlockEditorUnit.RootBlock;
            }
            else
            {
                return this;
            }
        }
    }

    public FlowBlockEditorUnit DescendantBlock
    {
        get
        {
            if (this.NextFlowBlockEditorUnit != null)
            {
                return this.NextFlowBlockEditorUnit.DescendantBlock;
            }
            else
            {
                return this;
            }
        }
    }


    private void GetChildFlowBlockRecursive(ref List<FlowBlockEditorUnit> flowBlockEditorUnits)
    {
        if(this.NextFlowBlockEditorUnit != null)
        {
            flowBlockEditorUnits.Add(this.NextFlowBlockEditorUnit);
            this.NextFlowBlockEditorUnit.GetChildFlowBlockRecursive(ref flowBlockEditorUnits);
        }
        
    }


    /// <summary>
    /// Don call this every tick, update
    /// </summary>
    /// <returns></returns>
    sealed public override bool IsAttatchable()
    {
        FlowBlockConnector flowBlockConnector = BlockEditorController.instance.GetTopBlockConnector<FlowBlockConnector>(transform.position, this);
        base.AttachableBlockConnector = null;

        if (flowBlockConnector == null || flowBlockConnector.ParentFlowBlockEditorUnit == this)
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
                this.NextFlowBlockEditorUnit = flowBlockConnector.ParentFlowBlockEditorUnit ;
            }
            else
            {

                flowBlockConnector.ParentFlowBlockEditorUnit.NextFlowBlockEditorUnit = this;
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
            if (flowBlockEditorUnit.PreviousFlowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.PreviousFlowBlockEditorUnit.name);
            }

            stringBuilder.Append("   |   ");

            if (flowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.name);
            }

            stringBuilder.Append("   |   ");

            if (flowBlockEditorUnit.NextFlowBlockEditorUnit != null)
            {
                stringBuilder.Append(flowBlockEditorUnit.NextFlowBlockEditorUnit.name);
            }

            stringBuilder.Append("   |   \n");

            this.DebugBlockUnitFlowRecursive(flowBlockEditorUnit.NextFlowBlockEditorUnit, ref stringBuilder);
        }
    }

    private void DebugBlockFlowRecursive(FlowBlock flowBlock, ref StringBuilder stringBuilder)
    {
        if (flowBlock != null)
        {
            if (flowBlock.PreviousBlock != null)
            {
                stringBuilder.Append(flowBlock.PreviousBlock.GetType().Name);
            }

            stringBuilder.Append("   |   ");

            if (flowBlock != null)
            {
                stringBuilder.Append(flowBlock.GetType().Name);
            }

            stringBuilder.Append("   |   ");

            if (flowBlock.NextBlock != null)
            {
                stringBuilder.Append(flowBlock.NextBlock.GetType().Name);
            }

            stringBuilder.Append("   |   \n");

            this.DebugBlockFlowRecursive(flowBlock.NextBlock, ref stringBuilder);
        }
    }
}


#endif