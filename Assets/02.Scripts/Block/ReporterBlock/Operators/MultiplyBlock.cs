﻿
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "x", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class MultiplyBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        return (base.Input1.GetReporterNumberValue(operatingRobotBase) * base.Input2.GetReporterNumberValue(operatingRobotBase)).ToString();
    }
}
