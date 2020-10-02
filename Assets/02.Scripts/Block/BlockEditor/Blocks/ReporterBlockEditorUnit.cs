using System;

[BlockShapeAttribute(typeof(ReporterBlock))]
public sealed class ReporterBlockEditorUnit : ValueBlockEditorUnit
{
    sealed protected override Type TargetEditorBlockType => typeof(ReporterBlockInputOfBlockUnit);
}
