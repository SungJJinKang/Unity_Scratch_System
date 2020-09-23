using UnityEngine;

/// <summary>
/// All Objects On Block Editor
/// </summary>
[System.Serializable]
public abstract class BlockEdidtorElement : MonoBehaviour
{
    public RectTransform _RectTransform;

    public const string BlockEdidtorElementTag = "BlockEdidtorElement";
    protected virtual void Awake()
    {
        gameObject.tag = BlockEdidtorElementTag;

        _RectTransform = GetComponent<RectTransform>();
    }

    public abstract void Release();

}
