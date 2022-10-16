using UnityEngine;
using UnityEditor;
using System.IO;
using LOK1game.Game;
using Newtonsoft.Json;

namespace LOK1game.Editor
{
#if UNITY_EDITOR
    [InitializeOnLoad()]

#endif
    public static class EditorConfig
    {
        private const string PATH = "Configs/Editor";
        private const string FILE_NAME = "EditorLaunchConfig.json";

        private static LaunchConfig _config;

        static EditorConfig()
        {
            var json = File.ReadAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}");

            _config = JsonConvert.DeserializeObject<LaunchConfig>(json);
        }

        public static void SetGameLaunchOption(ELaunchGameOption option)
        {
            _config.LaunchGameOption = option;

            Save();
        }

        public static void SetSpawnType(ESpawnType type)
        {
            _config.SpawnType = type;

            Save();
        }

        public static LaunchConfig GetConfig()
        {
            var json = File.ReadAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}");

            return JsonConvert.DeserializeObject<LaunchConfig>(json);
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(_config, Formatting.Indented);

            File.WriteAllText($"{Application.dataPath}/{PATH}/{FILE_NAME}", json);
        }
    }
}