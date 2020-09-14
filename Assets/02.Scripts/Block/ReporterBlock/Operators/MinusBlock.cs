[BlockTitle("Minus")]
public sealed class MinusBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        return (base.Input1.GetReporterNumberValue(operatingRobotBase) - base.Input2.GetReporterNumberValue(operatingRobotBase)).ToString();
    }
}
