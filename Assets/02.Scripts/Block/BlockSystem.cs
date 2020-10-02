using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    public static BlockSystem instance;

    private void Awake()
    {
        instance = this;

        this.StoredRobotSourceCodeList = new List<RobotSourceCode>();
    }

    private List<RobotSourceCode> StoredRobotSourceCodeList;

    private bool AddToStoredRobotSourceCodeList(RobotSourceCode robotSourceCode)
    {
        if (this.StoredRobotSourceCodeList.Contains(robotSourceCode) == true)
            return false;

        this.StoredRobotSourceCodeList.Add(robotSourceCode);
        return true;
    }

    public RobotSourceCode CreateRobotSourceCode(string sourceCodeName)
    {
        RobotSourceCode createdRobotSourceCode = new RobotSourceCode(sourceCodeName);
        this.AddToStoredRobotSourceCodeList(createdRobotSourceCode);
        return createdRobotSourceCode;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
