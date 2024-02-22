using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class UpdateTimers : ActionNode
{
    [SerializeField] NodeProperty<float> timer = new NodeProperty<float>();

    protected override State OnUpdate()
    {
        if (timer.Value > 0)
            timer.Value -= Time.deltaTime;
        else
            timer.Value = 0;

        state = State.Success;
        return state;
    }
}
