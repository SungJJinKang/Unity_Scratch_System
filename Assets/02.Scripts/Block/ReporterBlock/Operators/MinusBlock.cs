[BlockTitle("Minus")]
public sealed class MinusBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue()
    {
        return (base.Input1.GetReporterNumberValue() - base.Input2.GetReporterNumberValue()).ToString();
    }
}
