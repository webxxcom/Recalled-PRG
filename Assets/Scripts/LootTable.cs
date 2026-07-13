using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


# if UNITY_EDITOR

[CustomPropertyDrawer(typeof(LootTable))]
public class LootTableDrawer : PropertyDrawer
{
    private const float Padding = 3f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty lootsProperty = property.FindPropertyRelative("_loots");
        float totalWeight = ComputeTotalWeight(lootsProperty);

        Rect foldoutRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        lootsProperty.isExpanded = EditorGUI.Foldout(foldoutRect, lootsProperty.isExpanded, label);

        if (lootsProperty.isExpanded)
        {
            float y = position.y + EditorGUIUtility.singleLineHeight + Padding;

            Rect sizeRect = new(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(sizeRect, lootsProperty.FindPropertyRelative("Array.size"), new GUIContent("Count"));
            y += EditorGUIUtility.singleLineHeight + Padding;

            for (int i = 0; i < lootsProperty.arraySize; i++)
            {
                SerializedProperty entry = lootsProperty.GetArrayElementAtIndex(i);
                SerializedProperty itemProp = entry.FindPropertyRelative("_item");
                SerializedProperty weightProp = entry.FindPropertyRelative("_weight");

                float pct = totalWeight > 0 ? (weightProp.intValue / totalWeight) * 100f : 0f;

                float lineH = EditorGUIUtility.singleLineHeight;
                float w = position.width;
                float x = position.x;

                Rect itemRect = new(x, y, w * 0.5f, lineH);
                Rect weightRect = new(x + w * 0.5f + Padding, y, w * 0.25f, lineH);
                Rect pctRect = new(x + w * 0.75f + Padding, y, w * 0.25f, lineH);

                EditorGUI.PropertyField(itemRect, itemProp, GUIContent.none);
                EditorGUI.PropertyField(weightRect, weightProp, GUIContent.none);
                EditorGUI.LabelField(pctRect, $"{pct:F1}%");

                y += lineH + Padding;
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty lootsProperty = property.FindPropertyRelative("_loots");
        float lineH = EditorGUIUtility.singleLineHeight;

        if (!lootsProperty.isExpanded)
            return lineH;

        return lineH + Padding                                        // foldout
             + lineH + Padding                                        // count field
             + lootsProperty.arraySize * (lineH + Padding);          // entries
    }

    private float ComputeTotalWeight(SerializedProperty lootsProperty)
    {
        float total = 0f;
        for (int i = 0; i < lootsProperty.arraySize; i++)
            total += lootsProperty.GetArrayElementAtIndex(i)
                                  .FindPropertyRelative("_weight").intValue;
        return total;
    }
}

#endif

[System.Serializable]
public class LootTable
{
    [System.Serializable]
    public class LootItem
    {
        [SerializeField] ItemDefinition _item;
        [SerializeField] int _weight;

        public int GetWeight() => _weight;
        public ItemDefinition GetItemDefinition() => _item;
    }

    [SerializeField] List<LootItem> _loots;

    int TotalWeight
    {
        get
        {
            int total = 0;
            foreach (var item in _loots)
            {
                total += item.GetWeight();
            }
            return total;
        }
    }

    public ItemDefinition GetItem()
    {
        float expectedWeight = Random.Range(0, TotalWeight);

        int totalWeight = 0;
        foreach (var item in _loots)
        {
            totalWeight += item.GetWeight();
            if (totalWeight >= expectedWeight)
                return item.GetItemDefinition();
        }
        Debug.LogError("Unexpected error while getting Loot");
        return null;
    }
}
