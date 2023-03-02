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

    [Serializable]
    public struct LaunchConfig
    {
        public ELaunchGameOption LaunchGameOption;
        public ESpawnType SpawnType;

        public override string ToString()
        {
            return $"{LaunchGameOption.ToString()} | {SpawnType.ToString()}";
        }
    }
}
