using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RobotPart : MonoBehaviour
{
    private RobotBase motherRobotBase ;
    public RobotBase MotherRobotBase
    {
        protected get 
        {
            if (this.motherRobotBase == null)
                Debug.LogError("MotherRobotBase is null");

            return this.motherRobotBase;
        }
        set
        {
            if (this.motherRobotBase != null)
            {
                Debug.LogError("MotherRobotBase Already Set!!!!!");
                return;
            }

            this.motherRobotBase = value;
        }
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
