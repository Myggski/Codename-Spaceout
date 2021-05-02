using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour, IEntity
{
  /// <summary>
  /// Max health of an entity, so the entity never can be healed more than max health
  /// </summary>
  [SerializeField]
  protected FloatReference maxHealth;
  protected float currentHealth;
  protected bool dying;

  /// <summary>
  /// The entity is dead if it has zero or less health
  /// /// </summary>
  public bool Dead => this.currentHealth <= 0;

  /// <summary>
  /// When the entity is getting hit, it loses health
  /// Despawning the entity if the entity is killed
  /// </summary>
  /// <param name="damage">The damage taken</param>
  public void DealDamage(float damage)
  {
    if (this.dying)
    {
      return;
    }

    this.currentHealth -= damage;

    if (Dead)
    {
      dying = true;
      this.Die();
    }
    else
    {
      this.Hit();
    }
  }

  /// <summary>
  /// When the entity is getting hit, it loses health
  /// Despawning the entity if the entity is killed
  /// </summary>
  /// <param name="damage">The damage taken</param>
  public void DealDamage(float damage, Vector2 hitFromPosition)
  {
    if (this.dying)
    {
      return;
    }

    this.currentHealth -= damage;

    if (Dead)
    {
      this.dying = true;
      this.Die(hitFromPosition);
    }
    else
    {
      this.Hit(hitFromPosition);
    }
  }

  protected virtual void Die()
  {
    throw new NotImplementedException();
  }

  protected virtual void Die(Vector2 hitFromPosition)
  {
    throw new NotImplementedException();
  }

  protected virtual void Hit()
  {
    throw new NotImplementedException();
  }

  protected virtual void Hit(Vector2 hitFromPosition)
  {
    throw new NotImplementedException();
  }
}
