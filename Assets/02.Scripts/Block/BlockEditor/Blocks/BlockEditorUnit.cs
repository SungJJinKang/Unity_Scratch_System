using System;
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

    protected override void Awake()
    {
        base.Awake();

        _BlockMockupHelper = GetComponent<BlockMockupHelper>();

        gameObject.tag = BlockEditorUnitTag;

        this.IsRemovable = true;
        this.IsShopBlock = false;

       
    }

    [HideInInspector]
    public bool IsShopBlock = false;
    [HideInInspector]
    public bool IsRemovable = true;

    #region BackUpUiPos
    private Vector3 backupedPos;
    private Transform backupedParentTransform;

    public void BackupUiTransform()
    {
        this.backupedPos = transform.position;
        this.backupedParentTransform = transform.parent;
    }

    public void RevertUiPos()
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
        return BlockEditorManager.instnace.CreateBlockEditorUnit(this.TargetBlock.GetType(), parent);
    }

    /// <summary>
    /// Release(Destroy) BlockEditorUnit
    /// Disable This Object
    /// Clean ElementOfBlockUnitList
    /// Return back to 
    /// </summary>
    sealed public override void Release()
    {
        // never touch element of targetBlock. Block is seperate from BlockEditorUnit
        // Removing BlockEditorUnit, Element Of BlockUnit shouldn't effect to Block instance
        this.targetBlock = null;

        if (this.ElementOfBlockUnitList != null && this.ElementOfBlockUnitList.Count > 0)
        {
            for (int i = 0; i < this.ElementOfBlockUnitList.Count; i++)
            {
                this.ElementOfBlockUnitList[i].Release();
            }

            this.ElementOfBlockUnitList.Clear();
        }

        PoolManager.Instance.releaseObject(gameObject);
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
    public BlockConnector AttachableBlockConnector
    {
        protected set;
        get;
    }
    public virtual bool AttachBlock() { return true; }
    public virtual Vector3 GetAttachPoint() { return Vector3.zero; }



    public virtual void OnStartControlling()
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

            if (BlockEditorUnitAttribute == null)
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
            }


        }
    }

    private BlockEditorUnitAttribute blockEditorUnitAttributeCache;
    private BlockEditorUnitAttribute BlockEditorUnitAttribute
    {
        get
        {
            if (this.blockEditorUnitAttributeCache == null)
            {
                this.blockEditorUnitAttributeCache = this.GetType().GetAttribute<BlockEditorUnitAttribute>();
            }

            return this.blockEditorUnitAttributeCache;
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
            return BlockEditorUnitAttribute.BlockType;
        }
    }


    protected virtual void OnSetTargetBlock()
    {
        if (this.targetBlock == null)
            return;

        InitElementsOfBlockUnit();
    }

    #endregion

    [SerializeField]
    private Transform MainBlockTransform;



    #region ElementsOfBlockUnit

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
                if (elementContents[i] != null)
                {
                    if (elementContents[i] == typeof(BooleanBlockInputContent).Name)
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
            if (parameterTypes != null)
            {
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameterTypes[i] != null)
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
    private ElementOfBlockUnit AddElementOfBlockUnit(ElementContent elementContent)
    {
        ElementOfBlockUnit elementOfBlockUnit = BlockEditorManager.instnace.SpawnElementOfBlockUnit(elementContent);

        if (elementOfBlockUnit == null)
        {
            Debug.LogError("Cant Find Proper Type : " + elementContent.GetType().Name);
            return null;
        }

        if (this.ElementOfBlockUnitList == null)
            this.ElementOfBlockUnitList = new List<ElementOfBlockUnit>();

        if (this.ElementOfBlockUnitList.Contains(elementOfBlockUnit) == false)
            this.ElementOfBlockUnitList.Add(elementOfBlockUnit);

        elementOfBlockUnit.transform.SetParent(this.MainBlockTransform);
        elementOfBlockUnit.transform.localScale = Vector3.one;
        elementOfBlockUnit.transform.SetSiblingIndex(this.MainBlockTransform.childCount - 2); // place elementOfBlockUnit To the last space of blockeditorunit
        elementOfBlockUnit.OwnerBlockUnit = this;
        elementOfBlockUnit.SetElementContent(elementContent);
        return elementOfBlockUnit;
    }

    #endregion


}

#if UNITY_EDITOR

[CustomEditor(typeof(BlockEditorUnit), true)]
public class BlockEditorUnitEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        
    }
}


#endif