using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBlockConnector : MonoBehaviour
{
    public const string FlowBlockConnectorTag = "FlowBlockConnector";

    [HideInInspector]
    public FlowBlockEditorUnit _FlowBlockEditorUnit;

    public enum ConnectorType
    { 
        UpNotch,
        DownBump
    }

    public ConnectorType _ConnectorType;

    private void Awake()
    {
        gameObject.tag = FlowBlockConnectorTag;

        _FlowBlockEditorUnit = GetComponentInParent<FlowBlockEditorUnit>();
        if (_FlowBlockEditorUnit == null)
            Debug.LogError("_FlowBlockEditorUnit is null");
    }
}
