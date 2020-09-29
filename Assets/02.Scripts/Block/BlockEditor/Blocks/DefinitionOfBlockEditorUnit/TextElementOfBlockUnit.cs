using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public sealed class TextElementOfBlockUnit : DefinitionOfBlockEditorUnit
{
    [SerializeField]
    private Text _Text;

    public void SetText(string text)
    {
        this._Text.text = text;
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

    public override void SetDefinitionContentOfBlock(DefinitionContentOfBlock definitionContentOfBlock)
    {
        base.SetDefinitionContentOfBlock(definitionContentOfBlock);

        TextDefinitionContentOfBlock textElementContent = definitionContentOfBlock as TextDefinitionContentOfBlock;
        if (textElementContent != null)
        {
            this.SetText(textElementContent.Text);
        }
    }

    sealed public override void Release()
    {
        this.SetText(string.Empty);

        base.Release();
    }
}
