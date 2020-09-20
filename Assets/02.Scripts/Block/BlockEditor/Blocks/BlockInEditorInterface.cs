public interface IUpNotchBlockEditorUnit
{
    UpNotchBlock UpNotchTypeTargetBlock { get; }
    IDownBumpBlockEditorUnit PreviousBlockInEditor { get; set; }

}

public interface IDownBumpBlockEditorUnit
{
    DownBumpBlock DownBumpTypeTargetBlock { get; }
    IUpNotchBlockEditorUnit NextBlockInEditor { get; set; }

}