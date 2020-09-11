using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : RobotPart
{
    [System.Serializable]
    public class Variable
    {
        [System.Serializable]
        public enum VariableType
        {
            Number,
            Text,
            Boolean
        }

        public virtual object GetValue()
        {
            return null;
        }
    }

    [System.Serializable]
    public class Number : Variable
    {
        public float value;

        public Number()
        {
            value = 0;
        }

        public override object GetValue()
        {
            return value;
        }
    }

    [System.Serializable]
    public class Text : Variable
    {
        public string value;

        public Text() 
        {
            value = "";
        }

        public override object GetValue()
        {
            return value;
        }
    }

    [System.Serializable]
    public class Boolean : Variable
    {
        public bool value;

        public Boolean() 
        {
            value = false;
        }

        public override object GetValue()
        {
            return value;
        }
    }

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

