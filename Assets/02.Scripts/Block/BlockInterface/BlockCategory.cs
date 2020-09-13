
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


/// <summary>
/// Robot part block type.
/// </summary>
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

[BlockCategoryAttribute("Speak")]
public interface ISpeakerBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("GPS")]
public interface IGPSBlockType : IRobotPartBlockType
{
}

///////

// About All Robots
public interface IRobotSystemType
{
}

