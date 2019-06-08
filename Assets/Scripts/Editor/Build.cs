using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Build : MonoBehaviour {

    [MenuItem("Build/Build")]
    public static void Build_()
    {
        string[] scenes = {"Assets/Scenes/MainMenu.unity", "Assets/Scenes/GameScene.unity", "Assets/Scenes/Shop.unity"};
        PlayerSettings.keyaliasPass = "android";
        PlayerSettings.keystorePass = "android";
        BuildPlayerOptions options;
        options.target = BuildTarget.Android;
    }
}
