using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : MonoBehaviour
{
    public const string BlockConnectorTag = "BlockConnector";

    [HideInInspector]
    public FlowBlockEditorUnit OwnerFlowBlockEditorUnit;
    public RectTransform ConnectionPoint;

    protected virtual void Awake()
    {
        gameObject.tag = BlockConnectorTag;

        OwnerFlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (OwnerFlowBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");
    }
}
