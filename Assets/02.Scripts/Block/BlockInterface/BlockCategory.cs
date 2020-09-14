
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

[BlockCategoryAttribute("InternetAntenna")]
public interface IInternetAntennaBlockType : IRobotPartBlockType
{
}

///////

// About All Robots
public interface IRobotSystemType
{
}

