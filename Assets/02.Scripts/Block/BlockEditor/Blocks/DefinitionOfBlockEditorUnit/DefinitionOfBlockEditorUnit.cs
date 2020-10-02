using UnityEngine;

[System.Serializable]
public abstract class DefinitionOfBlockEditorUnit : BlockEditorElement
{
    sealed public override BlockEditorElement ParentBlockEditorElement => OwnerBlockEditorUnit;


    [SerializeField]
    public BlockEditorUnit OwnerBlockEditorUnit
    {
        get;
        set;
    }

    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected virtual void OnDisable()
    {

    }

    public virtual void SetDefinitionContentOfBlock(DefinitionContentOfBlock definitionContentOfBlock)
    {

    }

    public override void Release()
    {
        // never touch element of targetBlock. Block is seperate from BlockEditorUnit
        // Removing BlockEditorUnit, Element Of BlockUnit don't effect to Block instance
        this.OwnerBlockEditorUnit = null;

        base.Release();
    }

}
