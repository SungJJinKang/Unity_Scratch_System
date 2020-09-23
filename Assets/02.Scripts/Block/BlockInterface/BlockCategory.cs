
[BlockMainCategoryAttribute("Event")]
public interface IEventBlockType
{
}

[BlockMainCategoryAttribute("Operator")]
public interface IOperatorBlockType
{
}

[BlockMainCategoryAttribute("Variable")]
public interface IVariableBlockType
{
}

[BlockMainCategoryAttribute("CustomBlock")]
public interface CustomBlockType { }


public interface ICallCustomBlockType : CustomBlockType
{
    DefinitionCustomBlock CustomBlockDefinitionBlock { get; }
}

public interface IDefinitionCustomBlockType : CustomBlockType
{
}


/// <summary>
/// Robot part block type.
/// </summary>
[BlockMainCategoryAttribute("RobotPart")]
public interface IRobotPartBlockType
{
}

/// <summary>
/// Look BlockCategoryAttribute Constructor
/// "/" works for Root Category
/// </summary>
[BlockSubCategoryAttribute("RobotBase")]
public interface IRobotBaseBlockType : IRobotPartBlockType
{
}

[BlockSubCategoryAttribute("JetEngine")]
public interface IJetEngineBlockType : IRobotPartBlockType
{
}

[BlockSubCategoryAttribute("Mining")]
public interface IMiningBlockType : IRobotPartBlockType
{
}

[BlockSubCategoryAttribute("Speaker")]
public interface ISpeakerBlockType : IRobotPartBlockType
{
}

[BlockSubCategoryAttribute("GPS")]
public interface IGPSBlockType : IRobotPartBlockType
{
}

[BlockSubCategoryAttribute("InternetAntenna")]
public interface IInternetAntennaBlockType : IRobotPartBlockType
{
}