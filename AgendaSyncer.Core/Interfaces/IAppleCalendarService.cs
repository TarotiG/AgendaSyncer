using AgendaSyncer.Core.Abstractions;

namespace AgendaSyncer.Core.Interfaces;

public interface IAppleCalendarService
{
    void CreateConnection();
    
    void GetEvents();
}