using UnityEngine;
public interface IAttachableEditorElement
{
    BlockEditorUnit OwnerBlockEditorUnit { get; }
    RectTransform AttachPointRectTransform { get; }

    /// <summary>
    /// when MockUp is Removed(destroyed), pass null,  
    /// </summary>
    /// <param name="attachedBlockEditorUnit"></param>
    void OnRootMockUpSet(BlockEditorUnit attachedBlockEditorUnit);

    /// <summary>
    /// if this IAttachableEditorElement can be attached by attachedBlockEditorUnit, parameter paased
    /// if not , passed null
    /// </summary>
    /// <param name="attachedBlockEditorUnit"></param>
    void OnSetIsAttachable(BlockEditorUnit attachedBlockEditorUnit = null);
}
