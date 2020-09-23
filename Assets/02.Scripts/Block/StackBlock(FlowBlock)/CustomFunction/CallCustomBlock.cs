using UnityEngine;
/// <summary>
/// Fuction Created By Player
/// This can be used Event through InternetAntenna_SendCommandThroughInternet
/// CustomFunctionBlock Can Have Just ReporterBlock Type !!!
/// 
/// 
/// 
/// Please Set Next to Next Block Of CallCustomBlock
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShopAttribute]
public abstract class CallCustomBlock : StackBlock, ICallCustomBlockType
{
    public abstract DefinitionCustomBlock CustomBlockDefinitionBlock { get; }


    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        if (this.CustomBlockDefinitionBlock == null)
        {
            Debug.LogError("CustomBlockDefinitionBlock is null!!!!!!!!!!!!!");
            return;
        }



        //Pass Paramter To 
        this.PassParameterToOperatingRobotBase(operatingRobotBase);

        // Push NextBlock Of CallCustomBlock(Returned Block After End Subroutine(DefinitionCustomBlock) ) To Block Call Stack
        operatingRobotBase.PushToBlockCallStack(this.NextBlock);
    }

    /// <summary>
    /// Pass Parameter Of CallCustomBlock To OperatingRobotBase
    /// </summary>
    /// <param name="operatingRobotBase">Operating robot base.</param>
    protected virtual void PassParameterToOperatingRobotBase(RobotBase operatingRobotBase)
    { }

    /// <summary>
    /// EndFlowBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// Next Block
    /// </returns>
    sealed public override FlowBlock EndFlowBlock(RobotBase operatingRobotBase)
    {
        return this.CustomBlockDefinitionBlock;
    }


}