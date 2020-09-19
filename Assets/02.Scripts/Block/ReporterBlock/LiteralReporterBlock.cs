﻿using System;

public sealed class LiteralReporterBlock : ReporterBlock
{
    public LiteralReporterBlock(string value = "")
    {
        if (String.IsNullOrEmpty(value))
            value = System.String.Empty;

        this.LiteralValue = value;
    }

    /// <summary>
    /// LiteralValue는 RobotSourceCode에서 정해지고 각 로봇에서는 변경안된다!!!!!!
    /// READ ONLY
    /// </summary>
    public string LiteralValue;

    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase = null)
    {
        return String.Copy(this.LiteralValue); // For Protecting LiteralValue, return cloned new string instnace
    }

    public override object Clone()
    {
        var block = (LiteralReporterBlock)base.Clone();
        block.LiteralValue = this.LiteralValue;

        return block;
    }

}
