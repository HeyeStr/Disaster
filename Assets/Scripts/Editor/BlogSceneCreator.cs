using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 博客场景创建工具
/// 用于快速创建7天的博客场景
/// </summary>
public class BlogSceneCreator : EditorWindow
{
    [MenuItem("Tools/创建多天博客场景")]
    public static void ShowWindow()
    {
        GetWindow<BlogSceneCreator>("博客场景创建器");
    }
    
    private string baseScenePath = "Assets/Scenes/BlogScene.unity";
    private string outputFolder = "Assets/Scenes/BlogScenes/";
    
    void OnGUI()
    {
        GUILayout.Label("博客场景创建器", EditorStyles.boldLabel);
        GUILayout.Space(10);
        
        GUILayout.Label("基础场景路径:");
        baseScenePath = EditorGUILayout.TextField(baseScenePath);
        
        GUILayout.Label("输出文件夹:");
        outputFolder = EditorGUILayout.TextField(outputFolder);
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("创建7天博客场景"))
        {
            CreateBlogScenes();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("打开输出文件夹"))
        {
            EditorUtility.RevealInFinder(outputFolder);
        }
    }
    
    void CreateBlogScenes()
    {
        // 检查基础场景是否存在
        if (!File.Exists(baseScenePath))
        {
            EditorUtility.DisplayDialog("错误", $"基础场景不存在: {baseScenePath}", "确定");
            return;
        }
        
        // 创建输出文件夹
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        
        // 创建7天的博客场景
        for (int day = 1; day <= 7; day++)
        {
            string sceneName = $"BlogScene_Day{day}";
            string outputPath = Path.Combine(outputFolder, $"{sceneName}.unity");
            
            // 复制场景文件
            if (File.Exists(outputPath))
            {
                Debug.Log($"场景已存在，跳过: {sceneName}");
                continue;
            }
            
            File.Copy(baseScenePath, outputPath);
            AssetDatabase.ImportAsset(outputPath);
            
            Debug.Log($"已创建场景: {sceneName}");
        }
        
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("完成", "7天博客场景创建完成！", "确定");
    }
}
