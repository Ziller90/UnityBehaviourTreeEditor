using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace TheKiwiCoder
{
    [CustomPropertyDrawer(typeof(BlackboardKeyKeyPair))]
    public class BlackboardKeyKeyPairPropertyDrawer : PropertyDrawer
    {
        VisualElement pairContainer;
        SerializedProperty key1Property;
        SerializedProperty key2Property;

        BehaviourTree GetBehaviourTree(SerializedProperty property)
        {
            if (property.serializedObject.targetObject is BehaviourTree tree)
            {
                return tree;
            }
            else if (property.serializedObject.targetObject is BehaviourTreeInstance instance)
            {
                return instance.RuntimeTree;
            }
            Debug.LogError("Could not find behaviour tree this is referencing");
            return null;
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            key1Property = property.FindPropertyRelative(nameof(BlackboardKeyKeyPair.key1));
            key2Property = property.FindPropertyRelative(nameof(BlackboardKeyKeyPair.key2));

            pairContainer = new VisualElement();
            var Key1Dropdown = CreateDropdown(key1Property, "Key 1");
            var Key2Dropdown = CreateDropdown(key2Property, "Key 2");
            pairContainer.Add(Key1Dropdown);
            pairContainer.Add(Key2Dropdown);
            return pairContainer;
        }

        private PopupField<BlackboardKey> CreateDropdown(SerializedProperty property, string label, System.Type typeRestriction = null)
        {
            PopupField<BlackboardKey> dropdown = new PopupField<BlackboardKey>(label);
            dropdown.formatListItemCallback = item => FormatItem(item);
            dropdown.formatSelectedValueCallback = item => FormatItem(item);
            dropdown.value = property.managedReferenceValue as BlackboardKey;

            BehaviourTree tree = GetBehaviourTree(property);

            dropdown.RegisterCallback<MouseEnterEvent>((evt) => UpdateDropdownChoices(dropdown, tree, typeRestriction));
            dropdown.RegisterCallback<ChangeEvent<BlackboardKey>>(evt => OnDropdownChange(evt, property, dropdown));

            return dropdown;
        }

        private void UpdateDropdownChoices(PopupField<BlackboardKey> dropdown, BehaviourTree tree, System.Type typeRestriction = null)
        {
            dropdown.choices.Clear();
            foreach (var key in tree.blackboard.keys)
            {
                if (typeRestriction == null)
                    dropdown.choices.Add(key);
                else if (key.underlyingType == typeRestriction)
                    dropdown.choices.Add(key);
            }
        }

        private void OnDropdownChange(ChangeEvent<BlackboardKey> evt, SerializedProperty property, PopupField<BlackboardKey> dropdown)
        {
            BlackboardKey newKey = evt.newValue;
            property.managedReferenceValue = newKey;
            property.serializedObject.ApplyModifiedProperties();

            if (dropdown.label == "Key 1")
            {
                key2Property = property.serializedObject.FindProperty(property.propertyPath.Replace("key1", "key2"));
                PopupField<BlackboardKey> secondDropdown = CreateDropdown(key2Property, "Key 2", newKey.underlyingType);

                var nullKeyOfKey1Type = BlackboardKey.CreateKey(newKey.GetType());
                key2Property.managedReferenceValue = nullKeyOfKey1Type;
                key2Property.serializedObject.ApplyModifiedProperties();
                secondDropdown.SetValueWithoutNotify(nullKeyOfKey1Type);

                if (pairContainer.childCount > 1)
                    pairContainer.RemoveAt(1); // Remove existing second dropdown if any
                pairContainer.Add(secondDropdown);
            }
        }

        private string FormatItem(BlackboardKey item)
        {
            return item == null || item.name == "" ? "(null)" : item.name;
        }
    }
}
