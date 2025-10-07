using UnityEngine;

public class GameExit : MonoBehaviour
{
    void Update()
    {
        // 按ESC键退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // 在编辑器中退出播放模式
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 在打包版本中退出应用程序
            Application.Quit();
#endif
    }

    void OnMouseDown()
    {
        ExitGame();
    }
}