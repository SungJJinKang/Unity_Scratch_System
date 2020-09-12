/// <summary>
/// reference from https://en.scratch-wiki.info/wiki/Blocks#Block_Shapes
/// </summary>
[System.Serializable]
public abstract class Block
{
    public RobotBase TargetRobotBase;


    public virtual string BlockName { get; set; }

    /// <summary>
    /// Check If All parameters is filled?
    /// </summary>
    public bool IsAllPrameterFilled 
    { 
        get
        {
            BlockParameter blockParameter = this as BlockParameter;
            if (blockParameter == null)
            {
                return true; // There is no parameter
            }
            else
            {//There is parameters
                if(blockParameter is BlockParameter<CanBeParameterBlockInterface>)
                {//Have 1 Parameter
                    BlockParameter<CanBeParameterBlockInterface> blockParameter1 = this as BlockParameter<CanBeParameterBlockInterface>;
                    return blockParameter1.Input1 != null;
                }
                else if (blockParameter is BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface>)
                {//Have 2 Parameter
                    BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface> blockParameter2 = this as BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface>;
                    return blockParameter2.Input1 != null && blockParameter2.Input2 != null;
                }
                else if (blockParameter is BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface>)
                {//Have 3 Parameter
                    BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface> blockParameter3 = this as BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface>;
                    return blockParameter3.Input1 != null && blockParameter3.Input2 != null && blockParameter3.Input3 != null;
                }
                else if (blockParameter is BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface>)
                {//Have 4 Parameter
                    BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface> blockParameter4 = this as BlockParameter<CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface, CanBeParameterBlockInterface>;
                    return blockParameter4.Input1 != null && blockParameter4.Input2 != null && blockParameter4.Input3 != null && blockParameter4.Input4 != null;
                }

            }

            return true;
                
        }
    
    }

    /// <summary>
    /// Get Cached Robot Part of Robot Instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetRobotPart<T>() where T : RobotPart
    {
        return null;
    }
}

/// <summary>
/// StackBlock, C Block, CapBlock
/// </summary>
public interface UpNotchBlock
{
    DownBumpBlock PreviousBlock { get; set; }
}

/// <summary>
/// This Block can have NextBlock
/// Hat Block, StackBlock, C Block
/// </summary>
public interface DownBumpBlock
{
    UpNotchBlock NextBlock { get; set; }
}
