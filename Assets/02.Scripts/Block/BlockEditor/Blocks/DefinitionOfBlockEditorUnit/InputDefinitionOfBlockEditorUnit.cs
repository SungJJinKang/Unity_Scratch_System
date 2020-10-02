using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class InputDefinitionOfBlockEditorUnit : DefinitionOfBlockEditorUnit, IAttachableEditorElement
{
    public const string InputDefinitionOfBlockEditorUnitTag = "InputDefinitionOfBlockEditorUnitTag";


    [SerializeField]
    protected GameObject DefaultBlockInputObj;

    private ValueBlockEditorUnit inputtedValueBlockEditorUnit;
    public ValueBlockEditorUnit InputtedValueBlockEditorUnit
    {
        get
        {
            return this.inputtedValueBlockEditorUnit;
        }
        set
        {
            if (this.inputtedValueBlockEditorUnit != null)
            {
                this.inputtedValueBlockEditorUnit.ParentInputDefinitionOfBlockEditorUnit = null;
            }
        
            this.inputtedValueBlockEditorUnit = value;

            if(this.inputtedValueBlockEditorUnit != null)
            {
                this.inputtedValueBlockEditorUnit._RectTransform.SetParent(this.AttachPointRectTransform);
                this.inputtedValueBlockEditorUnit._RectTransform.SetSiblingIndex(this.inputtedValueBlockEditorUnit._RectTransform.childCount - 1);



                this.inputtedValueBlockEditorUnit.ParentInputDefinitionOfBlockEditorUnit = this;
            }

            if (DefaultBlockInputObj != null)
                DefaultBlockInputObj.SetActive(this.inputtedValueBlockEditorUnit == null);

            base.RecursiveRefreshRectTransform();
        }
    }

    sealed public override void Release()
    {
        //Set DropDown Show First Item
        if (this.inputtedValueBlockEditorUnit != null)
        {
            this.inputtedValueBlockEditorUnit.Release();
            this.inputtedValueBlockEditorUnit = null;
        }
           



        base.Release();
    }

    public bool IsEmpty => InputtedValueBlockEditorUnit == null;
    /// <summary>
    /// 1 ~ 4
    /// </summary>
    /// <value>The input index of block unit.</value>
    public int inputIndexOfBlockUnit
    {
        set;
        protected get;
    }

    [SerializeField]
    private RectTransform attachPointRectTransform;
    public RectTransform AttachPointRectTransform => attachPointRectTransform;

    protected override void Awake()
    {
        base.Awake();

        gameObject.tag = InputDefinitionOfBlockEditorUnitTag;
        _Image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private Image _Image;
    public static Color AttachableColor = Color.cyan;
    public void ShowIsAttachable(BlockEditorUnit attachedBlockEditorUnit = null)
    {
        _Image.color = attachedBlockEditorUnit != null ? attachedBlockEditorUnit.BlockColor : Color.white;
    }
}
