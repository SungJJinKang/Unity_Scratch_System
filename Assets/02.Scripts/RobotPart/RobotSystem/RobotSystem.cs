using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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

        this.SpawnedRobotList = new List<RobotBase>();
        this.SpawnedRobotDictionary = new Dictionary<string, RobotBase>();
    }

    /// <summary>
    /// The spawned robot list.
    /// This is updated when SpawnedRobotDictionary is dirty For Performance!!!!!
    /// </summary>
    private RobotBase[] SpawnedRobotList;

    private void StartLoopedBlockForAllSpawnedRobot()
    {
        for (int i = 0; i < this.SpawnedRobotList.Length; i++)
        {
            if (this.SpawnedRobotList[i] != null)
                this.SpawnedRobotList[i].StartLoopedBlock();
        }
    }
    private Dictionary<string, RobotBase> SpawnedRobotDictionary;

    public void AddToSpawnedRobotList(RobotBase robot)
    {
        if (robot == null)
            return;

        if (this.SpawnedRobotDictionary.ContainsKey(robot.UniqueRobotId) == false)
        {
            this.SpawnedRobotDictionary.Add(robot.UniqueRobotId, robot);
            SpawnedRobotList = SpawnedRobotDictionary.Values.ToArray(); // SpawnedRobotList is updated when SpawnedRobotDictionary is dirty
        }


       
    }

    public void RemoveFromSpawnedRobotList(RobotBase robot)
    {
        if(this.SpawnedRobotDictionary.Remove(robot.UniqueRobotId))
        {
            SpawnedRobotList = SpawnedRobotDictionary.Values.ToArray(); // SpawnedRobotList is updated when SpawnedRobotDictionary is dirty
        }

    }

    public RobotBase GetSpawnedRobot(string uniqueId)
    {
        if(this.SpawnedRobotDictionary.ContainsKey(uniqueId) == false)
        {
            return null;
        }
        else
        {
            return this.SpawnedRobotDictionary[uniqueId];
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
