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
        this.SpawnedBlockEditorUnitDictionary = new Dictionary<Block, BlockEditorUnit>();
    }

    private void Start()
    {
        this.InitBlockEditorSystem();

        this.EditingRobotSourceCode = new RobotSourceCode("123");
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


            if (type.GetCustomAttribute(typeof(NotAutomaticallyMadeOnBlockShopAttribute), true) != null)
            {
                Debug.LogWarning(" \" " + type.Name + " \" Containing NotAutomaticallyMadeOnBlockShopAttribute");
                continue;
            }



            BlockEditorUnit createdBlockEditorUnit = this.CreateBlockEditorUnit(type, this.BlockShopContentTransform);
            if (createdBlockEditorUnit != null)
            {
                createdBlockEditorUnit.IsShopBlock = true;
            }


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
        BlockEditorUnit initBlockEditorUnit = this.SpawnBlockEditorUnitOnBlockWorkSpace(this.EditingRobotSourceCode.InitBlock);
        BlockEditorUnit loopedBlockEditorUnit = this.SpawnBlockEditorUnitOnBlockWorkSpace(this.EditingRobotSourceCode.LoopedBlock);

        if(initBlockEditorUnit != null)
        {
            initBlockEditorUnit._RectTransform.anchoredPosition = Vector2.zero;
            initBlockEditorUnit.IsRemovable = false;
            initBlockEditorUnit.BackupUiTransform();
        }

        if (loopedBlockEditorUnit != null)
        {
            loopedBlockEditorUnit._RectTransform.anchoredPosition = Vector2.zero;
            loopedBlockEditorUnit.IsRemovable = false;
            loopedBlockEditorUnit.BackupUiTransform();
        }


        EventBlock[] eventBlocks = this.EditingRobotSourceCode.StoredEventBlocks;
        if (eventBlocks != null)
        {
            foreach (var eventBlock in this.EditingRobotSourceCode.StoredEventBlocks)
            {
                this.SpawnBlockEditorUnitOnBlockWorkSpace(eventBlock);
            }
        }
       
        //
    }

    /// <summary>
    /// Spawn Hat Block On Block Editor Work Space
    /// </summary>
    /// <param name="hatBlock"></param>
    private BlockEditorUnit SpawnBlockEditorUnitOnBlockWorkSpace(HatBlock hatBlock)
    {
        if (hatBlock == null)
            return null;

        BlockEditorUnit blockEditorUnit = this.CreateBlockEditorUnit(hatBlock, this.BlockWorkSpaceContentTransform);
        this.SpawnBlockEditorUnitOnBlockWorkSpace(hatBlock, hatBlock.NextBlock);
        return blockEditorUnit;
    }

    /// <summary>
    /// Used When Spawn Next Block, and Attach next block to DownBumpBlock
    /// </summary>
    /// <param name="parentBlock">Block what nextBlock attach to</param>
    /// <param name="childBlock">Block Attached To DownBumpBlock</param>
    private void SpawnBlockEditorUnitOnBlockWorkSpace(FlowBlock parentBlock, FlowBlock childBlock)
    {
        if (parentBlock == null || childBlock == null)
            return;

        FlowBlockEditorUnit parentBlockEditorUnit = null;
        if (this.SpawnedBlockEditorUnitDictionary.ContainsKey(parentBlock) == false)
        {
            parentBlockEditorUnit = this.CreateBlockEditorUnit(parentBlock, this.BlockWorkSpaceContentTransform) as FlowBlockEditorUnit;
        }
        else
        {
            parentBlockEditorUnit = this.SpawnedBlockEditorUnitDictionary[parentBlock] as FlowBlockEditorUnit;
        }

        FlowBlockEditorUnit childBlockEditorUnit = null;
        if (this.SpawnedBlockEditorUnitDictionary.ContainsKey(childBlock) == false)
        {
            childBlockEditorUnit = this.CreateBlockEditorUnit(childBlock, this.BlockWorkSpaceContentTransform) as FlowBlockEditorUnit;
        }
        else
        {
            childBlockEditorUnit = this.SpawnedBlockEditorUnitDictionary[childBlock] as FlowBlockEditorUnit;
        }

        if (parentBlockEditorUnit != null && childBlockEditorUnit != null)
        {
            parentBlockEditorUnit.NextFlowBlockEditorUnit = childBlockEditorUnit;
        }
        else
        {
            if (parentBlockEditorUnit == null)
            {
                Debug.LogError("parentBlockEditorUnit is null");
            }

            if (childBlockEditorUnit == null)
            {
                Debug.LogError("schildBlockEditorUnit is null");
            }

            return;
        }
    }


    #endregion

    #region BlockEditorUnit




    /// <summary>
    /// Create New Block Instance with blockType And Create BlockEditorUnit
    /// </summary>
    /// <param name="blockType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public BlockEditorUnit CreateBlockEditorUnit(Type blockType, Transform parent = null)
    {
        if (blockType == null)
            return null;

        return this.CreateBlockEditorUnit(Block.CreatBlock(blockType), parent);
    }

    /// <summary>
    /// CreateBlockEditorUnit With Block Instance
    /// </summary>
    /// <param name="block"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    private BlockEditorUnit CreateBlockEditorUnit(Block block, Transform parent = null)
    {
        if (block == null)
        {
            Debug.LogError("block is null");
            return null;
        }

        Type blockType = block.GetType();

        if (blockType.IsSubclassOf(typeof(Block)) == false)
        {
            Debug.LogError(blockType.Name + " is not subclass of Block");
            return null;
        }


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

            if (blockEditorUnit.TargetBlock.IsAllParameterFilled)
            {

            }

            this.AddToSpawnedBlockEditorUnitList(blockEditorUnit.TargetBlock, blockEditorUnit);
        }


        return blockEditorUnit;

    }

    #endregion






    #region BlockEdidtorElementObjectPool 

    private void WarmPoolBlockEditorUnit()
    {
        PoolManager.WarmPool(booleanBlockEditorUnit?.gameObject, 20);
        PoolManager.WarmPool(capBlockEditorUnit?.gameObject, 5);
        PoolManager.WarmPool(cBlockEditorUnit?.gameObject, 10);
        PoolManager.WarmPool(hatBlockEditorUnit?.gameObject, 10);
        PoolManager.WarmPool(reporterBlockEditorUnit?.gameObject, 20);
        PoolManager.WarmPool(stackBlockEditorUnit?.gameObject, 20);
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
    /// The boolean block input in block element.
    /// </summary>
    [Header("ElementOfBlockUnit")]
    [SerializeField]
    private BooleanBlockInputOfBlockUnit booleanBlockInputInBlockElement;
    [SerializeField]
    private GlobalVariableSelectorDropDownOfBlockUnit globalVariableSelectorDropDownInBlockElement;
    [SerializeField]
    private ReporterBlockInputOfBlockUnit reporterBlockInputInBlockElement;
    [SerializeField]
    private TextElementOfBlockUnit textInBlockElement;

    private void WarmPoolInitElementOfBlockUnit()
    {
        PoolManager.WarmPool(booleanBlockInputInBlockElement?.gameObject, 10);
        PoolManager.WarmPool(globalVariableSelectorDropDownInBlockElement?.gameObject, 5);
        PoolManager.WarmPool(reporterBlockInputInBlockElement?.gameObject, 20);
        PoolManager.WarmPool(textInBlockElement?.gameObject, 20);
    }

    public ElementOfBlockUnit SpawnElementOfBlockUnit(ElementContent elementContent)
    {
        if (elementContent is BooleanBlockInputContent)
        {
            return PoolManager.SpawnObject(booleanBlockInputInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (elementContent is GlobalVariableSelectorDropDownContent)
        {
            return PoolManager.SpawnObject(globalVariableSelectorDropDownInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (elementContent is ReporterBlockInputContent)
        {
            return PoolManager.SpawnObject(reporterBlockInputInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else if (elementContent is TextElementContent)
        {
            return PoolManager.SpawnObject(textInBlockElement?.gameObject).GetComponent<ElementOfBlockUnit>();
        }
        else
        {
            Debug.LogError("Cant Find Proper Type : " + elementContent.GetType().Name);
            return null;
        }
    }

    #endregion
}

[CustomEditor(typeof(BlockEditorManager))]
public class BlockEditorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(30);

        if (GUILayout.Button("Create RobotSourceCode"))
        {
            RobotSystem.instance.CreateRobotSourceCode(System.DateTime.Now.ToString());
        }
    }

}
