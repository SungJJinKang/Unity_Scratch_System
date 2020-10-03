public interface ILiteralBlock 
{
}

public interface ILiteralReporterBlock : ILiteralBlock
{   
    string GetStringValue();
}

public interface ILiteralBooleanBlock  : ILiteralBlock
{
   
}
