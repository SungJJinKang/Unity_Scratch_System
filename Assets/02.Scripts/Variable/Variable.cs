[System.Serializable]
public class Variable
{
    [System.Serializable]
    public enum VariableType
    {
        NULL,
        Number,
        Text,
        Boolean
    }

    public VariableType GetVariableType()
    {
        if (this is Number)
            return Variable.VariableType.Number;
        else if (this is Text)
            return Variable.VariableType.Text;
        else if (this is Boolean)
            return Variable.VariableType.Boolean;

        return Variable.VariableType.NULL;
    }

    public virtual object GetValue()
    {
        return null;
    }
}

[System.Serializable]
public class Number : Variable
{
    public float value;

    public Number()
    {
        value = 0;
    }

    public override object GetValue()
    {
        return value;
    }
}

[System.Serializable]
public class Text : Variable
{
    public string value;

    public Text()
    {
        value = "";
    }

    public override object GetValue()
    {
        return value;
    }
}

[System.Serializable]
public class Boolean : Variable
{
    public bool value;

    public Boolean()
    {
        value = false;
    }

    public override object GetValue()
    {
        return value;
    }
}