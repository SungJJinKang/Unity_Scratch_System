[System.Serializable]
public abstract class BooleanBlock : ValueBlock
{
    public static ValueBlock DefaultValueBlock => new LiteralBooleanBlock(true);

    public abstract bool GetBooleanValue(RobotBase operatingRobotBase);


}
