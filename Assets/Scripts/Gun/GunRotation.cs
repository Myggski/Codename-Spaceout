using UnityEngine;

[
  RequireComponent(typeof(SpriteRenderer))
]
/// <summary>
/// Rotates the gun barrel, so the barrel "looks" at the cursor
/// </summary>
public class GunRotation : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  /// <summary>
  /// Weapons comes in diffrent shapes and forms, this helps projectiles, muzzle flashes and other things to spawn at the center of the barrel.
  /// The gun barrel is always facing right by default, so the top-right of the image is the barrel.
  /// Lets say that the gun-sprite is 15x13px in size, but the barrel starts after two empty pixels from the top.
  /// Also the center of the barrel is another two pixels down, that means you need to set the offset to four.
  /// </summary>
  private FloatReference gunBarrelYCenterPositionInPixels;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.SetRotation();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  /// <summary>
  /// The gun always rotates on the pivot of the gun sprite.
  /// But depending on how close the mouse cursor is to the character, or the gun barrel, it gets the rotation angle either from the tip of the gun barrel, or the center of the character sprite
  /// When the cursor is far away it get the radius from the gun barrel.
  /// But if the cursor is getting to close, the radius changes to the center of the character
  /// This is prevent the gun for wobbling, and not to aim backwards (shoots left, but is looking right).
  /// </summary>
  private void SetRotation()
  {
    if (this.spriteRenderer.sprite == null)
    {
      return;
    }

    float gunBarrelOffsetLocal = PixelHelper.CovertPixelsToUnits(this.gunBarrelYCenterPositionInPixels);
    Vector2 gunBarrelLocalPosition = new Vector2(this.spriteRenderer.sprite.bounds.max.x, this.spriteRenderer.sprite.bounds.max.y + gunBarrelOffsetLocal / 2);
    Vector2 gunBarrelWorldPosition = (Vector2)this.transform.TransformPoint(this.transform.localPosition + (Vector3)gunBarrelLocalPosition);

    Vector2 mouseToCharacterRadius = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.parent.position;
    Vector2 mouseToGunRadius = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gunBarrelWorldPosition;
    Vector2 characterToGunRadius = (Vector2)this.transform.parent.position - gunBarrelWorldPosition;
    Vector2 radius = Vector3.Distance(characterToGunRadius, mouseToGunRadius) > Vector3.Distance(gunBarrelWorldPosition, this.transform.parent.position) * 2 ? mouseToGunRadius : mouseToCharacterRadius;

    this.Rotate(radius);
  }

  /// <summary>
  /// Rotates the gun
  /// </summary>
  /// <param name="radius"></param>
  private void Rotate(Vector2 radius)
  {
    float angle = Mathf.Atan2(radius.y, radius.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, angle);
  }
}