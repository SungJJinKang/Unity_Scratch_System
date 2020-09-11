using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                this.SpawnedRobotList[i].ExecuteLoopedMethod(); // Call Looped Method Every Time
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


    public Dictionary<string, Method> StoredMethodDictionary;
    public bool ExecuteMethod(string methodName, RobotBase robot)
    {
        if(StoredMethodDictionary.ContainsKey(methodName))
        {
            StoredMethodDictionary[methodName].ExecuteMethod(ref robot);
            return true;
        }
        else
        {
            return false;
        }
    }
   
   
}

[System.Serializable]
public class Method
{
    public string MathodName;
    public UnityAction[]

    /// <summary>
    /// Lenth of this value is count of parameter for this method
    /// each item is parameter type of method.
    /// ex) void Move(Text s, Number a) -> ParameterList = [VariableType.Text, VariableType.Number]
    /// </summary>
    public Memory.Variable.VariableType[] ParameterTypeList;
    public Memory.Variable.VariableType ReturnType;

    public void ExecuteMethod(ref RobotBase robot)
    {

    }
}

