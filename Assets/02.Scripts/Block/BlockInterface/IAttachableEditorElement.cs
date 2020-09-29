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

}
