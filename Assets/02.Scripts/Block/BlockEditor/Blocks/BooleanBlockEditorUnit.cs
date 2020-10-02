using System;

[BlockShapeAttribute(typeof(BooleanBlock))]
public sealed class BooleanBlockEditorUnit : ValueBlockEditorUnit
{
    sealed protected override Type TargetEditorBlockType => typeof(BooleanBlockInputOfBlockUnit);
}
