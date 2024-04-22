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

            var subtrees = context.GameObject.GetComponent<BehaviourTreeInstance>().subTreesBlackboardOverrides;
            foreach (var subtree in subtrees)
            {
                if (subtree.behaviourTree == treeAsset.Value)
                {
                    ApplyBlackboardOverrides(treeInstance, subtree.blackboardOverrides);
                }
            }
        }

        void ApplyBlackboardOverrides(BehaviourTree tree, List<BlackboardKeyOverride> blackboardOverrides)
        {
            foreach (var pair in blackboardOverrides)
            {
                var targetKey = tree.blackboard.Find(pair.key.name);
                var sourceKey = pair.value;
                if (targetKey != null && sourceKey != null)
                    targetKey.CopyFrom(sourceKey);
            }
        }

        protected override void OnStart()
        {
            if (treeInstance)
                treeInstance.treeState = State.Running;
        }

        protected override State OnUpdate()
        {
            if (treeInstance)
                return treeInstance.Update();

            return State.Failure;
        }
    }
}
