using System;
using System.Reflection;
using UnityEngine;
/// <summary>
/// reference from https://en.scratch-wiki.info/wiki/Blocks#Block_Shapes
/// All Global, Local Variable in Block class shouldn't be changed during operating robot except editing block
/// </summary>
[System.Serializable]
public abstract class Block
{
    public byte BlockIndexInSouceCode;

    public static Block CreatBlock(Type type)
    {
        if (type == null)
            return null;

        if (type.IsSubclassOf(typeof(Block)) == false)
            return null;

        return Activator.CreateInstance(type) as Block;
    }

    public static Block CreatBlock<T>() where T : Block
    {
        return Activator.CreateInstance(typeof(T)) as Block;
    }

    /// <summary>
    /// Clone Intance
    /// Deep Copy!!
    /// </summary>
    /// <returns></returns>
    public virtual Block Clone()
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




    #region Parameter

    private bool isParametersInitialized = false;
    private ValueBlock[] parameters;
    private ValueBlock[] Parameters
    {
        get
        {
            if (isParametersInitialized == false)
                this.InitParameters();

            return this.parameters;
        }
    }
    private void InitParameters()
    {
        if (this.ParametersTypes == null)
            return;

        this.parameters = new ValueBlock[ParametersTypes.Length];
        this.isParametersInitialized = true;
    }

    public bool IsParameterPassable(int index, ValueBlock value)
    {
        if (value == null)
            return false;

        if (this.Parameters.Length <= index || this.ParametersTypes.Length <= index)
            return false;

        Type valueType = value.GetType();
        if (this.ParametersTypes[index] != valueType || valueType.IsSubclassOf(this.ParametersTypes[index]) == false)
            return false; // different type
        else
            return true;
    }

    public bool PassParameter(int index, ValueBlock value)
    {
        if (this.IsParameterPassable(index, value) == false)
            return false;

        this.Parameters[index] = value;
        return true;
    }

    /// <summary>
    /// Check If All parameters is filled?
    /// This is called just one time when modify sourrcede, so this expensive performance is accepted
    /// </summary>
    public bool IsAllParameterFilled
    {
        get
        {
            if (this.Parameters.Length == 0)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < this.Parameters.Length; i++)
                {
                    if (this.Parameters[i] == null)
                        return false;
                }

                return true;
            }
        }
    }


    private bool IsParametersTypesCached = false;

  


    private Type[] parametersTypes;
    /// <summary>
    /// Type of Parameters
    /// </summary>
    /// <value>The parameters types.</value>
    public Type[] ParametersTypes
    {
        get
        {
            if (this.IsParametersTypesCached == false)
            {
                if (this is IContainingParameter)
                {
                    foreach (Type interfaceType in this.GetType().GetInterfaces())
                    {
                        if (typeof(IContainingParameter).IsAssignableFrom(interfaceType) && interfaceType.IsGenericType)
                        {
                            this.parametersTypes = interfaceType.GetGenericArguments(); // Caching
                            break;
                        }
                    }
                }



                this.IsParametersTypesCached = true;


            }


            return this.parametersTypes;
        }
    }

    public int ParamaterCount
    {
        get
        {
            if (this.ParametersTypes == null)
                return 0;
            else
                return ParametersTypes.Length;
        }
    }

    #endregion



}

