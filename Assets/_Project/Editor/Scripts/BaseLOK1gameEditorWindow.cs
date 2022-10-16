using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public class BaseLOK1gameEditorWindow : EditorWindow
    {
        #region Paths

        public const string MENU_ITEM_NAME = "LOK1game Tools";
        public const string BRAND_PATH = "Assets/_Project/Brand";

        #endregion

        public void DrawLogo(float yPos)
        {
            GUI.DrawTexture(new Rect(0, yPos, 512, 512), GetLogoTexture(), ScaleMode.ScaleAndCrop);
        }

        public Texture GetLogoTexture()
        {
            return AssetDatabase.LoadAssetAtPath<Texture>($"{BRAND_PATH}/Logo.png");
        }

        //public App GetApp()
        //{
        //    return AssetDatabase.LoadAssetAtPath<App>(Constants.Editor.APP_PATH);
        //}
    }
}