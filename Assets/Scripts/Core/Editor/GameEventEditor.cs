using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a button to trigger events manually, good for testing
/// </summary>
[CustomEditor(typeof(GameEvent))]
public class EventEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    GUI.enabled = Application.isPlaying;

    GameEvent e = target as GameEvent;
    if (GUILayout.Button("Call"))
      e.Call();
  }
}