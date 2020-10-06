using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
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

    private static JsonSerializerSettings jsonSerializerSettings;
#if UNITY_EDITOR
    public static ITraceWriter TraceWriter;
#endif
    public static JsonSerializerSettings JsonSerializerSettings
    {
        get
        {
            if (jsonSerializerSettings == null)
            {
                jsonSerializerSettings = new JsonSerializerSettings();
                jsonSerializerSettings.Converters.Add(new Vector2Converter());
                jsonSerializerSettings.Converters.Add(new BlockConverter());
                jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
                jsonSerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
                jsonSerializerSettings.Formatting = Formatting.Indented;

#if UNITY_EDITOR
                TraceWriter = new MemoryTraceWriter();
                jsonSerializerSettings.TraceWriter = TraceWriter;
#endif

            }




            return jsonSerializerSettings;
        }
    }
}


