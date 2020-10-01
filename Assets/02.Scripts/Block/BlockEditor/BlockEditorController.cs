﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif

public class BlockEditorController : MonoBehaviour
{
    public static BlockEditorController instance;

    [SerializeField]
    private Canvas _Canvas;

    private Camera _Camera;
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

        _Camera = _Canvas.worldCamera;
    }

    void Start()
    {
        this.InitBlockMockUp();
    }


    private void OnEnable()
    {
        UiUtility.SetTargetCanvas(_Canvas);
    }

    void Update()
    {
        this.UpdateControllingBlockEdidtorElement();
    }


    #region RayHitBlock

    private ScrollRect UpdateHoveringBodyScrollRect()
    {
        List<RaycastResult> raycastResults = UiUtility.GetUiRayHitListWithScreenPoint(Input.mousePosition);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.CompareTag(HoveringBodyTag))
            {
                if (raycastResults[i].gameObject == BlockShopScrollRect.gameObject || raycastResults[i].gameObject == BlockWorkSpaceScrollRect.gameObject)
                {
                    return BlockShopScrollRect;
                }

            }
        }

        //this.GetTopBlockEditorElementWithScreenPoint<>

        return null;
    }



    #endregion

    #region ControllingBlock

    private const string HoveringBodyTag = "BlockEditorBody";

    [SerializeField]
    private ScrollRect BlockShopScrollRect;
    [SerializeField]
    private RectTransform BlockShopContentBody;

    [SerializeField]
    private ScrollRect BlockWorkSpaceScrollRect;
    [SerializeField]
    private RectTransform BlockWorkSpaceContentBody;

    public void SetBlockHoverOnBlockWorkSpaceContentBody(BlockEditorElement blockEditorElement)
    {
        if (blockEditorElement == null)
            return;

        blockEditorElement.transform.SetParent(BlockWorkSpaceContentBody);
    }

    /// <summary>
    /// Make This Flow Block EditorUnit RootBlock
    /// This block will not have previous block
    /// </summary>
    public void SetBlockHoverOnEditorWindow(BlockEditorElement blockEditorElement)
    {
        if (blockEditorElement == null)
            return; 

        blockEditorElement.transform.SetParent(_Canvas.transform);
    }

 

    [SerializeField]
    private RectTransform BlockEditorBodyTransform;

    private Vector2 screenPointWhenMouseClicked;

    private Vector3 controllOffset;

    private void UpdateControllingBlockEdidtorElement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.screenPointWhenMouseClicked = Input.mousePosition;
            this.PinchingBlockEditorUnit = UiUtility.GetTopBlockEditorElementWithScreenPoint<BlockEditorUnit>(Input.mousePosition, BlockEditorUnit.BlockEditorUnitTag); ;
        }
        else if (Input.GetMouseButton(0))
        {
            if (this.ControllingBlockEditorUnit == null)
            {
                if (this.PinchingBlockEditorUnit != null && Mathf.Abs(this.screenPointWhenMouseClicked.x - Input.mousePosition.x) > 1.5f)
                {   // If Move Mouse After Clicking Block, it start control clicked block
                    //Set this.ControllingBlockEditorUnit 

                    bool IsShopBlock = this.PinchingBlockEditorUnit.IsShopBlock;
                    this.ControllingBlockEditorUnit = IsShopBlock  == true ? this.PinchingBlockEditorUnit.Duplicate(this.BlockEditorBodyTransform) : this.PinchingBlockEditorUnit;

                    if (this.ControllingBlockEditorUnit != null)
                    {
                        Vector3 mouseWorldPos = UiUtility.GetUiWorldPos(this.BlockEditorBodyTransform, Input.mousePosition);
                        this.controllOffset = IsShopBlock == true ? Vector3.zero : mouseWorldPos - this.ControllingBlockEditorUnit.transform.position ;
                        this.ControllingBlockEditorUnit.transform.position = mouseWorldPos - controllOffset;



                    }
                    else
                    {
                        Debug.LogError("ControllingBlockEditorUnit is null");
                    }

                }
            }
            else
            {
                this.ControllingBlockEditorUnit.transform.position = UiUtility.GetUiWorldPos(this.BlockEditorBodyTransform, Input.mousePosition) - controllOffset;


            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (this.ControllingBlockEditorUnit != null)
            {
                ScrollRect HoveringBodyScrollRect = this.UpdateHoveringBodyScrollRect();

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
                {//if player end controlling block on BlockWorkSpace UI

                    this.SetBlockHoverOnBlockWorkSpaceContentBody(this.ControllingBlockEditorUnit);
                    
                    if (this.ControllingBlockEditorUnit.IsAttatchable() == true)
                    {//if block can attach to other block as flowbloc or valueblock
                        this.controllingBlockEditorUnit.AttachBlock();
                    }
                    else
                    {
                    }

                    this.ControllingBlockEditorUnit.BackupUiTransform();
                    this.ControllingBlockEditorUnit.OnEndControlling();
                }
            }

            this.ControllingBlockEditorUnit = null;
            this.PinchingBlockEditorUnit = null;
        }

    }

    private BlockEditorUnit originalControllingBlockEditorUnit;
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
            originalControllingBlockEditorUnit = this.controllingBlockEditorUnit;

            this.controllingBlockEditorUnit = value;

            if (this.controllingBlockEditorUnit == null)
            {//If controllingBlockEditorUnit is null
                this.BlockShopScrollRect.enabled = true;

                this.BlockWorkSpaceScrollRect.enabled = true;


                this.StopUpdateIsControllingBlockEditorUnitAttachableCoroutine();

                this.ReleaseAllBlockMockUp();
            }
            else if (originalControllingBlockEditorUnit != this.controllingBlockEditorUnit)
            {//If controllingBlockEditorUnit is not null and controllingBlockEditorUnit changed from originalControllingBlockEditorUnit

                //Make scrollrect stop
                this.BlockShopScrollRect.StopMovement();
                this.BlockShopScrollRect.enabled = false;

                this.BlockWorkSpaceScrollRect.StopMovement();
                this.BlockWorkSpaceScrollRect.enabled = false;

                this.ReleaseAllBlockMockUp();
                this.StartUpdateIsControllingBlockEditorUnitAttachableCoroutine();

                this.ControllingBlockEditorUnit.OnStartControllingByPlayer();
            }
           
        }
    }

    private void StartUpdateIsControllingBlockEditorUnitAttachableCoroutine()
    {
        this.StopUpdateIsControllingBlockEditorUnitAttachableCoroutine();
        this.UpdateIsControllingBlockEditorUnitAttachableCoroutine = StartCoroutine(this.UpdateIsControllingBlockEditorUnitAttachableIEnumerator());
    }

    private void StopUpdateIsControllingBlockEditorUnitAttachableCoroutine()
    {
        if (this.UpdateIsControllingBlockEditorUnitAttachableCoroutine != null)
        {
            StopCoroutine(this.UpdateIsControllingBlockEditorUnitAttachableCoroutine);
            this.UpdateIsControllingBlockEditorUnitAttachableCoroutine = null;
        }

        this.PreviousIAttachableEditorElement = null;
    }

    private Coroutine UpdateIsControllingBlockEditorUnitAttachableCoroutine = null;


    private IAttachableEditorElement previousIAttachableEditorElement = null;
    private IAttachableEditorElement PreviousIAttachableEditorElement
    {
        get
        {
            return this.previousIAttachableEditorElement;
        }
        set
        {
            if(this.previousIAttachableEditorElement != null)
            {
                this.previousIAttachableEditorElement.OnSetIsAttachable(null);
            }

            this.previousIAttachableEditorElement = value;

            if (this.previousIAttachableEditorElement != null)
            {
                this.previousIAttachableEditorElement.OnSetIsAttachable(this.ControllingBlockEditorUnit);
            }
        }
    }

    /// <summary>
    /// Update ControllingBlockEditorUnit Is Attachable
    /// If attachable, show preview when attached ( see scratch )
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateIsControllingBlockEditorUnitAttachableIEnumerator()
    {
        
        while (true)
        {
            if (this.ControllingBlockEditorUnit == null)
                yield break;

            

            if (this.ControllingBlockEditorUnit.IsAttatchable() == true && this.ControllingBlockEditorUnit.AttachableEditorElement != null)
            {
                if (this.PreviousIAttachableEditorElement != this.ControllingBlockEditorUnit.AttachableEditorElement)
                {
                    this.ReleaseAllBlockMockUp();
                    if (this.previousIAttachableEditorElement != null)
                        this.previousIAttachableEditorElement.OnRootMockUpSet(null);

                    this.ControllingBlockEditorUnit.AttachableEditorElement.OnRootMockUpSet(this.ControllingBlockEditorUnit);
                    this.PreviousIAttachableEditorElement = this.ControllingBlockEditorUnit.AttachableEditorElement;
                }
            }
            else
            {
                this.ReleaseAllBlockMockUp();
                if (this.previousIAttachableEditorElement != null)
                    this.previousIAttachableEditorElement.OnRootMockUpSet(null);
                this.PreviousIAttachableEditorElement = null;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }


    private BlockEditorUnit PinchingBlockEditorUnit;

    
#endregion


    #region BlockMockUp

    [SerializeField]
    public GameObject BlockMockUpPrefab;
    private void InitBlockMockUp()
    {
        PoolManager.WarmPool(BlockMockUpPrefab, 5);
    }

    private List<BlockMockupHelper> SpawnedBlockMockUp;
    public void AddToSpawnedBlockMockUp(BlockMockupHelper blockMockupHelper)
    {
        if (this.SpawnedBlockMockUp == null)
            this.SpawnedBlockMockUp = new List<BlockMockupHelper>();

        this.SpawnedBlockMockUp.Add(blockMockupHelper);
    }



    private void ReleaseAllBlockMockUp()
    {
        if (this.SpawnedBlockMockUp == null)
            return;

        for(int i=0;i< this.SpawnedBlockMockUp.Count;i++)
        {
            PoolManager.ReleaseObject(this.SpawnedBlockMockUp[i].gameObject);
        }

        this.SpawnedBlockMockUp.Clear();
    }

    #endregion

    #region DEBUG

#if UNITY_EDITOR

    StringBuilder stringBuilder;
    string pointHitResultStr;

    /*
    private void DebugHitUiList()
    {
        stringBuilder.Clear();

        //stringBuilder.Append("TopElementOfBlockUnit : " + this.TopElementOfBlockUnit?.name + "\n");
        stringBuilder.Append("PinchingBlockEditorUnit : " + this.PinchingBlockEditorUnit?.name + "\n");
        stringBuilder.Append("ControllingBlockEditorUnit : " + this.ControllingBlockEditorUnit?.name);
        stringBuilder.Append("\n\n\n");

        for (int i = 0; i < this.hitUiList.Count; i++)
        {
            stringBuilder.Append(this.hitUiList[i].gameObject.name + "\n");
        }

        this.pointHitResultStr = stringBuilder.ToString();
    }
    */

    public GUIStyle _GUIStyle;
    [SerializeField]
    private Rect rectPos;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(this.rectPos, this.pointHitResultStr, this._GUIStyle);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(_PointerEventData != null)
            Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(_PointerEventData.position), 0.2f);
    }
    */

#endif
    #endregion
}
