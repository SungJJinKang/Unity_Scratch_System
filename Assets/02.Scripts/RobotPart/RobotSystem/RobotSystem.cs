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

    private Dictionary<uint, RobotBase> SpawnedRobotList;

    public void AddToSpawnedRobotList(RobotBase robot)
    {
        if (robot == null)
            return;

        if (this.SpawnedRobotList.ContainsKey(robot.UniqueRobotId) == true)
            return;

        this.SpawnedRobotList.Add(robot.UniqueRobotId, robot);
    }

    public RobotBase GetSpawnedRobot(uint uniqueId)
    {
        if(this.SpawnedRobotList.ContainsKey(uniqueId) == false)
        {
            return null;
        }
        else
        {
            return this.SpawnedRobotList[uniqueId];
        }

    }

    #region RobotSourceCodeTemplate
    private Dictionary<string, RobotSourceCodeTemplate> StoredRobotSourceCodeTemplate;
    public bool AddStoredRobotSourceCodeTemplate(RobotSourceCodeTemplate robotSourceCodeTemplate)
    {
        if (robotSourceCodeTemplate == null || this.StoredRobotSourceCodeTemplate.ContainsKey(robotSourceCodeTemplate.SourceCodeName) == true)
            return false;

        this.StoredRobotSourceCodeTemplate.Add(robotSourceCodeTemplate.SourceCodeName, robotSourceCodeTemplate);
        return true;
    }

    public RobotSourceCodeTemplate GetRobotSourceCodeTemplate(string sourceCodeTemplateName)
    {
        if (this.StoredRobotSourceCodeTemplate.ContainsKey(sourceCodeTemplateName) == false)
            return null;

        
        return this.StoredRobotSourceCodeTemplate[sourceCodeTemplateName];
    }
    #endregion

}
