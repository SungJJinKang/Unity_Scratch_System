using System;

[BlockEditorUnitAttribute(typeof(BooleanBlock))]
public sealed class BooleanBlockEditorUnit : ValueBlockEditorUnit
{
    sealed protected override Type TargetEditorBlockType => typeof(BooleanBlockInputOfBlockUnit);
}
