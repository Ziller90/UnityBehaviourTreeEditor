using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class BlackboardKeyKeyPair
    {
        [SerializeReference] public BlackboardKey key1;
        [SerializeReference] public BlackboardKey key2;

        public void WriteValue()
        {
            if (key1 != null && key2 != null)
                key1.CopyFrom(key2);
        }
    }
}
