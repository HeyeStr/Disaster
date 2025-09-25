using UnityEngine;

namespace Common
{
    public class ExitGameButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            ExitGame();
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
    }
}