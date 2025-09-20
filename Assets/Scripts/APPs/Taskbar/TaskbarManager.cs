using System.Collections.Generic;
using APPs.AppBasic;
using UnityEngine;

namespace APPs.Taskbar
{
    public class TaskbarManager : MonoBehaviour
    {
        public static TaskbarManager Instance;

        [Header("任务栏")] 
        public Transform taskbarContent; // 任务栏容器
        
        private Dictionary<string, Transform> taskbarItems = new Dictionary<string, Transform>();

        void Awake()
        {
            Instance = this;
        }
        
        // 添加应用
        public void AddAppToTaskbar(AppBaseItem appBaseItem)
        {
            if (taskbarItems.ContainsKey(appBaseItem.appName))
            {
                // TODO: 设置页面激活
            }
            else
            {
                GameObject itemObj = Instantiate(appBaseItem.taskPrefab, taskbarContent);
                taskbarItems.Add(appBaseItem.appName, itemObj.transform);
            }
        }

        public void MiniAppItem(GameObject obj)
        {
            obj.SetActive(false);
        }

        // 从任务栏移除应用
        public void RemoveAppFromTaskbar(string appId)
        {
            if (taskbarItems.ContainsKey(appId))
            {
                Destroy(taskbarItems[appId].gameObject);
                taskbarItems.Remove(appId);
            }
        }
    }
}