
public sealed class GPS_GetRobotLocationY : ReporterBlock, IGPSBlockType
{
    public override string GetReporterStringValue(RobotBase operatingRobotBase)
    {
        GPS gps = operatingRobotBase.GetRobotPart<GPS>();
        if (gps != null)
        {
            return gps.GetRobotLocationY().ToString();
        }
        else
        {
            return System.String.Empty;
        }
    }
}
