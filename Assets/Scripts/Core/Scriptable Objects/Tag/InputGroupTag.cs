using UnityEngine;

/// <summary>
/// See this as a typed and dynamic enum for input actions
/// Create this, set a descriptive name, and now you have a new an enum value
/// Example: keyCode W, A, S, D can have InputGroupTags named Movement and Player Action.
/// While keyCode mouse0 can have InputGroupTags named Shooter and Player Action, same as W, A, S, D
/// </summary>
[CreateAssetMenu(menuName = "Tags/Input Group Tag")]
public class InputGroupTag : ScriptableObject { }