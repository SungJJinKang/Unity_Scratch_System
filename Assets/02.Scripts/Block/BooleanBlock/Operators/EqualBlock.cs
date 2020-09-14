﻿using UnityEngine;
[BlockTitle("Equal")]
public sealed class EqualBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue(RobotBase operatingRobotBase)
    {
        return Mathf.Approximately(base.Input1.GetReporterNumberValue(operatingRobotBase), base.Input2.GetReporterNumberValue(operatingRobotBase));
    }
}
