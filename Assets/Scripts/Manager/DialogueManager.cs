using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] TMP_Text textLabel;
        
        [SerializeField] Image dialogueImage;
        
        [SerializeField] TextAsset textFile;
        
        [SerializeField] int index;

        [SerializeField] float textSpeed;

        private List<string> textList = new List<string>();

        public bool textFinished;

        public bool cancelTyping;
        
        public PlayerInputManager playerInput;
        // Start is called before the first frame update
        void Awake()
        {   
            InitTextList(textFile);
        }

        private void OnEnable()
        {
            textFinished = true;
            cancelTyping = false;
            StartCoroutine(FillTexWordByWord());
        }

        // Update is called once per frame
        void Update()
        {
            if (playerInput.ClickDialogue)
            {
                DialogueInput();
            }
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
            if (index >= textList.Count)
            {
                gameObject.SetActive(false);
                index = 0;
                return;
            }
            if (textFinished && !cancelTyping) //一行文字输出完 并且是逐个打字状态
            {
                StartCoroutine(FillTexWordByWord());
            } else if (!textFinished){ // 一行文字还没输出完
                cancelTyping = !cancelTyping;
            }
        }

        IEnumerator FillTexWordByWord()
        {
            textFinished = false;
            textLabel.text = "";

            switch (textList[index])
            {
                case "A" :
                    index++;
                    break;
                case "B" :
                    index++;
                    break;
            }
            foreach (var single in textList[index])
            {
                // 中断逐个打字
                if (cancelTyping)
                {
                    textLabel.text = textList[index];
                    cancelTyping = false;
                    break;
                }

                textLabel.text += single;
                yield return new WaitForSeconds(textSpeed);
            }
            textFinished = true;
            index++;
        }
    }
}
