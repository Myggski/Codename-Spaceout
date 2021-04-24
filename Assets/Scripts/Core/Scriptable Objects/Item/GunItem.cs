using UnityEngine;

/// <summary>
/// Gun information
/// </summary>
[CreateAssetMenu(menuName = "Items/Gun")]
public class GunItem : Item
{
  [Tooltip("The fire rate of the gun, how long it has to wait until it can fire again")]
  public float fireRateInSeconds;

  /// <summary>
  /// Weapons comes in diffrent shapes and forms, this helps projectiles, muzzle flashes and other things to spawn at the center of the barrel.
  /// The gun barrel is always facing right by default, so the top-right of the image is the barrel.
  /// Lets say that the gun-sprite is 15x13px in size, but the barrel starts after two empty pixels from the top.
  /// Also the center of the barrel is another two pixels down, that means you need to set the offset to four.
  /// </summary>
  [Tooltip("Helps to get the center position of the barrel at the Y-axis")]
  public float gunBarrelYCenterPositionInPixels;

  [Tooltip("How many degrees the gun should rotate when the gun fires")]
  public float recoilDegrees;

  [Tooltip("How long it takes in seconds for the gun to rotate back to the original position")]
  public float recoilDuration;

  [Tooltip("GameObject of a projectile to spawn, when the gun fires")]
  public GameObject projectilePrefab;

  [Tooltip("GameObject of a muzzle flash, that will spawn with the projectile")]
  public GameObject muzzleFlashPrefab;
}