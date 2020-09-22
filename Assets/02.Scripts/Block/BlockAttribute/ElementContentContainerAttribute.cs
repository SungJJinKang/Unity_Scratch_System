using Boo.Lang;
using System;

/// <summary>
/// BlockEditorElementAttribute
/// 
/// How To USE :
/// Example )
/// [BlockEditorElementAttribute("Set Variable", typeof(GlobalVariableSelectorDropDownContent).Name, "To", typeof(ReporterBlockInputContent).Name]
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ElementContentContainerAttribute : Attribute
{
	public ElementContentContainerAttribute(params string[] elementContents)
	{
         ElementContents = elementContents;
    }

	public string[] ElementContents { get; private set; }
}







