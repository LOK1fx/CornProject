using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public class TestTools
    {
        private const string TEST_TOOLS_MENU_ITEM = "/TestTools/";

        [MenuItem(BaseLOK1gameEditorWindow.MENU_ITEM_NAME + TEST_TOOLS_MENU_ITEM + "Print all scene cameras")]
        private static void PrintAllSceneCameras()
        {
            var cameras = SceneView.GetAllSceneCameras();

            foreach (var camera in cameras)
            {
                Debug.Log($"{camera} at pos: {camera.transform.position}");
            }
        }
    }

}