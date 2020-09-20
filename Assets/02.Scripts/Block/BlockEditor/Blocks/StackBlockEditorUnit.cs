using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBlockEditorUnit : FlowBlockEditorUnit, IUpNotchBlockEditorUnit, IDownBumpBlockEditorUnit
{
    public UpNotchBlock UpNotchTypeTargetBlock { get => base.targetFlowBlock as UpNotchBlock; }
    public DownBumpBlock DownBumpTypeTargetBlock { get => base.targetFlowBlock as DownBumpBlock; }


    [SerializeField]
    private IDownBumpBlockEditorUnit previousBlockInEditor;
    public IDownBumpBlockEditorUnit PreviousBlockInEditor
    {
        get => this.previousBlockInEditor;
        set
        {
            this.previousBlockInEditor = value;
            UpNotchTypeTargetBlock.PreviousBlock = this.previousBlockInEditor.DownBumpTypeTargetBlock;
        }

    }

    [SerializeField]
    private IUpNotchBlockEditorUnit nextBlockInEditor;
    public IUpNotchBlockEditorUnit NextBlockInEditor
    {
        get => this.nextBlockInEditor;
        set
        {
            this.nextBlockInEditor = value;
            DownBumpTypeTargetBlock.NextBlock = this.nextBlockInEditor.UpNotchTypeTargetBlock;
        }
    }
 
  
}
