using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    protected RobotBase RobotBase;

    private List<Function> BuiltInMethod;
    /*
    protected bool AddRobotPartMethod()
    {

    }

    private List<Variable> BuiltInVariable;
    protected bool AddPartVariable(ref Variable variable)
    {

    }

    public Variable ExcuteMethod(string methodName)
    {
        //Check Global Method



        //Check Static Method 
    }
    */

    public virtual void OnPreStartMainLoopedFunction()
    {

    }

    public virtual void OnEndMainLoopedFunction()
    {

    }


    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    //Use "TextAsset" To Save method
}
