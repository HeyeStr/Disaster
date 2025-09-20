using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public PlayerInputManager playerInput;

        [SerializeField] TMP_Text textLabel;

        [SerializeField] Image dialogueImage;

        [SerializeField] TextAsset textFile;

        [SerializeField] int index;

        [SerializeField] float textSpeed;

        [SerializeField] private string branchSign = "[OPTION]";

        [SerializeField] private string branchEnd = "[/OPTION]";
        
        [SerializeField] private string dialogueEnd = "[END]";

        [SerializeField] private string playerName;
        
        [SerializeField] private string personName;
        
        [SerializeField] private GameObject optionPrefab;

        [SerializeField] private Transform optionContainer;
        
        [SerializeField] private GameObject branchPanel;

        private List<string> textList = new List<string>();

        public bool textFinished;

        public bool cancelTyping;

        public bool isInBranchSelection;
        
        void Awake()
        {
            InitTextList(textFile);
        }

        private void OnEnable()
        {
            textFinished = true;
            cancelTyping = false;
            isInBranchSelection = false;
            HideBranchPanel();
            ClearBranchOptions();
            isInBranchSelection = false;
            StartCoroutine(FillTextWordByWord());
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInput.ClickDialogue && !isInBranchSelection)
            {
                DialogueInput();
            }
        }
        
        // 当对话框禁用时清理
        private void OnDisable()
        {
            HideBranchPanel();
            ClearBranchOptions();
            isInBranchSelection = false;
        }

        void InitTextList(TextAsset text)
        {
            textList.Clear();
            index = 0;
            textList.AddRange(text.text.Replace("\r", "").Split('\n'));
        }

        private void DialogueInput()
        {
            // 文本全部输出完，结束对话
            if (dialogueEnd.StartsWith(textList[index]) || index >= textList.Count)
            {
                gameObject.SetActive(false);
                index = 0;
                return;
            }

            if (textFinished && !cancelTyping) //一行文字输出完 并且是逐个打字状态
            {
                StartCoroutine(FillTextWordByWord());
            }
            else if (!textFinished)
            {
                // 一行文字还没输出完
                cancelTyping = !cancelTyping;
            }
        }

        IEnumerator FillTextWordByWord()
        {
            textFinished = false;
            textLabel.text = "";

            // 检查是否需要切换对话
            SwitchDialogue();

            string currentText = textList[index];
            // 逐个显示一行文字
            foreach (var single in currentText)
            {
                // 中断逐个打字
                if (cancelTyping)
                {
                    textLabel.text = currentText;
                    cancelTyping = false;
                    break;
                }

                textLabel.text += single;
                yield return new WaitForSeconds(textSpeed);
            }

            textFinished = true;
            index++;
            
            if (index < textList.Count && textList[index].StartsWith(branchSign))
            {
                yield return null; // 延迟一帧
                ParseAndShowBranches();
            }
        }

        public void SwitchDialogue()
        {
            switch (textList[index])
            {
                case "A":
                    index++;
                    break;
                case "B":
                    index++;
                    break;
            }
        }

        public void ParseAndShowBranches()
        {
            isInBranchSelection = true;
            // 清空现有的选项
            foreach (Transform child in optionContainer)
            {
                Destroy(child.gameObject);
            }

            int currentIndex = index + 1;
            while (currentIndex < textList.Count &&
                   !textList[currentIndex].Equals(branchEnd, StringComparison.OrdinalIgnoreCase))
            {
                string line = textList[currentIndex];
                string[] parts = line.Split("|");
                if (parts.Length >= 2)
                {
                    if (int.TryParse(parts[0].Trim(), out int jumpToIndex))
                    {
                        string optionText = parts[1].Trim();
                        GameObject option = Instantiate(optionPrefab, optionContainer);
                        option.GetComponent<TMP_Text>().text = optionText;
                        Button optionButton = option.GetComponent<Button>();
                        optionButton.onClick.RemoveAllListeners();
                        optionButton.onClick.AddListener(() => OnBranchSelected(jumpToIndex - 1));
                    }
                }

                currentIndex++;

                // 显示分支面板
            }
            ShowBranchPanel();
            index = currentIndex + 1;
        }

        private void OnBranchSelected(int targetIndex)
        {
            // 隐藏分支面板
            HideBranchPanel();
            isInBranchSelection = false;

            // 跳转到目标行数
            if (targetIndex >= 0 && targetIndex < textList.Count)
            {
                index = targetIndex;

                // 继续显示对话
                DialogueInput();
                Debug.Log(index);
            }
            else
            {
                Debug.LogError($"无效的目标行数: {targetIndex}");
                gameObject.SetActive(false);
            }
        }

        private void ShowBranchPanel()
        {
            branchPanel?.SetActive(true);
            optionContainer.gameObject.SetActive(true);
        }

        private void HideBranchPanel()
        {
            branchPanel?.SetActive(false);
            optionContainer.gameObject.SetActive(false);
        }

        /**
        * 清空分支选项
        */
        private void ClearBranchOptions()
        {
            foreach (Transform child in optionContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}