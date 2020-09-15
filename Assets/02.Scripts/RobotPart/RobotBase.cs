using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    #region RobotPart

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

    #endregion

    #region MemoryVariable
    /// <summary>
    /// Memory Variable
    /// You can access to this variable through Block.Memory_SetValue Class or Momory_ChangeValue Class
    /// </summary>
    private Dictionary<string, string> MemoryVariable;

    /// <summary>
    /// Clean Only Value Of Dictionary
    /// </summary>
    private void CleanMemoryVariableValue()
    {
        this.MemoryVariable.Keys.ToList().ForEach(x => this.MemoryVariable[x] = "");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deepCopiedMemoryVariableTamplate"></param>
    /// <param name="maintainOriginalValue">If MemoryVariable have same key with deepCopiedMemoryVariableTamplate, </param>
    private void InitMemoryVariable(Dictionary<string, string> deepCopiedMemoryVariableTamplate, bool maintainOriginalValue = false)
    {
        if(maintainOriginalValue == true)
        {//Copy this.MemoryVariable Value To deepCopiedMemoryVariableTamplate
            this.MemoryVariable.Keys.ToList().ForEach(x =>
            {
                if (deepCopiedMemoryVariableTamplate.ContainsKey(x))
                {
                    deepCopiedMemoryVariableTamplate[x] = this.MemoryVariable[x];
                }
            });
        }

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

    #region FunctionLocalVariable

    /// <summary>
    /// Memory Variable
    /// You can access to this variable through CustomBlock
    /// </summary>
    private Dictionary<string, string> CustomBlockLocalVariables;

    /// <summary>
    /// Init this.CustomBlockLocalVariable
    /// </summary>
    /// <param name="CustomBlockLocalVariableKeyArray"></param>
    /// <param name="maintainOriginalValue">If MemoryVariable have same key with FunctionLocalVariableKeyArray, </param>
    private void InitCustomBlockLocalVariables(List<string> CustomBlockLocalVariableKeyArray, bool maintainOriginalValue = false)
    { 
        if(CustomBlockLocalVariableKeyArray == null)
        {
            Debug.LogError("CustomBlockLocalVariable is null");
            return;
        }

        if (CustomBlockLocalVariables == null)
        {
            this.CustomBlockLocalVariables = new Dictionary<string, string>();
        }
        else
        {
            this.CustomBlockLocalVariables.Clear();
        }
           

        for (int i = 0; i < CustomBlockLocalVariableKeyArray.Count; i++)
        {
            this.CustomBlockLocalVariables.Add(CustomBlockLocalVariableKeyArray[i], ""); // Add Key with FunctionLocalVariableKeyArray
        }
        
    }

    public void SetCustomBlockLocalVariables(string key, string text)
    {
        if (this.CustomBlockLocalVariables.ContainsKey(key) == false)
        {
            Debug.LogError("CustomBlockLocalVariable Dont Have Key : " + key);
        }

        this.CustomBlockLocalVariables[key] = text;
        this.OnUpdateMemoryVariable(key);
    }

    public string GetCustomBlockLocalVariables(string key)
    {
        if (this.CustomBlockLocalVariables.ContainsKey(key) == false)
        {
            Debug.LogError("CustomBlockLocalVariable Dont Have Key : " + key);
            return "";
        }

        return string.Copy(this.CustomBlockLocalVariables[key]);
    }

    private void OnUpdateCustomBlockLocalVariables(string key)
    {

    }

    #endregion


    

    #region Event

    public void StartEventBlock(string eventName)
    {
        this.RobotSourceCode.StartEventBlock(this, eventName);
    }

    #endregion

    #region WaitBlock

    public float WaitingTime = 0;

    private FlowBlock WaitingBlock;

    /// <summary>
    /// ex ) After Call CallCustomBlock And Finished CustomBlock operation
    /// We should get back NextBlock Of CallCustomBlock!!!!!!!!!!
    /// 
    /// So We set this variable to NextBlock Of CallCustomBlock When Start CallCustomBlock
    /// 
    /// You Should Set This At Start { }
    /// Like Call Function , You should come back to NextBlock of Call Function, After Execute CustomBlock
    /// </summary>
    public FlowBlock ComeBackFlowBlockAfterFinishFlow
    {
        private get;
        set;
    }


    public void SetWaitingBlock(FlowBlock flowBlock)
    {
        this.WaitingBlock = flowBlock;
    }
    public void ExecuteWaitingBlock(float waitTime)
    {
        this.WaitingTime += waitTime;
        if(this.WaitingBlock != null)
        {

            FlowBlock.FlowBlockState flowBlockState = this.WaitingBlock.StartFlowBlock(this);

            switch (flowBlockState)
            {
                case FlowBlock.FlowBlockState.EndFlowAfterOperation:

                    SetWaitingBlock(ComeBackFlowBlockAfterFinishFlow);

                    break;
            }

        }
    }

    public void SetInitBlockToWaitingBlock()
    {
        if (this.RobotSourceCode.InitBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.InitBlock is null");
            return;
        }
        this.RobotSourceCode.InitBlock.StartFlowBlock(this);
    }

    public void SetLoopedBlockToWaitingBlock()
    {
        if (this.RobotSourceCode.LoopedBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.LoopedBlock is null");
            return;
        }

        this.WaitingBlock = this.RobotSourceCode.LoopedBlock;
    }

    #endregion

    #region RobotSourceCode
    /// <summary>
    /// Installed Robot Source Code On Thie Robot Instance
    /// Should Reference From RobotSystem.instance.StoredRobotSourceCode
    /// 
    /// 
    /// 
    /// 
    /// If RobotSourceCodeTemplate is changed or RobotSourceCode is set newly, WaitingBlock is cleaned, ReStart SourceCode Newly From InitBlock To LoopedBlcok
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
        this.RobotSourceCode._OriginalRobotSourceCodeTemplate.AddToInstalledRobotList(this);

        OnSetRobotSourceCode(robotSourceCodeTemplate);

        return true;
    }

    private void OnSetRobotSourceCode(RobotSourceCodeTemplate robotSourceCodeTemplate)
    {
        this.InitMemoryVariable(robotSourceCodeTemplate.GetDeepCopyOfMemoryVariableTemplate());  // Deep copy MemoryVariable
        this.InitCustomBlockLocalVariables(robotSourceCodeTemplate.CustomBlockLocalVariableParameterNames);

    }
    #endregion
}
