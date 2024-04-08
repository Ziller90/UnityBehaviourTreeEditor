using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace TheKiwiCoder
{
    [CustomEditor(typeof(BehaviourTreeInstanceBase))]
    public class BehaviourTreeInstanceEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement container = new VisualElement();

            PropertyField treeField = new PropertyField();
            treeField.bindingPath = nameof(BehaviourTreeInstanceBase.behaviourTree);

            PropertyField validateField = new PropertyField();
            validateField.bindingPath = nameof(BehaviourTreeInstanceBase.validate);

            PropertyField blackboardOverrides = new PropertyField();
            blackboardOverrides.bindingPath = nameof(BehaviourTreeInstanceBase.blackboardOverrides);

            PropertyField subTreesblackboardOverrides = new PropertyField();
            subTreesblackboardOverrides.bindingPath = nameof(BehaviourTreeInstanceBase.subTreesBlackboardOverrides);

            container.Add(treeField);
            container.Add(validateField);
            container.Add(blackboardOverrides);
            container.Add(subTreesblackboardOverrides);

            return container;
        }
    }
}
