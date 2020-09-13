using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotPart : MonoBehaviour
{
    protected RobotBase MotherRobotBase = null;
    public void SetMotherRobotBase(RobotBase robotBase)
    {
        if(this.MotherRobotBase != null)
        {
            Debug.LogError("MotherRobotBase Already Set!!!!!");
            return;
        }

        this.MotherRobotBase = robotBase;
    }

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
