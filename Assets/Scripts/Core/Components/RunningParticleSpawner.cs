using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns a running "particle" effect when the gameObject has walked x units in length
/// </summary>
/// <returns></returns>
[
  RequireComponent(typeof(SpriteRenderer))
  ]
public class RunningParticleSpawner : MonoBehaviour
{
  [SerializeField]
  [Tooltip("Prefab of the pixel particle")]
  private GameObject runningParticlePrefab;
  [SerializeField]
  [Tooltip("Number of units to move until the particle will spawn again")]
  private FloatReference spawningDistance;
  private SpriteRenderer spriteRenderer;
  private Vector3 startRunningPosition = Vector3.zero;
  private Coroutine resetPositionCorutine;

  private void Start()
  {
    this.SetComponents();
  }
  private void FixedUpdate()
  {
    this.TrySpawnParticles();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  /// <summary>
  /// Resets the starting run position
  /// </summary>
  /// <returns></returns>
  private IEnumerator ResetStartPosition()
  {
    float numberOfFixedFrames = 2;

    while (numberOfFixedFrames > 0)
    {
      numberOfFixedFrames--;
      yield return new WaitForFixedUpdate();
    }

    this.startRunningPosition = Vector3.zero;
  }

  /// <summary>
  /// If the gameObject stands still, it will reset the starting position
  /// </summary>
  private void TryResetStartPosition()
  {
    if (this.resetPositionCorutine != null)
    {
      StopCoroutine(this.resetPositionCorutine);
    }

    this.resetPositionCorutine = StartCoroutine(this.ResetStartPosition());
  }

  /// <summary>
  /// Instantiate the particle into the game world
  /// </summary>
  private void SpawnRunningParticles()
  {
    if (this.runningParticlePrefab != null)
    {
      Vector3 bottomCenterOfEntity = new Vector3(this.spriteRenderer.bounds.center.x, this.spriteRenderer.bounds.min.y + this.runningParticlePrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
      GameObject particle = Instantiate(this.runningParticlePrefab, bottomCenterOfEntity, this.transform.rotation);
      this.startRunningPosition = Vector3.zero;
    }
  }

  /// <summary>
  /// Checks the distance between the staring position and the current position
  /// If the distance are longer than spawning distance, it will spawn a "dust cloud" behind the gameObject
  /// </summary>
  private void TrySpawnParticles()
  {
    if (this.runningParticlePrefab == null || this.spriteRenderer == null)
    {
      return;
    }

    if (this.startRunningPosition.Equals(Vector3.zero))
    {
      this.startRunningPosition = this.transform.position;
    }

    if (!this.startRunningPosition.Equals(Vector3.zero))
    {
      float distance = Vector3.Distance(this.startRunningPosition, this.transform.position);

      if (Mathf.Abs(distance) > this.spawningDistance)
      {
        this.SpawnRunningParticles();
      }
      else
      {
        this.TryResetStartPosition();
      }
    }
  }
}