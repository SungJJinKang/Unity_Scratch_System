[BlockTitle("Speak")]
public class Speaker_Speak : StackBlock, IContainingParameter<ReporterBlock>, IVariableBlockType
{
    public ReporterBlock Input1 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        Speaker speaker = operatingRobotBase.GetRobotPart<Speaker>();
        if (speaker != null)
        {
            speaker.Speak(this.Input1.GetReporterStringValue(operatingRobotBase));
        }

    }
}
