using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR

[CustomEditor(typeof(GameobjectGameEvent))]
public class GameObjectGameEventRaiseButton : Editor
{
    GameObject _changer;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Event parameters");
        GUILayout.BeginHorizontal();

        _changer = EditorGUILayout.ObjectField(_changer, typeof(GameObject), true) as GameObject;

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Raise"))
        {
            GameobjectGameEvent gameEvent = serializedObject.targetObject as GameobjectGameEvent;

            gameEvent.Invoke(_changer);
        }
    }
}
# endif
