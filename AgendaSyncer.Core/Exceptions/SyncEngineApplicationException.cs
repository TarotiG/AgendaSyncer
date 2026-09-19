namespace AgendaSyncer.Core.Exceptions;

public class SyncEngineApplicationException : Exception
{
    public SyncEngineApplicationException(string message) : base(message)
    {
    }
    
    public SyncEngineApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}