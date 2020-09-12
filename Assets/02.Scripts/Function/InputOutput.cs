using System.Threading;
public abstract class InputOutput<T>
{
    private T value;

    public T GetValue()
    {
        return this.value;
    }

    public abstract bool SetValue(T parameter);
}

public sealed class InputOutputTest : InputOutput<string>
{
    public override bool SetValue(string parameter)
    {
        throw new System.NotImplementedException();
    }
}

public sealed class InputOutputMemory : InputOutput<Memory>
{
    public override bool SetValue(Memory parameter)
    {
        throw new System.NotImplementedException();
    }
}

public sealed class InputOutputNumber : InputOutput<float>
{
    public override bool SetValue(float parameter)
    {
        throw new System.NotImplementedException();
    }
}
