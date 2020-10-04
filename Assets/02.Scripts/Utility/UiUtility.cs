using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class UiUtility : MonoBehaviour
{
    public static UiUtility instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Update()
    {
        MousePointRaycastResultCache = null;
        MousePointRaycastResultCacheDeepCopied = false;
    }

    private static bool MousePointRaycastResultCacheDeepCopied = false;
    private static List<RaycastResult> MousePointRaycastResultCache;

    public static List<RaycastResult> hitUiList
    {
        private set;
        get;
    }

    private static Canvas targetCanvas;
    public static Canvas TargetCanvas
    {
        get
        {
            return targetCanvas;
        }
        set
        {
            _GraphicRaycaster = null;

            targetCanvas = value;

            _GraphicRaycaster = targetCanvas?.GetComponent<GraphicRaycaster>();
        }
    }


    private static GraphicRaycaster _GraphicRaycaster;

   

    private static PointerEventData _PointerEventData = new PointerEventData(null);

    public static List<RaycastResult> GetUiRayHitListWithWorldPoint(Vector3 worldPoint)
    {
        return GetUiRayHitListWithScreenPoint(RectTransformUtility.WorldToScreenPoint(Utility.MainCamera, worldPoint));
    }

    private static bool IsMousePosition(Vector3 screenPoint)
    {
        return Vector3.Distance(Input.mousePosition, screenPoint) < 0.5f;
    }

    /// <summary>
    /// Dont Call this at every tick(update)
    /// </summary>
    /// <param name="screenPoint"></param>
    public static List<RaycastResult> GetUiRayHitListWithScreenPoint(Vector3 screenPoint)
    {
        bool isMousePoint = IsMousePosition(screenPoint);
        if (isMousePoint && MousePointRaycastResultCache != null)
        {
            Debug.Log("MousePointRaycastResultCache hit");
            return MousePointRaycastResultCache; // hit mouse point raycastResultCache hit
        }

        if (isMousePoint == false && MousePointRaycastResultCache != null && MousePointRaycastResultCacheDeepCopied == false)
        {
            MousePointRaycastResultCache.AddRange(MousePointRaycastResultCache.ToArray());
            MousePointRaycastResultCacheDeepCopied = true;
        }

        if (hitUiList == null)
            hitUiList = new List<RaycastResult>(15);

        hitUiList.Clear();

        _PointerEventData.position = screenPoint;
        _GraphicRaycaster.Raycast(_PointerEventData, hitUiList);

        if (isMousePoint == true && MousePointRaycastResultCache == null)
        { // save to MousePointRaycastResultCache
            MousePointRaycastResultCache = hitUiList;
        }


        return hitUiList;
    }

    public static T GetTopBlockEditorElementWithWorldPoint<T>(Vector3 worldPoint, string compareTag, Predicate<T> match = null)
    {
        return GetTopBlockEditorElementWithScreenPoint<T>(RectTransformUtility.WorldToScreenPoint(Utility.MainCamera, worldPoint), compareTag, match);
    }


    public static T GetTopBlockEditorElementWithScreenPoint<T>(Vector3 screenPoint, string compareTag, Predicate<T> match = null)
    {
        List<RaycastResult> raycastResults = GetUiRayHitListWithScreenPoint(screenPoint);
        if (raycastResults.Count > 0)
        {
            for (int i = 0; i < raycastResults.Count; i++)
            {//Top Object comes first

                if (string.IsNullOrEmpty(compareTag) == false)
                {//if compareTag exists
                    if (raycastResults[i].gameObject.CompareTag(compareTag) == false)
                        continue; // tag is different
                }

                T hitBlockEditorElement = raycastResults[i].gameObject.GetComponent<T>();
                if (hitBlockEditorElement != null)
                {//find type t hitObj
                    if (match != null && match(hitBlockEditorElement) == false)
                    {// if match exists and match fail
                        continue;
                    }
                    return hitBlockEditorElement;
                }

            }
        }

        return default(T);
    }

    /// <summary>
    /// Set Ui Position To Mouse point
    /// </summary>
    /// <param name="uiTransform"></param>
    /// <param name="parentRect"></param>
    /// <param name="offset"></param>
    public static Vector3 GetUiWorldPos(RectTransform parentRect, Vector3 ScreenPos)
    {
        if (parentRect == null)
            return Vector3.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, ScreenPos, TargetCanvas.worldCamera, out Vector2 mousePosOnBlockEditorBodyTransform);
        return parentRect.TransformPoint(mousePosOnBlockEditorBodyTransform);
    }



}