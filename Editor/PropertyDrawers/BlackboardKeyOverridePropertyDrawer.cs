using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace TheKiwiCoder
{
    [CustomPropertyDrawer(typeof(BlackboardKeyOverride))]
    public class BlackboardOverridePropertyDrawer : PropertyDrawer
    {
        VisualElement pairContainer;

        BehaviourTree GetBehaviourTree(SerializedProperty property)
        {
            if (property.serializedObject.targetObject is BehaviourTreeInstance instance)
            {
                string propertyPath = property.propertyPath;
                if (propertyPath.StartsWith("blackboardOverrides"))
                {
                    return instance.RuntimeTree;
                }
                else if (propertyPath.StartsWith("subTreesBlackboardOverrides"))
                {
                    string[] pathParts = propertyPath.Split('.');
                    if (pathParts.Length > 1)
                    {
                        string listIndexStr = pathParts[2][5].ToString();
                        if (int.TryParse(listIndexStr, out int listIndex))
                        {
                            var subTreeOverride = instance.subTreesBlackboardOverrides[listIndex];
                            return subTreeOverride.behaviourTree;
                        }
                    }
                }
            }

            Debug.LogError("Could not find behaviour tree this is referencing");
            return null;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty first = property.FindPropertyRelative(nameof(BlackboardKeyOverride.key));
            SerializedProperty second = property.FindPropertyRelative(nameof(BlackboardKeyOverride.value));

            PopupField<BlackboardKey> dropdown = new PopupField<BlackboardKey>();
            dropdown.label = first.displayName;
            dropdown.formatListItemCallback = FormatItem;
            dropdown.formatSelectedValueCallback = FormatItem;
            dropdown.value = first.managedReferenceValue as BlackboardKey;

            BehaviourTree tree = GetBehaviourTree(property);
            dropdown.RegisterCallback<MouseEnterEvent>((evt) =>
            {
                dropdown.choices.Clear();
                foreach (var key in tree.blackboard.keys)
                {
                    dropdown.choices.Add(key);
                }
            });

            dropdown.RegisterCallback<ChangeEvent<BlackboardKey>>((evt) =>
            {
                BlackboardKey newKey = evt.newValue;
                first.managedReferenceValue = newKey;
                property.serializedObject.ApplyModifiedProperties();

                if (pairContainer.childCount > 1)
                {
                    pairContainer.RemoveAt(1);
                }

                if (second.managedReferenceValue == null || second.managedReferenceValue.GetType() != dropdown.value.GetType())
                {
                    second.managedReferenceValue = BlackboardKey.CreateKey(dropdown.value.GetType());
                    second.serializedObject.ApplyModifiedProperties();
                }
                PropertyField field = new PropertyField();
                field.label = second.displayName;
                field.BindProperty(second.FindPropertyRelative(nameof(BlackboardKey<object>.value)));
                pairContainer.Add(field);
            });

            pairContainer = new VisualElement();
            pairContainer.Add(dropdown);

            if (dropdown.value != null)
            {
                if (second.managedReferenceValue == null || first.managedReferenceValue.GetType() != second.managedReferenceValue.GetType())
                {
                    second.managedReferenceValue = BlackboardKey.CreateKey(dropdown.value.GetType());
                    second.serializedObject.ApplyModifiedProperties();
                }

                PropertyField field = new PropertyField();
                field.label = second.displayName;
                field.bindingPath = nameof(BlackboardKey<object>.value);
                pairContainer.Add(field);
            }

            return pairContainer;
        }

        private string FormatItem(BlackboardKey item)
        {
            if (item == null)
            {
                return "(null)";
            }
            else
            {
                return item.name;
            }
        }
    }
}
