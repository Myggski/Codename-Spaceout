using UnityEngine;

/// <summary>
/// This component instantiates everything that a player-gameObject need to know to operate.
/// </summary>
[
    RequireComponent(typeof(DissolveObject)),
]
public class PlayerEntity : Entity
{
  [Tooltip("Event for when a player dies")]
  [SerializeField]
  private GameEvent DeathEvent;

  [Tooltip("Event for when a player take damage")]
  [SerializeField]
  private GameEvent DamageTakenEvent;
  [SerializeField]
  private FloatReference health;

  private void Awake()
  {
    this.SetHealth();
  }

  private void OnEnable()
  {
    this.SetHealth();
  }

  private void SetHealth()
  {
    this.dying = false;
    this.currentHealth = this.maxHealth.Value;
    this.health.Variable.Value = this.maxHealth.Value;
  }

  private void Update()
  {
    this.CheckHealth();
  }

  private void CheckHealth()
  {
    if (this.health != this.currentHealth)
    {
      this.health.Variable.SetValue(this.currentHealth);
    }
  }

  protected override void Hit()
  {
    if (this.DamageTakenEvent != null)
    {
      this.DamageTakenEvent.Call();
    }
  }

  protected override void Die()
  {
    this.health.Variable.Value = 0f;
    this.currentHealth = 0f;

    if (this.DeathEvent != null)
    {
      this.DeathEvent.Call();
    }
  }
}