
using Boo.Lang;
using System.Collections.Generic;

public class RobotSourceCode 
{
    public string SourceCodeName;
    
    public HatBlock InitBlock;
    public HatBlock LoopedBlock;

    /// <summary>
    /// Please Check if Same Function Name is existing In Block
    /// </summary>
    public Dictionary<string, CustomFunctionBlock> StoredCustomFunctionBlock;
    

    ///////////////////////////

    /// <summary>
    /// Variable List
    /// Key Variable Name, Value Variable Value
    /// </summary>
    public Dictionary<string, string> StoredVariableBlock;
    public RobotBase TargetRobotBase;
}
