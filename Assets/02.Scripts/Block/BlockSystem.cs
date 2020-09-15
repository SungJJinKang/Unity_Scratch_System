using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    public static BlockSystem instance;

    private void Awake()
    {
        instance = this;

        this.StoredRobotSourceCodeTemplate = new List<RobotSourceCodeTemplate>();
    }

    private List<RobotSourceCodeTemplate> StoredRobotSourceCodeTemplate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
