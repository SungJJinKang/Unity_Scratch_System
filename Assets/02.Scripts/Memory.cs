using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : RobotPart
{


    private string MemoryName;
    private Variable _Variable;

    public void CreateVariable(string memoryName, Variable.VariableType variableType)
    {
        this.MemoryName = memoryName;

        switch (variableType)
        {

            case Variable.VariableType.Number:
                _Variable = new Number();
                break;

            case Variable.VariableType.Text:
                _Variable = new Text();
                break;

            case Variable.VariableType.Boolean:
                _Variable = new Boolean();
                break;
        }
    }

    public bool GetMemoryName(ref string memoryName)
    {
        if (_Variable == null)
        {
            Debug.LogError("Please Create Variable");
            return false;
        }

        memoryName = this.MemoryName;
        return true;
    }

    public bool SetValue(float value)
    {
        if(_Variable == null)
        {
            Debug.LogError("Please Create Variable");
            return false;
        }

        if (_Variable is Number)
        {
            ((Number)_Variable).value = value;
            return true;
        }
        else
        {
            Debug.LogError("Please Pass proper value type");
            return false; 
        }

    }

    public bool SetValue(string value)
    {
        if (_Variable == null)
        {
            Debug.LogError("Please Create Variable");
            return false;
        }

        if (_Variable is Text)
        {
            ((Text)_Variable).value = value;
            return true;
        }
        else
        {
            Debug.LogError("Please Pass proper value type");
            return false;
        }
    }

    public bool SetValue(bool value)
    {
        if (_Variable == null)
        {
            Debug.LogError("Please Create Variable");
            return false;
        }

        if (_Variable is Boolean)
        {
            ((Boolean)_Variable).value = value;
            return true;
        }
        else
        {
            Debug.LogError("Please Pass proper value type");
            return false;
        }
    }

    public object GetValue()
    {
        if (_Variable == null)
        {
            Debug.LogError("Please Create Variable");
            return false;
        }

        //Boxing, Unboxing is really expensive, make a lot of gc
        //so change code this later
        return _Variable.GetValue();
    }


}

