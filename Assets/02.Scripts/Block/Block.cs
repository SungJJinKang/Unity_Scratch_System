using System;
using System.Linq;
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
                if(blockParameter is IContainingParameter<ValueBlock>)
                {//Have 1 Parameter
                    IContainingParameter<ValueBlock> blockParameter1 = this as IContainingParameter<ValueBlock>;
                    return blockParameter1.Input1 != null;
                }
                else if (blockParameter is IContainingParameter<ValueBlock, ValueBlock>)
                {//Have 2 Parameter
                    IContainingParameter<ValueBlock, ValueBlock> blockParameter2 = this as IContainingParameter<ValueBlock, ValueBlock>;
                    return blockParameter2.Input1 != null && blockParameter2.Input2 != null;
                }
                else if (blockParameter is IContainingParameter<ValueBlock, ValueBlock, ValueBlock>)
                {//Have 3 Parameter
                    IContainingParameter<ValueBlock, ValueBlock, ValueBlock> blockParameter3 = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock>;
                    return blockParameter3.Input1 != null && blockParameter3.Input2 != null && blockParameter3.Input3 != null;
                }
                else if (blockParameter is IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>)
                {//Have 4 Parameter
                    IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock> blockParameter4 = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>;
                    return blockParameter4.Input1 != null && blockParameter4.Input2 != null && blockParameter4.Input3 != null && blockParameter4.Input4 != null;
                }

            }

            return true;
                
        }
    
    }

    public Type[] ParametersTypes
    {
        get
        {
            IContainingParameter blockParameter = this as IContainingParameter;
            if (blockParameter != null)
            {
                
                if (this.GetType().GetInterfaces()
    .Where(i => i.IsGenericType)
    .Select(i => i.GetGenericTypeDefinition())
    .Contains(typeof(IContainingParameter<>)))
                {//Have 1 Parameter

                    //이게 안되는 이유 https://stackoverflow.com/questions/58592905/c-sharp-downcasting-generic-object-with-derived-interfaces-to-the-base-interfa/
                    // IContainingParameter<ValueBlock>와 IContainingParameter<ReporterBlock> 은 근본적으로 다른 타입이다
                    // ReporterBlock는 ValueBlock를 상속하지만 IContainingParameter<ValueBlock>와 IContainingParameter<ReporterBlock>는 완전 별개의 타입이다
                    IContainingParameter<ValueBlock> blockParameter1 = this as IContainingParameter<ValueBlock>;
                    return new Type[] { blockParameter1.Input1.GetType() };
                }
                else if (this.GetType().GetInterfaces()
    .Where(i => i.IsGenericType)
    .Select(i => i.GetGenericTypeDefinition())
    .Contains(typeof(IContainingParameter<,>)))
                {//Have 2 Parameter
                    //IContainingParameter<ICanBeParameter, ICanBeParameter> blockParameter2 = this as IContainingParameter<ICanBeParameter, ICanBeParameter>;
                    IContainingParameter<ValueBlock, ValueBlock> blockParameter2 = this as IContainingParameter<ValueBlock, ValueBlock>;
                    return new Type[] { blockParameter2.Input1.GetType(), blockParameter2.Input2.GetType() };
                }
                else if (this.GetType().GetInterfaces()
    .Where(i => i.IsGenericType)
    .Select(i => i.GetGenericTypeDefinition())
    .Contains(typeof(IContainingParameter<,,>)))
                {//Have 3 Parameter
                    IContainingParameter<ValueBlock, ValueBlock, ValueBlock> blockParameter3 = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock>;
                    return new Type[] { blockParameter3.Input1.GetType(), blockParameter3.Input2.GetType(), blockParameter3.Input3.GetType() };
                }
                else if (this.GetType().GetInterfaces()
    .Where(i => i.IsGenericType)
    .Select(i => i.GetGenericTypeDefinition())
    .Contains(typeof(IContainingParameter<,,,>)))
                {//Have 4 Parameter
                    IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock> blockParameter4 = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>;
                    return new Type[] { blockParameter4.Input1.GetType(), blockParameter4.Input2.GetType(), blockParameter4.Input3.GetType(), blockParameter4.Input4.GetType() };
                }
            }

            return null;
        }
    }


    public virtual object Clone()
    {
        Block clonedBlock = (Block)this.MemberwiseClone();
        clonedBlock.BlockIndexInSouceCode = this.BlockIndexInSouceCode;

        if (this is IContainingParameter<ValueBlock>)
        {
            IContainingParameter<ValueBlock> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ValueBlock>;
            IContainingParameter<ValueBlock> blockContainingParameter = this as IContainingParameter<ValueBlock>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ValueBlock;
        }
        else if (this is IContainingParameter<ValueBlock, ValueBlock>)
        {
            IContainingParameter<ValueBlock, ValueBlock> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ValueBlock, ValueBlock>;
            IContainingParameter<ValueBlock, ValueBlock> blockContainingParameter = this as IContainingParameter<ValueBlock, ValueBlock>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ValueBlock;
        }
        else if (this is IContainingParameter<ValueBlock, ValueBlock, ValueBlock>)
        {
            IContainingParameter<ValueBlock, ValueBlock, ValueBlock> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ValueBlock, ValueBlock, ValueBlock>;
            IContainingParameter<ValueBlock, ValueBlock, ValueBlock> blockContainingParameter = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input3 = blockContainingParameter.Input3.Clone() as ValueBlock;
        }
        else if (this is IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>)
        {
            IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock> clonedBlockContainingParameter = clonedBlock as IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>;
            IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock> blockContainingParameter = this as IContainingParameter<ValueBlock, ValueBlock, ValueBlock, ValueBlock>;
            clonedBlockContainingParameter.Input1 = blockContainingParameter.Input1.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input2 = blockContainingParameter.Input2.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input3 = blockContainingParameter.Input3.Clone() as ValueBlock;
            clonedBlockContainingParameter.Input4 = blockContainingParameter.Input4.Clone() as ValueBlock;
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
