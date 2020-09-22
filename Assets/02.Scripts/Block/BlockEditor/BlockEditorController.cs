
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BlockEditorController : MonoBehaviour
{
    [SerializeField]
    private Canvas _Canvas;
    private GraphicRaycaster _GraphicRaycaster;
    private PointerEventData _PointerEventData;

    private void Awake()
    {
        stringBuilder = new StringBuilder();

        _GUIStyle = new GUIStyle();
        _GUIStyle.fontSize = 65;
        _GUIStyle.fontStyle = FontStyle.Bold;
       
        _GUIStyle.normal.textColor = Color.white;
        _GUIStyle.active.textColor = Color.white;
   
    }

    void Start()
    {
        //m_canvas = 자신이 사용하는 캔버스 넣기.
        _GraphicRaycaster = _Canvas.GetComponent<GraphicRaycaster>();
        _PointerEventData = new PointerEventData(null);
    }

    List<RaycastResult> hitUiList;

    void Update()
    {
        UpdateMousePointHitUiList();

    }

    StringBuilder stringBuilder;
    string pointHitResultStr;
    private void UpdateMousePointHitUiList()
    {
        _PointerEventData.position = Input.mousePosition;

        if (this.hitUiList == null)
            this.hitUiList = new List<RaycastResult>();

        this.hitUiList.Clear();
        _GraphicRaycaster.Raycast(_PointerEventData, this.hitUiList);
        DebugHitUiList();

    }

#if UNITY_EDITOR
    private void DebugHitUiList()
    {
        stringBuilder.Clear();
        for (int i = 0; i < this.hitUiList.Count; i++)
        {
            stringBuilder.Append(this.hitUiList[i].gameObject.name + "\n");
        }

        this.pointHitResultStr = stringBuilder.ToString();
    }

    private GUIStyle _GUIStyle;
    [SerializeField]
    private Rect rectPos;
    void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(this.rectPos, this.pointHitResultStr, this._GUIStyle);
    }

   


#endif
}
