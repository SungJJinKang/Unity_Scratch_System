
[System.Serializable]
public abstract class Function
{
    public string FunctionName;


   

    /// <summary>
    /// Lenth of this value is count of parameter for this method
    /// each item is parameter type of method.
    /// ex) void Move(Text s, Number a) -> ParameterList = [VariableType.Text, VariableType.Number]
    /// </summary>
    public Memory.Variable.VariableType[] ParameterTypeList;
    public Memory.Variable.VariableType ReturnType;

    public virtual bool Operation(RobotBase robot) { }
}


public class TextBasedFunction : Function
{
    public string[] Commands;
    public void LexerAnalysis(string code)
    {

    }
}

public class CodeBasedFunction : Function
{

}
