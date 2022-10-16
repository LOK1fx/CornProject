using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public class EditorPlayerPrefsWindow : BaseLOK1gameEditorWindow
    {
        [MenuItem(MENU_ITEM_NAME+"/Manage PlayerPrefs")]
        public static void ShowWindow()
        {
            GetWindow<EditorPlayerPrefsWindow>("PlayerPrefs manager");
        }
        
        private void OnGUI()
        {
            GUILayout.Space(20f);

            if (GUILayout.Button("Delete all keys(Editor)"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}