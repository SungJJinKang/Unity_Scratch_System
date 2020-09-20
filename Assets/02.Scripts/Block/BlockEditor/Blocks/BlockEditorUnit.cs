using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public abstract class BlockEditorUnit : MonoBehaviour
{
    private Block targetBlock;
    public Block TargetBlock
    {
        get
        {
            return this.targetBlock;
        }
        set
        {
            this.targetBlock = value;
            OnSetTargetBlock();
        }
    }

    protected virtual void OnSetTargetBlock()
    {
        if (this.targetBlock == null)
            return;

        InitElementsOfBlockUnit();
    }

    [SerializeField]
    private Transform MainBlockTransform;


    public enum ElementTypeInBlockElement
    {
        BooleanBlockInput,
        GlobalVariableSelectorDropDown,
        ReporterBlockInput,
        Text
    }


    private void ClearBlockElement()
    {

    }

    //private ElementContent[] ElementContentInBlockElements;

    private void InitElementsOfBlockUnit()
    {
        this.ClearBlockElement();

        ElementContentAttribute blockEditorElementAttribute = this.targetBlock.GetType().GetCustomAttribute(typeof(BlockEditorUnit)) as ElementContentAttribute;
        if(blockEditorElementAttribute != null)
        {
            //If Block class have ElementContentAttribute
            ElementContent[] elementContents = blockEditorElementAttribute.ElementContents;
            for (int i = 0; i < elementContents.Length; i++)
            {
                if(elementContents[i] != null)
                {
                    this.AddElementOfBlockUnit(elementContents[i]);
                }
            }
        }
        else
        {
            AddElementOfBlockUnit(new TextElementContent(this.targetBlock.GetType().Name));//First Add Text Element with class name

            //If Block class don't have ElementContentAttribute
            //Automatically add ElementOfBlockUnit 
            Type[] parameterTypes = this.targetBlock.ParametersTypes;
            if(parameterTypes != null)
            {
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if(parameterTypes[i] != null)
                    {
                        if (parameterTypes[i] is BooleanBlock)
                        {
                            this.AddElementOfBlockUnit(new BooleanBlockInputContent());
                        }
                        else if (parameterTypes[i] is VariableBlock)
                        {
                            this.AddElementOfBlockUnit(new GlobalVariableSelectorDropDownContent());
                        }
                        else if (parameterTypes[i] is ReporterBlock)
                        {
                            this.AddElementOfBlockUnit(new ReporterBlockInputContent());
                        }
                        else
                        {
                            Debug.LogError("Cant Find Proper Parameter Element. " + parameterTypes[i].Name);
                        }
                    }
                }
            }

        }

    }
        
    /// <summary>
    /// Add Element Of Block Unit In Block Unit UI
    /// </summary>
    /// <param name="elementContent">Element content.</param>
    private void AddElementOfBlockUnit(ElementContent elementContent)
    {

        //Put this ElementOfBlockUnit.OwnerBlockUnit = this; 
    }





}
