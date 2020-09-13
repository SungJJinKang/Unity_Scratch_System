using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : RobotPart
{
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
    }

    public float GetRobotLocationX()
    {
        return base.MotherRobotBase.transform.position.x;
    }

    public float GetRobotLocationY()
    {
        return base.MotherRobotBase.transform.position.y;
    }
}

