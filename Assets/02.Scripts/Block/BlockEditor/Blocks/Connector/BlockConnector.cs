using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnector : MonoBehaviour
{
    public const string BlockConnectorTag = "BlockConnector";

    public BlockEditorUnit ParentBlockEditorUnit;
    public RectTransform ConnectionPoint;

    protected virtual void Awake()
    {
        gameObject.tag = BlockConnectorTag;

        ParentBlockEditorUnit = GetComponentInParent<BlockEditorUnit>();
        if (ParentBlockEditorUnit == null)
            Debug.LogError("ParentBlockEditorUnit is null");
    }
}
