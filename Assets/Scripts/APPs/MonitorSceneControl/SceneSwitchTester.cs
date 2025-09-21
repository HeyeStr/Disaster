using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTester : MonoBehaviour
{
    [Header("测试设置")]
    public KeyCode testBlogSceneKey = KeyCode.B;
    public KeyCode testBlogDetailKey = KeyCode.D;
    public KeyCode testBackToBlogKey = KeyCode.R;
    public KeyCode testBackToInitialKey = KeyCode.Escape;
    
    private SceneControlMono sceneControl;
    
    void Start()
    {
        sceneControl = FindObjectOfType<SceneControlMono>();
        if (sceneControl == null)
        {
            Debug.LogError("未找到SceneControlMono组件！");
        }
    }
    
    void Update()
    {
        if (sceneControl == null) return;
        
        // 测试博客场景加载
        if (Input.GetKeyDown(testBlogSceneKey))
        {
            Debug.Log("测试：加载博客场景");
            sceneControl.LoadBlogScene();
        }
        
        // 测试博客详情场景加载
        if (Input.GetKeyDown(testBlogDetailKey))
        {
            Debug.Log("测试：加载博客详情场景");
            sceneControl.LoadBlogDetailScene();
        }
        
        // 测试返回博客列表
        if (Input.GetKeyDown(testBackToBlogKey))
        {
            Debug.Log("测试：返回博客列表");
            sceneControl.BackToBlogList();
        }
        
        // 测试返回初始场景
        if (Input.GetKeyDown(testBackToInitialKey))
        {
            Debug.Log("测试：返回初始场景");
            sceneControl.BackToInitialScene();
        }
    }
    
    void OnGUI()
    {
        if (sceneControl == null) return;
        
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("场景切换测试控制台");
        GUILayout.Label($"按 {testBlogSceneKey} 加载博客场景");
        GUILayout.Label($"按 {testBlogDetailKey} 加载博客详情场景");
        GUILayout.Label($"按 {testBackToBlogKey} 返回博客列表");
        GUILayout.Label($"按 {testBackToInitialKey} 返回初始场景");
        
        GUILayout.Space(10);
        GUILayout.Label("当前已加载的场景：");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            GUILayout.Label($"- {scene.name} ({(scene.isLoaded ? "已加载" : "未加载")})");
        }
        
        GUILayout.EndArea();
    }
}
