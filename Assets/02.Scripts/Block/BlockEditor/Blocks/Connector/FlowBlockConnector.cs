using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBlockConnector : BlockConnector
{
   


    protected override void Awake()
    {
        base.Awake();


    }

    public enum ConnectorType
    { 
        UpNotch,
        DownBump
    }

    public ConnectorType _ConnectorType;

   
}
