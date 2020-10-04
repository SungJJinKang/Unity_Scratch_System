using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BlockEditorWindow : MonoBehaviour
{
    public static BlockEditorWindow ActiveBlockEditorWindow;

    private Canvas canvas;
    protected Canvas _Canvas
    {
        get
        {
            if (this.canvas == null)
                this.canvas = GetComponentInParent<Canvas>();

            return this.canvas;
        }
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
#if UNITY_EDITOR
        tempSetRobotSourceCode();
#endif
    }
#if UNITY_EDITOR
    private void tempSetRobotSourceCode()
    {
        if (RobotSystem.instance == null)
            return;

        if(RobotSystem.instance.RobotSourceCodeCount == 0)
        {
            _RobotSourceCode = RobotSystem.instance.CreateRobotSourceCode("fdf");
        }
        else
        {
            _RobotSourceCode = RobotSystem.instance.RobotSourceCodeList[0];
        }

        
    }
#endif
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

        UiUtility.TargetCanvas = _Canvas;

#if UNITY_EDITOR
        tempSetRobotSourceCode();
#endif

    }

    protected virtual void OnDisable()
    {
        this._RobotSourceCode = null; //for refrash editor window
        ActiveBlockEditorWindow = null;

        System.GC.Collect();
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
            this.InitSourceCodeViewer();
        }
        else
        {
            this.ClearAllSpawnedBlockEditorUnitInSourceCode();
        }
    }

    /// <summary>
    /// Active Block Editor Unit at this RectTransform
    /// Parent Of Root Block Editor Unit
    /// </summary>
    /// <value>The source code viewer rect transform.</value>
    protected abstract RectTransform SourceCodeViewerRectTransform { get; }
    public void SetBlockEditorUnitRootAtSourceCodeViewer(BlockEditorUnit blockEditorUnit)
    {
        if (blockEditorUnit == null)
            return;

        blockEditorUnit.transform.SetParent(this.SourceCodeViewerRectTransform);
    }

    private void InitSourceCodeViewer()
    {
        if (this._RobotSourceCode == null)
            return;


        //Spawn Hat Blocks
        BlockEditorUnit initBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this._RobotSourceCode.InitBlock, null, this, SourceCodeViewerRectTransform);
        BlockEditorUnit loopedBlockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(this._RobotSourceCode.LoopedBlock, null, this, SourceCodeViewerRectTransform);

        if (initBlockEditorUnit != null)
        {
            initBlockEditorUnit.IsRemovable = false;
            initBlockEditorUnit.BackupTransformInfo();
        }

        if (loopedBlockEditorUnit != null)
        {
            loopedBlockEditorUnit.IsRemovable = false;
            loopedBlockEditorUnit.BackupTransformInfo();
        }


        EventBlock[] eventBlocks = this._RobotSourceCode.StoredEventBlocks;
        if (eventBlocks != null)
        {
            foreach (var eventBlock in this._RobotSourceCode.StoredEventBlocks)
            {
                BlockEditorUnit blockEditorUnit = BlockEditorManager.instnace.SpawnFlowBlockEditorUnit(eventBlock, null, this, SourceCodeViewerRectTransform);
                loopedBlockEditorUnit.IsRemovable = false;
                loopedBlockEditorUnit.BackupTransformInfo();
            }
        }

        //
    }


    /// <summary>
    /// Match One Block Instance To One BlockEditorUnit Object(Instance)
    /// Item Should be at SourceCodeViewr
    /// Items will be released when disable window 
    /// so dont put shopBlockEditorUnit in here
    /// </summary>
    private Dictionary<Block, BlockEditorUnit> SpawnedBlockEditorUnitInSourceCode;
    public bool AddToSpawnedBlockEditorUnitInSourceCode(BlockEditorUnit blockEditorUnit)
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

    public void RemoveFromSpawnedBlockEditorUnitInSourceCode(BlockEditorUnit blockEditorUnit)
    {
        if (this.SpawnedBlockEditorUnitInSourceCode == null)
            return;

        this.SpawnedBlockEditorUnitInSourceCode.Remove(blockEditorUnit?.TargetBlock);
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

        BlockEditorUnit[] spawnedBlockEditorUnit = this.SpawnedBlockEditorUnitInSourceCode.Values.ToArray();

        for (int i = 0; i < spawnedBlockEditorUnit.Length; i++)
        {
            if (spawnedBlockEditorUnit[i].IsSpawned && spawnedBlockEditorUnit[i].IsRemovable == true)
            {
                spawnedBlockEditorUnit[i].Release();
            }
        }
        this.SpawnedBlockEditorUnitInSourceCode.Clear();
    }

}
