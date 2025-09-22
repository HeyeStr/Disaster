using System.Collections.Generic;
using UnityEngine;

namespace SettingSo
{
    [System.Serializable]
    public class StringPair
    {
        public string key;
        public int value;
        
        public StringPair(string key, int value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [CreateAssetMenu(menuName = "CustomDialogueTurnMap")]
    public class DialogueTurnMapSo : ScriptableObject
    {
        [SerializeField] private List<StringPair> mappings = new List<StringPair>();
        
        public Dictionary<string, int> Mapping
        {
            get
            {
                var dict = new Dictionary<string, int>();
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
        
        public void AddMapping(string key, int value)
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
        
        public int GetValue(string key)
        {
            var pair = mappings.Find(p => p.key == key);
            return pair.value;
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