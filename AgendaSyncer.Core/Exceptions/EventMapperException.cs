namespace AgendaSyncer.Core.Exceptions;

public class EventMapperException : Exception
{
    public EventMapperException(string message) : base(message)
    {
    }
    
    public EventMapperException(string message, Exception innerException) : base(message, innerException)
    {
    }
}