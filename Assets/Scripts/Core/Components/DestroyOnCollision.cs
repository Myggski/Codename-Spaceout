using UnityEngine;

/// <summary>
/// Destroys gameObject if it collides with something
/// </summary>
public class DestroyOnCollision : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    this.DestroyObject();
  }

  private void DestroyObject()
  {
    Destroy(gameObject);
  }
}
