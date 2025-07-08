using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class Selector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = 0; 
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                switch (child.Update())
                {
                    case State.Running:

                        if (current != i && children[current].state == State.Running)
                        {
                            children[current].Abort();
                        }

                        current = i;
                        return State.Running;

                    case State.Success:
                        if (current != i && children[current].state == State.Running)
                        {
                            children[current].Abort();
                        }

                        current = 0;
                        return State.Success;

                    case State.Failure:
                        continue;
                }
            }

            current = 0;
            return State.Failure;
        }
    }
}