using System;

/// <summary>
/// BlockEditorElementAttribute
/// 
/// How To USE :
/// Example )
/// [BlockEditorElementAttribute(new TextElementContent("Set Variable"), new GlobalVariableSelectorDropDownContent(), new TextContentInBlockElement("To"), new ReporterBlockInputContent()]
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ElementContentAttribute : Attribute
{
	public ElementContentAttribute(params ElementContent[] elementContents)
	{
        ElementContents = elementContents;

    }

	public ElementContent[] ElementContents { get; private set; }
}







