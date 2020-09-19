using System;
/// <summary>
/// reference from https://en.scratch-wiki.info/wiki/Blocks#Block_Shapes
/// All Global, Local Variable in Block class shouldn't be changed during operating robot except editing block
/// </summary>
[System.Serializable]
public abstract class Block : ICloneable
{
    public byte BlockIndexInSouceCode;

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


    public virtual object Clone()
    {
        Block clonedBlock = (Block)this.MemberwiseClone();
        clonedBlock.BlockIndexInSouceCode = this.BlockIndexInSouceCode;

        if (this is IContainingParameter<ICanBeParameter>)
        {
            IContainingParameter<ICanBeParameter> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ICanBeParameter>;
            IContainingParameter<ICanBeParameter> blockContainingParameter = this as IContainingParameter<ICanBeParameter>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ICanBeParameter;
        }
        else if (this is IContainingParameter<ICanBeParameter, ICanBeParameter>)
        {
            IContainingParameter<ICanBeParameter, ICanBeParameter> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ICanBeParameter, ICanBeParameter>;
            IContainingParameter<ICanBeParameter, ICanBeParameter> blockContainingParameter = this as IContainingParameter<ICanBeParameter, ICanBeParameter>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ICanBeParameter;
        }
        else if (this is IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter>)
        {
            IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter>;
            IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter> blockContainingParameter = this as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input3 = blockContainingParameter.Input3.Clone() as ICanBeParameter;
        }
        else if (this is IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter>)
        {
            IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter>;
            IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter> blockContainingParameter = this as IContainingParameter<ICanBeParameter, ICanBeParameter, ICanBeParameter, ICanBeParameter>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input3 = blockContainingParameter.Input3.Clone() as ICanBeParameter;
            clonedBlockContainingParameter.Input4 = blockContainingParameter.Input4.Clone() as ICanBeParameter;
        }

        return clonedBlock;
    }

   
}


/// <summary>
/// This Block can have PreviousBlock
/// StackBlock, C Block, CapBlock
/// </summary>
public interface UpNotchBlock
{
    FlowBlock PreviousBlock { get; set; }

}

/// <summary>
/// This Block can have NextBlock
/// Hat Block, StackBlock, C Block
/// </summary>
public interface DownBumpBlock
{
    FlowBlock NextBlock { get; set; }

   
}
