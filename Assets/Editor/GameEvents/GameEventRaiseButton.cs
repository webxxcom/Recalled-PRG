using UnityEditor;
using UnityEngine;

# if UNITY_EDITOR

[CustomEditor(typeof(VoidGameEvent))]
public class GameEventRaiseButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Raise"))
        {
            VoidGameEvent gameEvent = serializedObject.targetObject as VoidGameEvent;

            //TODO
            //gameEvent.Invoke();
        }
    }
}
# endif
