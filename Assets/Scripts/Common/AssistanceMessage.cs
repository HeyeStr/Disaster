using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class AssistanceMessage : MonoBehaviour
    {
        /**
         * 电话号码
         */
        public string phoneNumber;
        
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