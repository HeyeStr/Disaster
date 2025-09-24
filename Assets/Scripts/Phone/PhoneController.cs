using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Dialogue;
using TMPro;
using UnityEngine;

namespace Phone
{
    public class PhoneController : AcceptMessage
    {
        [Header("UI引用")] public TMP_Text numberLabel;

        [Header("时间设置")] public float durTime;

        public string phoneNumber;

        public DialogueManager dialogueManager;

        public AudioSource audioSource;

        public SlideMoveComponent slideMove;

        private void Awake()
        {
            dialogueManager = GameObject.FindGameObjectWithTag("Dialogue")?.GetComponent<DialogueManager>();
            slideMove = GetComponent<SlideMoveComponent>();
            audioSource = GetComponent<AudioSource>();
            numberLabel = GetComponentInChildren<TMP_Text>();
            numberLabel.text = "";
        }

        IEnumerator CallPhoneWordByWord(string message)
        {
            numberLabel.text = "";
            foreach (var num in message)
            {
                if (slideMove.startClose)
                    yield break;
                numberLabel.text += num;
                yield return new WaitForSeconds(durTime);
            }

            phoneNumber = message;
        }

        public override void AcceptString(SendMessageButton button, string message)
        {
            string number = MatchNumbers(message);
            Debug.Log("电话号码为" + phoneNumber);
            if (!slideMove.buttonClose)
                StartCoroutine(CallPhoneWordByWord(number));
        }
        
        public string MatchNumbers(string input)
        {
            string result = "";
            // \d+ 匹配一个或多个数字
            string pattern = @"\d+";
            MatchCollection matches = Regex.Matches(input, pattern);
            Debug.Log(matches.Count);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    result += match.Value;
                }
                if (result.Length == 10)
                {
                    Debug.Log($"找到电话号码:{result}");
                    return result;
                }
            } else
            {
                Debug.Log("未找到数字");
            }
            Debug.Log(result);
            return result;
        }
    }
}