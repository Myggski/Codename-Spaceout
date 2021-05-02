using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GunShooter : MonoBehaviour
{
  [SerializeField]
  private UnityEvent projectileFired;
  [SerializeField]
  private GameObject muzzleFlashPrefab;
  [SerializeField]
  private GameObject projectilePrefab;
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private FloatReference fireRateInSeconds;
  [SerializeField]
  private FloatReference gunBarrelYCenterPositionInPixels;
  private bool canShoot = true;

  private void Start()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  /// <summary>
  /// Shoots when shooting event is triggered
  /// </summary>
  public void Shoot()
  {
    if (this.canShoot)
    {
      StartCoroutine(Shooting());
    }
  }

  /// <summary>
  /// Changes the projectile and muzzle flash of the gun
  /// </summary>
  /// <param name="projectilePrefab"></param>
  /// <param name="muzzleFlashPrefab"></param>
  public void Setup(GameObject projectilePrefab, GameObject muzzleFlashPrefab)
  {
    this.projectilePrefab = projectilePrefab;
    this.muzzleFlashPrefab = muzzleFlashPrefab;
  }

  /// <summary>
  /// Shoots, and waiting for the fire rate
  /// </summary>
  /// <returns></returns>
  private IEnumerator Shooting()
  {
    this.canShoot = false;

    this.SpawnProjectile();

    if (this.projectileFired != null)
    {
      this.projectileFired.Invoke();
    }

    yield return new WaitForSeconds(this.fireRateInSeconds);

    this.canShoot = true;
  }

  /// <summary>
  /// This method calculates and finds the position of the gun barrel (top right of the weapon sprite).
  /// </summary>
  private void SpawnProjectile()
  {
    if (!this.HasSprite())
    {
      return;
    }

    float gunBarrelOffsetLocal = PixelHelper.CovertPixelsToUnits(this.gunBarrelYCenterPositionInPixels);
    Vector2 gunBarrelLocalPosition = new Vector2(this.spriteRenderer.sprite.bounds.max.x, this.spriteRenderer.sprite.bounds.max.y + gunBarrelOffsetLocal / 2);

    GameObject projectile = Instantiate(this.projectilePrefab, this.transform.position, this.transform.rotation, transform);
    Vector2 projectileSize = projectile.GetComponent<SpriteRenderer>().sprite.bounds.size;

    // Sets the position of the projectile, relative to the parent (gun)
    projectile.transform.localPosition = (Vector2)(gunBarrelLocalPosition - projectileSize / 2);
    projectile.transform.SetParent(null);

    this.TrySpawnMuzzleFlash(gunBarrelLocalPosition, projectileSize);
  }

  private void TrySpawnMuzzleFlash(Vector2 gunBarrelLocalPosition, Vector2 projectileSize)
  {
    if (this.muzzleFlashPrefab != null)
    {
      GameObject muzzleFlash = Instantiate(this.muzzleFlashPrefab, this.transform.position, this.transform.rotation, transform);
      muzzleFlash.transform.localPosition = (Vector2)(gunBarrelLocalPosition - projectileSize / 2);
      muzzleFlash.transform.SetParent(transform.parent);
    }
  }

  private bool HasSprite()
  {
    return this.spriteRenderer != null && this.spriteRenderer.sprite != null;
  }
}
