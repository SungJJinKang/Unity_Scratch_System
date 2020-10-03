using System;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public sealed class ReporterBlockInputOfBlockUnit : InputDefinitionOfBlockEditorUnit
{

    [SerializeField]
    private InputField DefaultReporterBlockInputField;

    public void OnEndEditDefaultReporterBlockInputField()
    {
        base.PassParameterToTargetBlock();
    }

    sealed protected override Type TargetParameterBlockType => typeof(ReporterBlock);

    sealed protected override ILiteralBlock DefaultValue 
    {
        set
        {
            ILiteralReporterBlock literalReporterBlock = value as ILiteralReporterBlock;
            DefaultReporterBlockInputField.text = literalReporterBlock.GetStringValue();
        }
        get
        {
            return new LiteralReporterBlock(DefaultReporterBlockInputField.text);
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
