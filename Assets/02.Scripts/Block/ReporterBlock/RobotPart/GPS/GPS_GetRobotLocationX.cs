[BlockTitle("GetRobotLocationX")]
public sealed class GPS_GetRobotLocationX : ReporterBlock, IGPSBlockType
{
    public override string GetReporterStringValue()
    {
        GPS gps = base.GetOperatingRobotPart<GPS>();
        if (gps != null)
        {
            return gps.GetRobotLocationX().ToString();
        }
        else
        {
            return "";
        }
    }
}
