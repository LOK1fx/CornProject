using System;

namespace LOK1game.Game
{
    public enum ELaunchGameOption
    {
        AsClient,
        AsServer,
        AsHost,
    }

    public enum ESpawnType
    {
        Standard,
        FromCameraPosition,
    }

    [System.Serializable]
    public struct LaunchConfig
    {
        public ELaunchGameOption LaunchGameOption;
        public ESpawnType SpawnType;
    }
}
