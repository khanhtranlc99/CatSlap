
using UnityEngine;
using UnityEditor;
using System.Collections;


public class AppSauce : EditorWindow
{
    [MenuItem("Window/AppSauce")]
    static void Init()
    {
        AppSauce aboutWindow = (AppSauce)EditorWindow.GetWindowWithRect
                (typeof(AppSauce), new Rect(0, 0, 350, 300), false, "AppSauce");
        aboutWindow.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(70, 20, 300, 100));
        GUILayout.Label("Slap Champ", EditorStyles.boldLabel);
        GUILayout.Label("by Appsauce");
        GUILayout.EndArea();
        GUILayout.Space(70);


        GUILayout.Label("Info", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("More Games");
        if (GUILayout.Button("Visit", GUILayout.Width(100)))
        {
            Help.BrowseURL("https://www.sellmyapp.com/author/halfbulletarts/");
        }
        GUILayout.EndHorizontal();

        

        
        GUILayout.Space(5);

        GUILayout.Label("Support", EditorStyles.boldLabel);
        

        GUILayout.BeginHorizontal();
        GUILayout.Label("Skype ID");
        EditorGUILayout.TextField("slipknot2543", GUILayout.Width(200));

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Email ID");
        EditorGUILayout.TextField("support@sellapp.codes", GUILayout.Width(200));
        GUILayout.EndHorizontal();
        GUILayout.Space(5);


    }
}