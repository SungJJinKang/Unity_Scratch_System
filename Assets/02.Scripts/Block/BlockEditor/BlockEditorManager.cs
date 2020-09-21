using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

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

    private void InitBlockEditorSystem()
    {
        WarmPoolBlockEditorUnit();
        WarmPoolInitElementOfBlockUnit();

        InitBlockShop();

        System.GC.Collect();
    }

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
               

            Block block = Activator.CreateInstance(type) as Block;
            BlockEditorUnit blockEditorUnit = CreateBlockEditorUnit(type);
            if(blockEditorUnit != null)
            {
                blockEditorUnit.gameObject.name = type.Name;
                blockEditorUnit.transform.SetParent(this.BlockShopContentTransform);
                blockEditorUnit.transform.localScale = Vector3.one;
                blockEditorUnit.TargetBlock = block;
            }
            

        }
    }

    #region BlockEditorElement

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

  

    private BlockEditorUnit CreateBlockEditorUnit(Type blockType)
    {
        BlockEditorUnit blockEditorElement = null;
        if(blockType.IsSubclassOf(typeof(BooleanBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(booleanBlockEditorUnit?.gameObject).GetComponent< BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CapBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(capBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(cBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(HatBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(hatBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(ReporterBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(reporterBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(StackBlock)))
        {
            blockEditorElement = PoolManager.SpawnObject(stackBlockEditorUnit?.gameObject).GetComponent<BlockEditorUnit>();
        }

        if (blockEditorElement == null)
            Debug.LogError("Cant Find Proper Type : " + blockType.Name);


        return blockEditorElement;

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


    public void ConnectFlowBlock(IDownBumpBlockEditorUnit topBlock, IUpNotchBlockEditorUnit bottomBlock)
    {
        topBlock.NextBlockInEditor = bottomBlock;
        bottomBlock.PreviousBlockInEditor = topBlock;
    }

    private void CreateBlockTemplate()
    {

    }
}
