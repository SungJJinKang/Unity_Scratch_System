public class InternetAntenna : RobotPart
{
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
    }


    #region Send Data
    /// <summary>
    /// This called From InternetAntenna_SendDataThroughInternet
    /// </summary>
    /// <param name="robotUniqueId">Robot unique identifier.</param>
    /// <param name="variableKey">Variable key.</param>
    /// <param name="data">Data.</param>
    public void SendDataThroughInternet(string robotUniqueId, string variableKey, string data)
    {
        RobotBase recieverRobotBase = RobotSystem.instance.GetSpawnedRobot(robotUniqueId);
        if(recieverRobotBase != null)
        {
            InternetAntenna recieverRobotInternetAntenna = recieverRobotBase.GetRobotPart<InternetAntenna>();
            if(recieverRobotInternetAntenna != null)
            { // To Recieve Data from Other Robot through InternetAntenna, Receiver Robot Should have InternetAntenna Robot Part
                recieverRobotInternetAntenna.RecieveDataThroughInternet(variableKey, data);
            }

        }
    }

    public void RecieveDataThroughInternet(string variableKey, string text)
    {
        base.MotherRobotBase.SetRobotGlobalVariable(variableKey, text);
    }
    #endregion

    #region Send Command
    /// <summary>
    /// This called From InternetAntenna_SendCommandThroughInternet
    /// </summary>
    /// <param name="robotUniqueId">Robot unique identifier.</param>
    /// <param name="commandName">Command Name.</param>
    public void SendCommandThroughInternet(string robotUniqueId, string commandName)
    {
        RobotBase recieverRobotBase = RobotSystem.instance.GetSpawnedRobot(robotUniqueId);
        if (recieverRobotBase != null)
        {
            InternetAntenna recieverRobotInternetAntenna = recieverRobotBase.GetRobotPart<InternetAntenna>();
            if (recieverRobotInternetAntenna != null)
            { // To Recieve Data from Other Robot through InternetAntenna, Receiver Robot Should have InternetAntenna Robot Part
                recieverRobotInternetAntenna.RecieveCommandThroughInternet(commandName);
            }

        }
    }

    public void RecieveCommandThroughInternet(string commandName)
    {
        base.MotherRobotBase.StartEventBlock(commandName); // Command Is Event
    }
    #endregion

}

