using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBase : RobotPart
{
    /// <summary>
    /// The attacehd robot parts.
    /// this can't contain RobotBase
    /// </summary>
    private List<RobotPart> AttacehdRobotParts;
    public bool AttachRobotPart(RobotPart robotPart)
    {
        if (robotPart is RobotBase || this.AttacehdRobotParts.Contains(robotPart) == true)
            return false;

        this.AttacehdRobotParts.Add(robotPart);
        return true;
    }

    public bool DetachRobotPart(RobotPart robotPart)
    {
        return this.AttacehdRobotParts.Remove(robotPart);
    }

    private RobotSourceCode RobotSourceCode;
    public bool CopyFrom(string robotSourceCodeName)
    {
        RobotSourceCode robotSourceCodeTemplate = RobotSystem.instance.GetRobotSourceCode(robotSourceCodeName);
        if(robotSourceCodeTemplate == null)
        {
            Debug.LogError("Cant Find Robot SourceCode : " + robotSourceCodeName);
            return false;
        }

        this.RobotSourceCode = new RobotSourceCode(); // create new instance
        this.RobotSourceCode.SourceCodeName = robotSourceCodeTemplate.SourceCodeName; // shallow copy
        this.RobotSourceCode.InitBlock = robotSourceCodeTemplate.InitBlock; // shallow copy
        this.RobotSourceCode.LoopedBlock = robotSourceCodeTemplate.LoopedBlock; // shallow copy
        this.RobotSourceCode.StoredCustomFunctionBlock = robotSourceCodeTemplate.StoredCustomFunctionBlock;  // shallow copy

        this.RobotSourceCode.StoredVariableBlock = new Dictionary<string, string>();
        foreach(KeyValuePair<string, string> pair in robotSourceCodeTemplate.StoredVariableBlock)
        {
            this.RobotSourceCode.StoredVariableBlock.Add(pair.Key, pair.Value); // deep copy string ( string is referce type )
        }

        this.RobotSourceCode.TargetRobotBase = this; // set this instance

        return true;
    }

}
