using System.Collections;
using System.Collections.Generic;
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

    public void OnRootMockUpSet(BlockEditorUnit attachedBlockEditorUnit)
    {
        FlowBlockEditorUnit flowBlockEditorUnit = attachedBlockEditorUnit as FlowBlockEditorUnit;

        if (flowBlockEditorUnit != null)
        {
            flowBlockEditorUnit.CreateFlowBlockMockUp(this);
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

    public void OnSetIsAttachable(BlockEditorUnit attachedBlockEditorUnit = null)
    {

    }


    public enum ConnectorType
    {
        None = 0x0,
        UpNotch = 0x1,
        DownBump = 0x2
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
}
