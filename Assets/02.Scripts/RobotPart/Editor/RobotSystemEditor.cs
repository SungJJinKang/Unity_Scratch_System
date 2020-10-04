using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RobotSystem), true)]
public class RobotSystemEditor : Editor
{
    RobotSystem robotSystem;


    private void Awake()
    {


    }

    private void OnEnable()
    {
        robotSystem = target as RobotSystem;

    }

    public override void OnInspectorGUI()
    {

        if (robotSystem != null)
        {
            if(GUILayout.Button("Debug All RobotSourceCode JSON"))
            {
                foreach(var code in robotSystem.RobotSourceCodeList)
                {
                    code.ConvertToJson();
                }
            }

            if (GUILayout.Button("Create RobotSourceCode"))
            {
                robotSystem.CreateRobotSourceCode(DateTime.Now.ToString());
            }
        }
    }
}