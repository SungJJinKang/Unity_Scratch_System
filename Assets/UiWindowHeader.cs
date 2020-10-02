using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiWindowHeader : MonoBehaviour
{
    private RectTransform TargetUiParent;
    [SerializeField]
    private RectTransform TargetUi;

    private void Awake()
    {
        this.TargetUiParent = TargetUi.parent.GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        this.IsControlling = false;
    }

    private bool isControlling;
    private bool IsControlling
    {
        get
        {
            return this.isControlling;
        }
        set
        {
            this.isControlling = value;
            this.controllOffset = UiUtility.GetUiWorldPos(this.TargetUiParent, Input.mousePosition) - TargetUi.transform.position;

        }
    }
    private Vector3 controllOffset;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            this.CheckHit();
        }
        else if (Input.GetMouseButton(0))
        {
            if (this.IsControlling == true)
            {
                TargetUi.transform.position = UiUtility.GetUiWorldPos(this.TargetUiParent, Input.mousePosition) - this.controllOffset;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.IsControlling = false;
        }
    }

    private void CheckHit()
    {
        List<RaycastResult> raycastResults = UiUtility.GetUiRayHitListWithScreenPoint(Input.mousePosition);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject == gameObject)
                this.IsControlling = true;
        }
    }
}
