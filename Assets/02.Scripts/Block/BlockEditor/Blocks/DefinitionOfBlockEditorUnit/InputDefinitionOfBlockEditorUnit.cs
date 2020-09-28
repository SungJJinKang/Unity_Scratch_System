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

    private ContentSizeFitter AttachPointRectTransformContentSizeFitter;
    [SerializeField]
    private RectTransform attachPointRectTransform;
    public RectTransform AttachPointRectTransform => attachPointRectTransform;




    [SerializeField]
    private List<Image> MockUpImage;

    public void OnRootMockUpSet(BlockEditorUnit attachedBlockEditorUnit, bool isSet)
    {
        if(this.MockUpImage != null)
        {
            Color color = isSet ? Color.gray : Color.white;
            for (int i = 0; i < this.MockUpImage.Count; i++)
            {
                this.MockUpImage[i].color = color;
            }
        }
        
        /*
        if (_ContentSizeFitterOfAttachPointRectTransform == null)
            _ContentSizeFitterOfAttachPointRectTransform = this.attachPointRectTransform.GetComponentInParent<ContentSizeFitter>();

        _ContentSizeFitterOfAttachPointRectTransform.SetLayoutHorizontal();
        DefaultReporterBlockInputObj.SetActive(!isSet);
        */
    }

    protected override void Awake()
    {
        base.Awake();

        gameObject.tag = InputDefinitionOfBlockEditorUnitTag;
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
}
