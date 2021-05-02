using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D)),
]
/// <summary>
/// Base class that has the essensial methods for moving a gameObject
/// </summary>
abstract public class Mover : MonoBehaviour
{
  [SerializeField]
  protected FloatReference movementSpeed;
  protected Rigidbody2D rb2d;

  private void Awake()
  {
    this.SetComponents();
    this.SetMoreComponents();
  }

  private void OnEnable()
  {
    this.SetComponents();
    this.SetMoreComponents();
  }

  private void SetComponents()
  {
    this.rb2d = GetComponent<Rigidbody2D>();
  }

  protected virtual void SetMoreComponents()
  {
    // Empty
  }
  /// <summary>
  /// Move a gameObject with physics
  /// For example a projectile
  /// </summary>
  /// <param name="position">Current position</param>
  protected void Move(Vector3 position)
  {
    if (position != Vector3.zero)
    {
      this.rb2d.MovePosition(this.GetNextPosition(position));
    }
  }

  /// <summary>
  /// Moving gameObjects without physics
  /// For example enemies that will be using pathfinding to move, and will never collide with obstacles
  /// </summary>
  /// <param name="position"></param>
  protected void MoveTowards(Vector3 moveTowards)
  {
    // TODO: Something sets this.rb2d to null, need to GetComponent directily until it's fixed
    this.rb2d.MovePosition(this.GetNextMoveTowardsPosition(moveTowards));
  }

  /// <summary>
  /// Gets the next position before MovePosition has been called
  /// </summary>
  /// <param name="position"></param>
  /// <returns></returns>
  protected Vector3 GetNextPosition(Vector3 position)
  {
    return this.rb2d.position + (Vector2)(position * Time.fixedDeltaTime * this.movementSpeed);
  }

  /// <summary>
  /// Gets the next position before MoveTowards has been called
  /// </summary>
  /// <param name="moveTowards"></param>
  /// <returns></returns>
  protected Vector3 GetNextMoveTowardsPosition(Vector3 moveTowards)
  {
    return Vector3.MoveTowards(transform.position, moveTowards, this.movementSpeed * Time.fixedDeltaTime);
  }
}
