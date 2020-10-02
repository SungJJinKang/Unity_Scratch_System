using UnityEngine;
public interface IAttachableEditorElement
{
    BlockEditorUnit OwnerBlockEditorUnit { get; }
    RectTransform AttachPointRectTransform { get; }

    /// <summary>
    /// if this IAttachableEditorElement can be attached by attachedBlockEditorUnit, parameter paased
    /// if not , passed null
    /// </summary>
    /// <param name="attachedBlockEditorUnit"></param>
    void ShowIsAttachable(BlockEditorUnit attachedBlockEditorUnit = null);
}
