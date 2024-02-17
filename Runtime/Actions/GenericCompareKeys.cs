using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GenericCompareKeys : ActionNode
{
    public BlackboardKeyValuePair key1;
    public BlackboardKeyValuePair key2;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() 
    {
        if (key1 != null && key2 != null && key1.key.Equals(key2.key))
            return State.Success;
        else
            return State.Failure;
    }
}
