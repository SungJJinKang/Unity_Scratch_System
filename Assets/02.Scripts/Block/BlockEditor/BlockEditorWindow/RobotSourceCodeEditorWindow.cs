using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
#endif

public class RobotSourceCodeEditorWindow : BlockEditorWindow
{
    public static RobotSourceCodeEditorWindow instance;

    protected override void Awake()
    {
        base.Awake();
        instance = this;

#if UNITY_EDITOR
        _GUIStyle = new GUIStyle();
        _GUIStyle.fontSize = 65;
        _GUIStyle.fontStyle = FontStyle.Bold;

        _GUIStyle.normal.textColor = Color.red;
        _GUIStyle.active.textColor = Color.red;
#endif

    }

    protected override void Start()
    {
        base.Start();

        this.InitBlockMockUp();
        this.InitBlockShop();
    }

    protected override void OnSetEditingRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        base.OnSetEditingRobotSourceCode(robotSourceCode);

        if (robotSourceCode != null)
        {
            this.InitCustomBlockOnBlockShop();
            this.InitRobotGlobalVariableOnBlockShop();
        }
        else
        {
            //Should Clear This
            //this.InitCustomBlockOnBlockShop();
            //this.InitRobotGlobalVariableOnBlockShop();
        }
    }



    private void InitBlockShop()
    {
        foreach (Type type in BlockReflector.GetAllSealedBlockTypeContainingBlockTitleAttribute())
        {
            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                Debug.LogWarning(" \" " + type.Name + " \" Dont Have Default Constructor");
                continue; // If Type don't have default constructor, continue loop
            }


            if (type.GetCustomAttribute(typeof(NotAutomaticallyMadeOnBlockShopAttribute), true) != null)
            {
                Debug.LogWarning(" \" " + type.Name + " \" Containing NotAutomaticallyMadeOnBlockShopAttribute");
                continue;
            }


            BlockEditorUnit createdBlockEditorUnit = BlockEditorManager.instnace.CreateBlockEditorUnit(type, this, this.BlockShopContent);
            if (createdBlockEditorUnit != null)
            {
                createdBlockEditorUnit.IsShopBlock = true;
                createdBlockEditorUnit.IsRemovable = false;
                //base.AddToSpawnedBlockEditorUnitList(createdBlockEditorUnit);
            }


        }
    }




    private void InitCustomBlockOnBlockShop()
    {
        if (this._RobotSourceCode == null)
            return;
    }

    private void InitRobotGlobalVariableOnBlockShop()
    {
        if (this._RobotSourceCode == null)
            return;
    }


    protected override void OnEnable()
    {
        base.OnEnable();


    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //clear all spawned BlockEditorElement

        this.StopUpdateIsControllingBlockEditorUnitAttachableCoroutine();
        this.ControllingBlockEditorUnit = null;
        this.originalControllingBlockEditorUnit = null;
        this.AttachableEditorElement = null;
        this.PinchingBlockEditorUnit = null;
        this.ReleaseAllBlockMockUp();
    }



    protected override void Update()
    {
        base.Update();
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
                if (raycastResults[i].gameObject == BlockShopScrollRect.gameObject || raycastResults[i].gameObject == SourceCodeViewerScrollRect.gameObject)
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
    private RectTransform BlockEditorBody;

    protected override RectTransform SourceCodeViewerRectTransform => this.SourceCodeViewerContent;

    [SerializeField]
    private ScrollRect BlockShopScrollRect;
    [SerializeField]
    private RectTransform BlockShopContent;

    [SerializeField]
    private ScrollRect SourceCodeViewerScrollRect;
    [SerializeField]
    private RectTransform SourceCodeViewerContent;



    /// <summary>
    /// Make This Flow Block EditorUnit RootBlock
    /// This block will not have previous block
    /// </summary>
    public void SetBlockHoverOnEditorWindow(BlockEditorUnit blockEditorUnit)
    {
        if (blockEditorUnit == null)
            return;

        blockEditorUnit.transform.SetParent(_Canvas.transform);
    }


   


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

                    //if PinchingBlockEditorUnit is shopblock, duplicate it, or controll it
                    this.ControllingBlockEditorUnit = IsShopBlock == true ? this.DuplicateBlockEditorUnit(this.PinchingBlockEditorUnit, this.BlockEditorBody) : this.PinchingBlockEditorUnit;

                    if (this.ControllingBlockEditorUnit != null)
                    {
                        Vector3 mouseWorldPos = UiUtility.GetUiWorldPos(this.BlockEditorBody, Input.mousePosition);
                        this.controllOffset = IsShopBlock == true ? Vector3.zero : mouseWorldPos - this.ControllingBlockEditorUnit.transform.position;
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
                this.ControllingBlockEditorUnit.transform.position = UiUtility.GetUiWorldPos(this.BlockEditorBody, Input.mousePosition) - controllOffset;


            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (this.ControllingBlockEditorUnit != null)
            {
                ScrollRect HoveringBodyScrollRect = this.UpdateHoveringBodyScrollRect();

                if (HoveringBodyScrollRect == null || HoveringBodyScrollRect == this.BlockShopScrollRect.gameObject)
                {//if player end controlling block on BlockShop UI or Outside of Editor UI
                    if (this.ControllingBlockEditorUnit.IsRemovable == true)
                    {
                        this.ControllingBlockEditorUnit.Release();
                    }
                    else
                    {
                        this.ControllingBlockEditorUnit.RevertTransformInfo();
                    }
                }
                else
                {//if player end controlling block on BlockWorkSpace UI

                    base.SetBlockEditorUnitRootAtSourceCodeViewer(this.ControllingBlockEditorUnit);

                    if (this.ControllingBlockEditorUnit.IsAttatchable() == true)
                    {//if block can attach to other block as flowbloc or valueblock
                        this.controllingBlockEditorUnit.AttachBlock();
                    }
                    this.ControllingBlockEditorUnit.OnEndControlling();
                }
            }

            this.ControllingBlockEditorUnit = null;
            this.PinchingBlockEditorUnit = null;
        }

    }

    private BlockEditorUnit DuplicateBlockEditorUnit(BlockEditorUnit blockEditorUnit, Transform parent)
    {
        BlockEditorUnit createdBlockEditorUnit = blockEditorUnit.Duplicate(parent);

        return createdBlockEditorUnit;
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

                this.SourceCodeViewerScrollRect.enabled = true;


                this.StopUpdateIsControllingBlockEditorUnitAttachableCoroutine();

                this.ReleaseAllBlockMockUp();
            }
            else if (originalControllingBlockEditorUnit != this.controllingBlockEditorUnit)
            {//If controllingBlockEditorUnit is not null and controllingBlockEditorUnit changed from originalControllingBlockEditorUnit

                //Make scrollrect stop
                this.BlockShopScrollRect.StopMovement();
                this.BlockShopScrollRect.enabled = false;

                this.SourceCodeViewerScrollRect.StopMovement();
                this.SourceCodeViewerScrollRect.enabled = false;

                this.ReleaseAllBlockMockUp();
                this.StartUpdateIsControllingBlockEditorUnitAttachableCoroutine();

              
                this.ControllingBlockEditorUnit.OnStartControllingByPlayer();

                this.ControllingBlockEditorUnit.BackupTransformInfo();
                this.SetBlockHoverOnEditorWindow(this.ControllingBlockEditorUnit);
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

        this.AttachableEditorElement = null;
    }

    private Coroutine UpdateIsControllingBlockEditorUnitAttachableCoroutine = null;


    private IAttachableEditorElement attachableEditorElement = null;
    private IAttachableEditorElement AttachableEditorElement
    {
        get
        {
            return this.attachableEditorElement;
        }
        set
        {
            if (this.attachableEditorElement == value)
                return; // if same value passed, return 

            if (this.attachableEditorElement != null)
            {
                this.attachableEditorElement.ShowIsAttachable(null);
                this.ReleaseAllBlockMockUp();
            }

            this.attachableEditorElement = value;

            if (this.attachableEditorElement != null)
            {
                this.attachableEditorElement.ShowIsAttachable(this.ControllingBlockEditorUnit);
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
                this.AttachableEditorElement = this.ControllingBlockEditorUnit.AttachableEditorElement;
            }
            else
            {
                this.AttachableEditorElement = null;
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

        for (int i = 0; i < this.SpawnedBlockMockUp.Count; i++)
        {
            PoolManager.ReleaseObject(this.SpawnedBlockMockUp[i].gameObject);
        }

        this.SpawnedBlockMockUp.Clear();
    }

    #endregion

    #region DEBUG

#if UNITY_EDITOR

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


#if UNITY_EDITOR
[CustomEditor(typeof(RobotSourceCodeEditorWindow))]
public class RobotSourceCodeEditorWindowEditor : Editor
{
    private RobotSourceCodeEditorWindow _RobotSourceCodeEditorWindow;

    private void Awake()
    {
        _RobotSourceCodeEditorWindow = target as RobotSourceCodeEditorWindow;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(30);

        if (GUILayout.Button("Create RobotSourceCode"))
        {
            RobotSystem.instance.CreateRobotSourceCode(System.DateTime.Now.ToString());
        }

        if (GUILayout.Button("Set First Robot Sourcode"))
        {
            _RobotSourceCodeEditorWindow._RobotSourceCode = RobotSystem.instance.RobotSourceCodeList[0];
        }
 
    }

}
#endif