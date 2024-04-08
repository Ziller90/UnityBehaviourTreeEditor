using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace TheKiwiCoder
{

    [System.Serializable]
    public class CompareProperty : ActionNode
    {
        public BlackboardKeyValuePair pair;

        protected override State OnUpdate()
        {
            BlackboardKey blackBoardKey = pair.key;
            BlackboardKey value = pair.value;

            if (value.Equals(blackBoardKey))
            {
                return State.Success;
            }
            return State.Failure;
        }
    }
}
