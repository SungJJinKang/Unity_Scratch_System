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

    /// <summary>
    /// Used Token Name List
    /// if, <=,, else, loop, loopout ... is token
    /// methodName, variable name is token
    /// </summary>
    [SerializeField]
    private List<string> UsedStaticTokenName;
    public bool IsStaticTokenNameUsed(string staticTokenName)
    {
        if (this.UsedStaticTokenName == null)
            this.UsedStaticTokenName = new List<string>();

        return this.UsedStaticTokenName.Contains(staticTokenName);
    }
    public bool AddUsedTokenName(string staticTokenName)
    {
        if (IsStaticTokenNameUsed(staticTokenName) == true)
            return false;

        this.UsedStaticTokenName.Add(staticTokenName);
        return true;
    }

    private void Awake()
    {
        instance = this;
    }

    private List<RobotBase> SpawnedRobotList;
    private IEnumerator ExecuteSpawnedRobotLoopedMethod()
    {
        while(true)
        {
            for (int i = 0; i < this.SpawnedRobotList.Count; i++)
            {
                this.SpawnedRobotList[i].OnPreStartMainLoopedFunction();
                this.SpawnedRobotList[i].MainLoopedFunction.Operation(this.SpawnedRobotList[i]); // Call Looped Method Every Time
                this.SpawnedRobotList[i].OnEndMainLoopedFunction();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////////////////////////


    public Dictionary<string, Function> StoredMethodDictionary;
    public bool ExecuteFunction(string funcName, RobotBase robot)
    {
        if (this.StoredMethodDictionary.ContainsKey(funcName) == false)
            return false;

        return this.StoredMethodDictionary[funcName].Operation(robot);
    }
   
   
}
