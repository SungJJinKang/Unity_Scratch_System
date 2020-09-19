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

            this.ExecuteRobotsWaitingBlock(ExecuteRobotsWaitingBlockRate);

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
    public const float ExecuteRobotsWaitingBlockRate = 0.5f;

    /// <summary>
    /// Executes all spawned the robot's waiting block.
    /// </summary>
    private void ExecuteRobotsWaitingBlock(float deltaTime)
    {
        for (int i = 0; i < this.SpawnedRobotList.Count; i++)
        {
            if (this.SpawnedRobotList[i] != null)
                this.SpawnedRobotList[i].ExecuteWaitingBlock(deltaTime);
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

        if (this.SpawnedRobotDictionary.ContainsKey(robot.RobotUniqueId) == false)
        {
            this.SpawnedRobotDictionary.Add(robot.RobotUniqueId, robot);
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

        this.SpawnedRobotDictionary.Remove(robot.RobotUniqueId);
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
    private Dictionary<string, RobotSourceCode> StoredRobotSourceCode;
    public bool AddStoredRobotSourceCodeTemplate(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode == null)
            return false;

        if(this.StoredRobotSourceCode.ContainsKey(robotSourceCode.SourceCodeName) == true)
        {
            Debug.LogError("Try AddStoredRobotSourceCodeTemplate. SourceCodeName : " + robotSourceCode.SourceCodeName);
            return false;
        }

        this.StoredRobotSourceCode.Add(robotSourceCode.SourceCodeName, robotSourceCode);
        return true;
    }

    public RobotSourceCode GetRobotSourceCodeTemplate(string sourceCodeName)
    {
        if (this.StoredRobotSourceCode.ContainsKey(sourceCodeName) == false)
            return null;

        
        return this.StoredRobotSourceCode[sourceCodeName];
    }
    #endregion

}
