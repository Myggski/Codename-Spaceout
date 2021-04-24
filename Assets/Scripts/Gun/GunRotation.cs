using UnityEngine;

/// <summary>
/// Rotates the gameObject to the mouse position
/// </summary>
public class GunRotation : RotateToMousePositionOnce
{
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private FloatReference gunBarrelYCenterPositionInPixels;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.Rotate();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  protected void Rotate()
  {
    if (!this.HasSprite())
    {
      return;
    }

    float gunBarrelOffsetLocal = PixelHelper.CovertPixelsToUnits(this.gunBarrelYCenterPositionInPixels);
    Vector2 gunBarrelLocalPosition = new Vector2(this.spriteRenderer.sprite.bounds.max.x, this.spriteRenderer.sprite.bounds.max.y + gunBarrelOffsetLocal / 2);
    Vector3 gunBarrelWorldPosition = this.transform.TransformPoint(this.transform.localPosition + (Vector3)gunBarrelLocalPosition);

    Vector3 gunBarrelPosition = !gunBarrelOffsetLocal.Equals(null) ? gunBarrelWorldPosition : this.transform.position;
    Vector3 mouseToCharacterRadius = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)this.transform.parent.position;
    Vector3 mouseToGunRadius = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gunBarrelPosition;
    Vector3 characterToGunRadius = this.transform.parent.position - gunBarrelPosition;
    Vector3 radius = Vector3.Distance(characterToGunRadius, mouseToGunRadius) > Vector3.Distance(gunBarrelWorldPosition, this.transform.parent.position) * 2 ? mouseToGunRadius : mouseToCharacterRadius;

    this.Rotate(radius);
  }

  private bool HasSprite()
  {
    return this.spriteRenderer.sprite != null;
  }

  private void OnDrawGizmos()
  {
    if (this.spriteRenderer != null)
    {
      float gunBarrelOffsetLocal = PixelHelper.CovertPixelsToUnits(this.gunBarrelYCenterPositionInPixels);
      Vector2 gunBarrelLocalPosition = new Vector2(this.spriteRenderer.sprite.bounds.max.x, this.spriteRenderer.sprite.bounds.max.y + gunBarrelOffsetLocal / 2);
      Vector3 gunBarrelWorldPosition = this.transform.TransformPoint(this.transform.localPosition + (Vector3)gunBarrelLocalPosition);
      Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), gunBarrelWorldPosition);
    }
  }
}