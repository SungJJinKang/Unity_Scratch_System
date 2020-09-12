using System.Text;

[BlockTitle("Add")]
public sealed class AddBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue()
    {
        return (base.Input1.GetReporterNumberValue() + base.Input2.GetReporterNumberValue()).ToString();
    }
}
