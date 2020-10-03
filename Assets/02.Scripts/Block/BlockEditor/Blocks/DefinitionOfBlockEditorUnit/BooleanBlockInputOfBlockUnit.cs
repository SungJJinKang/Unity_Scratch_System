using System;

[System.Serializable]
public sealed class BooleanBlockInputOfBlockUnit : InputDefinitionOfBlockEditorUnit
{
    sealed protected override Type TargetParameterBlockType => typeof(BooleanBlock);

   

    protected override ILiteralBlock DefaultValue
    {
        get => new LiteralBooleanBlock(true);
        set
        {
            //do nothing,
            //because what BooleanBlockInputOfBlockUnit is empty means boolcean value true
        }
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
