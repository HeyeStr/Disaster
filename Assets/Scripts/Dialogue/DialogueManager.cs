using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using EventSo;
using Manager;
using Phone;
using SettingSo;
using SettingSo.Setting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : AcceptMessage
    {
        public DialogueTurnMapSo turnMap;
        
        public CustomMessageSo messageMap;
        
        public static DialogueManager Instance;
        
        public PlayerInputManager playerInput;

        [Header("事件引用")]
        public SendMessageEventSo dialogueEvent;
        
        [SerializeField] TMP_Text textLabel;

        [SerializeField] private GameObject dialogueObj;
        
        [SerializeField] Image dialogueImage;

        [SerializeField] TextAsset textFile;

        [SerializeField] int index;

        [SerializeField] float textSpeed;

        [SerializeField] private string branchSign = "[OPTION]";

        [SerializeField] private string branchEnd = "[/OPTION]";
        
        [SerializeField] private string pauseSign = "[SELECT]";
        
        [SerializeField] private string endSign = "[END]";

        [SerializeField] private string playerName;
        
        [SerializeField] private string personName;
        
        [SerializeField] private GameObject optionPrefab;

        [SerializeField] private Transform optionContainer;
        
        [SerializeField] private GameObject branchPanel;

        private List<string> textList = new List<string>();

        public bool textFinished;

        public bool cancelTyping;

        public bool isInBranchSelection;

        public bool dialoguePause;
        
        public bool canSelect;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            dialogueObj.SetActive(false);
        }

        private void OnEnable()
        {
            dialogueEvent.Event += InitDialogue;
            
            textFinished = true;
            cancelTyping = false;
            isInBranchSelection = false;
            HideBranchPanel();
            ClearBranchOptions();
        }

        void Update()
        {
            if (isInBranchSelection)
                return;
            if (canSelect)
                return;
            if (playerInput.ClickDialogue)
            {
                DialogueInput();
            }
        }
        
        // 当对话框禁用时清理
        private void OnDisable()
        {
            dialogueEvent.Event -= InitDialogue;
            
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
            if (index >= textList.Count || endSign.StartsWith(textList[index]))
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
            // 检查是否需要切换对话
            SwitchDialogue();
            if (textList[index].StartsWith(pauseSign, StringComparison.CurrentCultureIgnoreCase))
            {
                canSelect = true;
                yield break;
            }

            if (isInBranchSelection)
                yield break;
            textLabel.text = "";
            string currentText = textList[index];
            // 逐个显示一行文字
            int letter = 0;
            while (!cancelTyping && letter < currentText.Length)
            {
                textLabel.text += currentText[letter];
                letter++;
                yield return new WaitForSeconds(textSpeed);
            } 
            // foreach (var single in currentText)
            // {
            //     // 中断逐个打字
            //     if (cancelTyping)
            //     {
            //         textLabel.text = currentText;
            //         cancelTyping = false;
            //         break;
            //     }
            //
            //     textLabel.text += single;
            //     yield return new WaitForSeconds(textSpeed);
            // }
            textLabel.text = textList[index];
            textFinished = true;
            index++;
        }

        public void SwitchDialogue()
        {
            if (textList[index].StartsWith(playerName))
            {
                index ++;
            }
            if (textList[index].StartsWith(personName))
            {
                index ++;
            }
            if (textList[index].StartsWith(branchSign, StringComparison.OrdinalIgnoreCase))
            {
                ParseAndShowBranches();
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
            
            int currentIndex = index;
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
        }

        private void OnBranchSelected(int targetIndex)
        {
            textFinished = true;
            // 隐藏分支面板
            HideBranchPanel();
            isInBranchSelection = false;

            // 跳转到目标行数
            if (targetIndex >= 0 && targetIndex < textList.Count)
            {
                index = targetIndex;

                // 继续显示对话
                DialogueInput();
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

        private void InitDialogue(string phoneNumber)
        {
            Debug.Log("开始对话");
            Message message = messageMap.GetValue(phoneNumber);
            textFile = message.dialogueFile;
            dialogueImage = message.headImage;
            personName = message.personName;
            dialogueObj.SetActive(true);
            InitTextList(textFile);
            StartCoroutine(FillTextWordByWord());
        }

        public void SwitchTargetRaw(string text)
        {
            if (canSelect)
            {
                canSelect = false;
                dialoguePause = false;
                Debug.Log("测试" + turnMap.GetValue(text));
                index = turnMap.GetValue(text) - 1;
                DialogueInput();
            }
        }

        public override void AcceptString(string message)
        {
            DialogueInput();
        }
    }
}