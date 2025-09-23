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
        [Header("信息点击跳转映射")]
        public DialogueTurnMapSo turnMap;
        
        [Header("电话信息映射")]
        public CustomMessageSo messageMap;
        
        static DialogueManager Instance;
        
        public PlayerInputManager playerInput;

        [Header("事件引用")]
        public SendMessageEventSo dialogueEvent;
        
        [Header("笔记本对象")]
        public ToDoList toDoList;
        
        [Header("对话框组件")]
        [SerializeField] private GameObject dialogueObj;
        
        [SerializeField] Image headImage;
        
        [SerializeField] TMP_Text textLabel;
        
        [Header("玩家头像")]
        [SerializeField] Sprite playerHead;

        [Header("对话人头像")]
        [SerializeField] Sprite personHead;

        [Header("对话文本")]
        [SerializeField] TextAsset textFile;

        [SerializeField] int index;

        [SerializeField] float textSpeed;

        [Header("文本特殊关键词")]
        [SerializeField] private string branchSign = "[OPTION]";

        [SerializeField] private string branchEnd = "[/OPTION]";
        
        [SerializeField] private string pauseSign = "[SELECT]";
        
        [SerializeField] private string endSign = "[END]";

        [SerializeField] private string playerName;
        
        [SerializeField] private string personName;
        
        [Header("对话选择对话框")]
        [SerializeField] private GameObject optionPrefab;

        [SerializeField] private Transform optionContainer;
        
        [SerializeField] private GameObject branchPanel;

        private List<string> textList = new List<string>();

        [Header("对话状态")]
        [SerializeField] public bool textFinished;

        [SerializeField] public bool cancelTyping;

        [SerializeField] public bool isInBranchSelection;
        
        [SerializeField] public bool canSelect;
        
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
            else if (!textFinished && !cancelTyping)
            {
                // 一行文字还没输出完
                cancelTyping = true;
            }
        }

        IEnumerator FillTextWordByWord()
        {
            textFinished = false;
            // 检查是否需要切换对话
            SwitchDialogue();
            if (textList[index].StartsWith(pauseSign, StringComparison.CurrentCultureIgnoreCase))
            {
                if (toDoList)
                    toDoList.StarttoMove = true;
                canSelect = true;
                textFinished = true;
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
            index++;
            cancelTyping = false;
            textFinished = true;
        }

        public void SwitchDialogue()
        {
            if (textList[index].StartsWith(playerName))
            {
                if (playerHead)
                    headImage.sprite = playerHead; 
                index ++;
            }
            if (textList[index].StartsWith(personName))
            {
                if (personHead)
                    headImage.sprite = personHead;
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
            Debug.Log(JsonUtility.ToJson(messageMap));
            Debug.Log(phoneNumber);
            Message message = messageMap.GetValue(phoneNumber);
            textFile = message.dialogueFile;
            personHead = message.headImage;
            personName = message.personName;
            dialogueObj.SetActive(true);
            InitTextList(textFile);
            DialogueInput();
        }

        public void SwitchTargetRaw(string text)
        {
            if (canSelect)
            {
                canSelect = false;
                index = turnMap.GetValue(text) - 1;
                Debug.Log(JsonUtility.ToJson(turnMap));
                Debug.Log(text);
                DialogueInput();
            }
        }

        public override void AcceptString(string message)
        {
            SwitchTargetRaw(message);
        }
    }
}