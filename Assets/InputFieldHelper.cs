using UnityEngine;
using UnityEngine.UI;

public class InputFieldHelper : MonoBehaviour
{
    [SerializeField]
    private ContentSizeFitter _ContentSizeFitter;

    public void OnValueChanged()
    {
        _ContentSizeFitter.SetLayoutHorizontal();
    }
}
