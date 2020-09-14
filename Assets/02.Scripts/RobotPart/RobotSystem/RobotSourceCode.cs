using System.Collections.Generic;
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
        this.StoredCustomBlockDefinitionBlock = new Dictionary<string, DefinitionCustomBlock>();
        this.VariableTemplateList = new Dictionary<string, string>();

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

    public void StartEventBlock(RobotBase robotBase, string eventName)
    {
        if (robotBase == null)
            return;

        if (this.StoredEventBlockList.ContainsKey(eventName) == false)
            return;

        this.StoredEventBlockList[eventName].StartFlowBlock(robotBase);
    }

    #endregion

    #region StoredCustomBlockDefinitionBlock

    /// <summary>
    /// Please Check if Same Function Name is existing In Block
    /// This can be called InternetAntenna_SendCommandThroughInternet!!!!!
    /// </summary>
    private Dictionary<string, DefinitionCustomBlock> StoredCustomBlockDefinitionBlock;



    #endregion

    #region VariableTemplate
    /// <summary>
    /// Variable List
    /// This Variable is just template
    /// Each Robot Instance Should Have their StoredVariableBlock Instance
    /// Key Variable Name, Value Variable Value
    /// Thie Should Set In Block Editor Step
    /// And In Other Step, This Variable Shoudlnt't be changed
    /// 
    /// Variable Can Have Init Value
    /// </summary>
    private Dictionary<string, string> VariableTemplateList;

    /// <summary>
    /// Sets to variable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if to variable template was set succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    /// <param name="text">Text.</param>
    public bool SetToVariableTemplateList(string key, string text)
    {
        if(this.IsEditing == false)
        {
            Debug.LogError("Cant Change VariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if(this.VariableTemplateList.ContainsKey(key) == true)
        {// If VariableTemplate Already Have Key
            Debug.Log("VariableTemplateList Already Have Key : " + key + " So Changed Value");
            this.VariableTemplateList[key] = text;
        }
        else
        {// If VariableTemplate Dont Have Key Yet
            Debug.Log("VariableTemplateList Dont Have Key Yet : " + key + " So Add new item");
            this.VariableTemplateList.Add(key, text);
        }

      
        return true;
    }

    /// <summary>
    /// Removes from variable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if item of variable template was removed succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    public bool RemoveFromVariableTemplateList(string key)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change VariableTemplateList, Because Source Code Editing Completely Finished");
            return false;
        }


        if (this.VariableTemplateList.ContainsKey(key) == true)
        {// If VariableTemplate Have Key
            Debug.Log("VariableTemplateList Have Key : " + key + " So Remove Item with key");
            this.VariableTemplateList.Remove(key);;
            return false;
        }
        else
        {// If VariableTemplate Dont Have Key Yet
            Debug.Log("VariableTemplateList Dont Have Key Yet : " + key);
            return false;
        }

    }

    public bool GetVariableTemplateValue(string key, ref string text)
    {
        if (this.VariableTemplateList.ContainsKey(key) == false)
        {
            Debug.LogError("VariableTemplateList Dont Have Key : " + key);
            return false;
        }
        else
        {//VariableTemplate Have Key 
            text = string.Copy(this.VariableTemplateList[key]);
            return true;
        }
    }

    public Dictionary<string, string> GetDeepCopyOfVariableTemplate()
    {
        Dictionary<string, string> deepCopiedVariableTemplate = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> pair in this.VariableTemplateList)
        {
            deepCopiedVariableTemplate.Add(pair.Key, string.Copy(pair.Value)); // deep copy string ( string is referce type )
        }

        return deepCopiedVariableTemplate;
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