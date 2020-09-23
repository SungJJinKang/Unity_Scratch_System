using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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

  


    #region Parameter
    private PropertyInfo[] parametersPropertyInfosCache;
    private PropertyInfo[] ParametersPropertyInfos
    {
        get
        {
            if (this.IsCachedParametersPropertyInfos == false)
                CachingParametersPropertyInfos();

            return this.parametersPropertyInfosCache;
        }
    }

    /// <summary>
    /// Gets the parameter property info.
    /// </summary>
    /// <returns>The parameter property info.</returns>
    /// <param name="parameterIndex">Index. 1 ~ 4</param>
    private PropertyInfo GetParameterPropertyInfo(int parameterIndex)
    {
        PropertyInfo[] parameterPropertyInfos = ParametersPropertyInfos;

        if (parameterIndex - 1 < 0 || parameterIndex - 1 >= parameterPropertyInfos.Length)
        {
            Debug.LogError("parameterIndex is not proper");
            return null;
        }


        if (parameterIndex - 1 < parameterPropertyInfos.Length)
        {
            return parameterPropertyInfos[parameterIndex - 1];
        }
        else
        {
            Debug.LogError("inputIndex is out of index(parameterPropertyInfos) ");
            return null;
        }
    }


    private bool IsCachedParametersPropertyInfos = false;
    private void CachingParametersPropertyInfos()
    {
        int paramaterCount = this.ParamaterCount;

        if (paramaterCount == 0)
        {
            this.parametersPropertyInfosCache = null;
        }
        else
        {
            this.parametersPropertyInfosCache = new PropertyInfo[paramaterCount];

            for (int i = 1; i <= paramaterCount; i++)
            {
                this.parametersPropertyInfosCache[i - 1] = this.GetType().GetProperty("Input" + i.ToString()); // Set Cache Variable
            }
        }
        IsCachedParametersPropertyInfos = true;
    }


    /// <summary>
    /// Passes the parameter to block
    /// </summary>
    /// <param name="inputIndex">Input index. 1 ~ 4</param>
    /// <param name="valueBlock">Value block. Passed Parameter Value Block</param>
    public void PassParameter(int inputIndex, ValueBlock valueBlock)
    {
        PropertyInfo parameterPropertyInfo = GetParameterPropertyInfo(inputIndex);
        if(parameterPropertyInfo != null)
        {
            parameterPropertyInfo.SetValue(this, valueBlock);
        }
    }

    /// <summary>
    /// Tries the get parameter value.
    /// </summary>
    /// <returns><c>true</c>, if get parameter value was tryed, <c>false</c> otherwise.</returns>
    /// <param name="inputIndex">Input index. 1 ~ 4</param>
    /// <param name="parameterValue">Parameter value.</param>
    public bool TryGetParameterValue(int inputIndex, out ValueBlock parameterValue)
    {
        PropertyInfo parameterPropertyInfo = GetParameterPropertyInfo(inputIndex);
        if (parameterPropertyInfo != null)
        {
            parameterValue = parameterPropertyInfo.GetValue(inputIndex) as ValueBlock;
            return true;
        }
        else
        {
            parameterValue = null;
            return false;
        }
    } 
    /// <summary>
    /// Check If All parameters is filled?
    /// This is called just one time when modify sourrcede, so this expensive performance is accepted
    /// </summary>
    public bool IsAllParameterFilled
    {
        get
        {
            if (this.ParamaterCount == 0)
            {
                return true;
            }
            else
            {
                PropertyInfo[] parametersPropertyInfos = ParametersPropertyInfos;
                if (parametersPropertyInfos == null)
                {
                    return true;
                }


                else
                {
                    for (int i = 0; i < parametersPropertyInfos.Length; i++)
                    {
                        if (parametersPropertyInfos[i].GetValue(this) == null) // Please Test !!!!!!!!!!!!!!!!
                            return false;
                    }

                    return true;
                }
            }
        }
    }

   

    private Type[] parametersTypes;
    /// <summary>
    /// Type of Parameters
    /// </summary>
    /// <value>The parameters types.</value>
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

    private int paramaterCountCache = -1;
    public int ParamaterCount
    {
        get
        {
            if (this.paramaterCountCache == -1)
                CachingParamaterCount();

            return this.paramaterCountCache;
        }
    }

    private void CachingParamaterCount()
    {
        Type[] pTypes = ParametersTypes;
        if (pTypes == null)
            this.paramaterCountCache = 0;
        else
            this.paramaterCountCache =  pTypes.Length;
    }
    #endregion

   
   
}

