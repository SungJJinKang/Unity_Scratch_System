﻿using System;
using UnityEngine;

[System.Serializable]
public abstract class ValueBlockEditorUnit : BlockEditorUnit
{
    protected abstract Type TargetEditorBlockType { get; }

    [HideInInspector]
    public InputDefinitionOfBlockEditorUnit ParentInputDefinitionOfBlockEditorUnit;

    sealed public override BlockEditorElement ParentBlockEditorElement => ParentInputDefinitionOfBlockEditorUnit?.OwnerBlockEditorUnit;
    sealed public override BlockEditorUnit ParentBlockEditorUnit => ParentInputDefinitionOfBlockEditorUnit?.OwnerBlockEditorUnit;

    public override void OnStartControllingByPlayer()
    {
        base.OnStartControllingByPlayer();

        if (ParentInputDefinitionOfBlockEditorUnit != null)
        {
            ParentInputDefinitionOfBlockEditorUnit.InputtedValueBlockEditorUnit = null;
        }
    }

    sealed public override bool IsAttatchable()
    {
        InputDefinitionOfBlockEditorUnit topInputDefinitionOfBlockEditorUnit = UiUtility.GetTopBlockEditorElementWithWorldPoint<InputDefinitionOfBlockEditorUnit>(transform.position, InputDefinitionOfBlockEditorUnit.InputDefinitionOfBlockEditorUnitTag, x => x.GetType() == this.TargetEditorBlockType);
        //Debug.Log("topInputSpaceElementOfBlockUnit " + topInputSpaceElementOfBlockUnit?.OwnerBlockEditorUnit?.name);
        if (topInputDefinitionOfBlockEditorUnit == null || topInputDefinitionOfBlockEditorUnit.OwnerBlockEditorUnit == this || topInputDefinitionOfBlockEditorUnit.OwnerBlockEditorUnit._BlockEditorUnitFlag.HasFlag(BlockEditorUnitFlag.IsAttachable) == false || topInputDefinitionOfBlockEditorUnit.IsEmpty == false)
        {
            base.AttachableEditorElement = null;
            return false;
        }
        else
        {
            base.AttachableEditorElement = topInputDefinitionOfBlockEditorUnit;
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
