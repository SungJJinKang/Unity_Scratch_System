using System;

/// <summary>
/// BlockEditorElementAttribute
/// 
/// How To USE :
/// Example )
/// [BlockEditorElementAttribute("Set Variable", typeof(GlobalVariableSelectorDropDownContent).Name, "To", typeof(ReporterBlockInputContent).Name]
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class BlockDefinitionAttribute : Attribute
{
    public enum BlockDefinitionType
    {
        BooleanBlockInput,
        GlobalVariableSelector,
        ReporterBlockInput
    }

    public BlockDefinitionAttribute(params object[] blockDefinitions)
    {
        _BlockDefinitions = blockDefinitions;
    }

    public object[] _BlockDefinitions { get; private set; }
}







