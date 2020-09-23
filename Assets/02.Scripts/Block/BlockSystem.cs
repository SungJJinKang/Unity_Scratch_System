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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
