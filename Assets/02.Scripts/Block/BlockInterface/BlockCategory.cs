
[BlockCategoryAttribute("Event")]
public interface IEventBlockType
{
}

[BlockCategoryAttribute("Operator")]
public interface IOperatorBlockType
{ 
}

[BlockCategoryAttribute("Variable")]
public interface IVariableBlockType
{
}

[BlockCategoryAttribute("CustomFunction")]
public interface ICustomBlockType
{
}

public interface IRobotPartBlockType
{
}

[BlockCategoryAttribute("JetEngine")]
public interface IJetEngineBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("Mining")]
public interface IMiningBlockType : IRobotPartBlockType
{
}

// ,,,, Robot Parts
