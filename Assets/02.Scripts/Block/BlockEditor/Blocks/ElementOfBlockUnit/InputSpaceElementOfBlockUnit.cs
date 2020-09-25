[System.Serializable]
public abstract class InputSpaceElementOfBlockUnit : ElementOfBlockUnit
{
    public const string InputSpaceElementOfBlockUnitTag = "InputSpaceElementOfBlockUnit";
    /// <summary>
    /// 1 ~ 4
    /// </summary>
    /// <value>The input index of block unit.</value>
    public int inputIndexOfBlockUnit
    {
        set;
        protected get;
    }

    protected override void Awake()
    {
        base.Awake();

        gameObject.tag = InputSpaceElementOfBlockUnitTag;
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
