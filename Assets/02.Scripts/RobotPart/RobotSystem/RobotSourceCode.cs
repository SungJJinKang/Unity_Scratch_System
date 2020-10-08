using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Data;
using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// Robot source code Template.
/// Instance Of This Class should exist just one thing!!!!!!!!
/// Instance Of This Class is stored In Robot System
/// This Class shouldn't have 
/// </summary>
[JsonObjectAttribute(MemberSerialization.OptIn)] // if set type to optIn, fields and properties of type is not serialized automatically, you should set JsonProperty manually
[System.Serializable]
public class RobotSourceCode 
{
    public RobotSourceCode()
    {
        this.IsEditing = true;
    }
    public RobotSourceCode(string sourceCodeName)
    {
        this.IsEditing = false;
        this.sourceCodeName = sourceCodeName;

        this.initBlock = new InitHatBlock();
        this.loopedBlock = new LoopedHatBlock();
        this.StoredCustomBlockDefinitionBlockList = new List<DefinitionCustomBlock>();
        this.RobotGlobalVariableTemplateDictionary = new Dictionary<string, string>();
        this.InstalledRobotList = new List<RobotBase>();

    }


    /// <summary>
    /// FlowBlockContainer
    /// used for RobotBase save/load system
    /// when save robotbase data, index of blockes on blockCallStack is saved
    /// index of blocks is get from this property
    /// </summary>
    private FlowBlock[] FlowBlockContainer
    {
        get
        {
            FlowBlock[] flowBlockContainer = new FlowBlock[16]; // default capacity, set pow of 2 
            int flowBlockIndex = 0;

            this.AddFlowBlockRecursively(ref flowBlockContainer, ref flowBlockIndex, this.InitBlock);
            this.AddFlowBlockRecursively(ref flowBlockContainer, ref flowBlockIndex, this.LoopedBlock);

            foreach(var eventBlock in this.StoredEventBlockDictonary.Values)
            {
                this.AddFlowBlockRecursively(ref flowBlockContainer, ref flowBlockIndex, eventBlock);
            }

            foreach(var customBlock in this.StoredCustomBlockDefinitionBlockList)
            {
                this.AddFlowBlockRecursively(ref flowBlockContainer, ref flowBlockIndex, customBlock);
            }

            return flowBlockContainer;
        }
    }

    private void AddFlowBlockRecursively(ref FlowBlock[] flowBlockContainer, ref int flowBlockIndex,  FlowBlock flowBlock)
    {
        if (flowBlock == null)
            return;

        flowBlock.IndexInRobotSourceCode = flowBlockIndex;

        if(flowBlockContainer.Length == flowBlockIndex)
        {//when flowBlockIndex meet flowBlockContainer size
            //expand flowBlockContainer array
            //this is little bit slow, so set large number to default capacity 
            int originalSize = flowBlockContainer.Length;
            FlowBlock[] newArray = new FlowBlock[originalSize * 2];
            System.Array.Copy(flowBlockContainer, 0, newArray, 0, originalSize); // copy original array to new expanded array

            flowBlockContainer = newArray; // set new 
        }

        flowBlockContainer[flowBlockIndex] = flowBlock;
        flowBlockIndex++;
        this.AddFlowBlockRecursively(ref flowBlockContainer, ref flowBlockIndex, flowBlock.NextBlock);
    }


    /// <summary>
    /// If Robot Source Code is being edited In Block Editor
    /// If Source Code completely created in Block Editor, set to false
    /// For Modifing, set to true
    /// </summary>
    [JsonIgnore]
    public bool IsEditing = false;

  
    [JsonIgnore]
    private string sourceCodeName;
    [JsonPropertyAttribute]
    public string SourceCodeName
    {
        get
        {
            return this.sourceCodeName;
        }
        set
        {
            
            if (this.IsEditing == false)
            {
                Debug.LogError("You can't set SourceCodeName for editing");
                return; // Can Change Only When IsEditing is true
            }
            

            this.sourceCodeName = value;
        }
    }

    [JsonIgnore]
    [SerializeField]
    private HatBlock initBlock;
    //[JsonConverter(typeof(BlockJsonConverter))]
    [JsonPropertyAttribute]
    public HatBlock InitBlock
    {
        get
        {
            return this.initBlock;
        }
        set
        {
            if (this.IsEditing == false)
                return; // Can Change Only When IsEditing is true

            this.initBlock = value;
        }
    }

    [JsonIgnore]
    [SerializeField]
    private HatBlock loopedBlock;
    //[JsonConverter(typeof(BlockJsonConverter))]
    [JsonPropertyAttribute]
    public HatBlock LoopedBlock
    {
        get
        {
            return this.loopedBlock;
        }
        set
        {
            if (this.IsEditing == false)
                return; // Can Change Only When IsEditing is true

            this.loopedBlock = value;
        }
    }

    #region EventBlock

    /// <summary>
    /// The stored event block list.
    /// Command is Event
    /// </summary>
    //[JsonConverter(typeof(BlockJsonConverter))]
    [JsonPropertyAttribute]
    private Dictionary<string, EventBlock> StoredEventBlockDictonary;
    [JsonIgnore]
    public EventBlock[] StoredEventBlocks
    {
        get
        {
            if (this.StoredEventBlockDictonary != null)
            {
                return this.StoredEventBlockDictonary.Values.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
    public bool AddToStoredEventBlockList(EventBlock eventBlock)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change StoredEventBlockList, Because Source Code Editing Completely Finished");
            return false;
        }

        if (eventBlock == null)
            return false;

        RemoveFromStoredEventBlockList(eventBlock);
        this.StoredEventBlockDictonary.Add(eventBlock.Input1.GetReporterStringValue(), eventBlock);
        return true;

    }

    public bool RemoveFromStoredEventBlockList(EventBlock eventBlock)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change StoredEventBlockList, Because Source Code Editing Completely Finished");
            return false;
        }

        if (eventBlock == null)
            return false;

        this.StoredEventBlockDictonary.Remove(eventBlock.Input1.GetReporterStringValue());
        return true;
    }

    public EventBlock GetEventBlock(string eventName)
    {
        if (IsEventBlockExist(eventName) == true)
            return StoredEventBlockDictonary[eventName];
        else
            return null;
    }

    public bool IsEventBlockExist(string eventName)
    {
        return this.StoredEventBlockDictonary.ContainsKey(eventName);
    }


    #endregion

    #region StoredCustomBlockDefinitionBlock

    /// <summary>
    /// Please Check if Same Function Name is existing In Block
    /// This can be called InternetAntenna_SendCommandThroughInternet!!!!!
    /// </summary>
    [SerializeField]
    [JsonPropertyAttribute]
    //[JsonConverter(typeof(BlockJsonConverter))]
    private List<DefinitionCustomBlock> StoredCustomBlockDefinitionBlockList;

    [JsonIgnore]
    public DefinitionCustomBlock[] StoredCustomBlockDefinitionBlockArray
    {
        get { return StoredCustomBlockDefinitionBlockList.ToArray(); }
    }


    public bool AddToStoredCustomBlockDefinitionBlock(DefinitionCustomBlock definitionCustomBlock)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change StoredEventBlockList, Because Source Code Editing Completely Finished");
            return false;
        }

        if (definitionCustomBlock == null)
            return false;

        RemoveFromStoredCustomBlockDefinitionBlockt(definitionCustomBlock);
        this.StoredCustomBlockDefinitionBlockList.Add(definitionCustomBlock);

        return true;

    }

    public bool RemoveFromStoredCustomBlockDefinitionBlockt(DefinitionCustomBlock definitionCustomBlock)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change StoredCustomBlockDefinitionBlock, Because Source Code Editing Completely Finished");
            return false;
        }

        if (definitionCustomBlock == null)
            return false;

        this.StoredCustomBlockDefinitionBlockList.Remove(definitionCustomBlock);

        return true;
    }



    #endregion

    #region RobotGlobalVariableTemplate
    /// <summary>
    /// MemoryVariable List
    /// This Variable is just template
    /// Each Robot Instance Should Have their StoredVariableBlock Instance
    /// Key Variable Name, Value Variable Value
    /// Thie Should Set In Block Editor Step
    /// And In Other Step, This Variable Shoudlnt't be changed
    /// 
    /// Variable Can Have Init Value
    /// </summary>
    [JsonPropertyAttribute]
    private Dictionary<string, string> RobotGlobalVariableTemplateDictionary;

    /// <summary>
    /// Sets to MemoryVariable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if to variable template was set succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    /// <param name="text">Text.</param>
    public bool AddToRobotGlobalVariableTemplateList(string key, string text)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change MemoryVariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if (this.RobotGlobalVariableTemplateDictionary.ContainsKey(key) == true)
        {// If MemoryVariableTemplate Already Have Key
            Debug.Log("MemoryVariableTemplateList Already Have Key : " + key + " So Changed Value");
            this.RobotGlobalVariableTemplateDictionary[key] = text;
        }
        else
        {// If MemoryVariableTemplate Dont Have Key Yet
            Debug.Log("MemoryVariableTemplateList Dont Have Key Yet : " + key + " So Add new item");
            this.RobotGlobalVariableTemplateDictionary.Add(key, text);
        }


        return true;
    }

    /// <summary>
    /// Removes from RobotGlobalVariableTemplateList
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if item of RobotGlobalVariableTemplateList was removed succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    public bool RemoveFromRobotGlobalVariableTemplateList(string key)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change RobotGlobalVariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if (this.RobotGlobalVariableTemplateDictionary.ContainsKey(key) == true)
        {// If MemoryVariableTemplate Have Key
            Debug.Log("RobotGlobalVariableTemplateList Have Key : " + key + " So Remove Item with key");
            this.RobotGlobalVariableTemplateDictionary.Remove(key); ;
            return false;
        }
        else
        {// If MemoryVariableTemplate Dont Have Key Yet
            Debug.Log("RobotGlobalVariableTemplateList Dont Have Key Yet : " + key);
            return false;
        }

    }

    /// <summary>
    /// This Should Called From Block Editor
    /// </summary>
    /// <param name="key"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public bool GetRobotGlobalVariableTemplateValue(string key, ref string text)
    {
        if (this.RobotGlobalVariableTemplateDictionary.ContainsKey(key) == false)
        {
            Debug.LogError("RobotGlobalVariableTemplateList Dont Have Key : " + key);
            return false;
        }
        else
        {//MemoryVariableTemplate Have Key 
            text = string.Copy(this.RobotGlobalVariableTemplateDictionary[key]);
            return true;
        }
    }

    public Dictionary<string, string> GetDeepCopyOfRobotGlobalVariableTemplate()
    {
        Dictionary<string, string> deepCopiedRobotGlobalVariableTemplate = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> pair in this.RobotGlobalVariableTemplateDictionary)
        {
            deepCopiedRobotGlobalVariableTemplate.Add(pair.Key, string.Copy(pair.Value)); // deep copy string ( string is referce type )
        }

        return deepCopiedRobotGlobalVariableTemplate;
    }

    #endregion


    #region InstalledRobotList

    /// <summary>
    /// Robot installed This Source Code List
    /// </summary>
    [JsonIgnore]
    private List<RobotBase> InstalledRobotList;
    public bool AddToInstalledRobotList(RobotBase robotBase)
    {
        if (this.InstalledRobotList == null)
            this.InstalledRobotList = new List<RobotBase>();

        if (this.InstalledRobotList.Contains(robotBase) == true)
            return false;

        this.InstalledRobotList.Add(robotBase);
        return true;
    }

    public bool RemoveFromInstalledRobotList(RobotBase robotBase)
    {
        if (this.InstalledRobotList == null)
            return false;

        return this.InstalledRobotList.Remove(robotBase);
    }

   
    #endregion


}
