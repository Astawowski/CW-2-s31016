namespace APBD_02.obj;

public class OverfillException : Exception
{
    public OverfillException(string message) 
    : base(message){ }
}