using Newtonsoft.Json;
/// <summary>
/// Event Block
/// This is HatBlock
[NotAutomaticallyMadeOnBlockShop]
[BlockDefinitionAttribute("Init Hat Block")]
[System.Serializable]
public sealed class InitHatBlock : HatBlock
{

    sealed public override void Operation(RobotBase operatingRobotBase)
    {
        //DO NOTHING, JUST CALL NEXT BLOCK
    }
}
