using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class JetEngine : RobotPart
{
    public float EnginePower;


    Vector2 vec2Cache;
    

    protected override void Awake()
    {
        base.Awake();

        vec2Cache = new Vector2();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Move(float x, float y)
    {
        vec2Cache.x = x;
        vec2Cache.y = y;

        //transform.Translate(vec2Cache * );
    }


}

