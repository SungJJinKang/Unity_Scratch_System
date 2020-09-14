using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Robot base.
/// Thie is used identify Specific Robot
/// This is Robot, There is no Robot class !!!!!!!!!!!!
/// This contains Every Robot Parts Attached To Robot
/// </summary>
public sealed class RobotBase : RobotPart
{
    protected override void Awake()
    {
        base.Awake();
        base.MotherRobotBase = this; // set itsetf to mother robotbase
    }

    protected override void Start()
    {
        base.Start();
    }

    public string UniqueRobotId
    {
        private set;
        get;
    }

    /// <summary>
    /// The attacehd robot parts.
    /// this can't contain RobotBase
    /// </summary>
    private List<RobotPart> AttacehdRobotParts;
    public bool AttachRobotPart(RobotPart robotPart)
    {
        if (robotPart is RobotBase || this.AttacehdRobotParts.Contains(robotPart) == true)
            return false;

        this.AttacehdRobotParts.Add(robotPart);
        robotPart.MotherRobotBase = this;
        return true;
    }

    /// <summary>
    /// Detach Robot Part From Robot
    /// </summary>
    /// <returns><c>true</c>, if robot part was detached successfully, <c>false</c> otherwise.</returns>
    /// <typeparam name="T">Robot Part Type</typeparam>
    public bool DetachRobotPart<T>() where T : RobotPart
    {
        for (int i = 0; i < this.AttacehdRobotParts.Count; i++)
        {
            if (this.AttacehdRobotParts[i] is T)
            {
                RobotPart robotPart = this.AttacehdRobotParts[i];
                this.AttacehdRobotParts.RemoveAt(i);

                Destroy(robotPart); // Destroy From Scene

                return true;
            }
                 
        }

        return false;
    }

    public bool DetachRobotPart(RobotPart robotPart)
    {
        return this.AttacehdRobotParts.Remove(robotPart);
    }

    /// <summary>
    /// Get Attached RobotPart Instance of Robot Instance
    /// </summary>
    /// <returns>The robot part.</returns>
    /// <typeparam name="T">Robot Part Type</typeparam>
    public T GetRobotPart<T>() where T : RobotPart
    {
        for (int i = 0; i < this.AttacehdRobotParts.Count; i++)
        {
            if (this.AttacehdRobotParts[i] is T)
                return this.AttacehdRobotParts[i] as T;
        }

        return null;
    }

    #region MemoryVariable
    /// <summary>
    /// Memory Variable
    /// You can access to this variable through Block.Memory_SetValue Class or Momory_ChangeValue Class
    /// </summary>
    private Dictionary<string, string> MemoryVariable;
    private void InitMemoryVariable(Dictionary<string, string> deepCopiedMemoryVariableTamplate)
    {
        this.MemoryVariable = deepCopiedMemoryVariableTamplate;
    }

    public void SetMemoryVariable(string key, string text)
    {
        if (this.MemoryVariable.ContainsKey(key) == false)
        {
            Debug.LogError("VariableTemplate Dont Have Key : " + key);
        }

        this.MemoryVariable[key] = text;
        this.OnUpdateMemoryVariable(key);
    }

    public string GetMemoryVariable(string key)
    {
        if (this.MemoryVariable.ContainsKey(key) == false)
        {
            Debug.LogError("VariableTemplate Dont Have Key : " + key);
            return "";
        }

        return string.Copy(this.MemoryVariable[key]);
    }

    private void OnUpdateMemoryVariable(string key)
    {

    }

    #endregion

    #region Event

    public void StartInitBlock()
    {
        if(this.RobotSourceCode.InitBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.InitBlock is null");
            return;
        }
        this.RobotSourceCode.InitBlock.StartFlowBlock(this);
    }

    public void StartLoopedBlock()
    {
        if (this.RobotSourceCode.LoopedBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.LoopedBlock is null");
            return;
        }
        this.RobotSourceCode.LoopedBlock.StartFlowBlock(this);
    }

    public void StartEventBlock(string eventName)
    {
        this.RobotSourceCode.StartEventBlock(this, eventName);
    }

    #endregion

    #region RobotSourceCode
    /// <summary>
    /// Installed Robot Source Code On Thie Robot Instance
    /// Should Reference From RobotSystem.instance.StoredRobotSourceCode
    /// </summary>
    private RobotSourceCode RobotSourceCode; 

    public bool CopyFromRobotSourceCodeTemplate(string robotSourceCodeName)
    {
        RobotSourceCodeTemplate robotSourceCodeTemplate = RobotSystem.instance.GetRobotSourceCodeTemplate(robotSourceCodeName);
        if(robotSourceCodeTemplate == null)
        {
            Debug.LogError("Cant Find Robot SourceCode : " + robotSourceCodeName);
            return false;
        }

        if(this.RobotSourceCode != null)
        {
            this.RobotSourceCode._OriginalRobotSourceCodeTemplate.RemoveFromInstalledRobotList(this);
            this.RobotSourceCode = null; // Clear Reference Of Installed Robot Source Code
        }

        this.RobotSourceCode = robotSourceCodeTemplate; //shallow Copy
        this.RobotSourceCode._OriginalRobotSourceCodeTemplate = robotSourceCodeTemplate; // Store Ref To Original RobotSourceCodeTemplate
       
        this.InitMemoryVariable(robotSourceCodeTemplate.GetDeepCopyOfVariableTemplate());  // Deep copy MemoryVariable

        robotSourceCodeTemplate.AddToInstalledRobotList(this);

        return true;
    }
    #endregion
}
