﻿/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallTwoParameterCustomBlock : CallCustomFunctionBlock, IContainingParameter<ReporterBlock, ReporterBlock>
{
    private DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock;

    public ReporterBlock Input1 { get; set; }
    public ReporterBlock Input2 { get; set; }

    sealed public override void Operation()
    {
        this.definitionTwoParameterCustomBlock.CopyParamter(this.Input1, this.Input2);
        base.Operation();
    }

    protected override void OnSetCustomBlockDefinitionBlock(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        this.definitionTwoParameterCustomBlock = customBlockDefinitionBlock as DefinitionTwoParameterCustomBlock;
    }
}
