using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public sealed class ReporterBlockInputOfBlockUnit : InputSpaceElementOfBlockUnit
{
    [SerializeField]
    private ReporterBlockEditorUnit inputtedReporterBlockEditorUnit;
    public ReporterBlockEditorUnit InputtedReporterBlockEditorUnit
    {
        get
        {
            return inputtedReporterBlockEditorUnit;
        }
        set
        {
            this.inputtedReporterBlockEditorUnit = value;
            DefaultReporterBlockInputObj.SetActive(this.inputtedReporterBlockEditorUnit == null);
        }
    }


    private ReporterBlock DefaultInputtedReporterBlock
    {
        get
        {
            return new LiteralReporterBlock(DefaultReporterBlockInputField.text);
        }
    }

    [SerializeField]
    private GameObject DefaultReporterBlockInputObj;
    [SerializeField]
    private InputField DefaultReporterBlockInputField;




    sealed public override bool IsEmpty
    {
        get
        {
            return InputtedReporterBlockEditorUnit == null;
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
