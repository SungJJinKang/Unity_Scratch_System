using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockEditorManager : MonoBehaviour
{

    public static BlockEditorManager instnace;

    private void Awake()
    {
        instnace = this;
    }

    private void Start()
    {
        InitBlockEditorSystem();
    }



    private RobotSourceCode editingRobotSourceCode;
    public RobotSourceCode EditingRobotSourceCode
    {
        get
        {
            return this.editingRobotSourceCode;
        }
        set
        {
            this.editingRobotSourceCode = value;

            if (this.editingRobotSourceCode != null)
            {
                this.InitCustomBlockOnBlockShop();
                this.InitRobotGlobalVariableOnBlockShop();

                this.InitBlockWorkSpace();
            }
        }
    }


    private void InitBlockEditorSystem()
    {
        WarmPoolBlockEditorUnit();
        WarmPoolInitElementOfBlockUnit();

        InitBlockShop();

        System.GC.Collect();
    }

    #region BlockShop
    [SerializeField]
    private Transform BlockShopContentTransform;
    private void InitBlockShop()
    {
        foreach (Type type in BlockReflector.GetAllSealedBlockTypeContainingBlockTitleAttribute())
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                Debug.LogWarning(" \" " + type.Name + " \" Dont Have Default Constructor");
                continue; // If Type don't have default constructor, continue loop
            }

            
            if(type.GetCustomAttribute(typeof(NotAutomaticallyMadeOnBlockShopAttribute), true) != null)
            {
                Debug.LogWarning(" \" " + type.Name + " \" Containing NotAutomaticallyMadeOnBlockShopAttribute");
                continue;
            }
               

          
            this.CreateBlockEditorUnit(type, this.BlockShopContentTransform);
            
            

        }
    }

    private void InitCustomBlockOnBlockShop()
    {
        if (this.EditingRobotSourceCode == null)
            return;
    }

    private void InitRobotGlobalVariableOnBlockShop()
    {
        if (this.EditingRobotSourceCode == null)
            return;
    }

    #endregion

    #region BlockWorkSpace

    [SerializeField]
    private Transform BlockWorkSpaceContentTransform;

    /// <summary>
    /// Match One Block Instance To One BlockEditorUnit Object(Instance)
    /// </summary>
    private Dictionary<Block, BlockEditorUnit> SpawnedBlockEditorUnitDictionary;
    private bool AddToSpawnedBlockEditorUnitList(Block block, BlockEditorUnit blockEditorUnit)
    {
        if (this.SpawnedBlockEditorUnitDictionary.ContainsKey(block) == true)
            return false;

        this.SpawnedBlockEditorUnitDictionary.Add(block, blockEditorUnit);
        return true;
    }


    private void InitBlockWorkSpace()
    {
        if (this.EditingRobotSourceCode == null)
            return;

        //Spawn Hat Blocks
        this.SpawnBlockEditorUnitOnBlockWorkSpace(this.EditingRobotSourceCode.InitBlock);
        this.SpawnBlockEditorUnitOnBlockWorkSpace(this.EditingRobotSourceCode.LoopedBlock);

        foreach(var eventBlock in this.EditingRobotSourceCode.StoredEventBlocks)
        {
            this.SpawnBlockEditorUnitOnBlockWorkSpace(eventBlock); 
        }
        //
    }

    /// <summary>
    /// Spawn Hat Block On Block Editor Work Space
    /// </summary>
    /// <param name="hatBlock"></param>
    private void SpawnBlockEditorUnitOnBlockWorkSpace(HatBlock hatBlock)
    {
        if (hatBlock == null)
            return;

        this.CreateBlockEditorUnit(hatBlock.GetType(), this.BlockWorkSpaceContentTransform);
        this.SpawnBlockEditorUnitOnBlockWorkSpace(hatBlock, hatBlock.NextBlock);
    }

    /// <summary>
    /// Used When Spawn Next Block, and Attach next block to DownBumpBlock
    /// </summary>
    /// <param name="downBumpBlock">Block what nextBlock attach to</param>
    /// <param name="nextBlock">Block Attached To DownBumpBlock</param>
    private void SpawnBlockEditorUnitOnBlockWorkSpace(FlowBlock parentBlock, FlowBlock childBlock)
    {
        if (parentBlock == null || childBlock == null)
            return;


    }

    /// <summary>
    /// Used When SpawnValueBlock, And Attach it to Parameter Input Of Other Blocks(IContainingParameter)
    /// </summary>
    /// <param name="blockContainingParameter">Block Containing Parameter</param>
    /// <param name="parameterIndex">Index Of Parameter what valueBlock wiil be attached to</param>
    /// <param name="valueBlock">Passed Parameter Blcok</param>
    private void SpawnBlockEditorUnitOnBlockWorkSpace(IContainingParameter blockContainingParameter, int parameterIndex, ValueBlock valueBlock)
    {
        if (blockContainingParameter == null || valueBlock == null)
            return;
    }

    #endregion

    #region BlockEditorUnit

    private const int BlockEditorUnitPoolCount = 3;
    private void WarmPoolBlockEditorUnit()
    {
        PoolManager.WarmPool(booleanBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
        PoolManager.WarmPool(capBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
        PoolManager.WarmPool(cBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
        PoolManager.WarmPool(hatBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
        PoolManager.WarmPool(reporterBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
        PoolManager.WarmPool(stackBlockEditorUnit?.gameObject, BlockEditorUnitPoolCount);
    }

    [Header("Block Editor Unit")]
    [SerializeField]
    private BooleanBlockEditorUnit booleanBlockEditorUnit;
    [SerializeField]
    private CapBlockEditorUnit capBlockEditorUnit;
    [SerializeField]
    private CBlockEditorUnit cBlockEditorUnit;
    [SerializeField]
    private HatBlockEditorUnit hatBlockEditorUnit;
    [SerializeField]
    private ReporterBlockEditorUnit reporterBlockEditorUnit;
    [SerializeField]
    private StackBlockEditorUnit stackBlockEditorUnit;


    /// <summary>
    /// Create New Block Instance with blockType And Create BlockEditorUnit
    /// </summary>
    /// <param name="blockType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private BlockEditorUnit CreateBlockEditorUnit(Type blockType, Transform parent)
    {
        Block block = Activator.CreateInstance(blockType) as Block;
        return this.CreateBlockEditorUnit(block, parent);
    }

    /// <summary>
    /// CreateBlockEditorUnit With Block Instance
    /// </summary>
    /// <param name="block"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private BlockEditorUnit CreateBlockEditorUnit(Block block, Transform parent)
    {
        Type blockType = block.GetType();

        if (blockType.IsSubclassOf(typeof(Block)) == false)
            return null;

        BlockEditorUnit blockEditorUnit = null;
        if (blockType.IsSubclassOf(typeof(BooleanBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(booleanBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CapBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(capBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(cBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(HatBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(hatBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(ReporterBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(reporterBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(StackBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(stackBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }

        if (blockEditorUnit == null)
        {//Creating BlockEditorUnit Fail!!
            Debug.LogError("Cant Find Proper Type : " + blockType.Name);
        }
        else
        {//Creating BlockEditorUnit Success!!
            blockEditorUnit.gameObject.name = blockType.Name;

            if (parent != null)
            {
                blockEditorUnit.transform.SetParent(parent);
            }

            blockEditorUnit.transform.localScale = Vector3.one;

            blockEditorUnit.TargetBlock = block;
        }


        return blockEditorUnit;

    }

    #endregion

    #region ElementOfBlockUnit

    private const int ElementOfBlockUnitPoolCount = 3;
    private void WarmPoolInitElementOfBlockUnit()
    {
        PoolManager.WarmPool(booleanBlockInputInBlockElement?.gameObject, ElementOfBlockUnitPoolCount);
        PoolManager.WarmPool(globalVariableSelectorDropDownInBlockElement?.gameObject, ElementOfBlockUnitPoolCount);
        PoolManager.WarmPool(reporterBlockInputInBlockElement?.gameObject, ElementOfBlockUnitPoolCount);
        PoolManager.WarmPool(textInBlockElement?.gameObject, ElementOfBlockUnitPoolCount);
    }

    [Header("ElementOfBlockUnit")]
    [SerializeField]
    private BooleanBlockInputOfBlockUnit booleanBlockInputInBlockElement;
    [SerializeField]
    private GlobalVariableSelectorDropDownOfBlockUnit globalVariableSelectorDropDownInBlockElement;
    [SerializeField]
    private ReporterBlockInputOfBlockUnit reporterBlockInputInBlockElement;
    [SerializeField]
    private TextElementOfBlockUnit textInBlockElement;

    public ElementOfBlockUnit CreateElementOfBlockUnit(Type t)
    {
        ElementOfBlockUnit elementOfBlockUnit = null;
        if (t == typeof(BooleanBlockInputContent))
        {
            elementOfBlockUnit = PoolManager.SpawnObject(booleanBlockInputInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (t == typeof(GlobalVariableSelectorDropDownContent))
        {
            elementOfBlockUnit = PoolManager.SpawnObject(globalVariableSelectorDropDownInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (t == typeof(ReporterBlockInputContent))
        {
            elementOfBlockUnit = PoolManager.SpawnObject(reporterBlockInputInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (t == typeof(TextElementContent))
        {
            elementOfBlockUnit = PoolManager.SpawnObject(textInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }

        if (elementOfBlockUnit == null)
            Debug.LogError("Cant Find Proper Type : " + t.Name);

        return elementOfBlockUnit;
    }
    #endregion



    public void ConnectFlowBlockEditorUnit(FlowBlockEditorUnit previousBlockEditorUnit, FlowBlockEditorUnit nextBlockEditorUnit)
    {
        previousBlockEditorUnit.NextFlowBlockEditorUnit = nextBlockEditorUnit;
        nextBlockEditorUnit.PreviousFlowBlockEditorUnit = previousBlockEditorUnit;
    }

    private void CreateBlockTemplate()
    {

    }
}

[CustomEditor(typeof(BlockEditorManager))]
public class BlockEditorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(30);

        if(GUILayout.Button("Create RobotSourceCode"))
        {
            RobotSystem.instance.CreateRobotSourceCode(System.DateTime.Now.ToString());
        }
    }

}
