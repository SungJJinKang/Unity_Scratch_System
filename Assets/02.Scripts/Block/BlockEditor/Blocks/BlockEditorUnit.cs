﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public abstract class BlockEditorUnit : BlockEditorElement
{
    public const string BlockEditorUnitTag = "BlockEditorUnit";

    [HideInInspector]
    public BlockMockupHelper _BlockMockupHelper;

    public abstract BlockEditorUnit ParentBlockEditorUnit { get; }

    /// <summary>
    /// Return Is RootBlock ( not included or connected to other block )
    /// </summary>
    /// <value><c>true</c> if is root block; otherwise, <c>false</c>.</value>
    public bool IsRootBlock => this.ParentBlockEditorUnit == null;


    protected override void Awake()
    {
        base.Awake();

        gameObject.tag = BlockEditorUnitTag;
        _BlockMockupHelper = GetComponent<BlockMockupHelper>();


    }

    protected override void Start()
    {
        base.Start();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    [Flags]
    public enum BlockEditorUnitFlag
    {
        None = 0,
        IsRemovable = 1,
        IsAttachable = 1 << 1,
        IsDuplicateType = 1 << 2,

    }

    public BlockEditorUnitFlag _BlockEditorUnitFlag;

    public const BlockEditorUnitFlag DefaultFlag = BlockEditorUnitFlag.IsRemovable | BlockEditorUnitFlag.IsAttachable;
    public const BlockEditorUnitFlag ShopBlockFlag = BlockEditorUnitFlag.IsDuplicateType;



    #region BackUpUiPos
    private Vector3 backupedPos;
    private Transform backupedParentTransform;

    public void BackupTransformInfo()
    {
        this.backupedPos = transform.position;
        this.backupedParentTransform = transform.parent;
    }

    public void RevertTransformInfo()
    {
        if (backupedParentTransform == null)
            Debug.LogError("not backuped yet");

        transform.SetParent(this.backupedParentTransform);
        transform.position = this.backupedPos;
    }
    #endregion


    /// <summary>
    /// Duplicate BlockEditorUnit
    /// Not Copying TargetBlock
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public BlockEditorUnit Duplicate(Transform parent = null)
    {
        return BlockEditorManager.instnace.CreateBlockEditorUnit(this.TargetBlock.GetType(), this.ParentBlockEditorWindow, parent);
    }

    public void SaveUnitAnchoredPosition()
    {
        if(this.TargetBlock != null)
            this.TargetBlock.BlockEditorUnitAnchoredPosition = _RectTransform.anchoredPosition;
    }

    public bool ReturnToSavedUnitAnchoredPosition()
    {
        if (this.TargetBlock != null && this.TargetBlock.IsBlockEditorUnitAnchoredPositionSaved && this.IsRootBlock)
        {
            _RectTransform.anchoredPosition = this.TargetBlock.BlockEditorUnitAnchoredPosition;
            return true;
        }
        else
        {
            return false;
        }
           
    }

    private BlockEditorWindow parentBlockEditorWindow;
    public BlockEditorWindow ParentBlockEditorWindow
    {
        get
        {
            return this.parentBlockEditorWindow;
        }
        set
        {
            if (this.parentBlockEditorWindow == value)
                return;

            if (this.parentBlockEditorWindow != null)
            {
                this.parentBlockEditorWindow.RemoveFromSpawnedBlockEditorUnitInSourceCode(this);
            }

            this.parentBlockEditorWindow = value;

            if (this.parentBlockEditorWindow != null)
            {
                this.parentBlockEditorWindow.AddToSpawnedBlockEditorUnitInSourceCode(this);
            }
        }
    }

    public override void OnSpawned()
    {
        base.OnSpawned();

        // basically set defualt block flag
        this._BlockEditorUnitFlag = BlockEditorUnit.DefaultFlag;
    }


    /// <summary>
    /// Release(Destroy) BlockEditorUnit
    /// Disable This Object
    /// Clean ElementOfBlockUnitList
    /// Return back to 
    /// </summary>
    public override void Release()
    {
        this.SaveUnitAnchoredPosition();
        // never touch element of targetBlock. Block is seperate from BlockEditorUnit
        // Removing BlockEditorUnit, Element Of BlockUnit shouldn't effect to Block instance
        this.ParentBlockEditorWindow = null;
        this.targetBlock = null;

        ClearDefinitionOfBlockEditorUnit();

        this._BlockEditorUnitFlag = BlockEditorUnitFlag.None;

        base.Release();
    }



    #region AttachBlock

    /// <summary>
    /// Return IsAttatchable
    /// Return if this BlockEditorUnit can be attached to any InputElementOfBlockUnit or as NextBlock, PreviousBlock
    /// 
    /// Don call this every tick, update
    /// </summary>
    /// <returns></returns>
    public abstract bool IsAttatchable();
    public virtual bool AttachBlock() { return true; }
    public IAttachableEditorElement AttachableEditorElement
    {
        protected set;
        get;
    }

    /// <summary>
    /// Detach From ParentBlock(Input or FlowBlock)
    /// </summary>
    public virtual void OnStartControllingByPlayer()
    {

    }

    public virtual void OnEndControlling()
    {

    }

    #endregion




    #region TargetBlock

    private Block targetBlock;
    public Block TargetBlock
    {
        get
        {
            return this.targetBlock;
        }
        set
        {
            if (targetBlock != null)
            {
                Debug.LogError("You cant set targetBlock already be set");
                return;
            }

            if (BlockShapeAttribute == null)
            {
                Debug.LogError("blockEditorUnitAttribute is null, Fail Set TargetBlock");
            }
            else
            {
                if (value.GetType().IsSubclassOf(TargetBlockType) || TargetBlockType.IsSubclassOf(value.GetType()))
                {// if type of value(TargetBlock) equal with blockEditorUnitAttribute.BlockEditorUnitType
                    this.targetBlock = value;
                    this.OnSetTargetBlock();
                }
                else
                {
                    Debug.LogError("You set wrong Block Type");
                }
            }


        }
    }

    private BlockShapeAttribute blockShapeAttributeCache;
    private BlockShapeAttribute BlockShapeAttribute
    {
        get
        {
            if (this.blockShapeAttributeCache == null)
            {
                this.blockShapeAttributeCache = this.GetType().GetAttribute<BlockShapeAttribute>();
            }

            return this.blockShapeAttributeCache;
        }
    }
    /// <summary>
    /// Target Block Type
    /// Target Block is subclass of This Type
    /// </summary>
    private Type TargetBlockType
    {
        get
        {
            return BlockShapeAttribute.BlockType;
        }
    }


    protected virtual void OnSetTargetBlock()
    {
        if (this.TargetBlock == null)
            return;

        InitBlockColor();
        InitElementsOfBlockUnit();

    }

    public Color BlockColor
    {
        private set;
        get;
    }
    private void InitBlockColor()
    {
        if (base.ColoredBlockImage == null || base.ColoredBlockImage.Count == 0 || this.TargetBlock == null)
            return;

        //IBlockCategory blockCategory = this.TargetBlock as IBlockCategory;
        //Type t = blockCategory.GetType();
        //BlockColorCategoryAttribute blockColorCategoryAttribute = blockCategory.GetType().GetCustomAttribute<BlockColorCategoryAttribute>(true);
        BlockColorCategoryAttribute blockColorCategoryAttribute = Utility.GetAncestorAttribute<BlockColorCategoryAttribute>(this.TargetBlock.GetType());
        if (blockColorCategoryAttribute != null)
        {
            BlockColor = blockColorCategoryAttribute.Color;
            for (int i = 0; i < base.ColoredBlockImage.Count; i++)
            {
                //BlockColor.a = base.ColoredBlockImage[i].color.a;
                base.ColoredBlockImage[i].color = BlockColor;
            }
        }
        else
        {
            Debug.LogError("blockColorCategoryAttribute is null : " + this.TargetBlock.ToString());
        }
    }

    #endregion

    [SerializeField]
    private Transform MainBlockTransform;



    #region ElementsOfBlockUnit

    private List<DefinitionOfBlockEditorUnit> DefinitionOfBlockEditorUnitList;



    //private ElementContent[] ElementContentInBlockElements;

    private void InitElementsOfBlockUnit()
    {
        this.ClearDefinitionOfBlockEditorUnit();

        Type[] parameterTypes = this.TargetBlock.ParametersTypes;

        BlockDefinitionAttribute blockDefinition = this.TargetBlock.GetType().GetCustomAttribute<BlockDefinitionAttribute>();
        if (blockDefinition != null)
        {//If Block class have BlockDefinitionAttribute

            object[] blockDefinitions = blockDefinition._BlockDefinitions;

           
            for (int i = 0; i < blockDefinitions.Length; i++)
            {
                if (blockDefinitions[i] != null)
                {
                    if (blockDefinitions[i] is string)
                    {
                        this.AddDefinitionOfBlockEditorUnit(new TextDefinitionContentOfBlock((string)blockDefinitions[i]));
                    }
                    else if (blockDefinitions[i] is BlockDefinitionAttribute.BlockDefinitionType)
                    {


#if UNITY_EDITOR
                        if(parameterTypes == null || this.parameterIndex >= parameterTypes.Length)
                        {
                            Debug.LogError("Can't add BlockInputDefinitionContent Please Change BlockDefinitionAttribute of Block ( " + this.TargetBlock.GetType().Name + " ) ");
                            continue;
                        }

                        BlockDefinitionAttribute.BlockDefinitionType blockDefinitionType = (BlockDefinitionAttribute.BlockDefinitionType)blockDefinitions[i];

                        if (this.GetIsProperBlockDefinitionType(parameterTypes[this.parameterIndex], blockDefinitionType) == false)
                        {
                            Debug.LogError("Improper BlockDefinitionAttribute of Block ( " + this.TargetBlock.GetType().Name + " ) , " + parameterTypes[this.parameterIndex].Name + " : " + blockDefinitionType.ToString());
                            continue;
                        }
#endif

                        switch (blockDefinitionType)
                        {
                            case BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput:

                                this.AddDefinitionOfBlockEditorUnit(new BooleanBlockInputDefinitionContentOfBlock());
                                break;

                            case BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector:
                                this.AddDefinitionOfBlockEditorUnit(new GlobalVariableSelectorDefinitionContentOfBlock());
                                break;

                            case BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput:
                                this.AddDefinitionOfBlockEditorUnit(new ReporterBlockInputDefinitionContentOfBlock());
                                break;
                        }
                    }
                }
            }

        }
        else
        {//If Block class don't have BlockDefinitionAttribute

            AddDefinitionOfBlockEditorUnit(new TextDefinitionContentOfBlock(this.TargetBlock.GetType().Name));//First Add Text Element with class name


            //Automatically add ElementOfBlockUnit 
            
            if (parameterTypes != null)
            {
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameterTypes[i] != null)
                    {
                        if (parameterTypes[i] == typeof(BooleanBlock) || parameterTypes[i].IsSubclassOf(typeof(BooleanBlock)))
                        {
                            this.AddDefinitionOfBlockEditorUnit(new BooleanBlockInputDefinitionContentOfBlock());
                        }
                        else if (parameterTypes[i] == typeof(ReporterBlock) || parameterTypes[i].IsSubclassOf(typeof(ReporterBlock)))
                        {
                            this.AddDefinitionOfBlockEditorUnit(new ReporterBlockInputDefinitionContentOfBlock());
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

    private bool GetIsProperBlockDefinitionType(Type parameterType, BlockDefinitionAttribute.BlockDefinitionType blockDefinitionType)
    {
        switch(blockDefinitionType)
        {
            case BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput:
                if(typeof(BooleanBlock) == parameterType || parameterType.IsSubclassOf(typeof(BooleanBlock)))
                {
                    return true;
                }
                break;
                

            case BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector:
                if (typeof(ReporterBlock) == parameterType || parameterType.IsSubclassOf(typeof(BooleanBlock)))
                {
                    return true;
                }
                break;


            case BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput:
                if (typeof(ReporterBlock) == parameterType || parameterType.IsSubclassOf(typeof(ReporterBlock)))
                {
                    return true;
                }
                break;

        }

        return false;
    }

    private int parameterIndex = 0;
    /// <summary>
    /// Add Element Of Block Unit In Block Unit UI
    /// </summary>
    /// <param name="definitionContentOfBlock">Element content.</param>
    private DefinitionOfBlockEditorUnit AddDefinitionOfBlockEditorUnit(DefinitionContentOfBlock definitionContentOfBlock)
    {
        DefinitionOfBlockEditorUnit definitionOfBlockEditorUnit = BlockEditorManager.instnace.SpawnDefinitionOfBlockEditorUnit(definitionContentOfBlock);

        if (definitionOfBlockEditorUnit == null)
        {
            Debug.LogError("Cant Find Proper Type : " + definitionContentOfBlock.GetType().Name);
            return null;
        }


        if (this.DefinitionOfBlockEditorUnitList == null)
            this.DefinitionOfBlockEditorUnitList = new List<DefinitionOfBlockEditorUnit>();

        if (this.DefinitionOfBlockEditorUnitList.Contains(definitionOfBlockEditorUnit) == false)
            this.DefinitionOfBlockEditorUnitList.Add(definitionOfBlockEditorUnit);

        definitionOfBlockEditorUnit.transform.SetParent(this.MainBlockTransform);
        definitionOfBlockEditorUnit.transform.localScale = Vector3.one;
        definitionOfBlockEditorUnit.transform.SetSiblingIndex(this.MainBlockTransform.childCount); // place elementOfBlockUnit To the last space of blockeditorunit
        
        definitionOfBlockEditorUnit.OwnerBlockEditorUnit = this;

        //
       
        if(definitionOfBlockEditorUnit is ParameterDefinitionOfBlockEditorUnit)
        {
            ParameterDefinitionOfBlockEditorUnit parameterDefinitionOfBlockEditorUnit = definitionOfBlockEditorUnit as ParameterDefinitionOfBlockEditorUnit;
            parameterDefinitionOfBlockEditorUnit.ParameterIndex = this.parameterIndex;
            this.parameterIndex++;
        }
        //

        definitionOfBlockEditorUnit.SetDefinitionContentOfBlock(definitionContentOfBlock);
        definitionOfBlockEditorUnit.OnSpawned();
        return definitionOfBlockEditorUnit;
    }

    private void ClearDefinitionOfBlockEditorUnit()
    {
        if (this.DefinitionOfBlockEditorUnitList != null && this.DefinitionOfBlockEditorUnitList.Count > 0)
        {
            for (int i = 0; i < this.DefinitionOfBlockEditorUnitList.Count; i++)
            {
                this.DefinitionOfBlockEditorUnitList[i].Release();
            }

            this.DefinitionOfBlockEditorUnitList.Clear();
        }

        this.parameterIndex = 0;
    }


    #endregion


}

#if UNITY_EDITOR

[CustomEditor(typeof(BlockEditorUnit), true)]
public class BlockEditorUnitEditor : Editor
{
    BlockEditorUnit targetBlockEditorUnit;
    private void Awake()
    {
        targetBlockEditorUnit = target as BlockEditorUnit;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if(GUILayout.Button("Debug Target Block Parameter"))
        {
            targetBlockEditorUnit.TargetBlock.DebugParameters();
        }
    }
}


#endif