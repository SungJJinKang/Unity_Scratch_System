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

    #region CustomBlockParameterVariables

    /// <summary>
    /// CustomBlock Parameter Vaiable ( Parameter )
    /// You can access to this variable through DefinitionCustomBlock, Parameter Name
    /// </summary>
    private Dictionary<DefinitionCustomBlock, Dictionary<string, string>> CustomBlockParameterVariables;

    /// <summary>
    /// Init this.CustomBlockLocalVariable
    /// </summary>
    /// <param name="CustomBlockLocalVariableKeyArray"></param>
    /// <param name="maintainOriginalValue">If MemoryVariable have same key with FunctionLocalVariableKeyArray, </param>
    private void InitCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock)
    { 
        if(customBlockDefinitionBlock == null)
        {
            Debug.LogError("customBlockDefinitionBlock is null");
            return;
        }

        if (CustomBlockParameterVariables == null)
        {
            this.CustomBlockParameterVariables = new Dictionary<DefinitionCustomBlock, Dictionary<string, string>>();
        }
        else
        {
            this.CustomBlockParameterVariables.Clear();
        }

        this.CustomBlockParameterVariables.Add(customBlockDefinitionBlock, new Dictionary<string, string>());

        for (int i = 0; i < customBlockDefinitionBlock.ParameterNames.Length; i++)
        {
            this.CustomBlockParameterVariables[customBlockDefinitionBlock].Add(customBlockDefinitionBlock.ParameterNames[i], ""); // Init Parameter Keys with customBlockDefinitionBlock;
        }
        
    }

    /// <summary>
    /// Set Value To Parameter Variable ( Parameter ) Of CustomBlock 
    /// 
    /// Should Be Called From CallCustomBlock
    /// 
    /// </summary>
    /// <param name="customBlockDefinitionBlock"></param>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    public void SetCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock, string parameterName, string value)
    {
        if (this.CustomBlockParameterVariables.ContainsKey(customBlockDefinitionBlock) == false || this.CustomBlockParameterVariables[customBlockDefinitionBlock].ContainsKey(parameterName) == false)
        {
            Debug.LogError("Plesae InitCustomBlockLocalVariables : " + customBlockDefinitionBlock.CustomBlockName + ",  Parameter Name : " + parameterName);
        }

        this.CustomBlockParameterVariables[customBlockDefinitionBlock][parameterName] = value;
        this.OnUpdateMemoryVariable(parameterName);
    }

    public string GetCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock, string parameterName)
    {
        if (this.CustomBlockParameterVariables.ContainsKey(customBlockDefinitionBlock) == false || this.CustomBlockParameterVariables[customBlockDefinitionBlock].ContainsKey(parameterName) == false)
        {
            Debug.LogError("Plesae InitCustomBlockLocalVariables : " + customBlockDefinitionBlock.CustomBlockName + ",  Parameter Name : " + parameterName);
        }

        return string.Copy(this.CustomBlockParameterVariables[customBlockDefinitionBlock][parameterName]);
    }

    private void OnUpdateCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock, string key)
    {

    }

    #endregion


    

    #region Event

    public void StartEventBlock(string eventName)
    {
        if(this.RobotSourceCode.IsEventBlockExist(eventName))
        {
            this.BlockCallStack.Push(this.WaitingBlock); // Push Current WaitingBlock To BlockCallStack

            EventBlock eventBlock = this.RobotSourceCode.GetEventBlock(eventName); // Set EventBlock To WaitingBlock
            if (eventBlock != null)
                this.SetWaitingBlock(eventBlock);
        }
    
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
    /// 
    /// 
    /// Pushing To ComeBackFlowBlockStack should be called before Start New Flow
    /// Pushing To ComeBackFlowBlockStack should be called before Start New Flow
    /// Pushing To ComeBackFlowBlockStack should be called before Start New Flow
    /// Pushing To ComeBackFlowBlockStack should be called before Start New Flow
    /// Pushing To ComeBackFlowBlockStack should be called before Start New Flow
    /// </summary>
    public Stack<FlowBlock> BlockCallStack;


    public void SetWaitingBlock(FlowBlock flowBlock)
    {
        this.WaitingBlock = flowBlock;
    }
    public void ExecuteWaitingBlock(float deltaTime)
    {
        this.WaitingTime += deltaTime; // Add WaitingTime

        if (this.WaitingBlock == null)
            SetWaitingBlock(this.RobotSourceCode.LoopedBlock); // If There is no WaitingBlock, Set LoopedBlock To WaitingBlock


        FlowBlock.FlowBlockState flowBlockState = this.WaitingBlock.StartFlowBlock(this); // Start WaitingBlock

        switch (flowBlockState)
        {
            case FlowBlock.FlowBlockState.ExitFlowAfterOperation:
                // If Exit Flow ( There is no more NextBlock )
                // Set Top Block Of BlockCallStack To WaitingBlock newly
                if(this.BlockCallStack.Count > 0)
                {
                    SetWaitingBlock(this.BlockCallStack.Pop()); 
                }
                break;
        }
    }

    private bool IsInitBlockCalled = false;
    public void SetInitBlockToWaitingBlock()
    {
        if (this.RobotSourceCode.InitBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.InitBlock is null");
            return;
        }

        IsInitBlockCalled = true;
        this.SetWaitingBlock(this.RobotSourceCode.InitBlock);
    }

    public void SetLoopedBlockToWaitingBlock()
    {
        if (this.RobotSourceCode.LoopedBlock == null)
        {
            Debug.LogError("this.RobotSourceCode.LoopedBlock is null");
            return;
        }

        this.SetWaitingBlock(this.RobotSourceCode.LoopedBlock);
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

        DefinitionCustomBlock[] definitionCustomBlocks = robotSourceCodeTemplate.StoredCustomBlockDefinitionBlockArray;
        for (int i = 0; i < definitionCustomBlocks.Length; i++)
        {
            this.InitCustomBlockParameterVariables(definitionCustomBlocks[i]); 
        }

        this.BlockCallStack.Push(this.RobotSourceCode.LoopedBlock); // After Init Block, 
        this.SetInitBlockToWaitingBlock();
        

    }
    #endregion
}
