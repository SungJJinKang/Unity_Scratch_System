using System.Collections.Generic;
using UnityEngine;

public class BlockEditorWindow : MonoBehaviour
{
    public static BlockEditorWindow ActiveBlockEditorWindow;

    protected Canvas _Canvas;

    protected virtual void Awake()
    {
        _Canvas = GetComponentInParent<Canvas>();
    }

    protected virtual void Start()
    {
        _RobotSourceCode = RobotSystem.instance.CreateRobotSourceCode("fdf");
    }

    protected virtual void OnEnable()
    {
        if (ActiveBlockEditorWindow == null)
        {
            ActiveBlockEditorWindow = this;
        }
        else
        {
            if (ActiveBlockEditorWindow != this)
            {
                ActiveBlockEditorWindow.gameObject.SetActive(false);
                ActiveBlockEditorWindow = this;
            }
        }

        UiUtility.SetTargetCanvas(_Canvas);


    }

    protected virtual void OnDisable()
    {
        this._RobotSourceCode = null; //for refrash editor window
        ActiveBlockEditorWindow = null;
    }

    public void ExitWindow()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {

    }

    private RobotSourceCode robotSourceCode;
    public RobotSourceCode _RobotSourceCode
    {
        get
        {
            return this.robotSourceCode;
        }
        set
        {
            if (this.robotSourceCode == value)
                return; // passed same value

            if (this.robotSourceCode != null)
            {
                this.OnSetEditingRobotSourceCode(null);
            }

            this.robotSourceCode = value;

            if (this.robotSourceCode != null)
            {
                this.OnSetEditingRobotSourceCode(this.robotSourceCode);
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
            this.ClearAllSpawnedBlockEditorUnitInSourceCode();
        }
    }

    [SerializeField]
    protected Transform BlockEditorUnitContentSpace;

    private void InitBlockWorkSpace()
    {
        if (this._RobotSourceCode == null)
            return;


        //Spawn Hat Blocks
        BlockEditorUnit initBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this._RobotSourceCode.InitBlock, null, BlockEditorUnitContentSpace);
        BlockEditorUnit loopedBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this._RobotSourceCode.LoopedBlock, null, BlockEditorUnitContentSpace);

        if (initBlockEditorUnit != null)
        {
            initBlockEditorUnit._RectTransform.anchoredPosition = Vector2.right * -20;
            initBlockEditorUnit.IsRemovable = false;
            initBlockEditorUnit.BackupUiTransform();
            this.AddToSpawnedBlockEditorUnitInSourceCode(initBlockEditorUnit);
        }

        if (loopedBlockEditorUnit != null)
        {
            loopedBlockEditorUnit._RectTransform.anchoredPosition = Vector2.zero;
            loopedBlockEditorUnit.IsRemovable = false;
            loopedBlockEditorUnit.BackupUiTransform();
            this.AddToSpawnedBlockEditorUnitInSourceCode(loopedBlockEditorUnit);
        }


        EventBlock[] eventBlocks = this._RobotSourceCode.StoredEventBlocks;
        if (eventBlocks != null)
        {
            foreach (var eventBlock in this._RobotSourceCode.StoredEventBlocks)
            {
                BlockEditorUnit blockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(eventBlock, null, BlockEditorUnitContentSpace);
                this.AddToSpawnedBlockEditorUnitInSourceCode(blockEditorUnit);
            }
        }

        //
    }


    /// <summary>
    /// Match One Block Instance To One BlockEditorUnit Object(Instance)
    /// </summary>
    private Dictionary<Block, BlockEditorUnit> SpawnedBlockEditorUnitInSourceCode;
    protected bool AddToSpawnedBlockEditorUnitInSourceCode(BlockEditorUnit blockEditorUnit)
    {
        if (blockEditorUnit.TargetBlock == null)
        {
            Debug.LogError("blockEditorUnit.TargetBlock is null");
            return false;
        }

        if (this.SpawnedBlockEditorUnitInSourceCode == null)
            this.SpawnedBlockEditorUnitInSourceCode = new Dictionary<Block, BlockEditorUnit>();

        if (this.SpawnedBlockEditorUnitInSourceCode.ContainsKey(blockEditorUnit.TargetBlock) == true)
            return false;

        this.SpawnedBlockEditorUnitInSourceCode.Add(blockEditorUnit.TargetBlock, blockEditorUnit);
        return true;
    }

    public BlockEditorUnit GetSpawnedBlockEditorUnitInSourceCode(Block block)
    {
        if (this.SpawnedBlockEditorUnitInSourceCode == null)
            return null;

        if (this.SpawnedBlockEditorUnitInSourceCode.ContainsKey(block) == false)
            return null;

        return this.SpawnedBlockEditorUnitInSourceCode[block];
    }

    private void ClearAllSpawnedBlockEditorUnitInSourceCode()
    {
        if (this.SpawnedBlockEditorUnitInSourceCode == null)
            return;

        foreach (var value in this.SpawnedBlockEditorUnitInSourceCode.Values)
        {
            if (value.IsSpawned)
                value.Release();
        }
        this.SpawnedBlockEditorUnitInSourceCode.Clear();
    }

}
