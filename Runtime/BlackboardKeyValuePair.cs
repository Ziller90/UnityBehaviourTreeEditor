using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class BlackboardKeyValuePair
    {
        [SerializeReference] public BlackboardKey key;
        [SerializeReference] public BlackboardKey value;

        public void WriteValue()
        {
            if (key != null && value != null)
                key.CopyFrom(value);
        }
    }
}
