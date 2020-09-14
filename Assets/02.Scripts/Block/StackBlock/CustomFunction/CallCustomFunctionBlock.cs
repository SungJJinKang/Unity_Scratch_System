using UnityEngine;
/// <summary>
/// Fuction Created By Player
/// This can be used Event through InternetAntenna_SendCommandThroughInternet
/// CustomFunctionBlock Can Have Just ReporterBlock Type !!!
/// </summary>
[System.Serializable]
public abstract class CallCustomFunctionBlock : StackBlock, ICallCustomBlockType
{
    private DefinitionCustomBlock customBlockDefinitionBlock;
    public DefinitionCustomBlock CustomBlockDefinitionBlock
    {
        get
        {
            return this.customBlockDefinitionBlock;
        }
        set
        {
            if (customBlockDefinitionBlock != null)
            {
                Debug.LogError("customBlockDefinitionBlock is already set");
                return;
            }

            this.customBlockDefinitionBlock = value;
        }
    }

    protected abstract void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock);

    public override void Operation()
    {
        if(this.customBlockDefinitionBlock == null)
        {
            Debug.LogError("customBlockDefinitionBlock is null");
            return;
        }

        // Operation Of CallCustomFunctionBlock is Passsing Parameters To CustomBlockDefinitionBlock And Starting Flow Of CustomBlockDefinitionBlock CustomBlockDefinitionBlock
        // Passing Parameter is called at child method
        this.customBlockDefinitionBlock.StartFlowBlock();
    }
}