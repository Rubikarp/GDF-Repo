using UnityEngine;
using UnityEditor;
using Karprod;

public class ToolTextureRampGenerator : EditorWindow
{
    private string textureName = "MyTexture";

    private Gradient gradient = new Gradient();

    private int xSize = 256;
    private int ySize = 256;

    private string filePath = "Assets/";

    [MenuItem("Tools/TextureRampGenerator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ToolTextureRampGenerator));
    }

    private void OnGUI()
    {
        GUILayout.Space(5);
        GUILayout.Label("Texture Ramp", EditorStyles.boldLabel);
        textureName = EditorGUILayout.TextField("Name", textureName);
        gradient = EditorGUILayout.GradientField("Gradient", gradient);

        GUILayout.Space(15);
        GUILayout.Label("Texture Size", EditorStyles.boldLabel);
        xSize = EditorGUILayout.IntField("Lenght", xSize);
        ySize = EditorGUILayout.IntField("Height", ySize);

        GUILayout.Space(5);
        GUILayout.Label("Ex : Assets/Folder/SubFolder/...", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("File Path", filePath);

        GUILayout.Space(15);
        if (GUILayout.Button("Generate Texture Ramp"))
        {
            CreateGradient();
        }
    }

    private void CreateGradient()
    {
        TextureRamp.Create(gradient, textureName, filePath, xSize, ySize);
    }
}
