using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBase : RobotPart
{
    public List<RobotPart> AttacehdRobotPart;

    [SerializeField]
    private List<string> loopedMethodNameList;
    public void ExecuteLoopedMethod()
    {
        for (int i = 0; i < loopedMethodNameList.Count; i++)
        {
            RobotSystem.instance.ExecuteMethod(this.loopedMethodNameList[i], this);
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
        for (int i = 0; i < this.AttacehdRobotPart.Count; i++)
        {
            if (this.AttacehdRobotPart[i] is Memory)
            {
                string tempMemoryName = "";
                memory = this.AttacehdRobotPart[i] as Memory;
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
