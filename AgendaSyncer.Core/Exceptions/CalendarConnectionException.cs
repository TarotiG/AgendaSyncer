namespace AgendaSyncer.Core.Exceptions;

public class CalendarConnectionException : Exception
{
    public CalendarConnectionException(string message) : base(message)
    {
    }
    
    public CalendarConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}