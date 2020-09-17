using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Robot Source Code
/// And Each Robot Instance sould copy shallow this instance(just reference instnace of this class)
/// </summary>
public class RobotSourceCode 
{
    public RobotSourceCodeTemplate _OriginalRobotSourceCodeTemplate;

    public RobotSourceCode()
    {
        this.IsEditing = false;
        this.sourceCodeName = "";
        this.initBlock = null;
        this.loopedBlock = null;
        this.StoredCustomBlockDefinitionBlockList = new List<DefinitionCustomBlock>();
        this.MemoryVariableTemplateList = new Dictionary<string, string>();

    }


    /// <summary>
    /// If Robot Source Code is being edited In Block Editor
    /// If Source Code completely created in Block Editor, set to false
    /// For Modifing, set to true
    /// </summary>
    public bool IsEditing = false;

    private string sourceCodeName;
    public string SourceCodeName
    {
        get
        {
            return this.sourceCodeName;
        }
        set
        {
            if (this.IsEditing == false)
                return; // Can Change Only When IsEditing is true

            this.sourceCodeName = value;
        }
    }

    private HatBlock initBlock;
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

    private HatBlock loopedBlock;
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
    private Dictionary<string, EventBlock> StoredEventBlockList;
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
        this.StoredEventBlockList.Add(eventBlock.Input1.GetReporterStringValue(), eventBlock);
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

        this.StoredEventBlockList.Remove(eventBlock.Input1.GetReporterStringValue());
        return true;
    }

    public EventBlock GetEventBlock(string eventName)
    {
        if (IsEventBlockExist(eventName) == true)
            return StoredEventBlockList[eventName];
        else
            return null;
    }

    public bool IsEventBlockExist(string eventName)
    {
        return this.StoredEventBlockList.ContainsKey(eventName);
    }


    #endregion

    #region StoredCustomBlockDefinitionBlock

    /// <summary>
    /// Please Check if Same Function Name is existing In Block
    /// This can be called InternetAntenna_SendCommandThroughInternet!!!!!
    /// </summary>
    private List<DefinitionCustomBlock> StoredCustomBlockDefinitionBlockList;
    public DefinitionCustomBlock[] StoredCustomBlockDefinitionBlockArray
    {
        get { return this.StoredCustomBlockDefinitionBlockList.ToArray(); }
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

    #region MemoryVariableTemplate
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
    private Dictionary<string, string> MemoryVariableTemplateList;

    /// <summary>
    /// Sets to MemoryVariable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if to variable template was set succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    /// <param name="text">Text.</param>
    public bool SetToMemoryVariableTemplateList(string key, string text)
    {
        if(this.IsEditing == false)
        {
            Debug.LogError("Cant Change MemoryVariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if(this.MemoryVariableTemplateList.ContainsKey(key) == true)
        {// If MemoryVariableTemplate Already Have Key
            Debug.Log("MemoryVariableTemplateList Already Have Key : " + key + " So Changed Value");
            this.MemoryVariableTemplateList[key] = text;
        }
        else
        {// If MemoryVariableTemplate Dont Have Key Yet
            Debug.Log("MemoryVariableTemplateList Dont Have Key Yet : " + key + " So Add new item");
            this.MemoryVariableTemplateList.Add(key, text);
        }

      
        return true;
    }

    /// <summary>
    /// Removes from MemoryVariable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if item of MemoryVariable template was removed succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    public bool RemoveFromMemoryVariableTemplateList(string key)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change MemoryVariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if (this.MemoryVariableTemplateList.ContainsKey(key) == true)
        {// If MemoryVariableTemplate Have Key
            Debug.Log("MemoryVariableTemplateList Have Key : " + key + " So Remove Item with key");
            this.MemoryVariableTemplateList.Remove(key);;
            return false;
        }
        else
        {// If MemoryVariableTemplate Dont Have Key Yet
            Debug.Log("MemoryVariableTemplateList Dont Have Key Yet : " + key);
            return false;
        }

    }

    public bool GetMemoryVariableTemplateValue(string key, ref string text)
    {
        if (this.MemoryVariableTemplateList.ContainsKey(key) == false)
        {
            Debug.LogError("MemoryVariableTemplateList Dont Have Key : " + key);
            return false;
        }
        else
        {//MemoryVariableTemplate Have Key 
            text = string.Copy(this.MemoryVariableTemplateList[key]);
            return true;
        }
    }

    public Dictionary<string, string> GetDeepCopyOfMemoryVariableTemplate()
    {
        Dictionary<string, string> deepCopiedMemoryVariableTemplate = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> pair in this.MemoryVariableTemplateList)
        {
            deepCopiedMemoryVariableTemplate.Add(pair.Key, string.Copy(pair.Value)); // deep copy string ( string is referce type )
        }

        return deepCopiedMemoryVariableTemplate;
    }

    #endregion

 

}

/// <summary>
/// Robot source code Template.
/// Instance Of This Class should exist just one thing!!!!!!!!
/// Instance Of This Class is stored In Robot System
/// </summary>
public class RobotSourceCodeTemplate : RobotSourceCode
{
    public RobotSourceCodeTemplate() : base()
    {
        InstalledRobotList = new List<RobotBase>();
    }

    #region InstalledRobotList

    /// <summary>
    /// Robot installed This Source Code List
    /// </summary>
    private List<RobotBase> InstalledRobotList;
    public bool AddToInstalledRobotList(RobotBase robotBase)
    {
        if (this.InstalledRobotList.Contains(robotBase) == true)
            return false;

        this.InstalledRobotList.Add(robotBase);
        return true;
    }

    public bool RemoveFromInstalledRobotList(RobotBase robotBase)
    {
        return this.InstalledRobotList.Remove(robotBase);
    }

    #endregion
}