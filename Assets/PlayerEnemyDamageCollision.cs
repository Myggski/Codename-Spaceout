using UnityEngine;
using System.Collections;

[
    RequireComponent(typeof(PlayerEntity))
]
public class PlayerEnemyDamageCollision : MonoBehaviour
{
  [SerializeField]
  [Tooltip("How long in seconds before the player can take damage again")]
  private float damageCooldown;
  private PlayerEntity playerEntity;
  private bool isOnDamageCooldown;

  private float cooldownTimer;

  private void OnEnable()
  {
    this.isOnDamageCooldown = false;
  }

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.TryResetCooldownTimer();
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    this.TryToDealDamage(other);
  }

  private void SetComponents()
  {
    this.playerEntity = GetComponent<PlayerEntity>();
  }

  private void TryToDealDamage(Collider2D other)
  {
    if (other.gameObject.GetComponent<EnemyEntity>() != null && !this.isOnDamageCooldown)
    {
      this.playerEntity.DealDamage(1f);
      this.isOnDamageCooldown = true;
    }
  }

  private void TryResetCooldownTimer()
  {
    if (!this.isOnDamageCooldown)
    {
      return;
    }

    if (this.cooldownTimer > this.damageCooldown)
    {
      this.isOnDamageCooldown = false;
      this.cooldownTimer = 0f;
    }
    else
    {
      this.cooldownTimer += Time.deltaTime;
    }
  }
}
