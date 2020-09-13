[System.Serializable]
public abstract class BooleanBlock : Block, ICanBeParameter
{
    public virtual bool GetBooleanValue()
    {
        return true;
    }


}
