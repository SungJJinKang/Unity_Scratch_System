using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Robot system.
/// Contain All Robots Info,
/// Contain All Methods Used In This Game
/// 
/// </summary>
public class RobotSystem : MonoBehaviour
{
    public static RobotSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private Dictionary<string, RobotSourceCode> StoredRobotSourceCode;
    public bool AddStoredRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode == null || this.StoredRobotSourceCode.ContainsKey(robotSourceCode.SourceCodeName) == true)
            return false;

        this.StoredRobotSourceCode.Add(robotSourceCode.SourceCodeName, robotSourceCode);
        return true;
    }

    public RobotSourceCode GetRobotSourceCode(string sourceCodeName)
    {
        if (this.StoredRobotSourceCode.ContainsKey(sourceCodeName) == false)
            return null;

        
        return this.StoredRobotSourceCode[sourceCodeName];
    }


}
