using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBlockConnector : MonoBehaviour, IAttachableEditorElement
{
    private RectTransform _RectTransform;

    public const string FlowBlockConnectorTag = "FlowBlockConnector";

    [HideInInspector]
    public FlowBlockEditorUnit OwnerFlowBlockEditorUnit;
    public BlockEditorUnit OwnerBlockEditorUnit => OwnerFlowBlockEditorUnit;

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
 
    public void SetRectHeightDoubling(bool isDoubling)
    {
        _RectTransform.sizeDelta = new Vector2(_RectTransform.sizeDelta.x, isDoubling ? OriginalRectHeight * 2 : OriginalRectHeight);
    }

    public void OnRootMockUpSet(BlockEditorUnit attachedBlockEditorUnit, bool isSet)
    {
        FlowBlockEditorUnit flowBlockEditorUnit = attachedBlockEditorUnit as FlowBlockEditorUnit;

        if (isSet == true)
            flowBlockEditorUnit.SetBlockMockUp(this);

        //If this FlowBlockConnector is UpNotch Type, MockUpHeight
        if (_ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            this.SetRectHeightDoubling(isSet);
    }


   

    public enum ConnectorType
    {
        None = 0x0,
        UpNotch = 0x1,
        DownBump = 0x2
    }

    public ConnectorType _ConnectorType;



    protected virtual void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();

        gameObject.tag = FlowBlockConnectorTag;

        OwnerFlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (OwnerFlowBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");

        this.OriginalRectHeight = _RectTransform.sizeDelta.y;

        this._RectTransform.anchorMax = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.up : Vector2.zero;
        this._RectTransform.anchorMin = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.up : Vector2.zero;

        this.connectionPointRectTransform.anchorMax = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.zero : Vector2.up;
        this.connectionPointRectTransform.anchorMin = _ConnectorType == FlowBlockConnector.ConnectorType.UpNotch ? Vector2.zero : Vector2.up;
    }
}
