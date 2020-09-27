using UnityEngine;

[System.Serializable]
public abstract class ElementOfBlockUnit : BlockEditorElement
{


    [SerializeField]
    public BlockEditorUnit OwnerBlockUnit
    {
        get;
        set;
    }

    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public virtual void SetElementContent(ElementContent elementContent)
    {

    }

    sealed public override void Release()
    {
        // never touch element of targetBlock. Block is seperate from BlockEditorUnit
        // Removing BlockEditorUnit, Element Of BlockUnit don't effect to Block instance
        OwnerBlockUnit = null;
        PoolManager.Instance.releaseObject(gameObject);
    }

}
