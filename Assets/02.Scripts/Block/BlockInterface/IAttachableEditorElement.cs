using UnityEngine;
public interface IAttachableEditorElement
{
    BlockEditorUnit OwnerBlockEditorUnit { get; }
    RectTransform AttachPointRectTransform { get; }

    void OnRootMockUpSet(BlockEditorUnit attachedBlockEditorUnit, bool isSet);

}
