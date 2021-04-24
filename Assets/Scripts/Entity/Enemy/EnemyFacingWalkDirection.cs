using UnityEngine;

/// <summary>
/// This component checks the gameObjects movement, if the direction changes, it flips the image
/// </summary>
public class EnemyFacingWalkDirection : MonoBehaviour
{
  private Vector3 previousPosition;
  private bool isFacingRight;

  private void Start()
  {
    this.Setup();
  }

  private void FixedUpdate()
  {
    this.TrySetHorizontalRotation();
  }
  private void Setup()
  {
    this.isFacingRight = true;
    this.previousPosition = this.transform.position;
  }

  private void TrySetHorizontalRotation()
  {
    if (this.previousPosition != transform.position)
    {
      Vector3 direction = (previousPosition - transform.position).normalized;

      if (direction.x <= 0.1 && this.isFacingRight || direction.x >= 0.1 && !isFacingRight)
      {
        this.ToggleFlip();
      }

      this.previousPosition = this.transform.position;
    }
  }

  private void ToggleFlip()
  {
    transform.localRotation = Quaternion.Euler(0, this.isFacingRight ? 0 : 180, 0);
    this.isFacingRight = !this.isFacingRight;
  }
}