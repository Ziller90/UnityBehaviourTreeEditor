using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    [AddComponentMenu("TheKiwiCoder/BehaviourTreeInstance")]
    public class BehaviourTreeInstance : MonoBehaviour
    {
        [Tooltip("BehaviourTree asset to instantiate during Awake")]
        public BehaviourTree behaviourTree;

        BehaviourTree runtimeTree;

        public BehaviourTree RuntimeTree
        {
            get
            {
                if (runtimeTree != null)
                    return runtimeTree;
                else
                    return behaviourTree;
            }
        }

        public bool validate = true;

        public List<BlackboardKeyOverride> blackboardOverrides = new();
        public List<SubTreeBlackboardOverrides> subTreesBlackboardOverrides = new();

        Context context;

        void OnEnable()
        {
            bool isValid = ValidateTree();
            if (isValid)
            {
                context = CreateBehaviourTreeContext();
                runtimeTree = behaviourTree.Clone();
                ApplyBlackboardOverrides();
                runtimeTree.Bind(context);
            }
            else
            {
                runtimeTree = null;
            }
        }

        void ApplyBlackboardOverrides()
        {
            foreach (var pair in blackboardOverrides)
            {
                var targetKey = runtimeTree.blackboard.Find(pair.key.name);
                var sourceKey = pair.value;
                if (targetKey != null && sourceKey != null)
                    targetKey.CopyFrom(sourceKey);
            }
        }

        void Update()
        {
            if (runtimeTree)
                runtimeTree.Update();
        }

        public Context CreateBehaviourTreeContext()
        {
            return new Context(gameObject);
        }

        bool ValidateTree()
        {
            if (!behaviourTree)
            {
                Debug.LogWarning($"No BehaviourTree assigned to {name}, assign a behaviour tree in the inspector");
                return false;
            }
            return true;
        }
        
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
                return;

            if (!runtimeTree)
                return;

            BehaviourTree.Traverse(runtimeTree.rootNode, (n) =>
            {
                if (n.drawGizmos)
                    n.OnDrawGizmos();
            });
        }

        public BlackboardKey<T> FindBlackboardKey<T>(string keyName)
        {
            if (runtimeTree)
                return runtimeTree.blackboard.Find<T>(keyName);
            return null;
        }

        public void SetBlackboardValue<T>(string keyName, T value)
        {
            if (runtimeTree)
                runtimeTree.blackboard.SetValue(keyName, value);
        }

        public T GetBlackboardValue<T>(string keyName)
        {
            if (runtimeTree)
                return runtimeTree.blackboard.GetValue<T>(keyName);
            return default(T);
        }
    }

    [Serializable]
    public class SubTreeBlackboardOverrides
    {
        [Tooltip("Subtree asset")]
        public BehaviourTree behaviourTree;

        public List<BlackboardKeyOverride> blackboardOverrides = new List<BlackboardKeyOverride>();
    }
}