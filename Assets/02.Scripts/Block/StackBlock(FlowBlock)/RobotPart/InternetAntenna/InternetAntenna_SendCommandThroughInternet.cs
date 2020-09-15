[BlockTitle("SendCommandThroughInternet")]
public sealed class InternetAntenna_SendCommandThroughInternet : StackBlock, IContainingParameter<ReporterBlock, ReporterBlock>, IInternetAntennaBlockType
{
    /// <summary>
    /// Reciever Robot Unique ID
    /// </summary>
    /// <value>The input1.</value>
    public ReporterBlock Input1 { get; set; }

    /// <summary>
    /// Command Name(Event Name)
    /// </summary>
    /// <value>The input2.</value>
    public ReporterBlock Input2 { get; set; }


    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        InternetAntenna internetAntenna = operatingRobotBase.GetRobotPart<InternetAntenna>();
        if (internetAntenna != null)
        {
            internetAntenna.SendCommandThroughInternet(Input1.GetReporterStringValue(operatingRobotBase), Input2.GetReporterStringValue(operatingRobotBase));
        }

    }
}
