namespace AgendaSyncer.Core.Exceptions;

public class SyncEventExeption : Exception
{
    public SyncEventExeption(string message) : base(message)
    {
    }
    
    public SyncEventExeption(string message, Exception innerException) : base(message, innerException)
    {
    }
}