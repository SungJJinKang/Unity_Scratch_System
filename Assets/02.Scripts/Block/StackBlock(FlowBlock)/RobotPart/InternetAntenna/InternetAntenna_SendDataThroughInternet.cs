
public sealed class InternetAntenna_SendDataThroughInternet : StackBlock, IContainingParameter<ReporterBlock, ReporterBlock, ReporterBlock>, IInternetAntennaBlockType
{
    /// <summary>
    /// Reciever Robot Unique ID
    /// </summary>
    /// <value>The input1.</value>
    public ReporterBlock Input1 { get; set; }

    /// <summary>
    ///  Memory Variable Key Of Sended Data
    /// </summary>
    /// <value>The input2.</value>
    public ReporterBlock Input2 { get; set; }

    /// <summary>
    /// Sended Data
    /// </summary>
    /// <value>The input3.</value>
    public ReporterBlock Input3 { get; set; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        InternetAntenna internetAntenna = operatingRobotBase.GetRobotPart<InternetAntenna>();
        if (internetAntenna != null)
        {
            internetAntenna.SendDataThroughInternet(Input1.GetReporterStringValue(operatingRobotBase), Input2.GetReporterStringValue(operatingRobotBase), Input3.GetReporterStringValue(operatingRobotBase));
        }

    }
}
