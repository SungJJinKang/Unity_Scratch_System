[System.Serializable]
public abstract class BooleanBlock : Block, CanBeParameterBlockInterface
{
    public virtual bool GetBooleanValue()
    {
        return true;
    }


}
