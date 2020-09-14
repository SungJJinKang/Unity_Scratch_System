[System.Serializable]
public abstract class BooleanBlock : Block, ICanBeParameter
{
    public abstract bool GetBooleanValue(RobotBase operatingRobotBase);


}
