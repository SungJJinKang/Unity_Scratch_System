using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBase : RobotPart
{
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
        return true;
    }

    public bool DetachRobotPart(RobotPart robotPart)
    {
        return this.AttacehdRobotParts.Remove(robotPart);
    }
    /// <summary>
    /// Should Referece from RobotSystem.StoredMethodDictionary
    /// </summary>
    [SerializeField]
    public Function MainLoopedFunction;

    public override void OnPreStartMainLoopedFunction()
    {
        for(int i=0;i< AttacehdRobotParts.Count;i++)
        {
            AttacehdRobotParts[i].OnPreStartMainLoopedFunction();
        }
    }

    public override void OnEndMainLoopedFunction()
    {
        for (int i = 0; i < AttacehdRobotParts.Count; i++)
        {
            AttacehdRobotParts[i].OnEndMainLoopedFunction();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        base.RobotBase = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    /////////////////
    /// Built In Method
    private Memory GetMemory(string memoryName)
    {
        if (string.IsNullOrEmpty(memoryName))
        {
            Debug.LogError("variableName is null");
            return null;
        }

        Memory memory = null;
        for (int i = 0; i < this.AttacehdRobotParts.Count; i++)
        {
            if (this.AttacehdRobotParts[i] is Memory)
            {
                string tempMemoryName = "";
                memory = this.AttacehdRobotParts[i] as Memory;
                if (memory.GetMemoryName(ref tempMemoryName))
                {
                    if (tempMemoryName == memoryName)
                    {
                        return memory;
                    }
                }
            }
        }

        return null;
    }

    public bool SetValueToMemory(string memoryName, string text)
    {
        Memory memory = GetMemory(memoryName);
        if(memory == null)
        {
            return false;
        }
        else
        {
            memory.SetValue(text);
            return true;
        }
    }

    public bool SetValueToMemory(string memoryName, bool boolean)
    {
        Memory memory = GetMemory(memoryName);
        if (memory == null)
        {
            return false;
        }
        else
        {
            memory.SetValue(boolean);
            return true;
        }
    }

    public bool SetValueToMemory(string memoryName, float number)
    {
        Memory memory = GetMemory(memoryName);
        if (memory == null)
        {
            return false;
        }
        else
        {
            memory.SetValue(number);
            return true;
        }
    }


}
