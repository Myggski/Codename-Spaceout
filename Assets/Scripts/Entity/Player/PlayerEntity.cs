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
  private DissolveObject dissolveObject;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.CheckHealth();
  }

  private void SetComponents()
  {
    this.dissolveObject = GetComponent<DissolveObject>();
  }

  private void CheckHealth()
  {
    if (this.health != this.currentHealth)
    {
      this.health.Variable.SetValue(this.currentHealth);
    }
  }

  private void Died()
  {
    if (this.DeathEvent != null)
    {
      this.DeathEvent.Call();
    }

    Destroy(gameObject);
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
    this.Hit();

    if (this.dissolveObject != null)
    {
      this.dissolveObject.StartDissolve(this.Died);
    }
    else
    {
      this.Died();
    }
  }
}