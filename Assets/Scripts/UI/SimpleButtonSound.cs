using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SimpleButtonSound : MonoBehaviour
{
    [Header("音效文件")]
    public AudioClip clickSound;
    
    [Header("音量设置")]
    [Range(0f, 1f)]
    public float volume = 1f;
    
    [Header("AudioSource设置")]
    public bool useGlobalAudioSource = true;
    public bool enableDebugLog = true;
    
    private Button button;
    private AudioSource localAudioSource;
    private static AudioSource globalAudioSource;
    
    void Awake()
    {
        // 在Awake中初始化，确保早于Start执行
        button = GetComponent<Button>();
        
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: 在 {gameObject.name} 上初始化");
    }
    
    void Start()
    {
        // 设置AudioSource
        SetupAudioSource();
        
        // 添加点击事件监听
        SetupButtonListener();
        
        // 验证设置
        ValidateSetup();
    }
    
    private void SetupAudioSource()
    {
        if (useGlobalAudioSource)
        {
            if (globalAudioSource == null)
            {
                CreateGlobalAudioSource();
            }
        }
        else
        {
            // 使用本地AudioSource
            localAudioSource = GetComponent<AudioSource>();
            if (localAudioSource == null)
            {
                localAudioSource = gameObject.AddComponent<AudioSource>();
            }
            
            localAudioSource.playOnAwake = false;
            localAudioSource.spatialBlend = 0f; // 2D音效
        }
    }
    
    private void SetupButtonListener()
    {
        bool listenerAdded = false;
        
        // 尝试添加Unity Button组件的监听器
        if (button != null)
        {
            // 移除可能存在的重复监听器
            button.onClick.RemoveListener(PlayClickSound);
            // 添加新的监听器
            button.onClick.AddListener(PlayClickSound);
            listenerAdded = true;
            
            if (enableDebugLog)
                Debug.Log($"SimpleButtonSound: 已为 {gameObject.name} 添加Button onClick监听器");
        }
        
        // 检查是否有Collider组件（用于OnMouseDown事件）
        Collider2D collider2D = GetComponent<Collider2D>();
        Collider collider3D = GetComponent<Collider>();
        
        if (collider2D != null || collider3D != null)
        {
            // 如果有Collider，说明可能使用OnMouseDown方式
            // 我们需要添加一个监听OnMouseDown事件的组件
            MouseClickSoundHelper helper = GetComponent<MouseClickSoundHelper>();
            if (helper == null)
            {
                helper = gameObject.AddComponent<MouseClickSoundHelper>();
            }
            helper.Initialize(this);
            listenerAdded = true;
            
            if (enableDebugLog)
                Debug.Log($"SimpleButtonSound: 已为 {gameObject.name} 添加OnMouseDown监听器");
        }
        
        if (!listenerAdded)
        {
            Debug.LogWarning($"SimpleButtonSound: 在 {gameObject.name} 上既没有Button组件也没有Collider组件，无法添加监听器！");
        }
    }
    
    private void CreateGlobalAudioSource()
    {
        GameObject audioObject = new GameObject("GlobalButtonAudioSource");
        globalAudioSource = audioObject.AddComponent<AudioSource>();
        globalAudioSource.playOnAwake = false;
        globalAudioSource.spatialBlend = 0f; // 2D音效
        globalAudioSource.volume = 1f;
        DontDestroyOnLoad(audioObject);
        
        if (enableDebugLog)
            Debug.Log("SimpleButtonSound: 全局AudioSource创建成功");
    }
    
    private void ValidateSetup()
    {
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: 验证 {gameObject.name} 的设置...");
            
        if (clickSound == null)
        {
            Debug.LogWarning($"SimpleButtonSound: {gameObject.name} 没有设置clickSound！请在Inspector中拖拽音效文件。");
        }
        else
        {
            if (enableDebugLog)
                Debug.Log($"SimpleButtonSound: ✓ 音效文件已设置: {clickSound.name}");
        }
        
        // 检查AudioListener
        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            Debug.LogError("SimpleButtonSound: 场景中没有AudioListener！请在Main Camera上添加AudioListener组件。");
        }
        else
        {
            if (enableDebugLog)
                Debug.Log($"SimpleButtonSound: ✓ 找到AudioListener: {listener.gameObject.name}");
        }
        
        // 检查AudioSource
        AudioSource currentAudioSource = useGlobalAudioSource ? globalAudioSource : localAudioSource;
        if (currentAudioSource == null)
        {
            Debug.LogError($"SimpleButtonSound: {gameObject.name} AudioSource设置失败！");
        }
        else
        {
            if (enableDebugLog)
                Debug.Log($"SimpleButtonSound: ✓ AudioSource已设置，类型: {(useGlobalAudioSource ? "全局" : "本地")}");
        }
        
        // 检查音量设置
        if (volume <= 0)
        {
            Debug.LogWarning($"SimpleButtonSound: {gameObject.name} 音量设置为 {volume}，可能听不到声音！");
        }
    }
    
    private void PlayClickSound()
    {
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: PlayClickSound被调用 - {gameObject.name}");
            
        if (clickSound == null)
        {
            Debug.LogWarning($"SimpleButtonSound: {gameObject.name} 没有音效文件，无法播放");
            return;
        }
        
        // 检查AudioListener是否存在
        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            Debug.LogError("SimpleButtonSound: 场景中没有AudioListener，无法播放音效！请在Main Camera上添加AudioListener组件。");
            return;
        }
        
        try
        {
            bool playSuccess = false;
            
            if (useGlobalAudioSource && globalAudioSource != null)
            {
                globalAudioSource.PlayOneShot(clickSound, volume);
                playSuccess = true;
                if (enableDebugLog)
                    Debug.Log($"SimpleButtonSound: ✓ 通过全局AudioSource播放 {clickSound.name}，音量: {volume}");
            }
            else if (!useGlobalAudioSource && localAudioSource != null)
            {
                localAudioSource.PlayOneShot(clickSound, volume);
                playSuccess = true;
                if (enableDebugLog)
                    Debug.Log($"SimpleButtonSound: ✓ 通过本地AudioSource播放 {clickSound.name}，音量: {volume}");
            }
            else
            {
                // 备用方案：使用Unity的静态方法
                Vector3 playPosition = Camera.main ? Camera.main.transform.position : Vector3.zero;
                AudioSource.PlayClipAtPoint(clickSound, playPosition, volume);
                playSuccess = true;
                if (enableDebugLog)
                    Debug.Log($"SimpleButtonSound: ✓ 通过PlayClipAtPoint播放 {clickSound.name}，音量: {volume}，位置: {playPosition}");
            }
            
            if (!playSuccess)
            {
                Debug.LogError($"SimpleButtonSound: 没有可用的AudioSource来播放音效！");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SimpleButtonSound: 播放音效时出错 - {e.Message}");
        }
    }
    
    // 公共方法，可以在Inspector或其他脚本中调用
    public void PlaySound()
    {
        PlayClickSound();
    }
    
    // 手动测试方法
    [ContextMenu("测试播放音效")]
    public void TestPlaySound()
    {
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: 手动测试播放音效 - {gameObject.name}");
        PlayClickSound();
    }
    
    // 动态设置音效
    public void SetSound(AudioClip newSound)
    {
        clickSound = newSound;
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: 音效已更改为 {(newSound ? newSound.name : "null")}");
    }
    
    // 动态设置音量
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (enableDebugLog)
            Debug.Log($"SimpleButtonSound: 音量已设置为 {volume}");
    }
    
    // 重新初始化（用于运行时添加组件的情况）
    public void Reinitialize()
    {
        SetupAudioSource();
        SetupButtonListener();
        ValidateSetup();
    }
    
    void OnDestroy()
    {
        // 清理事件监听
        if (button != null)
        {
            button.onClick.RemoveListener(PlayClickSound);
        }
    }
    
    // 调试信息
    [ContextMenu("打印调试信息")]
    public void PrintDebugInfo()
    {
        Debug.Log($"=== {gameObject.name} 调试信息 ===");
        Debug.Log($"Button组件: {(button != null ? "存在" : "缺失")}");
        Debug.Log($"音效文件: {(clickSound != null ? clickSound.name : "未设置")}");
        Debug.Log($"音量: {volume}");
        Debug.Log($"使用全局AudioSource: {useGlobalAudioSource}");
        Debug.Log($"全局AudioSource: {(globalAudioSource != null ? "存在" : "缺失")}");
        Debug.Log($"本地AudioSource: {(localAudioSource != null ? "存在" : "缺失")}");
        Debug.Log($"Button可交互: {(button != null ? button.interactable.ToString() : "N/A")}");
    }
}

// 辅助类，用于处理OnMouseDown事件
public class MouseClickSoundHelper : MonoBehaviour
{
    private SimpleButtonSound soundScript;
    
    public void Initialize(SimpleButtonSound script)
    {
        soundScript = script;
    }
    
    void OnMouseDown()
    {
        if (soundScript != null)
        {
            soundScript.PlaySound();
            if (soundScript.enableDebugLog)
                Debug.Log($"MouseClickSoundHelper: 通过OnMouseDown触发音效 - {gameObject.name}");
        }
    }
}