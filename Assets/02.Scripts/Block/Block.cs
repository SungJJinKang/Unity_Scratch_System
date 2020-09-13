﻿using UnityEngine;
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
    /// This is called just one time when modify sourrcede, so this expensive performance is accepted
    /// </summary>
    public bool IsAllPrameterFilled 
    { 
        get
        {
            IContainingParameter blockParameter = this as IContainingParameter;
            if (blockParameter == null)
            {
                return true; // There is no parameter
            }
            else
            {//There is parameters
                if(blockParameter is IContainingParameter<ICanBeParameter>)
                {//Have 1 Parameter
                    IContainingParameter<ICanBeParameter> blockParameter1 = this as IContainingParameter<ICanBeParameter>;
                    return blockParameter1.Input1 != null;
                }
                else if (blockParameter is IContainingParameter<ICanBeParameter, ICanBeParameter>)
                {//Have 2 Parameter
                    IContainingParameter<ICanBeParameter, ICanBeParameter> blockParameter2 = this as IContainingParameter<ICanBeParameter, ICanBeParameter>;
                    return blockParameter2.Input1 != null && blockParameter2.Input2 != null;
                }
                else if (blockParameter is IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter>)
                {//Have 3 Parameter
                    IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter> blockParameter3 = this as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter>;
                    return blockParameter3.Input1 != null && blockParameter3.Input2 != null && blockParameter3.Input3 != null;
                }
                else if (blockParameter is IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter>)
                {//Have 4 Parameter
                    IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter> blockParameter4 = this as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter>;
                    return blockParameter4.Input1 != null && blockParameter4.Input2 != null && blockParameter4.Input3 != null && blockParameter4.Input4 != null;
                }

            }

            return true;
                
        }
    
    }

     /// <summary>
    /// Get Robot Base Instance of Operating Robot Instance
    /// </summary>
    /// <returns></returns>
    protected RobotBase GetOperatingRobotBase()
    {

        Debug.LogError("Cant Find Robot Base");
        return null;
    }

    /// <summary>
    /// Get Robot Part Instance of Operating Robot Instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetOperatingRobotPart<T>() where T : RobotPart
    {
        return GetOperatingRobotBase()?.GetRobotPart<T>();
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