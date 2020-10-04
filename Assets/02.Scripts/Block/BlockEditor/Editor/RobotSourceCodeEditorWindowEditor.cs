
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RobotSourceCodeEditorWindow))]
public class RobotSourceCodeEditorWindowEditor : Editor
{
    private RobotSourceCodeEditorWindow _RobotSourceCodeEditorWindow;

    private void Awake()
    {
        _RobotSourceCodeEditorWindow = target as RobotSourceCodeEditorWindow;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(30);

        if (GUILayout.Button("Create RobotSourceCode"))
        {
            RobotSystem.instance.CreateRobotSourceCode(System.DateTime.Now.ToString());
        }

        if (GUILayout.Button("Set First Robot Sourcode"))
        {
            _RobotSourceCodeEditorWindow._RobotSourceCode = RobotSystem.instance.RobotSourceCodeList[0];
        }

    }

}
#endif