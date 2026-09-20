using DotNetEnv;


namespace AgendaSyncer.Core.SyncEngine.Utilities;

public static class SecretsConfig
{
    public static void Load()
    {
        Env.TraversePath().Load();
    }
}