using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [CreateAssetMenu(menuName = "MessageObj")]
    public class Message : ScriptableObject
    {
        /**
         * 对话内容
         */
        public TextAsset dialogueFile;

        /**
         * 对话头像
         */
        public Image headImage;
        
        /**
         * 名字
         */
        public string personName;
        
    }
}