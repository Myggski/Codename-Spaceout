using UnityEngine;

/// <summary>
/// This component checks the gameObjects movement, if the direction changes, it flips the image
/// </summary>
public class EnemyFacingTarget : MonoBehaviour
{
  private Vector3 previousPosition;
  private GameObject target;
  private bool isFacingRight;

  private void OnEnable()
  {
    this.Setup();
  }

  private void FixedUpdate()
  {
    this.TrySetHorizontalRotation();
  }
  private void Setup()
  {
    this.target = GameObject.FindGameObjectWithTag("Player");
  }

  private void TrySetHorizontalRotation()
  {
    if (target != null)
    {
      Vector3 direction = (transform.position - this.target.transform.position).normalized;

      if (direction.x <= 0.05 && this.isFacingRight || direction.x >= 0.05 && !isFacingRight)
      {
        this.ToggleFlip();
      }
    }
  }

  private void ToggleFlip()
  {
    transform.localRotation = Quaternion.Euler(0, this.isFacingRight ? 0 : 180, 0);
    this.isFacingRight = !this.isFacingRight;
  }
}