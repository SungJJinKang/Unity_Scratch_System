using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextElementOfBlockUnit : ElementOfBlockUnit
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

    public override void SetElementContent(ElementContent elementContent)
    {
        base.SetElementContent(elementContent);

        TextElementContent textElementContent = elementContent as TextElementContent;
        if (textElementContent != null)
        {
            this.SetText(textElementContent.Text);
        }
    }
}
