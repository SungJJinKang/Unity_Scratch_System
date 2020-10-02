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

    sealed protected override Type TargetParameterBlockType => typeof(VariableBlock);

    protected override ValueBlock PassedParameterValueBlock => new VariableBlock(this.GlobalVariableNameListInDropDown[_Dropdown.value]); // 

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
