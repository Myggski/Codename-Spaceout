using UnityEngine;
using UnityEngine.Events;

public class EnemyEntity : Entity
{
  public UnityEvent DeathEvent;
  public UnityEvent<Vector2> DamageTakenEvent;

  private void Awake()
  {
    this.SetHealth();
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
    this.Hit(hitFromPosition);
    this.DeathEvent.Invoke();
    //Destroy(gameObject);
  }
}
