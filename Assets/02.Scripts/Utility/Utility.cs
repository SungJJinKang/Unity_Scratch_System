﻿using System;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class Utility
{
    private static Camera mainCamera;
    public static Camera MainCamera
    {
        get
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            return mainCamera;
        }
    }

    public static bool IsNumeric(this string str)
    {
        float f;
        return float.TryParse(str, out f);
    }

    public static bool IsNumeric(this string str, out float f)
    {
        return float.TryParse(str, out f);
    }



    public static StringBuilder stringBuilderCache = new StringBuilder();


    public static T GetAncestorAttribute<T>(Type t) where T : Attribute
    {
        if (t == null)
            return null;

        if (t.IsInterface == false)
        {
            Type[] inheritedInterface = t.GetInterfaces();

            for (int i = 0; i < inheritedInterface.Length; i++)
            {
                T type = GetAncestorAttribute<T>(inheritedInterface[i]);
                if (type != null)
                    return type;
            }
        }


        T blockColorCategoryAttribute = t.GetCustomAttribute<T>();
        if (blockColorCategoryAttribute != null)
            return blockColorCategoryAttribute;
        else
            return GetAncestorAttribute<T>(t.BaseType);
    }

}


