using UnityEngine;

public class FlowBlockConnector : BlockEditorElement, IAttachableEditorElement
{
    public const string FlowBlockConnectorTag = "FlowBlockConnector";


    [HideInInspector]
    public FlowBlockEditorUnit OwnerFlowBlockEditorUnit;
    public BlockEditorUnit OwnerBlockEditorUnit => OwnerFlowBlockEditorUnit;
    public override BlockEditorElement ParentBlockEditorElement => OwnerFlowBlockEditorUnit;

    [SerializeField]
    private RectTransform connectionPointRectTransform;
    public RectTransform AttachPointRectTransform
    {
        get
        {
            if (this._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch && this.OwnerFlowBlockEditorUnit.PreviousFlowBlockEditorUnit != null)
            {
                return this.OwnerFlowBlockEditorUnit.PreviousFlowBlockEditorUnit.NextBlockConnector.AttachPointRectTransform;
            }
            else
            {
                return connectionPointRectTransform;
            }
        }

    }



    private float OriginalRectHeight;

    public void SetRectHeightScaling(int scaledNum)
    {
        _RectTransform.sizeDelta = new Vector2(_RectTransform.sizeDelta.x, OriginalRectHeight * scaledNum);
    }


    public void ShowIsAttachable(BlockEditorUnit attachedBlockEditorUnit = null)
    {
        FlowBlockEditorUnit flowBlockEditorUnit = attachedBlockEditorUnit as FlowBlockEditorUnit;

        if (flowBlockEditorUnit != null)
        {
            this.CreateFlowBlockMockUp(flowBlockEditorUnit);
            //If this FlowBlockConnector is UpNotch Type, MockUpHeight
            if (_ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {
                this.SetRectHeightScaling(flowBlockEditorUnit.DescendantBlockCount + 2);
            }
        }
        else
        {
            this.SetRectHeightScaling(1);
        }
    }


    /// <summary>
    /// Creates the flow block mock up.
    /// </summary>
    /// <param name="flowBlockEditorUnit">Flow block editor unit.</param>
    /// <param name="siblingIndex">Sibling index.</param>
    public void CreateFlowBlockMockUp(FlowBlockEditorUnit flowBlockEditorUnit, int siblingIndex = 0)
    {
        if (flowBlockEditorUnit == null)
            return;

        BlockMockupHelper spawnedMockUp = PoolManager.SpawnObject(RobotSourceCodeEditorWindow.instance.BlockMockUpPrefab).GetComponent<BlockMockupHelper>();
        RobotSourceCodeEditorWindow.instance.AddToSpawnedBlockMockUp(spawnedMockUp);

        spawnedMockUp.CopyTargetBlock(flowBlockEditorUnit);

        Transform attachPointRectTransform = this.AttachPointRectTransform;


        spawnedMockUp._RectTransform.SetParent(attachPointRectTransform);
        spawnedMockUp._RectTransform.SetAsFirstSibling();
        spawnedMockUp._RectTransform.SetSiblingIndex(siblingIndex);

        spawnedMockUp._RectTransform.localScale = Vector3.one;
        //if copyBlockEditorUnit is FlowBlockEditorUnit
        //SetBlockMockUp nextblock of FlowBlockEditorUnit
        if (flowBlockEditorUnit.NextFlowBlockEditorUnit != null)
        {
            this.CreateFlowBlockMockUp(flowBlockEditorUnit.NextFlowBlockEditorUnit, siblingIndex + 1);
        }
    }



    public enum ConnectorType
    {
        None = 0,
        UpNotch = 1,
        DownBump = 1<<1
    }

    public ConnectorType _ConnectorType;



    protected override void Awake()
    {
        base.Awake();

        gameObject.tag = FlowBlockConnectorTag;

        OwnerFlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (OwnerFlowBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");

        this.OriginalRectHeight = _RectTransform.sizeDelta.y;

        /*
        this._RectTransform.anchorMax = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.up : Vector2.zero;
        this._RectTransform.anchorMin = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.up : Vector2.zero;

        this.connectionPointRectTransform.anchorMax = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.zero : Vector2.up;
        this.connectionPointRectTransform.anchorMin = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.zero : Vector2.up;
        */
    }


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
