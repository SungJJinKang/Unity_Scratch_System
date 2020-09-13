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
        this.StoredCustomFunctionBlock = new Dictionary<string, CustomBlock>();
        this.VariableTemplate = new Dictionary<string, string>();
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

    #region StoredCustomFunctionBlock

    /// <summary>
    /// Please Check if Same Function Name is existing In Block
    /// </summary>
    private Dictionary<string, CustomBlock> StoredCustomFunctionBlock;

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
    private Dictionary<string, string> VariableTemplate;

    /// <summary>
    /// Sets to variable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if to variable template was set succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    /// <param name="text">Text.</param>
    public bool SetToVariableTemplate(string key, string text)
    {
        if(this.IsEditing == false)
        {
            Debug.LogError("Cant Change VariableTemplate, Because Source Code Editing Completely Finished");
            return false;
        }


        if(this.VariableTemplate.ContainsKey(key) == true)
        {// If VariableTemplate Already Have Key
            Debug.Log("VariableTemplate Already Have Key : " + key + " So Changed Value");
            this.VariableTemplate[key] = text;
        }
        else
        {// If VariableTemplate Dont Have Key Yet
            Debug.Log("VariableTemplate Dont Have Key Yet : " + key + " So Add new item");
            this.VariableTemplate.Add(key, text);
        }

      
        return true;
    }

    /// <summary>
    /// Removes from variable template.
    /// This Should Called From Block Editor
    /// </summary>
    /// <returns><c>true</c>, if item of variable template was removed succesfully, <c>false</c> otherwise.</returns>
    /// <param name="key">Key.</param>
    public bool RemoveFromVariableTemplate(string key)
    {
        if (this.IsEditing == false)
        {
            Debug.LogError("Cant Change VariableTemplate, Because Source Code Editing Completely Finished");
            return false;
        }


        if (this.VariableTemplate.ContainsKey(key) == true)
        {// If VariableTemplate Have Key
            Debug.Log("VariableTemplate Have Key : " + key + " So Remove Item with key");
            this.VariableTemplate.Remove(key);;
            return false;
        }
        else
        {// If VariableTemplate Dont Have Key Yet
            Debug.Log("VariableTemplate Dont Have Key Yet : " + key);
            return false;
        }

    }

    public bool GetVariableTemplateValue(string key, ref string text)
    {
        if (this.VariableTemplate.ContainsKey(key) == false)
        {
            Debug.LogError("VariableTemplate Dont Have Key : " + key);
            return false;
        }
        else
        {//VariableTemplate Have Key 
            text = string.Copy(this.VariableTemplate[key]);
            return true;
        }
    }

    public Dictionary<string, string> GetDeepCopyOfVariableTemplate()
    {
        Dictionary<string, string> deepCopiedVariableTemplate = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> pair in this.VariableTemplate)
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