using UnityEngine;
using UnityEngine.Events;

public class EnemyEntity : Entity
{
  [SerializeField]
  private GameObjectRuntimeSet enemyList;
  public UnityEvent DeathEvent;
  public UnityEvent<Vector2> DamageTakenEvent;

  private void Awake()
  {
    this.SetHealth();
  }

  private void OnEnable()
  {
    this.enemyList.Add(this.gameObject);
  }

  private void OnDestroy()
  {
    this.enemyList.RemoveById(this.gameObject.GetInstanceID());
  }

  private void SetHealth()
  {
    this.currentHealth = this.maxHealth;
  }

  protected override void Hit(Vector2 hitFromPosition)
  {
    if (this.DamageTakenEvent != null)
    {
      this.DamageTakenEvent.Invoke(hitFromPosition);
    }
  }

  protected override void Die(Vector2 hitFromPosition)
  {
    if (this.DeathEvent != null)
    {
      this.DeathEvent.Invoke();
    }
  }
}
