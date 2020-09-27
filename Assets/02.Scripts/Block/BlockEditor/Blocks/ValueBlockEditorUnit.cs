using System;
using UnityEngine;

[System.Serializable]
public abstract class ValueBlockEditorUnit : BlockEditorUnit
{
    protected abstract Type TargetEditorBlockType { get; }

    public InputSpaceElementOfBlockUnit _InputSpaceElementOfBlockUnit;

    sealed public override BlockEditorElement ParentBlockEditorElement => _InputSpaceElementOfBlockUnit?.OwnerBlockEditorUnit;

    public override void OnStartControllingByPlayer()
    {
        base.OnStartControllingByPlayer();

        if(_InputSpaceElementOfBlockUnit != null)
        {
            _InputSpaceElementOfBlockUnit.InputtedValueBlockEditorUnit = null;
        }
    }

    sealed public override bool IsAttatchable()
    {
        InputSpaceElementOfBlockUnit topInputSpaceElementOfBlockUnit = BlockEditorController.instance.GetTopInputSpaceElementOfBlockUnit(this.TargetEditorBlockType, transform.position);
        //Debug.Log("topInputSpaceElementOfBlockUnit " + topInputSpaceElementOfBlockUnit?.OwnerBlockEditorUnit?.name);
        if (topInputSpaceElementOfBlockUnit == null || topInputSpaceElementOfBlockUnit.OwnerBlockEditorUnit == this || topInputSpaceElementOfBlockUnit.OwnerBlockEditorUnit.IsShopBlock == true || topInputSpaceElementOfBlockUnit.IsEmpty == false)
        {
            base.AttachableEditorElement = null;
            return false;
        }
        else
        {
            base.AttachableEditorElement = topInputSpaceElementOfBlockUnit;
            Debug.Log("topInputSpaceElementOfBlockUnit hit" + base.AttachableEditorElement?.OwnerBlockEditorUnit?.name);
            return true;
        }

    }



    sealed public override bool AttachBlock()
    {
        if (base.AttachableEditorElement != null)
        {
            
            InputSpaceElementOfBlockUnit inputSpaceElementOfBlockUnit = base.AttachableEditorElement as InputSpaceElementOfBlockUnit;
            inputSpaceElementOfBlockUnit.InputtedValueBlockEditorUnit = this;


            return true;
        }
        else
        {
            return false;
        }
    }

}
