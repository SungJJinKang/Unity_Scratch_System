using System;

public sealed class LiteralBlock : ReporterBlock
{
    public LiteralBlock(string value = "")
    {
        this.LiteralValue = value;
    }

    /// <summary>
    /// LiteralValue는 RobotSourceCode에서 정해지고 각 로봇에서는 변경안된다!!!!!!
    /// READ ONLY
    /// </summary>
    public readonly string LiteralValue;

    sealed public override string GetReporterStringValue()
    {
        return String.Copy(this.LiteralValue); // For Protecting LiteralValue, return cloned new string instnace
    }
}
