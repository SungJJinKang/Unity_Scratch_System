#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(Block), true)]
public class BlockDrawerUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var BlockIndexInSouceCodeField = new PropertyField(property.FindPropertyRelative("BlockIndexInSouceCode"));
        var IsAllPrameterFilledField = new PropertyField(property.FindPropertyRelative("IsAllPrameterFilled"));
     
        // Add fields to the container.
        container.Add(BlockIndexInSouceCodeField);
        container.Add(IsAllPrameterFilledField);

        return container;
    }
}
#endif