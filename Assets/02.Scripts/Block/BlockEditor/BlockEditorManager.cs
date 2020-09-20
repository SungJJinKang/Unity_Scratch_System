using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockEditorManager : MonoBehaviour
{
    private void Start()
    {
        InitBlockShop();
    }

    private void InitBlockShop()
    {
        foreach (Type type in BlockReflector.GetAllSealedBlockTypeContainingBlockTitleAttribute())
        {
            Block block = Activator.CreateInstance(type) as Block;
            CreateBlockEditorUnit(block);


        }
    }

    #region BlockEditorElement

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

  

    private BlockEditorUnit CreateBlockEditorUnit(Block blockInstance)
    {
        BlockEditorUnit blockEditorElement = null;
        if(blockInstance is BooleanBlock)
        {
            blockEditorElement = Instantiate(booleanBlockEditorUnit.gameObject).GetComponent< BlockEditorUnit>();
        }
        else if (blockInstance is CapBlock)
        {
            blockEditorElement = Instantiate(capBlockEditorUnit.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockInstance is CBlock)
        {
            blockEditorElement = Instantiate(cBlockEditorUnit.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockInstance is HatBlock)
        {
            blockEditorElement = Instantiate(hatBlockEditorUnit.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockInstance is ReporterBlock)
        {
            blockEditorElement = Instantiate(reporterBlockEditorUnit.gameObject).GetComponent<BlockEditorUnit>();
        }
        else if (blockInstance is StackBlock)
        {
            blockEditorElement = Instantiate(stackBlockEditorUnit.gameObject).GetComponent<BlockEditorUnit>();
        }

        blockEditorElement.TargetBlock = blockInstance;
        return blockEditorElement;

    }

    #endregion

    #region ElementInBlockEditorElement

    [SerializeField]
    private BooleanBlockInputOfBlockUnit booleanBlockInputInBlockElement;
    [SerializeField]
    private GlobalVariableSelectorDropDownOfBlockUnit globalVariableSelectorDropDownInBlockElement;
    [SerializeField]
    private ReporterBlockInputOfBlockUnit reporterBlockInputInBlockElement;
    [SerializeField]
    private TextElementOfBlockUnit textInBlockElement;

    #endregion


    public void ConnectFlowBlock(IDownBumpBlockEditorUnit topBlock, IUpNotchBlockEditorUnit bottomBlock )
    {
        topBlock.NextBlockInEditor = bottomBlock;
        bottomBlock.PreviousBlockInEditor = topBlock;
    }

    private void CreateBlockTemplate()
    {

    }
}
