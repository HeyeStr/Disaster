using Common;

namespace SettingSo
{
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
    [System.Serializable]
    public class StringPair
    {
        public string key;
        public Message value;
        
        public StringPair(string key, Message value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [CreateAssetMenu(menuName = "CustomMessageMap")]
    public class CustomMessageSo : ScriptableObject
    {
        [SerializeField] private List<StringPair> mappings = new List<StringPair>();
        
        public Dictionary<string, Message> Mapping
        {
            get
            {
                var dict = new Dictionary<string, Message>();
                foreach (var pair in mappings)
                {
                    if (!string.IsNullOrEmpty(pair.key))
                    {
                        dict[pair.key] = pair.value;
                    }
                }
                return dict;
            }
        }
        
        public void AddMapping(string key, Message value)
        {
            var existing = mappings.Find(p => p.key == key);
            if (existing != null)
            {
                existing.value = value;
            }
            else
            {
                mappings.Add(new StringPair(key, value));
            }
        }
        
        public Message GetValue(string key)
        {
            var pair = mappings.Find(p => p.key == key);
            return pair?.value;
        }
        
        public bool ContainsKey(string key)
        {
            return mappings.Exists(p => p.key == key);
        }
        
        public void Clear()
        {
            mappings.Clear();
        }
    }
}
}