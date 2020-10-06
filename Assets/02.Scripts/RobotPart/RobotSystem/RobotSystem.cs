using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System;

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
        this.StoredRobotSourceCodes = new List<RobotSourceCode>();
    }

    private void Start()
    {
        //StartExecuteRobotsWaitingBlockCoroutine();
    }

    private void Update()
    {
        this.ExecuteRobotsWaitingBlock(Time.deltaTime);
    }

    #region ExecuteRobotsWaitingBlock
    /*
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

    private Coroutine ExecuteRobotsWaitingBlockCoroutine;
    private IEnumerator ExecuteRobotsWaitingBlockIEnumerator()
    {
        while (true)
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

    */


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
    [SerializeField]
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

        if (this.SpawnedRobotList.Contains(robot) == false)
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
        if (this.SpawnedRobotDictionary.ContainsKey(uniqueId) == false)
        {
            return null;
        }
        else
        {
            return this.SpawnedRobotDictionary[uniqueId];
        }

    }

    #endregion

    #region RobotSourceCode
    private List<RobotSourceCode> StoredRobotSourceCodes;

    public RobotSourceCode CreateRobotSourceCode(string sourceCodeName)
    {
        RobotSourceCode createdRobotSourceCode = new RobotSourceCode(sourceCodeName);

        if (createdRobotSourceCode != null)
        {
            this.AddStoredRobotSourceCodeTemplate(createdRobotSourceCode);
        }
        return createdRobotSourceCode;
    }

    private bool AddStoredRobotSourceCodeTemplate(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode == null)
            return false;

        if (this.StoredRobotSourceCodes.Contains(robotSourceCode) == true)
        {
            Debug.LogError("Try AddStoredRobotSourceCodeTemplate. SourceCodeName : " + robotSourceCode.SourceCodeName);
            return false;
        }

        this.StoredRobotSourceCodes.Add(robotSourceCode);
        return true;
    }

    public RobotSourceCode GetRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        if (this.StoredRobotSourceCodes.Contains(robotSourceCode) == false)
            return null;


        return this.StoredRobotSourceCodes.Find(x => x == robotSourceCode);
    }

    public void ClearStoredRobotSourceCodes()
    {
        this.StoredRobotSourceCodes.Clear();
    }

    public RobotSourceCode[] RobotSourceCodeList => StoredRobotSourceCodes.ToArray();
    public int RobotSourceCodeCount => this.StoredRobotSourceCodes.Count;

    #endregion

    #region Save RobotSourceCode
    public void SaveAllRobotSourceCodes()
    {
        foreach(var robotSourceCode in this.StoredRobotSourceCodes)
        {
            this.SaveRobotSourceCode(robotSourceCode);
        }

        System.GC.Collect();
    }

  

    private string saveFolderPath;
    private string SaveFolderPath
    {
        get
        {
            if (string.IsNullOrEmpty(this.saveFolderPath))
            {
                this.saveFolderPath = Application.persistentDataPath + "/RobotSourceCode/";
                System.IO.Directory.CreateDirectory(this.saveFolderPath);
            }

            return this.saveFolderPath;
        }
    }

    public void SaveRobotSourceCode(RobotSourceCode robotSourceCode)
    {
        if (robotSourceCode == null)
            return;

       

        string json = JsonConvert.SerializeObject(robotSourceCode, Formatting.Indented, Utility.JsonSerializerSettings);
        string savePath = GetSaveFilePath(robotSourceCode);
        Debug.Log(savePath);

        File.WriteAllText(savePath, json);
#if UNITY_EDITOR
        Utility.stringBuilderCache.Clear();
        Utility.stringBuilderCache.Append("Success Serialize RobotSourceCode To Json : ");
        Utility.stringBuilderCache.Append(robotSourceCode.SourceCodeName);
        Utility.stringBuilderCache.Append("\n\n");
        Utility.stringBuilderCache.Append(json);
        Debug.Log(Utility.stringBuilderCache.ToString());
#endif
    }

    private string GetSaveFilePath(RobotSourceCode robotSourceCode)
    {
        
        return this.SaveFolderPath + robotSourceCode.SourceCodeName + ".json";
    }


    public void LoadAllRobotSourceCodes()
    {
        foreach (string file in Directory.EnumerateFiles(SaveFolderPath, "*.json"))
        {
            string json = File.ReadAllText(file);
            RobotSourceCode createdRobotSourceCode = JsonConvert.DeserializeObject<RobotSourceCode>(json, Utility.JsonSerializerSettings);
            this.AddStoredRobotSourceCodeTemplate(createdRobotSourceCode);
#if UNITY_EDITOR
            Debug.Log(Utility.TraceWriter.ToString());
#endif
        }


        System.GC.Collect();
    }

    
    #endregion


}
