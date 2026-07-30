using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR

[CustomEditor(typeof(GameobjectIntGameEvent))]
public class GameObjectIntGameEventRaiseButton : Editor
{
    GameObject _changer;
    int _value;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Event parameters");
        GUILayout.BeginHorizontal();

        _changer = EditorGUILayout.ObjectField(_changer, typeof(GameObject), true) as GameObject;
        _value = EditorGUILayout.IntField(_value);

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Raise"))
        {
            GameobjectIntGameEvent gameEvent = serializedObject.targetObject as GameobjectIntGameEvent;

            gameEvent.Invoke(_changer, _value);
        }
    }
}
# endif
