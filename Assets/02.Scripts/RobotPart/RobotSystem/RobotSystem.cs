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

        this.SpawnedRobotDictionary = new Dictionary<string, RobotBase>();
        this.SpawnedRobotList = new List<RobotBase>();
    }

    private void Start()
    {
        StartExecuteRobotsWaitingBlockCoroutine();
    }

    #region ExecuteRobotsWaitingBlock

    private void StartExecuteRobotsWaitingBlockCoroutine()
    {
        StopExecuteRobotsWaitingBlockCoroutine();
        ExecuteRobotsWaitingBlockCoroutine = StartCoroutine(ExecuteRobotsWaitingBlockIEnumerator());
    }

    private void StopExecuteRobotsWaitingBlockCoroutine()
    {
        if (ExecuteRobotsWaitingBlockCoroutine != null)
            StopCoroutine(ExecuteRobotsWaitingBlockCoroutine);

        ExecuteRobotsWaitingBlockCoroutine = null;
    }

    private Coroutine ExecuteRobotsWaitingBlockCoroutine ;
    private IEnumerator ExecuteRobotsWaitingBlockIEnumerator()
    {
        while(true)
        {

            this.ExecuteRobotsWaitingBlock();

            // WaitForSeconds works as scaledTime(deltaTime, not RealTime)
            // So You don't need 
            yield return new WaitForSeconds(ExecuteRobotsWaitingBlockRate); 
        }
    }

    /// <summary>
    /// Set Proper Value
    /// This affects to game performance
    /// too small rate can cause jitter
    /// </summary>
    private const float ExecuteRobotsWaitingBlockRate = 0.2f;

    /// <summary>
    /// Executes all spawned the robot's waiting block.
    /// </summary>
    private void ExecuteRobotsWaitingBlock()
    {
        for (int i = 0; i < this.SpawnedRobotList.Count; i++)
        {
            if (this.SpawnedRobotList[i] != null)
                this.SpawnedRobotList[i].ExecuteWaitingBlock(ExecuteRobotsWaitingBlockRate);
        }
    }

    #endregion

    #region SpawnedRobotList

    /// <summary>
    /// The spawned robot list.
    /// This is updated when SpawnedRobotDictionary is dirty For Performance!!!!!
    /// </summary>
    private List<RobotBase> SpawnedRobotList;

    private Dictionary<string, RobotBase> SpawnedRobotDictionary;

    public void AddToSpawnedRobotList(RobotBase robot)
    {
        if (robot == null)
            return;

        /*
        if (this.SpawnedRobotDictionary.ContainsKey(robot.UniqueRobotId) == false)
        {
            this.SpawnedRobotDictionary.Add(robot.UniqueRobotId, robot);
            SpawnedRobotList = SpawnedRobotDictionary.Values.ToArray(); // SpawnedRobotList is updated when SpawnedRobotDictionary is dirty
        }
        */

        if (this.SpawnedRobotDictionary.ContainsKey(robot.UniqueRobotId) == false)
        {
            this.SpawnedRobotDictionary.Add(robot.UniqueRobotId, robot);
        }

        if(this.SpawnedRobotList.Contains(robot) == false)
        {
            this.SpawnedRobotList.Add(robot);
        }
     
    }

    public void RemoveFromSpawnedRobotList(RobotBase robot)
    {
        if (robot == null)
            return;

        /*
        if(this.SpawnedRobotDictionary.Remove(robot.UniqueRobotId))
        {
            SpawnedRobotList = SpawnedRobotDictionary.Values.ToArray(); // SpawnedRobotList is updated when SpawnedRobotDictionary is dirty
        }
        */

        this.SpawnedRobotDictionary.Remove(robot.UniqueRobotId);
        this.SpawnedRobotList.Remove(robot);

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

    #endregion

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
