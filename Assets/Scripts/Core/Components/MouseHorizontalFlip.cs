using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer))
]
/// <summary>
/// TODO: Change to a better name
/// When mouse pointer passes the center point of a gameObject, it should flip horizontal
/// For example when mouse pointer passes center of player, the player should flip to "look" at the mouse pointer
/// </summary>
public class MouseHorizontalFlip : MonoBehaviour
{
  private bool isFacingRight = true;

  private void FixedUpdate()
  {
    this.CheckFlipStatus();
  }

  /// <summary>
  /// Checking the distance between the gameObject and the mouse position
  /// </summary>
  private void CheckFlipStatus()
  {
    float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    float distance = this.transform.position.x - mouseX;

    if (distance > 0 && this.isFacingRight ||
        distance < 0 && !this.isFacingRight)
    {
      Flip();
    }
  }

  /// <summary>
  /// Flips the gameObject
  /// </summary>
  private void Flip()
  {
    Vector3 localScale = transform.localScale;
    localScale.x *= -1;
    this.isFacingRight = localScale.x > 0;

    transform.localScale = localScale;
    transform.rotation = Quaternion.Inverse(transform.rotation);

    this.FlipChildren(this.transform);
  }

  /// <summary>
  /// Flip the localScale for every child on the first level
  /// </summary>
  /// <param name="transform"></param>
  private void FlipChildren(Transform transform)
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      Transform child = transform.GetChild(i);
      child.localScale *= -1;
    }
  }
}
