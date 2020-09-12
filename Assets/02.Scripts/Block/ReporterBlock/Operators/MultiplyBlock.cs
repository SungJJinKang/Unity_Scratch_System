
[BlockTitle("Multiply")]
public sealed class MultiplyBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue()
    {
        return (base.Input1.GetReporterNumberValue() * base.Input2.GetReporterNumberValue()).ToString();
    }
}
