
using UnityEngine;

public interface IBlockCategory
{ 
}

[BlockColorCategoryAttribute(0.2980392f, 0.5921569f, 1)]
[BlockMainCategoryAttribute("Event")]
public interface IEventBlockType : IBlockCategory
{
}

[BlockColorCategoryAttribute(0.3490196f, 0.7529413f, 0.3490196f)]
[BlockMainCategoryAttribute("Operator")]
public interface IOperatorBlockType : IBlockCategory
{
}

[BlockColorCategoryAttribute(1, 0.5490196f, 0.1019608f)]
[BlockMainCategoryAttribute("Variable")]
public interface IVariableBlockType : IBlockCategory
{
}

[BlockColorCategoryAttribute(1, 0.4f, 0.5019608f)]
[BlockMainCategoryAttribute("CustomBlock")]
public interface CustomBlockType : IBlockCategory 
{ 
}


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
[BlockColorCategoryAttribute(1, 0, 0)]
[BlockMainCategoryAttribute("RobotPart")]
public interface IRobotPartBlockType : IBlockCategory
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