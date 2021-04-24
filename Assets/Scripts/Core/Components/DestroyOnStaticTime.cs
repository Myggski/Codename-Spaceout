using UnityEngine;

/// <summary>
/// Destroys gameObject direcly after it has been instantiated
/// For example dust clouds when the player is running
/// </summary>
public class DestroyOnStaticTime : MonoBehaviour
{
  [SerializeField]
  private float lifeTimeInSeconds;

  private void Start()
  {
    this.DestroyObject();
  }

  private void DestroyObject()
  {
    Destroy(this.gameObject, this.lifeTimeInSeconds);
  }
}
