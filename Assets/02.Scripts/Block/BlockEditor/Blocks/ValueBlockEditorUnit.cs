using System;
using UnityEngine;

[System.Serializable]
public abstract class ValueBlockEditorUnit : BlockEditorUnit
{
    protected abstract Type TargetEditorBlockType { get; }

    [HideInInspector]
    public InputDefinitionOfBlockEditorUnit ParentInputDefinitionOfBlockEditorUnit;

    sealed public override BlockEditorElement ParentBlockEditorElement => ParentInputDefinitionOfBlockEditorUnit?.OwnerBlockEditorUnit;

    public override void OnStartControllingByPlayer()
    {
        base.OnStartControllingByPlayer();

        if(ParentInputDefinitionOfBlockEditorUnit != null)
        {
            ParentInputDefinitionOfBlockEditorUnit.InputtedValueBlockEditorUnit = null;
        }
    }

    sealed public override bool IsAttatchable()
    {
        InputDefinitionOfBlockEditorUnit topInputDefinitionOfBlockEditorUnit = BlockEditorController.instance.GetTopInputSpaceElementOfBlockUnit(this.TargetEditorBlockType, transform.position);
        //Debug.Log("topInputSpaceElementOfBlockUnit " + topInputSpaceElementOfBlockUnit?.OwnerBlockEditorUnit?.name);
        if (topInputDefinitionOfBlockEditorUnit == null || topInputDefinitionOfBlockEditorUnit.OwnerBlockEditorUnit == this || topInputDefinitionOfBlockEditorUnit.OwnerBlockEditorUnit.IsShopBlock == true || topInputDefinitionOfBlockEditorUnit.IsEmpty == false)
        {
            base.AttachableEditorElement = null;
            return false;
        }
        else
        {
            base.AttachableEditorElement = topInputDefinitionOfBlockEditorUnit;
            Debug.Log("topInputSpaceElementOfBlockUnit hit" + base.AttachableEditorElement?.OwnerBlockEditorUnit?.name);
            return true;
        }

    }



    sealed public override bool AttachBlock()
    {
        if (base.AttachableEditorElement != null)
        {
            
            InputDefinitionOfBlockEditorUnit inputDefinitionOfBlockEditorUnit = base.AttachableEditorElement as InputDefinitionOfBlockEditorUnit;
            inputDefinitionOfBlockEditorUnit.InputtedValueBlockEditorUnit = this;


            return true;
        }
        else
        {
            return false;
        }
    }

}
