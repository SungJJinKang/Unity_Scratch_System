using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public sealed class ReporterBlockInputOfBlockUnit : InputDefinitionOfBlockEditorUnit
{

    private ReporterBlock DefaultInputtedReporterBlock
    {
        get
        {
            return new LiteralReporterBlock(DefaultReporterBlockInputField.text);
        }
    }


    [SerializeField]
    private InputField DefaultReporterBlockInputField;





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
