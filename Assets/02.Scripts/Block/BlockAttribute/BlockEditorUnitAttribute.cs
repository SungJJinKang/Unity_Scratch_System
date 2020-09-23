using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class BlockEditorUnitAttribute : Attribute
{
    public BlockEditorUnitAttribute(Type t)
    {
        if (t.IsSubclassOf(typeof(Block)) == false)
        {
            Debug.LogError("t is not subclass of Block");
            return;
        }

        this.BlockType = t;
    }

    public Type BlockType { get; private set; }
}