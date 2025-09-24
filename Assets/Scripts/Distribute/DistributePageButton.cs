using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Distribute
{
    public class DistributePageButton : MonoBehaviour
    {
        [Header("应用页面")]
        public GameObject appPage;
        
        [Header("结算页面")]
        public GameObject endPage;
        
        public Button createMessageButton;
        

        public GameObject Parentobject;

        [Header("信息条目预制体")] public GameObject messagePrefab;

        [Header("信息条目间距")] public float spacing;

        [Header("信息条目起始位置")] public Vector3 startPosition;

        public Transform contentContainer;

        [Header("淡出时间")]
        public float fadeDurTime;
        
        public int index;

        private void Awake()
        {
            index = 0;
            endPage.SetActive(false);
            createMessageButton.onClick.AddListener(CreateNewMessage);
        }

        public void CreateNewMessage()
        {
            //Instantiate(messagePrefab, contentContainer);
            GameObject NewMessageBar= Instantiate(messagePrefab);
            DistributeControlMono distributeControlMono = gameObject.GetComponent<DistributeControlMono>();
            distributeControlMono.DistributeBars.Add(NewMessageBar);
            NewMessageBar.transform.position = new Vector3(0, 0, 0);
            NewMessageBar.transform.parent = Parentobject.transform;
            index++;
        }
        
        public void ClosePage()
        {
            appPage.SetActive(false);
        }

        public void CommitPlan()
        {
            endPage.SetActive(true);
            StartCoroutine(FadeOut(endPage.GetComponent<Image>()));
        }

        private IEnumerator FadeOut(Image image)
        {
            if (!image)
            {
                yield break;
            }
            
            Color originalColor = new Color(0, 0, 0, 0);
            Color targetColor = Color.black;

            image.color = originalColor;
            float timer = 0f;
            // 淡出过程
            while (timer < fadeDurTime)
            {
                timer += Time.deltaTime;
                float progress = Mathf.Clamp01(timer / fadeDurTime);
                image.color = Color.Lerp(originalColor, targetColor, progress);
                yield return null;
            }
            image.color = targetColor;
            DestroyAllContent();
        }

        private void DestroyAllContent()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }
            index = 0;
        }

    }
}