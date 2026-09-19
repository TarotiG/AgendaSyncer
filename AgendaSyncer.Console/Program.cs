Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting Syncer!");
    
    SyncEngine.ConnectToCalendars();
    
}
catch (SyncEngineApplicationException ex)
{
    throw new SyncEngineApplicationException("Application terminated unexpectedly", ex);
}
finally
{
    Log.Information("Closing Syncer");
    Log.CloseAndFlush();
}