using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class SetKey : ActionNode
    {
        public BlackboardKeyKeyPair pair;

        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            pair.WriteValue();

            return State.Success;
        }
    }
}
