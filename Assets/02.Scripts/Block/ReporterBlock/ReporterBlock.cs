﻿[System.Serializable]
public abstract class ReporterBlock : ValueBlock
{
    /*
    public virtual string GetReporterStringValue()
    {
        return DefaultStringValue;
    }
    */
    public abstract string GetReporterStringValue(RobotBase operatingRobotBase);

    private string ReporterStringValueCache = string.Empty;
    private float ReporterNumberValueCache = 0;
    public float GetReporterNumberValue(RobotBase operatingRobotBase)
    {
        string reporterStringValue = this.GetReporterStringValue(operatingRobotBase);
        if (reporterStringValue.Equals(ReporterStringValueCache))
        {
            return ReporterNumberValueCache;
        }
        else
        {
            ReporterNumberValueCache = BlockUtility.ConvertStringToFloat(reporterStringValue);
            ReporterStringValueCache = reporterStringValue; //save value to cache
            return ReporterNumberValueCache;
        }

       

    }

}
