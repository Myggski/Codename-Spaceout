using UnityEngine;

[
    RequireComponent(typeof(BoxCollider2D))
]
public class TriggerOnPlayerEnter : MonoBehaviour
{
  [SerializeField]
  private GameEvent eventToTrigger;

  private void TryToTrigger(Collider2D other)
  {
    if (other.GetComponent<PlayerEntity>() != null && this.eventToTrigger != null)
    {
      this.eventToTrigger.Call();
      this.gameObject.SetActive(true);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    this.TryToTrigger(other);
  }
}
