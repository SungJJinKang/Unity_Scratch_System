using System;
using System.Reflection;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;
/// <summary>
/// reference from https://en.scratch-wiki.info/wiki/Blocks#Block_Shapes
/// All Global, Local Variable in Block class shouldn't be changed during operating robot except editing block
/// </summary>
[System.Serializable]
[JsonObjectAttribute(MemberSerialization.OptIn)]
[JsonConverter(typeof(BlockConverter))]
public abstract class Block
{

    #region JSONCONVERT

    public virtual void ConvertToJson()
    {
        //Dont use JSonConverter on Block Class
        //write json converter ( BlockJsonConverter ) code manually       
    }


    #endregion
    public bool IsBlockEditorUnitAnchoredPositionSaved
    {
        private set;
        get;
    }

    [JsonIgnore]
    private Vector2 blockEditorUnitAnchoredPosition;
    [JsonConverter(typeof(Vector2Converter))]
    [JsonProperty]
    public Vector2 BlockEditorUnitAnchoredPosition
    {
        set
        {
            this.blockEditorUnitAnchoredPosition = value;
            this.IsBlockEditorUnitAnchoredPositionSaved = true;
        }
        get
        {
            return this.blockEditorUnitAnchoredPosition;
        }
    }

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

            for (int i = 0; i < paramaterCount; i++)
            {
                this.parametersPropertyInfosCache[i] = this.GetType().GetProperty("Input" + (i+1).ToString()); // Set Cache Variable
            }
        }

        this.IsCachedParametersPropertyInfos = true;
        this.SetDefaultValueToParameter(); // delete this line on build,

       
    }

    private void LoadParameterData(params ValueBlock[] valueBlock)
    {
        for (int i = 0; i < valueBlock.Length; i++)
        {
            this.PassParameter(i, valueBlock[i]);
        }
    }


    private void SetDefaultValueToParameter()
    {
        int parameterCount = this.ParamaterCount;


        for (int i = 0; i < parameterCount; i++)
        {
            if(this.TryGetParameterValue(i, out ValueBlock parameterValue) == true && parameterValue == null )
            {
                //parameter exists, but it have null value
                //set default valeu block
                Type parameterType = this.GetParameterType(i);
                if(parameterType == null)
                {
                    Debug.LogError("Cant Find parameterType");
                    return;
                }
                else
                {
                    if (parameterType == typeof( ReporterBlock) || parameterType.IsSubclassOf(typeof(ReporterBlock)))
                    {
                        this.PassParameter(i, ReporterBlock.DefaultValueBlock);
                    }
                    else if (parameterType == typeof(BooleanBlock) || parameterType.IsSubclassOf(typeof(BooleanBlock)))
                    {
                        this.PassParameter(i, BooleanBlock.DefaultValueBlock);
                    }
                }
            }
        }
    }


    #if UNITY_EDITOR
    public void DebugParameters()
    {
        Utility.stringBuilderCache.Clear();
        
        for (int i = 0; i < ParametersPropertyInfos.Length; i++)
        {
            this.TryGetParameterValue(i, out ValueBlock paremeterValue);

            Utility.stringBuilderCache.Append(i.ToString() + " : " + paremeterValue?.GetType().Name + "   ");
        }

        Debug.Log(GetType().Name + "s Parameter List : " + Utility.stringBuilderCache.ToString());
        Utility.stringBuilderCache.Clear();
    }
#endif

    /// <summary>
    /// Gets the parameter property info.
    /// </summary>
    /// <returns>The parameter property info.</returns>
    /// <param name="parameterIndex">Index. 0 ~ 3</param>
    private PropertyInfo GetParameterPropertyInfo(int parameterIndex)
    {
        PropertyInfo[] parameterPropertyInfos = ParametersPropertyInfos;

        if (parameterIndex  < 0 || parameterIndex  >= parameterPropertyInfos.Length)
        {
            Debug.LogError("parameterIndex is not proper");
            return null;
        }


        return parameterPropertyInfos[parameterIndex];
    }



    /// <summary>
    /// Passes the parameter to block
    /// </summary>
    /// <param name="inputIndex">Input index. 0 ~ 3</param>
    /// <param name="valueBlock">Value block. Passed Parameter Value Block</param>
    public void PassParameter(int inputIndex, ValueBlock valueBlock)
    {
        PropertyInfo parameterPropertyInfo = GetParameterPropertyInfo(inputIndex);
        if (parameterPropertyInfo != null)
        {
            parameterPropertyInfo.SetValue(this, valueBlock);
        }
    }

    /// <summary>
    /// Tries the get parameter value.
    /// </summary>
    /// <returns><c>true</c>, when parameter exist and return parameter, <c>false</c> otherwise.</returns>
    /// <param name="inputIndex">Input index. 1 ~ 4</param>
    /// <param name="parameterValue">Parameter value.</param>
    public bool TryGetParameterValue(int inputIndex, out ValueBlock parameterValue)
    {
        PropertyInfo parameterPropertyInfo = GetParameterPropertyInfo(inputIndex);
        if (parameterPropertyInfo != null)
        {
            parameterValue = parameterPropertyInfo.GetValue(this) as ValueBlock;
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


    private bool isParametersTypesCached;
    private Type[] parametersTypes;


    /// <summary>
    /// Type of Parameters
    /// </summary>
    /// <value>The parameters types.</value>
    public Type[] ParametersTypes
    {
        get
        {
            if (this.isParametersTypesCached == false)
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



                this.isParametersTypesCached = true;


            }


            return this.parametersTypes;
        }
    }

    private Type GetParameterType(int index)
    {
        Type[] parameterTypes = this.ParametersTypes;
        if (index < 0 || index >= parameterTypes.Length)
        {
            return null;
        }
        else
        {
            return parameterTypes[index];
        }
    }


    public int ParamaterCount
    {
        get
        {
            if (ParametersTypes == null)
                return 0;
            else
                return ParametersTypes.Length;
        }
    }

  
    #endregion



}

