using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class Blackboard
    {
        [SerializeReference]
        public List<BlackboardKey> keys = new List<BlackboardKey>();

        public BlackboardKey Find(string keyName)
        {
            return keys.Find((key) =>
            {
                return key.name == keyName;
            });
        }

        public BlackboardKey<T> Find<T>(string keyName)
        {
            var foundKey = Find(keyName);

            if (foundKey == null)
            {
                Debug.LogWarning($"Failed to find blackboard key, invalid keyname:{keyName}");
                return null;
            }

            if (foundKey.underlyingType != typeof(T))
            {
                Debug.LogWarning($"Failed to find blackboard key, invalid keytype:{typeof(T)}, Expected:{foundKey.underlyingType}");
                return null;
            }

            var foundKeyTyped = foundKey as BlackboardKey<T>;
            if (foundKeyTyped == null)
            {
                Debug.LogWarning($"Failed to find blackboard key, casting failed:{typeof(T)}, Expected:{foundKey.underlyingType}");
                return null;
            }
            return foundKeyTyped;
        }

        public void SetValue<T>(string keyName, T value)
        {
            BlackboardKey<T> key = Find<T>(keyName);
            if (key != null)
            {
                key.value = value;
            }
        }

        public T GetValue<T>(string keyName)
        {
            BlackboardKey<T> key = Find<T>(keyName);
            if (key != null)
            {
                return key.value;
            }
            return default(T);
        }
    }
}