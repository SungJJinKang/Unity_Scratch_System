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
public abstract class CallCustomBlock : StackBlock, ICallCustomBlockType
{
    public abstract DefinitionCustomBlock CustomBlockDefinitionBlock { get; }

  
    public override void Operation(RobotBase operatingRobotBase)
    {
        if(this.CustomBlockDefinitionBlock == null)
        {
            Debug.LogError("customBlockDefinitionBlock is null");
            return;
        }

        // Operation Of CallCustomFunctionBlock is Passsing Parameters To CustomBlockDefinitionBlock And Starting Flow Of CustomBlockDefinitionBlock CustomBlockDefinitionBlock
        // Passing Parameter is called at child method
        //
        //You should Set CustomBlockLocalVariables before Start CustomBlockDefinitionBlock
        //Maybe CustomBlockLocalVariables was set in child method
        this.CustomBlockDefinitionBlock.StartFlowBlock(operatingRobotBase);
    }

    public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        operatingRobotBase.ComeBackFlowBlockAfterFinishFlow = base.NextBlock as FlowBlock; // Set NextBlockAfterExitFlow to NextBlock Of This CallCustomBlock
        return true; 

    }
}