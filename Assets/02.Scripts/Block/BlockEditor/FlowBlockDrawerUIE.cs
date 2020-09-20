#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(FlowBlock), true)]
public class FlowBlockDrawerUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var DurationTimeField = new PropertyField(property.FindPropertyRelative("durationTime"));
    
        // Add fields to the container.
        container.Add(DurationTimeField);

        return container;
    }
}
#endif