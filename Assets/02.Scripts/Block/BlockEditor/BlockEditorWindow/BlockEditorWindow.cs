using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEditorWindow : MonoBehaviour
{
    protected Canvas _Canvas;

    protected virtual void Awake()
    {
        _Canvas = GetComponentInParent<Canvas>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        UiUtility.SetTargetCanvas(_Canvas);
    }

    protected virtual void OnDisable()
    {
        this.EditingRobotSourceCode = null; //for refrash editor window
    }

    protected virtual void Update()
    {

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
            if (this.editingRobotSourceCode == value)
                return; // passed same value

            if(this.editingRobotSourceCode != null)
            {
                this.OnSetEditingRobotSourceCode(null);
            }

            this.editingRobotSourceCode = value;

            if (this.editingRobotSourceCode != null)
            {
                this.OnSetEditingRobotSourceCode(this.editingRobotSourceCode);
            }
        }
    }

    protected virtual void OnSetEditingRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode != null)
        {
            this.InitBlockWorkSpace();
        }
        else
        {
            BlockEditorManager.instnace.ClearAllSpawnedBlockEditorUnits();
        }
    }

    [SerializeField]
    protected Transform BlockEditorUnitContentSpace;

    private void InitBlockWorkSpace()
    {
        if (this.EditingRobotSourceCode == null)
            return;


        //Spawn Hat Blocks
        BlockEditorUnit initBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this.EditingRobotSourceCode.InitBlock, null, BlockEditorUnitContentSpace);
        BlockEditorUnit loopedBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this.EditingRobotSourceCode.LoopedBlock, null, BlockEditorUnitContentSpace);

        if (initBlockEditorUnit != null)
        {
            initBlockEditorUnit._RectTransform.anchoredPosition = Vector2.right * -20;
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
                BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(eventBlock, null, BlockEditorUnitContentSpace);
            }
        }

        //
    }


}
