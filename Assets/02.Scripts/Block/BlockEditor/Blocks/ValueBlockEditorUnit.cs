public abstract class ValueBlockEditorUnit : BlockEditorUnit
{
    sealed public override bool IsAttatchable()
    {
        base.AttachableBlockConnector = null;
        return true;
    }
}
