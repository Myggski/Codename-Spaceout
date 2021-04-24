using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// KeyCode = What key that's being watched
/// InputKeyAction = Has a GameEvent that will trigger when it's pressed.
/// It can also contain a list of InputGroupTags
/// KeyPressType = What kind of keypress that's being watched
/// For example Hold, pressed, or released
/// </summary>
[Serializable]
public struct KeyAction
{
  public KeyCode KeyCode;
  public InputKeyAction InputKeyAction;
  public KeyPressActionType KeyPressType;
}

/// <summary>
/// KeyPressActionType is what type of keypress that's beeing watched
/// KeyHold = Triggers event if the key is held down
/// KeyDown = Triggers once, when the key is pressed down
/// KeyUp = Triggers once, when the key is released
/// </summary>
public enum KeyPressActionType
{
  KeyHold,
  KeyDown,
  KeyUp
}

/// <summary>
/// Watches if any required inputs has been pressed.
/// If any required keys is being pressed, and is not included in the disabledInputGroupTags, it will trigger the connected GameEvent.
/// </summary>
public class PlayerInputWatcher : MonoBehaviour
{
  public KeyAction[] keyActions;

  private List<InputGroupTag> disabledInputGroupTags = new List<InputGroupTag>();

  private void Update()
  {
    this.CheckForInputs();
  }

  private void CheckForInputs()
  {
    if (Input.anyKey)
    {
      foreach (KeyAction keyAction in this.keyActions)
      {
        if (this.disabledInputGroupTags.Intersect(keyAction.InputKeyAction.InputGroupTags).Any())
        {
          continue;
        }

        if (keyAction.KeyPressType == KeyPressActionType.KeyHold && Input.GetKey(keyAction.KeyCode))
        {
          keyAction.InputKeyAction.GameEvent.Call();
        }
        else if (keyAction.KeyPressType == KeyPressActionType.KeyDown && Input.GetKeyDown(keyAction.KeyCode))
        {
          keyAction.InputKeyAction.GameEvent.Call();
        }
        else if (keyAction.KeyPressType == KeyPressActionType.KeyUp && Input.GetKeyUp(keyAction.KeyCode))
        {
          keyAction.InputKeyAction.GameEvent.Call();
        }
      }
    }
  }

  /// <summary>
  /// Disable an inputGroupTag.
  /// For example can the inputGroupTag be Movement, then all keyCodes that has the Movement-tag, will be disabled.
  /// This can be handy if we want to disable some parts of the keys at once, for example in a cut scene, or when a dialogue appears.
  /// </summary>
  /// <param name="inputGroupTag"></param>
  public void DisableInputGroupTag(InputGroupTag inputGroupTag)
  {
    this.disabledInputGroupTags.Add(inputGroupTag);
  }

  /// <summary>
  /// Enables an inputGroupTag.
  /// For example can the inputGroupTag be Shooting, then all keyCodes that has the Shooting-tag, will be enabled.
  /// This can be handy if we want to enable some parts of the keys at once, for example when the player exits a shop, or when a dialogue disappears.
  /// </summary>
  /// <param name="inputGroupTag"></param>
  public void EnableInputGroupTag(InputGroupTag inputGroupTag)
  {
    this.disabledInputGroupTags.Remove(inputGroupTag);
  }
}