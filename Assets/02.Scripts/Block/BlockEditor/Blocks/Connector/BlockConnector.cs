using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : MonoBehaviour
{
    public const string BlockConnectorTag = "BlockConnector";

    [HideInInspector]
    public FlowBlockEditorUnit ParentFlowBlockEditorUnit;
    public RectTransform ConnectionPoint;

    protected virtual void Awake()
    {
        gameObject.tag = BlockConnectorTag;

        ParentFlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (ParentFlowBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");
    }
}
