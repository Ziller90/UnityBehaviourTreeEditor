using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CompareKeys : ActionNode
{
    public BlackboardKeyKeyPair pair;

    protected override State OnUpdate()
    {
        BlackboardKey key1 = pair.key1;
        BlackboardKey key2 = pair.key2;

        if (key2.Equals(key1))
        {
            Debug.Log("Equals");
            return State.Success;
        }
        Debug.Log("NotEquals");
        return State.Failure;
    }
}
