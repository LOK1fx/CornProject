using LOK1game.Editor;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad()]
public static class ToolbarExtensions
{
    #region Textes

    private const string NAV_APP_TEXT = "Navigate the App";
    private const string NAV_APP_TOOLTIP = "Navigate the app object";

    private const string NAV_LEVEL_DB_TEXT = "Navigate the Levels DB";
    private const string NAV_LEVEL_DB_TOOLTIP = "Navigate the levels database";

    private const string PLAY_AS_CLIENT_TEXT = "Set Client";
    private const string PLAY_AS_CLIENT_TOOLTIP = "Set launch option to play as client";

    private const string PLAY_AS_SERVER_TEXT = "Set Server";
    private const string PLAY_AS_SERVER_TOOLTIP = "Set launch option to play as server";

    private const string PLAY_AS_HOST_TEXT = "Set Host";
    private const string PLAY_AS_HOST_TOOLTIP = "Set launch option to play as Host(Player with server on it)";

    #endregion

    //private static ELaunchGameOption _currentGameOptions;

    static ToolbarExtensions()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
        ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);

        //_currentGameOptions = EditorConfig.GetConfig().LaunchGameOption;
    }

    private static void DrawLeftGUI()
    {
        GUILayout.FlexibleSpace();

        DrawNavAppButton();
        DrawNavLevelsDatabaseButton();
    }

    private static void DrawRightGUI()
    {
        if (EditorApplication.isPlaying == false)
        {
            DrawPlayAsClientButton();
            DrawPlayAsServerButton();
            DrawPlayAsHostButton();
        }

        DrawCurrentGameLaunchOption();

        GUILayout.FlexibleSpace();
    }

    private static void DrawCurrentGameLaunchOption()
    {
        //GUILayout.Label(new GUIContent($"Current launch option: {_currentGameOptions.ToString()}"));
    }

    private static void DrawPlayAsClientButton()
    {
        if(GUILayout.Button(new GUIContent(PLAY_AS_CLIENT_TEXT, PLAY_AS_CLIENT_TOOLTIP)))
        {
            //EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsClient);

            //_currentGameOptions = ELaunchGameOption.AsClient;
        }
    }

    private static void DrawPlayAsServerButton()
    {
        if (GUILayout.Button(new GUIContent(PLAY_AS_SERVER_TEXT, PLAY_AS_SERVER_TOOLTIP)))
        {
            //EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsServer);

            //_currentGameOptions = ELaunchGameOption.AsServer;
        }
    }

    private static void DrawPlayAsHostButton()
    {
        if (GUILayout.Button(new GUIContent(PLAY_AS_HOST_TEXT, PLAY_AS_HOST_TOOLTIP)))
        {
            //EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsHost);

            //_currentGameOptions = ELaunchGameOption.AsHost;
        }
    }

    private static void Play()
    {
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    private static void DrawNavAppButton()
    {
        if (GUILayout.Button(new GUIContent(NAV_APP_TEXT, NAV_APP_TOOLTIP)))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Constants.Editor.APP_PATH);
        }
    }

    private static void DrawNavLevelsDatabaseButton()
    {
        if (GUILayout.Button(new GUIContent(NAV_LEVEL_DB_TEXT, NAV_LEVEL_DB_TOOLTIP)))
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Constants.Editor.LEVEL_DB_PATH);
        }
    }
}