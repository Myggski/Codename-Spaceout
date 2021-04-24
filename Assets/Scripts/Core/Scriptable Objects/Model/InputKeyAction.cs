using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is for performance purposes, with the Object pooler
/// </summary>
[CreateAssetMenu(menuName = "Models/InputKeyAction")]
public class InputKeyAction : ScriptableObject
{
  public List<InputGroupTag> InputGroupTags;
  public GameEvent GameEvent;
}

