using UnityEngine;

namespace Common
{
    [CreateAssetMenu(menuName = "MessageObj")]
    public class Message : ScriptableObject
    {
        /**
         * 是对话选择
         */
        public bool isDialogueSelect;
        
        /**
        * 电话号码
         */
        public int phoneNumber;
        
        /**
         * 对话内容
         */
        public TextAsset dialogueFile;

        /**
         * 对话头像
         */
        public Sprite headImage;
        
        /**
         * 名字
         */
        public string personName;
        
        /**
         * 跳转行数
         */
        public int turnRaw;
    }
}