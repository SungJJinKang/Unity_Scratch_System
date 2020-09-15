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


        //Pushing To ComeBackFlowBlockStack should be called before Start New Flow
        //Pushing To ComeBackFlowBlockStack should be called before Start New Flow
        //Pushing To ComeBackFlowBlockStack should be called before Start New Flow
        //Pushing To ComeBackFlowBlockStack should be called before Start New Flow
        //Pushing To ComeBackFlowBlockStack should be called before Start New Flow

        FlowBlock nextBlock = base.NextBlock as FlowBlock;
        if (nextBlock != null)
        {
            //Add NextBlock To ComeBackFlowBlockStack
            operatingRobotBase.ComeBackFlowBlockStack.Push(nextBlock); // Set NextBlockAfterExitFlow to NextBlock Of This CallCustomBlock
        }

        // Operation Of CallCustomFunctionBlock is Passsing Parameters To CustomBlockDefinitionBlock And Starting Flow Of CustomBlockDefinitionBlock CustomBlockDefinitionBlock
        // Passing Parameter is called at child method
        //
        //You should Set CustomBlockLocalVariables before Start CustomBlockDefinitionBlock
        //Maybe CustomBlockLocalVariables was set in child method
        //
        //Make New Flow Inside Of Flow
        this.CustomBlockDefinitionBlock.StartFlowBlock(operatingRobotBase);
    }

    /// <summary>
    /// EndFlowBlock
    /// </summary>
    /// <param name="operatingRobotBase"></param>
    /// <returns>
    /// If There is NextBlock , return true.
    /// otherwise, return false
    /// </returns>
    public override bool EndFlowBlock(RobotBase operatingRobotBase)
    {
        return true;
    }
}