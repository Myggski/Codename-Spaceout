using System.Collections;
using UnityEngine;

/// <summary>
/// Rotates the gameObject to the mouse position
/// </summary>
public class GunRecoil : MonoBehaviour
{
  private float recoilTimer = 0f;
  [SerializeField]
  private FloatReference recoilDegrees;
  [SerializeField]
  private FloatReference recoilDuration;
  private Quaternion originalRotationValue;

  /// <summary>
  /// Want to set the recoil after the gun has rotated to the position of the mouse pointer
  /// </summary>
  private void LateUpdate()
  {
    this.Recoil();
  }

  public void StartRecoil()
  {
    this.recoilTimer = this.recoilDuration;
    this.originalRotationValue = this.transform.rotation;
  }

  private void Recoil()
  {
    if (this.recoilTimer > 0)
    {
      this.recoilTimer -= Time.deltaTime;

      if (this.recoilTimer <= 0)
      {
        this.recoilTimer = 0f;
      }
      else
      {
        float degrees = this.recoilDegrees;

        // If the transform is flipped (facing left), invert the rotation
        if (this.transform.localScale.x < 0)
        {
          degrees *= -1;
        }

        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + degrees);
        transform.rotation = Quaternion.Lerp(this.originalRotationValue, rotation, this.recoilTimer / this.recoilDuration);
      }
    }
  }
}