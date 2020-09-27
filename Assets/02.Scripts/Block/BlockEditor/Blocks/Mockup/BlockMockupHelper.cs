using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMockupHelper : MonoBehaviour
{
    [HideInInspector]
    public RectTransform _RectTransform;

    private void Awake()
    {
        _RectTransform = GetComponent<RectTransform>();
    }

    [SerializeField]
    public Image[] images;

    public void CopyTargetBlock(BlockEditorUnit blockEditorUnit)
    {
        if (blockEditorUnit == null)
            return;

        //transform.SetParent(blockEditorUnit.transform.parent);
        transform.SetAsFirstSibling();

        BlockMockupHelper targetBlockMockupHelper = blockEditorUnit._BlockMockupHelper;
        for (int i = 0; i < this.images.Length; i++)
        {
            if(targetBlockMockupHelper.images.Length > i)
            {
                if (targetBlockMockupHelper.images[i] == null)
                    continue;

                this.images[i].gameObject.SetActive(targetBlockMockupHelper.images[i].gameObject.activeSelf);

                if(this.images[i].gameObject.activeSelf == true)
                {
                    this.images[i].sprite = targetBlockMockupHelper.images[i].sprite;
                    this.images[i].rectTransform.pivot = targetBlockMockupHelper.images[i].rectTransform.pivot;
                    this.images[i].rectTransform.anchoredPosition = targetBlockMockupHelper.images[i].rectTransform.anchoredPosition;
                    this.images[i].rectTransform.sizeDelta = targetBlockMockupHelper.images[i].rectTransform.sizeDelta;
                }
            }
            else
            {
                this.images[i].gameObject.SetActive(false);
            }
        }
    }
}
