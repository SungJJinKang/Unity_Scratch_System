using System;

public abstract class ValueBlockEditorUnit : BlockEditorUnit
{
    protected abstract Type TargetEditorBlockType { get; }

    sealed public override bool IsAttatchable()
    {
        InputSpaceElementOfBlockUnit topInputSpaceElementOfBlockUnit = BlockEditorController.instance.GetTopInputSpaceElementOfBlockUnit(this.TargetEditorBlockType, transform.position);
   

        if (AttachableEditorElement == null || topInputSpaceElementOfBlockUnit.OwnerBlockUnit == this || topInputSpaceElementOfBlockUnit.IsEmpty == false)
        {
            base.AttachableEditorElement = null;
            return false;
        }
        else
        {
            base.AttachableEditorElement = topInputSpaceElementOfBlockUnit;
            return true;
        }

    }



    sealed public override bool AttachBlock()
    {
        if (base.AttachableEditorElement != null)
        {
            /*
            InputSpaceElementOfBlockUnit inputSpaceElementOfBlockUnit = base.AttachableEditorElement as InputSpaceElementOfBlockUnit;
            if (flowBlockConnector._ConnectorType == FlowBlockConnector.ConnectorType.UpNotch)
            {//if hit connector is up notch type
                ConnectFlowBlockEditorUnit(flowBlockConnector.OwnerFlowBlockEditorUnit.PreviousFlowBlockEditorUnit, this);
                ConnectFlowBlockEditorUnit(this.DescendantBlockUnit, flowBlockConnector.OwnerFlowBlockEditorUnit);

            }
            else
            {
                FlowBlockEditorUnit originalNextBlock = flowBlockConnector.OwnerFlowBlockEditorUnit.NextFlowBlockEditorUnit;
                ConnectFlowBlockEditorUnit(flowBlockConnector.OwnerFlowBlockEditorUnit, this);
                ConnectFlowBlockEditorUnit(this.DescendantBlockUnit, originalNextBlock);
            }
            */
            return true;
        }
        else
        {
            return false;
        }
    }

}
