using LOK1game.Editor;
using LOK1game.Game;
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

    private const string SWITCH_SPAWN_TYPE_TEXT = "Switch Spawn";
    private const string SWITCH_SPAWN_TYPE_TOOLTIP = "Switches between SpawnTypes (Works only with the BaseGameMode game modes)";

    #endregion

    private static LaunchConfig _editorLaunchConfig;

    static ToolbarExtensions()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
        ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);

        RefreshConfigInfo();
    }

    private static void DrawLeftGUI()
    {
        GUILayout.FlexibleSpace();

        GUILayout.Label("Navigation");

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
            DrawSwitchSpawnTypeButton();
        }

        DrawCurrentGameLaunchOption();

        GUILayout.FlexibleSpace();
    }

    private static void DrawCurrentGameLaunchOption()
    {
        GUILayout.Label(new GUIContent($"Current launch option: {_editorLaunchConfig.ToString()}"));
    }

    private static void DrawSwitchSpawnTypeButton()
    {
        GUILayout.Space(2f);

        if (GUILayout.Button(new GUIContent(SWITCH_SPAWN_TYPE_TEXT, SWITCH_SPAWN_TYPE_TOOLTIP)))
        {
            var spawnType = EditorConfig.GetSpawnType();

            if (spawnType == ESpawnType.Standard)
                EditorConfig.SetSpawnType(ESpawnType.FromCameraPosition);
            else
                EditorConfig.SetSpawnType(ESpawnType.Standard);

            RefreshConfigInfo();
        }

        GUILayout.Space(2f);
    }

    private static void DrawPlayAsClientButton()
    {
        if(GUILayout.Button(new GUIContent(PLAY_AS_CLIENT_TEXT, PLAY_AS_CLIENT_TOOLTIP)))
        {
            EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsClient);

            RefreshConfigInfo();
        }
    }

    private static void DrawPlayAsServerButton()
    {
        if (GUILayout.Button(new GUIContent(PLAY_AS_SERVER_TEXT, PLAY_AS_SERVER_TOOLTIP)))
        {
            EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsServer);

            RefreshConfigInfo();
        }
    }

    private static void DrawPlayAsHostButton()
    {
        if (GUILayout.Button(new GUIContent(PLAY_AS_HOST_TEXT, PLAY_AS_HOST_TOOLTIP)))
        {
            EditorConfig.SetGameLaunchOption(ELaunchGameOption.AsHost);

            RefreshConfigInfo();
        }
    }

    private static void RefreshConfigInfo()
    {
        _editorLaunchConfig = EditorConfig.GetConfig();
    }

    private static void Play()
    {
        EditorApplication.ExecuteMenuItem("Edit/Play");

        RefreshConfigInfo();
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