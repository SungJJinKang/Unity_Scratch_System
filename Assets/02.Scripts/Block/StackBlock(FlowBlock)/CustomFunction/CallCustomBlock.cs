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
            Debug.LogError("CustomBlockDefinitionBlock is null!!!!!!!!!!!!!");
            return;
        }


        //Pushing To NextBlock Of CallCustomBlock should be called before Start New Flow
        //Pushing To NextBlock Of CallCustomBlock should be called before Start New Flow
        //Pushing To NextBlock Of CallCustomBlock should be called before Start New Flow
        FlowBlock nextFlowBlock = base.NextBlock as FlowBlock;
        if (nextFlowBlock != null)
        {
            //Add NextBlock To ComeBackFlowBlockStack
            operatingRobotBase.BlockCallStack.Push(nextFlowBlock); // Set NextBlockAfterExitFlow to NextBlock Of This CallCustomBlock
        }



        // Operation Of CallCustomFunctionBlock is Passsing Parameters To CustomBlockDefinitionBlock And Starting Flow Of CustomBlockDefinitionBlock CustomBlockDefinitionBlock
        // Passing Parameter is called at child method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Passing Parameter is called at child method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Passing Parameter is called at child method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        // You should Set CustomBlockLocalVariables before Start CustomBlockDefinitionBlock
        // Maybe CustomBlockLocalVariables was set in child method
        //
        // Add this.CustomBlockDefinitionBlock To WaitingBlock
        operatingRobotBase.SetWaitingBlock(this.CustomBlockDefinitionBlock);
    }

    /// <summary>
    /// EndFlowBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    sealed public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        return true;
    }


}