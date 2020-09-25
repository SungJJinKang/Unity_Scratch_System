
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif

public class BlockEditorController : MonoBehaviour
{
    public static BlockEditorController instance;

    [SerializeField]
    private Canvas _Canvas;
    private GraphicRaycaster _GraphicRaycaster;
    private PointerEventData _PointerEventData;

    private void Awake()
    {
        instance = this;

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
        //this.UpdateTopBlockEdidtorElementAtMouse();

        this.UpdateControllingBlockEdidtorElement();
    }


    /*
    [SerializeField]
    public BlockEdidtorElement TopBlockEdidtorElementAtMousePoint
    {
        get;
        private set;
    }
    */

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

                this.BlockWorkSpaceScrollRect.StopMovement();
                this.BlockWorkSpaceScrollRect.enabled = false;



                UpdateIsControllingBlockEditorUnitAttachableCoroutine = StartCoroutine(UpdateIsControllingBlockEditorUnitAttachableIEnumerator());
            }
            else
            {
                this.BlockShopScrollRect.enabled = true;

                this.BlockWorkSpaceScrollRect.enabled = true;




                if(UpdateIsControllingBlockEditorUnitAttachableCoroutine != null)
                {
                    StopCoroutine(UpdateIsControllingBlockEditorUnitAttachableCoroutine);
                    UpdateIsControllingBlockEditorUnitAttachableCoroutine = null;
                }
            }
        }
    }

    private Coroutine UpdateIsControllingBlockEditorUnitAttachableCoroutine = null;
    /// <summary>
    /// Update ControllingBlockEditorUnit Is Attachable
    /// If attachable, show preview when attached ( see scratch )
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateIsControllingBlockEditorUnitAttachableIEnumerator()
    {
        while(true)
        {
            if (this.ControllingBlockEditorUnit == null)
                yield break;

            if(this.ControllingBlockEditorUnit.IsAttatchable() == true)
            {

            }

            yield return new WaitForSeconds(0.1f);
        }

    }


    private BlockEditorUnit PinchingBlockEditorUnit;


    private BlockEditorUnit TopBlockEditorUnit;
    //private ElementOfBlockUnit TopElementOfBlockUnit;

    /// <summary>
    /// Dont Call this at every tick(update)
    /// </summary>
    /// <param name="screenPos"></param>
    private void UpdateHitUiList(Vector3 screenPos)
    {
        _PointerEventData.position = screenPos;

        if (this.hitUiList == null)
            this.hitUiList = new List<RaycastResult>();

        this.hitUiList.Clear();
        _GraphicRaycaster.Raycast(_PointerEventData, this.hitUiList);
    }


    private void UpdateTopHitBlockEdidtorElement()
    {
        this.UpdateHitUiList(Input.mousePosition);

        this.TopBlockEditorUnit = null;
        //this.TopElementOfBlockUnit = null;
        this.HoveringBodyScrollRect = null;

        if (this.hitUiList.Count > 0)
        {
            for (int i = 0; i < this.hitUiList.Count; i++)
            {//Top Object comes first

                if (this.TopBlockEditorUnit != null && HoveringBodyScrollRect != null)
                    break;

                //if (this.TopBlockEditorUnit != null && this.TopElementOfBlockUnit != null && HoveringBodyScrollRect != null)
                //    break;

                if (this.TopBlockEditorUnit == null && this.hitUiList[i].gameObject.CompareTag(BlockEditorUnit.BlockEditorUnitTag))
                {
                    this.TopBlockEditorUnit = this.hitUiList[i].gameObject.GetComponent<BlockEditorUnit>();

                    if (this.TopBlockEditorUnit != null)
                        continue;
                }

                /*
                if (this.TopElementOfBlockUnit == null && this.hitUiList[i].gameObject.CompareTag(ElementOfBlockUnit.ElementOfBlockUnitTag))
                {
                    this.TopElementOfBlockUnit = this.hitUiList[i].gameObject.GetComponent<ElementOfBlockUnit>();

                    if (this.TopElementOfBlockUnit != null)
                        continue;
                }
                */

                if (this.HoveringBodyScrollRect == null && this.hitUiList[i].gameObject.CompareTag(HoveringBodyTag))
                {
                    if (this.hitUiList[i].gameObject == BlockShopScrollRect.gameObject)
                    {
                        HoveringBodyScrollRect = BlockShopScrollRect;
                        continue;
                    }

                    if (this.hitUiList[i].gameObject == BlockWorkSpaceScrollRect.gameObject)
                    {
                        HoveringBodyScrollRect = BlockWorkSpaceScrollRect;
                        continue;
                    }

                    if (this.HoveringBodyScrollRect != null)
                        continue;
                }
            }
        }


#if UNITY_EDITOR
        DebugHitUiList();
#endif
    }

    
    public FlowBlockConnector GetTopFlowBlockConnector(Vector2 screenPos)
    {
        this.UpdateHitUiList(screenPos);

        if (this.hitUiList.Count > 0)
        {
            for (int i = 0; i < this.hitUiList.Count; i++)
            {//Top Object comes first
             
                if(this.hitUiList[i].gameObject.CompareTag(FlowBlockConnector.FlowBlockConnectorTag))
                {
                    return this.hitUiList[i].gameObject.GetComponent<FlowBlockConnector>();
                }
            }
        }

        return null;
    }



    private const string HoveringBodyTag = "BlockEditorBody";

    [SerializeField]
    private ScrollRect BlockShopScrollRect;
    [SerializeField]
    private RectTransform BlockShopContentBody;

    [SerializeField]
    private ScrollRect BlockWorkSpaceScrollRect;
    [SerializeField]
    private RectTransform BlockWorkSpaceContentBody;

    private ScrollRect HoveringBodyScrollRect;


    [SerializeField]
    private RectTransform BlockEditorBodyTransform;

    Vector2 screenPointWhenMouseClicked;
    Vector2 MousePosOnBlockEditorBodyTransform;
    private void UpdateControllingBlockEdidtorElement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.UpdateTopHitBlockEdidtorElement();

            this.screenPointWhenMouseClicked = Input.mousePosition;
            this.PinchingBlockEditorUnit = this.TopBlockEditorUnit ;
        }
        else if (Input.GetMouseButton(0))
        {
            if (this.ControllingBlockEditorUnit == null)
            {
                if (this.PinchingBlockEditorUnit != null && Mathf.Abs(this.screenPointWhenMouseClicked.x - Input.mousePosition.x) > 1.5f)
                {   // If Move Mouse After Clicking Block, it start control clicked block
                    //Set this.ControllingBlockEditorUnit 

                    this.ControllingBlockEditorUnit = this.PinchingBlockEditorUnit.IsShopBlock == true ? this.PinchingBlockEditorUnit.Duplicate(this.BlockEditorBodyTransform) : this.PinchingBlockEditorUnit;

                    if (this.ControllingBlockEditorUnit != null)
                    {
                        this.ControllingBlockEditorUnit.transform.position = GetUiWorldPos(this.BlockEditorBodyTransform, Input.mousePosition) - Vector3.right * 50;

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
                this.ControllingBlockEditorUnit.transform.position = GetUiWorldPos(this.BlockEditorBodyTransform, Input.mousePosition) - Vector3.right * 100;
             
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (this.ControllingBlockEditorUnit != null)
            {
                this.UpdateTopHitBlockEdidtorElement();

                if (HoveringBodyScrollRect == null || HoveringBodyScrollRect == this.BlockShopScrollRect.gameObject)
                {//if player end controlling block on BlockShop UI or Outside of Editor UI
                    if(this.ControllingBlockEditorUnit.IsRemovable == true)
                    {
                        this.ControllingBlockEditorUnit.Release();
                    }
                    else
                    {
                        this.ControllingBlockEditorUnit.RevertUiPos();
                    }
                }
                else
                {//if player end controlling block on BlockWorkdSpace UI

                    this.ControllingBlockEditorUnit.transform.SetParent(this.BlockWorkSpaceContentBody);
                    
                    if (this.ControllingBlockEditorUnit.IsAttatchable() == true)
                    {//if block can attach to other block as flowbloc or valueblock
                        Debug.Log("this.ControllingBlockEditorUnit.IsAttatchable() is true");
                    }
                    else
                    {
                        Debug.Log("this.ControllingBlockEditorUnit.IsAttatchable() is false");
                    }

                    this.ControllingBlockEditorUnit.BackupUiTransform();
                    this.ControllingBlockEditorUnit.OnEndControlling();
                }
            }

            this.ControllingBlockEditorUnit = null;
            this.PinchingBlockEditorUnit = null;
            this.HoveringBodyScrollRect = null;
        }

    }

    /// <summary>
    /// Set Ui Position To Mouse point
    /// </summary>
    /// <param name="uiTransform"></param>
    /// <param name="parentRect"></param>
    /// <param name="offset"></param>
    private Vector3 GetUiWorldPos( RectTransform parentRect, Vector3 ScreenPos)
    {
        if (parentRect == null)
            return Vector3.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, _Canvas.worldCamera, out MousePosOnBlockEditorBodyTransform);
        return parentRect.TransformPoint(MousePosOnBlockEditorBodyTransform);
    }







#if UNITY_EDITOR

    StringBuilder stringBuilder;
    string pointHitResultStr;

    private void DebugHitUiList()
    {
        stringBuilder.Clear();

        stringBuilder.Append("TopBlockEditorUnit : " + this.TopBlockEditorUnit?.name + "\n");
        //stringBuilder.Append("TopElementOfBlockUnit : " + this.TopElementOfBlockUnit?.name + "\n");
        stringBuilder.Append("HoveringBodyScrollRect : " + this.HoveringBodyScrollRect?.name + "\n");
        stringBuilder.Append("PinchingBlockEditorUnit : " + this.PinchingBlockEditorUnit?.name + "\n");
        stringBuilder.Append("ControllingBlockEditorUnit : " + this.ControllingBlockEditorUnit?.name);
        stringBuilder.Append("\n\n\n");

        for (int i = 0; i < this.hitUiList.Count; i++)
        {
            stringBuilder.Append(this.hitUiList[i].gameObject.name + "\n");
        }

        this.pointHitResultStr = stringBuilder.ToString();
    }

    public GUIStyle _GUIStyle;
    [SerializeField]
    private Rect rectPos;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(this.rectPos, this.pointHitResultStr, this._GUIStyle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(_PointerEventData != null)
            Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(_PointerEventData.position), 0.2f);
    }


#endif
}
