using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public sealed class GlobalVariableSelectorDropDownOfBlockUnit : ParameterDefinitionOfBlockEditorUnit
{
    [SerializeField]
    private Dropdown _Dropdown;

    /// <summary>
    /// GlobalVariableNameList
    /// index of this variable should match with DropDown Field Index
    /// </summary>
    private string[] GlobalVariableNameListInDropDown;
    public void InitGlobalVariableDropdown(string[] globalVariableNameList)
    {
        this.GlobalVariableNameListInDropDown = globalVariableNameList;
    }

    sealed protected override Type TargetParameterBlockType => typeof(ReporterBlock);

    //Pass name of Global Variable
    protected override ValueBlock PassedParameterValueBlock
    {
        set
        {
            LiteralReporterBlock passedParameter = value as LiteralReporterBlock;
            this.SetDropDownValue(passedParameter?.GetReporterStringValue());
        }
        get => new LiteralReporterBlock(this.GlobalVariableNameListInDropDown[_Dropdown.value]); //

    } 

    private void SetDropDownValue(string value)
    {
        _Dropdown.value = 0;

        for (int i = 0; i < this._Dropdown.options.Count; i++)
        {
            if(this._Dropdown.options[i].text.Equals(value))
            {
                _Dropdown.value = i;
            }
        }

    }
    public void OnDropDownValueChanged()
    {
        base.PassParameterToTargetBlock();
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

    sealed public override void Release()
    {
        //Set DropDown Show First Item


        base.Release();
    }
}
