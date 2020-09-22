using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
/// <summary>
/// reference from https://en.scratch-wiki.info/wiki/Blocks#Block_Shapes
/// All Global, Local Variable in Block class shouldn't be changed during operating robot except editing block
/// </summary>
[System.Serializable]
public abstract class Block
{

    public static Block CreatBlock(Type type)
    {
        if (type.IsSubclassOf(typeof(Block)) == false)
            return null;

        return Activator.CreateInstance(type) as Block;
    }

    public static Block CreatBlock<T>() where T : Block
    {
        return Activator.CreateInstance(typeof(T)) as Block;
    }

    public byte BlockIndexInSouceCode;

    private PropertyInfo[] ParametersPropertyInfosCache;
    /// <summary>
    /// Check If All parameters is filled?
    /// This is called just one time when modify sourrcede, so this expensive performance is accepted
    /// </summary>
    public bool IsAllParameterFilled 
    { 
        get
        {
            Type[] parameterTypes = this.ParametersTypes;
            if(parameterTypes == null || parametersTypes.Length == 0)
            {
                return true;
            }
            else
            {
                if(this.ParametersPropertyInfosCache == null)
                {
                    this.ParametersPropertyInfosCache = new PropertyInfo[parametersTypes.Length];

                    for (int i = 1; i <= parametersTypes.Length; i++)
                    {
                        this.ParametersPropertyInfosCache[i - 1] = this.GetType().GetProperty("Input" + i.ToString()); // Set Cache Variable
                    }
                }

                for (int i = 0; i < this.ParametersPropertyInfosCache.Length; i++)
                {
                    if (this.ParametersPropertyInfosCache[i].GetValue(this) == null) // Please Test !!!!!!!!!!!!!!!!
                        return false;
                }

            }

            return true;
                
        }
    
    }

    private Type[] parametersTypes;
    public Type[] ParametersTypes
    {
        get
        {
            if (parametersTypes == null)
            {
                Type t = this.GetType();
                if (typeof(IContainingParameter).IsAssignableFrom(t))
                {
                    foreach (Type intType in t.GetInterfaces())
                    {
                        if (typeof(IContainingParameter).IsAssignableFrom(intType) && intType.IsGenericType)
                        {
                            this.parametersTypes = intType.GetGenericArguments(); // Caching
                            break;
                        }
                    }
                }

               
            }

            
            return this.parametersTypes;
        }
    }


    /// <summary>
    /// Clone Intance
    /// Deep Copy!!
    /// </summary>
    /// <returns></returns>
    public virtual Block CloneDeepCopy()
    {
        
        Block clonedBlock = (Block)this.MemberwiseClone();
        //clonedBlock.BlockIndexInSouceCode = this.BlockIndexInSouceCode;

        Type[] parameterTypes = this.ParametersTypes;
        if (parameterTypes != null && parametersTypes.Length > 0)
        {
            for (int i = 1; i <= parametersTypes.Length; i++)
            {
                //Bad Performance!!!!!!!
                ValueBlock thisInput = (ValueBlock)(this.GetType().GetProperty("Input" + i.ToString()).GetValue(this)); // input of this instance
                this.GetType().GetProperty("Input" + i.ToString()).SetValue(clonedBlock, thisInput); // set thisInput to input of clonedBlock 
            }
        }


        return clonedBlock;
    }

   
}

