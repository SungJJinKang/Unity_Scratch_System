using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// All Objects On Block Editor
/// </summary>
[System.Serializable]
public abstract class BlockEditorElement : MonoBehaviour
{
    /// <summary>
    /// Block that this BlockEditorElement is attached to
    /// </summary>
    public abstract BlockEditorElement ParentBlockEditorElement { get; }

    /// <summary>
    /// Ancestor ParentBlockEditorElement
    /// Highest Parent ParentBlockEditorElement
    /// </summary>
    public BlockEditorElement HighestAncestorBlockEditorElement
    {
        get
        {
            BlockEditorElement parentBlockEditorElement = this.ParentBlockEditorElement;
            if (parentBlockEditorElement == null)
                return this;
            else
                return parentBlockEditorElement.HighestAncestorBlockEditorElement;
        }
    }


    /// <summary>
    /// Call LayoutRebuilder.ForceRebuildLayoutImmediate At All Ancestors BlockEditorElement
    /// </summary>
    public void RecursiveRefreshRectTransform()
    {
        //https://ssscool.tistory.com/367
        //sometimes ContentSizeFilter doesn't work, so this method fix this
        LayoutRebuilder.ForceRebuildLayoutImmediate(this._RectTransform);

        BlockEditorElement blockEditorElement = ParentBlockEditorElement;
        if (blockEditorElement != null)
            blockEditorElement.RecursiveRefreshRectTransform();


    }

    [HideInInspector]
    public RectTransform _RectTransform;

    protected virtual void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
        this.IsSpawned = false;
    }
    protected virtual void Start()
    {
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }



    public bool IsSpawned
    {
        private set;
        get;
    }

    public virtual void OnSpawned()
    {
        this.IsSpawned = true;
    }

    public virtual void Release()
    {
        this.IsSpawned = false;
        PoolManager.Instance.releaseObject(gameObject);
    }

    [SerializeField]
    protected List<Image> ColoredBlockImage;
}
