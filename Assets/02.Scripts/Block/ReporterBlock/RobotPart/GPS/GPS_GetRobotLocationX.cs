
public sealed class GPS_GetRobotLocationX : ReporterBlock, IGPSBlockType
{
    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        GPS gps = operatingRobotBase.GetRobotPart<GPS>();
        if (gps != null)
        {
            return gps.GetRobotLocationX().ToString();
        }
        else
        {
            return System.String.Empty;
        }
    }
}
