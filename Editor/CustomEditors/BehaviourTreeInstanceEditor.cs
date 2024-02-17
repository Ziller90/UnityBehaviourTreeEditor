using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace TheKiwiCoder {
    [CustomEditor(typeof(BehaviourTreeInstanceBase))]
    public class BehaviourTreeInstanceEditor : Editor {

        public override VisualElement CreateInspectorGUI() {

            VisualElement container = new VisualElement();

            PropertyField treeField = new PropertyField();
            treeField.bindingPath = nameof(BehaviourTreeInstanceBase.behaviourTree);

            PropertyField validateField = new PropertyField();
            validateField.bindingPath = nameof(BehaviourTreeInstanceBase.validate);

            PropertyField publicKeys = new PropertyField();
            publicKeys.bindingPath = nameof(BehaviourTreeInstanceBase.blackboardOverrides);

            container.Add(treeField);
            container.Add(validateField);
            container.Add(publicKeys);

            return container;
        }
    }
}
