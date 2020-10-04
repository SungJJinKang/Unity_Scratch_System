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
        this.DrawDefaultInspector();

        if (robotSystem != null)
        {
            if (GUILayout.Button("Save All RobotSourceCodes"))
            {
                robotSystem.SaveAllRobotSourceCodes();
            }

            if (GUILayout.Button("Load All RobotSourceCodes"))
            {
                robotSystem.LoadAllRobotSourceCodes();
            }

            if (GUILayout.Button("Create RobotSourceCode"))
            {
                robotSystem.CreateRobotSourceCode(DateTime.Now.ToString());
            }

            if (GUILayout.Button("Clear StoredRobotSourceCodes"))
            {
                robotSystem.ClearStoredRobotSourceCodes();
            }

            if (GUILayout.Button("StoredRobotSourceCodes Count"))
            {
                Debug.Log(robotSystem.RobotSourceCodeCount.ToString());
            }
        }
    }
}