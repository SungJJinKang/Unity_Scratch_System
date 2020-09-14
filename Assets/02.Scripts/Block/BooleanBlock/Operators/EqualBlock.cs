using UnityEngine;
[BlockTitle("Equal")]
public sealed class EqualBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue()
    {
        return Mathf.Approximately(base.Input1.GetReporterNumberValue(), base.Input2.GetReporterNumberValue());
    }
}
