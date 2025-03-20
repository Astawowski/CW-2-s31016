namespace APBD_02;

public class OverfillException : Exception
{
    public OverfillException(string message) 
    : base(message){ }
}