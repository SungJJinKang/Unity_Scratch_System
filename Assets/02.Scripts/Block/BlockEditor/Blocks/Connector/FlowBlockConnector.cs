using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBlockConnector : MonoBehaviour, IAttachableEditorElement
{
    public const string FlowBlockConnectorTag = "FlowBlockConnector";

    [HideInInspector]
    public FlowBlockEditorUnit OwnerFlowBlockEditorUnit;
    [SerializeField]
    private RectTransform connectionPointRectTransform;
    public RectTransform AttachPointRectTransform => connectionPointRectTransform;

    public enum ConnectorType
    {
        None = 0x0,
        UpNotch = 0x1,
        DownBump = 0x2
    }

    public ConnectorType _ConnectorType;



    protected virtual void Awake()
    {
        gameObject.tag = FlowBlockConnectorTag;

        OwnerFlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (OwnerFlowBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");
    }
}
