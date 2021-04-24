using UnityEngine;

/// <summary>
/// This is a living thing, like a player, or an enemy
/// </summary>
public interface IEntity
{
  bool Dead { get; }

  GameObject gameObject { get; }

  void DealDamage(float damage, Vector2 hitFromPosition);
}
