using System;
using System.Numerics;

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

    public readonly object[] _BlockDefinitions;

    public static readonly Vector3 rrr = new Vector3(1, 1, 1);
    public static ref readonly Vector3 R => ref rrr;
}







