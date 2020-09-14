/// <summary>
/// Event Block
/// This is HatBlock
/// Command is Event
/// 
/// How This Works??
/// From RobotBase, Call StartFlowBlock(RobotBase), Then Flow Block attached To This Block is called
/// 
/// In Block Editor, Place EventBlock, And Set Event Name To Input1, And Attach next block To This Placed Event Block
/// </summary>
public sealed class EventBlock : HatBlock, IContainingParameter<LiteralBlock>
{
    /// <summary>
    /// Event Name
    /// </summary>
    /// <value>The input1.</value>
    public LiteralBlock Input1 { get ; set ; }

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        //DO NOTHING, JUST CALL NEXT BLOCK
    }
}
