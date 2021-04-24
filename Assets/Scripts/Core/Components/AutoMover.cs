/// <summary>
/// Moves forward, the movement depends on the gameObjects rotation
/// </summary>
public class AutoMover : Mover
{
  /// <summary>
  /// The gameObject should go the opposite way if the gameObject is flipped
  /// </summary>
  private void FixedUpdate()
  {
    this.MoveRight();
  }

  private void MoveRight()
  {
    Move(transform.right);
  }
}
