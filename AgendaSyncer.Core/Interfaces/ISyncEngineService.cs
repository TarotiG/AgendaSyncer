namespace AgendaSyncer.Core.Interfaces;

public interface ISyncEngineService
{
    void TransformEvent<TEvent>();
    
    void SyncEvents();
}