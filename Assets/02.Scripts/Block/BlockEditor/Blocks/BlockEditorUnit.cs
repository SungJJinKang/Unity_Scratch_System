using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public abstract class BlockEditorUnit : BlockEdidtorElement
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
            if(BlockEditorUnitAttribute == null)
            {
                Debug.LogError("blockEditorUnitAttribute is null, Fail Set TargetBlock");
            }    
            else
            {
                if (value.GetType().IsSubclassOf(BlockEditorUnitAttribute.BlockEditorUnitType) || BlockEditorUnitAttribute.BlockEditorUnitType.IsSubclassOf(value.GetType()))
                {// if type of value(TargetBlock) equal with blockEditorUnitAttribute.BlockEditorUnitType
                    this.CleanBlockEditorUnit();
                    this.targetBlock = value;
                    this.OnSetTargetBlock();
                }
            }

            
        }
    }

    private BlockEditorUnitAttribute blockEditorUnitAttributeCache;
    protected BlockEditorUnitAttribute BlockEditorUnitAttribute
    { 
        get
        {
            if(this.blockEditorUnitAttributeCache == null)
            {
                this.blockEditorUnitAttributeCache = this.GetType().GetAttribute<BlockEditorUnitAttribute>();
            }

            return this.blockEditorUnitAttributeCache;
        }
    }


    protected virtual void OnSetTargetBlock()
    {
        if (this.targetBlock == null)
            return;

        InitElementsOfBlockUnit();
    }

    protected virtual void CleanBlockEditorUnit()
    {
        if (this.ElementOfBlockUnitList == null)
            return;

        for (int i = 0; i < this.ElementOfBlockUnitList.Count; i++)
        {
            //Remove this.ElementOfBlockUnitList
            //Release Pool this.ElementOfBlockUnitList[i] sssss
            //Please Use Object Pool system.
            //
        }
    }

    [SerializeField]
    private Transform MainBlockTransform;


    private List<ElementOfBlockUnit> ElementOfBlockUnitList;

   

    //private ElementContent[] ElementContentInBlockElements;

    private void InitElementsOfBlockUnit()
    {
        ElementContentContainerAttribute elementContentContainerAttribute = this.targetBlock.GetType().GetCustomAttribute<ElementContentContainerAttribute>();
        if (elementContentContainerAttribute != null)
        {//If Block class have ElementContentAttribute

            string[] elementContents = elementContentContainerAttribute.ElementContents;
            for (int i = 0; i < elementContents.Length; i++)
            {
                if(elementContents[i] != null)
                {
                    if(elementContents[i] == typeof(BooleanBlockInputContent).Name)
                    {
                        this.AddElementOfBlockUnit(new BooleanBlockInputContent());
                    }
                    else if (elementContents[i] == typeof(GlobalVariableSelectorDropDownContent).Name)
                    {
                        this.AddElementOfBlockUnit(new GlobalVariableSelectorDropDownContent());
                    }
                    else if (elementContents[i] == typeof(ReporterBlockInputContent).Name)
                    {
                        this.AddElementOfBlockUnit(new ReporterBlockInputContent());
                    }
                    else
                    {// if elementContents[i] is just text
                        AddElementOfBlockUnit(new TextElementContent(elementContents[i]));
                    }

                }
            }
        }
        else
        {//If Block class don't have ElementContentAttribute

            AddElementOfBlockUnit(new TextElementContent(this.targetBlock.GetType().Name));//First Add Text Element with class name

            
            //Automatically add ElementOfBlockUnit 
            Type[] parameterTypes = this.targetBlock.ParametersTypes;
            if(parameterTypes != null)
            {
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if(parameterTypes[i] != null)
                    {
                        if (parameterTypes[i] == typeof(BooleanBlock) || parameterTypes[i].IsSubclassOf(typeof(BooleanBlock)))
                        {
                            this.AddElementOfBlockUnit(new BooleanBlockInputContent());
                        }
                        else if (parameterTypes[i] == typeof(VariableBlock) || parameterTypes[i].IsSubclassOf(typeof(VariableBlock)))
                        {
                            this.AddElementOfBlockUnit(new GlobalVariableSelectorDropDownContent());
                        }
                        else if (parameterTypes[i] == typeof(ReporterBlock) || parameterTypes[i].IsSubclassOf(typeof(ReporterBlock)))
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
        ElementOfBlockUnit elementOfBlockUnit = BlockEditorManager.instnace.CreateElementOfBlockUnit(elementContent.GetType());
        if(elementOfBlockUnit != null)
        {
            if (this.ElementOfBlockUnitList == null)
                this.ElementOfBlockUnitList = new List<ElementOfBlockUnit>();

            if(this.ElementOfBlockUnitList.Contains(elementOfBlockUnit) == false)
                this.ElementOfBlockUnitList.Add(elementOfBlockUnit);

            elementOfBlockUnit.transform.SetParent(this.MainBlockTransform);
            elementOfBlockUnit.transform.localScale = Vector3.one;
            elementOfBlockUnit.transform.SetSiblingIndex(this.MainBlockTransform.childCount - 2);
            elementOfBlockUnit.SetElementContent(elementContent);



        }
    }





}
