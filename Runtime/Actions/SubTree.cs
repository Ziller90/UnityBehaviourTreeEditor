using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class SubTree : ActionNode
    {
        [Tooltip("Behaviour tree asset to run as a subtree")]
        public NodeProperty<BehaviourTree> treeAsset = new NodeProperty<BehaviourTree>();
        [HideInInspector] public BehaviourTree treeInstance;

        public override void OnInit()
        {
            if (treeAsset.Value)
            {
                treeInstance = treeAsset.Value.Clone();
                treeInstance.Bind(context);
            }
        }

        protected override void OnStart()
        {
            if (treeInstance)
                treeInstance.treeState = Node.State.Running;
        }

        protected override State OnUpdate()
        {
            if (treeInstance)
                return treeInstance.Update();

            return State.Failure;
        }
    }
}
