using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockEditorManager : MonoBehaviour
{

    public static BlockEditorManager instnace;

    private void Awake()
    {
        instnace = this;
        this.InitBlockEditorSystem();
    }

    private void Start()
    {


    }






    private void InitBlockEditorSystem()
    {
        WarmPoolBlockEditorUnit();
        WarmPoolInitElementOfBlockUnit();

        System.GC.Collect();
    }



    #region BlockWorkSpace





    /// <summary>
    /// Spawn Flow Block Recursivly
    /// Spawned Block create child block again,
    /// and spawned child block create child of block of that ..........
    /// </summary>
    /// <param name="flowBlock"></param>
    public FlowBlockEditorUnit SpawnFlowBlockEditorUnit(FlowBlock createdNewFlowBlock, FlowBlockEditorUnit parentBlockEditorUnit, BlockEditorWindow blockEditorWindow, Transform parent)
    {
        if (createdNewFlowBlock == null)
            return null;

        FlowBlockEditorUnit blockEditorUnit = this.CreateBlockEditorUnit(createdNewFlowBlock, blockEditorWindow, parent) as FlowBlockEditorUnit;

        if (parentBlockEditorUnit != null)
        {
            FlowBlockEditorUnit.ConnectFlowBlockEditorUnit(parentBlockEditorUnit, blockEditorUnit);
        }

        if (createdNewFlowBlock.NextBlock != null)
        {
            this.SpawnFlowBlockEditorUnit(createdNewFlowBlock.NextBlock, blockEditorUnit, blockEditorWindow, parent);
        }



        return blockEditorUnit;
    }



    #endregion

    #region BlockEditorUnit




    /// <summary>
    /// Create New Block Instance with blockType And Create BlockEditorUnit
    /// </summary>
    /// <param name="blockType"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public BlockEditorUnit CreateBlockEditorUnit(Type blockType, BlockEditorWindow blockEditorWindow, Transform parent = null)
    {
        if (blockType == null)
        {
            return null;
        }

        return this.CreateBlockEditorUnit(Block.CreatBlock(blockType), blockEditorWindow, parent);
    }

    /// <summary>
    /// CreateBlockEditorUnit With Block Instance
    /// </summary>
    /// <param name="block"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public BlockEditorUnit CreateBlockEditorUnit(Block block, BlockEditorWindow blockEditorWindow, Transform parent = null)
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

#if UNITY_EDITOR
        if(typeof(ILiteralBlock).IsAssignableFrom(blockType) == true)
        {
            Debug.LogError("Cang Make BlockEditrUnit Of LiteralBlock");
            return null;
        }
#endif

        BlockEditorUnit blockEditorUnit = null;
        if (blockType.IsSubclassOf(typeof(BooleanBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(booleanBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CapBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(capBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(CBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(cBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(HatBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(hatBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(ReporterBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(reporterBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
        }
        else if (blockType.IsSubclassOf(typeof(StackBlock)))
        {
            blockEditorUnit = PoolManager.SpawnObject(stackBlockEditorUnit?.gameObject)?.GetComponent<BlockEditorUnit>();
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

            blockEditorUnit.ParentBlockEditorWindow = blockEditorWindow;

            if(blockEditorUnit.IsRootBlock == true)
            {//if rootblock
                if(blockEditorUnit.ReturnToSavedUnitAnchoredPosition() == false)
                {//if fail return to SavedUnitAnchoredPosition
                    blockEditorUnit._RectTransform.anchoredPosition = Vector2.zero; // manually set 
                }
            }

            blockEditorUnit.OnSpawned();
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
    [Header("DefinitionOfBlockEditorUnit")]
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

    public DefinitionOfBlockEditorUnit SpawnDefinitionOfBlockEditorUnit(DefinitionContentOfBlock definitionContentOfBlock)
    {
        if (definitionContentOfBlock is BooleanBlockInputDefinitionContentOfBlock)
        {
            return PoolManager.SpawnObject(booleanBlockInputInBlockElement?.gameObject)?.GetComponent<DefinitionOfBlockEditorUnit>();
        }
        else if (definitionContentOfBlock is GlobalVariableSelectorDefinitionContentOfBlock)
        {
            return PoolManager.SpawnObject(globalVariableSelectorDropDownInBlockElement?.gameObject)?.GetComponent<DefinitionOfBlockEditorUnit>();
        }
        else if (definitionContentOfBlock is ReporterBlockInputDefinitionContentOfBlock)
        {
            return PoolManager.SpawnObject(reporterBlockInputInBlockElement?.gameObject)?.GetComponent<DefinitionOfBlockEditorUnit>();
        }
        else if (definitionContentOfBlock is TextDefinitionContentOfBlock)
        {
            return PoolManager.SpawnObject(textInBlockElement?.gameObject)?.GetComponent<DefinitionOfBlockEditorUnit>();
        }
        else
        {
            Debug.LogError("Cant Find Proper Type : " + definitionContentOfBlock.GetType().Name);
            return null;
        }
    }

    #endregion
}

#if UNITY_EDITOR
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
#endif