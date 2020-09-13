[BlockTitle("GetRobotLocationY")]
public sealed class GPS_GetRobotLocationY : ReporterBlock, IGPSBlockType
{
    public override string GetReporterStringValue()
    {
        GPS gps = base.GetOperatingRobotPart<GPS>();
        if (gps != null)
        {
            return gps.GetRobotLocationY().ToString();
        }
        else
        {
            return "";
        }
    }
}
