
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif

public class BlockEditorController : MonoBehaviour
{
    [SerializeField]
    private Canvas _Canvas;
    private GraphicRaycaster _GraphicRaycaster;
    private PointerEventData _PointerEventData;

    private void Awake()
    {
#if UNITY_EDITOR
        stringBuilder = new StringBuilder();


        _GUIStyle = new GUIStyle();
        _GUIStyle.fontSize = 65;
        _GUIStyle.fontStyle = FontStyle.Bold;

        _GUIStyle.normal.textColor = Color.red;
        _GUIStyle.active.textColor = Color.red;
#endif

        _GraphicRaycaster = _Canvas.GetComponent<GraphicRaycaster>();
        _PointerEventData = new PointerEventData(null);
    }

    void Start()
    {

    }

    List<RaycastResult> hitUiList;

    void Update()
    {
        UpdateTopBlockEdidtorElementAtMouse();

        UpdateControllingBlockEdidtorElement();
    }



    [SerializeField]
    public BlockEdidtorElement TopBlockEdidtorElementAtMousePoint
    {
        get;
        private set;
    }

    private BlockEditorUnit controllingBlockEditorUnit;
    [SerializeField]
    public BlockEditorUnit ControllingBlockEditorUnit
    {
        get
        {
            return this.controllingBlockEditorUnit;
        }
        private set
        {
            this.controllingBlockEditorUnit = value;

            if(this.controllingBlockEditorUnit != null)
            {
                this.BlockShopScrollRect.StopMovement();
                this.BlockShopScrollRect.enabled = false;
            }
            else
            {
                this.BlockShopScrollRect.enabled = true;
            }
        }
    }


    private BlockEditorUnit PinchingBlockEditorUnit;



    private void UpdateTopBlockEdidtorElementAtMouse()
    {
        _PointerEventData.position = Input.mousePosition;

        if (this.hitUiList == null)
            this.hitUiList = new List<RaycastResult>();

        this.hitUiList.Clear();
        _GraphicRaycaster.Raycast(_PointerEventData, this.hitUiList);

        TopBlockEdidtorElementAtMousePoint = null;

        for (int i = 0; i < this.hitUiList.Count; i++)
        {//Top Object comes first
            if (TopBlockEdidtorElementAtMousePoint != null && TopBlockEdidtorElementAtMousePoint.gameObject == this.hitUiList[i].gameObject)
                break;

            if (this.hitUiList[i].gameObject.CompareTag(BlockEdidtorElement.BlockEdidtorElementTag))
            {
                TopBlockEdidtorElementAtMousePoint = this.hitUiList[i].gameObject.GetComponent<BlockEdidtorElement>();

                if (TopBlockEdidtorElementAtMousePoint != null)
                    break;
            }
        }

#if UNITY_EDITOR
        DebugHitUiList();
#endif
    }

    [SerializeField]
    private ScrollRect BlockShopScrollRect;
    [SerializeField]
    private RectTransform BlockEditorBodyTransform;

    Vector2 screenPointWhenMouseClicked;
    Vector2 MousePosOnBlockEditorBodyTransform;
    private void UpdateControllingBlockEdidtorElement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.screenPointWhenMouseClicked = Input.mousePosition;
            this.PinchingBlockEditorUnit = this.TopBlockEdidtorElementAtMousePoint as BlockEditorUnit;
        }
        else if (Input.GetMouseButton(0))
        {
            if (this.ControllingBlockEditorUnit == null)
            {
                if (this.PinchingBlockEditorUnit != null && Mathf.Abs(this.screenPointWhenMouseClicked.x - Input.mousePosition.x) > 0.5f)
                {// If Move Mouse After Clicking Block, it start control clicked block
                    this.ControllingBlockEditorUnit = this.PinchingBlockEditorUnit.GetComponent<BlockTemplateInBlockShop>() != null ? this.PinchingBlockEditorUnit.Duplicate(this.BlockEditorBodyTransform) : this.PinchingBlockEditorUnit;

                    if (this.ControllingBlockEditorUnit != null)
                    {
                        SetUIAtMousePoint(this.ControllingBlockEditorUnit.transform, this.BlockEditorBodyTransform, -Vector3.right * 50);

                        this.ControllingBlockEditorUnit.transform.SetParent(this.BlockEditorBodyTransform);
                        this.ControllingBlockEditorUnit.OnStartControlling();
                    }
                    else
                    {
                        Debug.LogError("ControllingBlockEditorUnit is null");
                    }

                }
            }
            else
            {
                SetUIAtMousePoint(this.ControllingBlockEditorUnit.transform, this.BlockEditorBodyTransform, -Vector3.right * 50);
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (this.ControllingBlockEditorUnit != null)
            {
                if(this.ControllingBlockEditorUnit.IsAttatchable == true)
                {

                    this.ControllingBlockEditorUnit.OnEndControlling();
                }
                else
                {
                    //Release(Destroy) ControllingBlockEditorUnit
                    this.ControllingBlockEditorUnit.Release();
                }
            }
            this.ControllingBlockEditorUnit = null;
            this.PinchingBlockEditorUnit = null;
        }

    }

    /// <summary>
    /// Set Ui Position To Mouse point
    /// </summary>
    /// <param name="uiTransform"></param>
    /// <param name="parentRect"></param>
    /// <param name="offset"></param>
    private void SetUIAtMousePoint(Transform uiTransform, RectTransform parentRect, Vector3 offset)
    {
        if (uiTransform == null || parentRect == null)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, _Canvas.worldCamera, out MousePosOnBlockEditorBodyTransform);
        uiTransform.position = parentRect.TransformPoint(MousePosOnBlockEditorBodyTransform) + offset;
    }




#if UNITY_EDITOR

    StringBuilder stringBuilder;
    string pointHitResultStr;

    private void DebugHitUiList()
    {
        stringBuilder.Clear();

        stringBuilder.Append("TopBlockEdidtorElementAtMousePoint : " + this.TopBlockEdidtorElementAtMousePoint?.name + "\n");
        stringBuilder.Append("PinchingBlockEditorUnit : " + this.PinchingBlockEditorUnit?.name + "\n");
        stringBuilder.Append("ControllingBlockEditorUnit : " + this.ControllingBlockEditorUnit?.name);
        stringBuilder.Append("\n\n\n");

        for (int i = 0; i < this.hitUiList.Count; i++)
        {
            stringBuilder.Append(this.hitUiList[i].gameObject.name + "\n");
        }

        this.pointHitResultStr = stringBuilder.ToString();
    }

    private GUIStyle _GUIStyle;
    [SerializeField]
    private Rect rectPos;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(this.rectPos, this.pointHitResultStr, this._GUIStyle);
    }




#endif
}
