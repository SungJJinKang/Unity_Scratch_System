
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
public interface CustomBlockType { }


public interface ICallCustomBlockType : CustomBlockType
{
    DefinitionCustomBlock CustomBlockDefinitionBlock { get; set; }
}

public interface IDefinitionCustomBlockType : CustomBlockType
{
}


/// <summary>
/// Robot part block type.
/// </summary>
public interface IRobotPartBlockType
{
}

/// <summary>
/// Look BlockCategoryAttribute Constructor
/// "/" works for Root Category
/// </summary>
[BlockCategoryAttribute("RobotPart/RobotBase")] // Look BlockCategoryAttribute Constructor.  "/" works for Root Category
public interface IRobotBaseBlockType : IRobotPartBlockType
{
}


[BlockCategoryAttribute("RobotPart/JetEngine")]
public interface IJetEngineBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("RobotPart/Mining")]
public interface IMiningBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("RobotPart/Speak")]
public interface ISpeakerBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("RobotPart/GPS")]
public interface IGPSBlockType : IRobotPartBlockType
{
}

[BlockCategoryAttribute("RobotPart/InternetAntenna")]
public interface IInternetAntennaBlockType : IRobotPartBlockType
{
}

///////

// About All Robots
public interface IRobotSystemType
{
}

