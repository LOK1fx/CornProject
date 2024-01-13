using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using UnityEditor;

namespace LOK1game.Editor
{
    [CustomEditor(typeof(CharacterSpawnPoint))]
    public class CharacterSpawnPointEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (CharacterSpawnPoint)target;

            GUILayout.Space(20f);
            GUILayout.Label($"Spawn point ID: {script.Id}");
            GUILayout.Space(10f);

            if (GUILayout.Button("Generate new ID"))
            {
                script.Editor_GenerateNewId();
            }
        }
    }
}