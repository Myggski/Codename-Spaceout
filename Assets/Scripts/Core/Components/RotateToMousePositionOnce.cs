using UnityEngine;

/// <summary>
/// Rotates the gameObject to the mouse position when instantiated
/// Example: Projectiles
/// </summary>
public class RotateToMousePositionOnce : MonoBehaviour
{
  private void Awake()
  {
    this.Setup();
  }

  /// <summary>
  /// Setting the rotation position, and calls rotation
  /// </summary>
  private void Setup()
  {
    this.Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position);
  }

  /// <summary>
  /// Rotates the gameObject
  /// </summary>
  /// <param name="radius"></param>
  protected void Rotate(Vector3 radius)
  {
    float angle = Mathf.Atan2(radius.y, radius.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, angle);
  }
}