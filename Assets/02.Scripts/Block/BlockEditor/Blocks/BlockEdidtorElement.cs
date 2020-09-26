using UnityEngine;

/// <summary>
/// All Objects On Block Editor
/// </summary>
[System.Serializable]
public abstract class BlockEditorElement : MonoBehaviour
{
    [HideInInspector]
    public RectTransform _RectTransform;

    protected virtual void Awake()
    {

        _RectTransform = GetComponent<RectTransform>();
    }

    public abstract void Release();

}
