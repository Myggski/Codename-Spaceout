using UnityEngine;

public class ProjectileDamageDealer : MonoBehaviour
{
  [SerializeField]
  private float damage;

  /// <summary>
  /// If the projectile hits anything and it's an entity, do damage
  /// </summary>
  /// <param name="collidedObject">A gameObject that has collided, possibly an entity</param>
  private void OnTriggerEnter2D(Collider2D collidedObject)
  {
    IEntity entity = collidedObject.gameObject.GetComponent<IEntity>();

    if (entity != null)
    {
      entity.DealDamage(this.damage, this.transform.position);
      Destroy(this.gameObject);
    }
    else if (collidedObject.GetComponent<CompositeCollider2D>() != null)
    {
      Destroy(this.gameObject);
    }
  }
}
