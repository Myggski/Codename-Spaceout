using UnityEngine;

/// <summary>
/// Removes the gameObject over time.
/// For example a projectile, it needs to despawn over time
/// </summary>
public class DestroyOnSetupTime : MonoBehaviour
{
  [SerializeField]
  private float lifeTimeInSeconds;
  private void DestroyOverTime() {
    Destroy(gameObject, this.lifeTimeInSeconds);
  }
}