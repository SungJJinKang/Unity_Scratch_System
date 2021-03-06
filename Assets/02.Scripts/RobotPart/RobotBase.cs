﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Robot base.
/// Thie is used identify Specific Robot
/// This is Robot, There is no Robot class !!!!!!!!!!!!
/// This contains Every Robot Parts Attached To Robot
/// </summary>
[System.Serializable]
public sealed class RobotBase : RobotPart
{
    protected override void Awake()
    {
        base.Awake();

        base.MotherRobotBase = this; // set itsetf to mother robotbase

        this.RobotPartCache = new Dictionary<Type, RobotPart>();
        this.BlockCallStack = new Stack<FlowBlock>();
    }

    protected override void Start()
    {
        base.Start();

        //
        //TEST
        this.AttachRobotPart<JetEngine>();
    }

    [SerializeField]
    private string robotUniqueId;

    public string RobotUniqueId
    {
        private set
        {
            this.robotUniqueId = value;
        }
        get
        {
            return this.robotUniqueId;
        }
    }

    #region RobotPart

    /// <summary>
    /// The attacehd robot parts.
    /// this can't contain RobotBase
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private List<RobotPart> AttacehdRobotParts;
    public bool AttachRobotPart<T>() where T : RobotPart
    {
        RobotPart newRobotPart = gameObject.AddComponent<T>();
        this.AttacehdRobotParts.Add(newRobotPart);
        newRobotPart.MotherRobotBase = this;

        RobotPartCache.Add(typeof(T), newRobotPart);

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

    private Dictionary<Type, RobotPart> RobotPartCache;

    /// <summary>
    /// Get Attached RobotPart Instance of Robot Instance
    /// 
    /// This method need optimisation
    /// need Robot Part Cache
    /// 
    /// </summary>
    /// <returns>The robot part.</returns>
    /// <typeparam name="T">Robot Part Type</typeparam>
    public T GetRobotPart<T>() where T : RobotPart
    {
        if (this.RobotPartCache.ContainsKey(typeof(T)))
            return RobotPartCache[typeof(T)] as T;
        else
            return null;
    }

    #endregion


    #region RobotGlobalVariable
    /// <summary>
    /// Memory Variable
    /// You can access to this variable through Block.Memory_SetValue Class or Momory_ChangeValue Class
    /// </summary>
    [SerializeField]
    private Dictionary<string, string> RobotGlobalVariable;

#if UNITY_EDITOR
    public List<KeyValuePair<string, string>> RobotGlobalVariableKeyValuePair
    {
        get
        {
            if (RobotGlobalVariable == null)
                return null;

            return this.RobotGlobalVariable.ToList();
        }
    }
#endif
    /*
    /// <summary>
    /// Clean Only Value Of Dictionary
    /// </summary>
    private void CleanRobotGlobalVariableValue()
    {
        this.RobotGlobalVariable.Keys.ToList().ForEach(x => this.RobotGlobalVariable[x] = "");
    }
    */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="deepCopiedRobotGlobalVariableTamplate"></param>
    /// <param name="maintainOriginalValue">If MemoryVariable have same key with deepCopiedMemoryVariableTamplate, </param>
    private void InitRobotGlobalVariable(Dictionary<string, string> deepCopiedRobotGlobalVariableTamplate, bool maintainOriginalValue = false)
    {
        if (maintainOriginalValue == true)
        {//Copy this.MemoryVariable Value To deepCopiedMemoryVariableTamplate
            this.RobotGlobalVariable.Keys.ToList().ForEach(x =>
            {
                if (deepCopiedRobotGlobalVariableTamplate.ContainsKey(x))
                {
                    deepCopiedRobotGlobalVariableTamplate[x] = this.RobotGlobalVariable[x];
                }
            });
        }

        this.RobotGlobalVariable = deepCopiedRobotGlobalVariableTamplate;
    }

    public void SetRobotGlobalVariable(string key, string text)
    {
        if (this.RobotGlobalVariable.ContainsKey(key) == false)
        {
            Debug.LogError("RobotGlobalVariableTemplate Dont Have Key : " + key);
            return;
        }

        this.RobotGlobalVariable[key] = text;
        this.OnUpdateRobotGlobalVariable(key);
    }

    public string GetRobotGlobalVariable(string key)
    {
        if (this.RobotGlobalVariable.ContainsKey(key) == false)
        {
            Debug.LogError("Robot Global Variable Dont Have Key : " + key);
            return String.Empty;
        }

        return string.Copy(this.RobotGlobalVariable[key]);
    }

    private void OnUpdateRobotGlobalVariable(string key)
    {

    }

    #endregion


    #region CustomBlockParameterVariables

    /// <summary>
    /// CustomBlock Parameter Vaiable ( Parameter )
    /// You can access to this variable through DefinitionCustomBlock, Parameter Name
    /// </summary>
    [SerializeField]
    private Dictionary<DefinitionCustomBlock, Dictionary<string, string>> CustomBlockParameterVariables;
#if UNITY_EDITOR
    public List<KeyValuePair<DefinitionCustomBlock, Dictionary<string, string>>> CustomBlockParameterVariablesKeyValuePair
    {
        get
        {
            if (CustomBlockParameterVariables == null)
                return null;

            return this.CustomBlockParameterVariables.ToList();
        }
    }
#endif

    /// <summary>
    /// Init CustomBlockParameterVariables
    /// </summary>
    /// <param name="customBlockDefinitionBlock">Custom block definition block.</param>
    private void InitCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock)
    {
        if (customBlockDefinitionBlock == null)
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
            this.CustomBlockParameterVariables[customBlockDefinitionBlock].Add(customBlockDefinitionBlock.ParameterNames[i], string.Empty); // Init Parameter Keys with customBlockDefinitionBlock;
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
        if (this.CustomBlockParameterVariables == null)
        {
            Debug.LogError("Plesae InitCustomBlockLocalVariables");
            return;
        }

        if (this.CustomBlockParameterVariables.ContainsKey(customBlockDefinitionBlock) == false || this.CustomBlockParameterVariables[customBlockDefinitionBlock].ContainsKey(parameterName) == false)
        {
            Debug.LogError("Plesae Add DefinitionCustomBlock : " + customBlockDefinitionBlock.CustomBlockName + ",  Parameter Name : " + parameterName);
            return;
        }

        this.CustomBlockParameterVariables[customBlockDefinitionBlock][parameterName] = value;
        this.OnUpdateRobotGlobalVariable(parameterName);
    }

    public string GetCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock, string parameterName)
    {
        if (this.CustomBlockParameterVariables == null)
        {
            Debug.LogError("Plesae InitCustomBlockLocalVariables");
            return string.Empty;
        }

        if (this.CustomBlockParameterVariables.ContainsKey(customBlockDefinitionBlock) == false || this.CustomBlockParameterVariables[customBlockDefinitionBlock].ContainsKey(parameterName) == false)
        {
            Debug.LogError("Plesae Add DefinitionCustomBlock : " + customBlockDefinitionBlock.CustomBlockName + ",  Parameter Name : " + parameterName);
            return string.Empty;
        }

        return string.Copy(this.CustomBlockParameterVariables[customBlockDefinitionBlock][parameterName]);
    }

    private void OnUpdateCustomBlockParameterVariables(DefinitionCustomBlock customBlockDefinitionBlock, string key)
    {

    }

    #endregion


    #region EventBlock

    public void StartEventBlock(string eventName)
    {
        if (this.installedRobotSourceCode.IsEventBlockExist(eventName))
        {
            EventBlock eventBlock = this.installedRobotSourceCode.GetEventBlock(eventName); // Set EventBlock To WaitingBlock
            if (eventBlock != null)
            {
                this.PushToBlockCallStack(eventBlock);
            }
        }

    }

    #endregion


    #region WaitBlock

    [SerializeField]
    public float WaitingTime = 0;

#if UNITY_EDITOR
    public FlowBlock WaitingBlock
    {
        get
        {
            if (this.BlockCallStack.Count == 0)
                return null;

            return this.BlockCallStack.Peek(); // return top block
        }
    }
#endif

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
    /// </summary>
    private Stack<FlowBlock> BlockCallStack;


#if UNITY_EDITOR

    /*
     *  if you push 1, 2, 3, 4, 5 then ToList will give you 5, 4, 3, 2, 1. 
     */
    public List<FlowBlock> BlockCallStackListForEditor
    {
        get
        {
            if (BlockCallStack == null)
                return null;
            else
                return BlockCallStack.ToList();
        }
    }
#endif

    public void PushToBlockCallStack(FlowBlock addedBlock)
    {
        if (addedBlock == null)
            return;

        this.BlockCallStack.Push(addedBlock);

    }


  
    /// <summary>
    /// Should called only From RobotSystem
    /// </summary>
    /// <param name="deltaTime"></param>
    public void ExecuteWaitingBlock(float deltaTime)
    {
        if(this.installedRobotSourceCode == null)
        {
            Debug.Log("Please add RobotSourceCode");
            return;
        }

        this.WaitingTime += deltaTime; // Add WaitingTime

      
        if (this.BlockCallStack.Count == 0)
        {
            //Still WaitingBlock is null, Set Top Of LoopedBlock To WaitingBlock
            this.PushToBlockCallStack(this.installedRobotSourceCode.LoopedBlock);
        }

        if (this.BlockCallStack.Count == 0)
        {
            return;
        }

        FlowBlock topBlock = this.BlockCallStack.Peek();
        bool isOperatable = topBlock.IsOperatable(this);
        if(isOperatable == true)
        {
            this.BlockCallStack.Pop(); //remove top block

            FlowBlock nextBlock = topBlock.StartFlowBlock(this); // Start WaitingBlock
            this.PushToBlockCallStack(nextBlock);
        }
       
    }


   


    #endregion


    #region RobotSourceCode
    /// <summary>
    /// Installed Robot Source Code On Thie Robot Instance
    /// Should Reference From RobotSystem.instance.StoredRobotSourceCode
    /// 
    /// 
    /// If RobotSourceCodeTemplate is changed or RobotSourceCode is set newly, WaitingBlock is cleaned, ReStart SourceCode Newly From InitBlock To LoopedBlcok
    /// </summary>
    private RobotSourceCode installedRobotSourceCode;
#if UNITY_EDITOR
    public RobotSourceCode InstalledRobotSourceCode => this.installedRobotSourceCode;
#endif
    public string RobotSourceCodeName => this.installedRobotSourceCode.SourceCodeName;




    public bool InstallRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode == null)
        {
            Debug.LogError("robotSourceCode is null");
            return false;
        }

        if (this.installedRobotSourceCode != null)
        {
            this.installedRobotSourceCode.RemoveFromInstalledRobotList(this);
            this.installedRobotSourceCode = null; // Clear Reference Of Installed Robot Source Code
        }

        this.installedRobotSourceCode = robotSourceCode; //shallow Copy
        this.installedRobotSourceCode.AddToInstalledRobotList(this);

        OnSetRobotSourceCode(robotSourceCode);

        return true;
    }

    private void OnSetRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        this.InitRobotGlobalVariable(robotSourceCode.GetDeepCopyOfRobotGlobalVariableTemplate());  // Deep copy MemoryVariable

        DefinitionCustomBlock[] definitionCustomBlocks = robotSourceCode.StoredCustomBlockDefinitionBlockArray;

        if (definitionCustomBlocks != null && definitionCustomBlocks.Length > 0)
        {
            for (int i = 0; i < definitionCustomBlocks.Length; i++)
            {
                this.InitCustomBlockParameterVariables(definitionCustomBlocks[i]);
            }
        }

        this.BlockCallStack.Clear(); // Cleare BlockCallStack
        this.PushToBlockCallStack(this.installedRobotSourceCode.InitBlock);

        this.WaitingTime = 0;
    }
#endregion

}
