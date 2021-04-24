using UnityEngine;
using System.Collections;

[
    RequireComponent(typeof(SpriteRenderer))
]
public class FakeShadow : MonoBehaviour
{
  [SerializeField]
  private GameObject fakeShadowPrefab;
  [Tooltip("How much bigger (+) or smaller (-) should the shadow be")]
  [SerializeField]
  private float widthOffsetInPixels;
  private SpriteRenderer spriteRenderer;

  private void Start()
  {
    this.SetComponents();
    StartCoroutine(this.Setup());
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private IEnumerator Setup()
  {
    yield return new WaitForFixedUpdate();

    if (this.fakeShadowPrefab != null && this.spriteRenderer != null)
    {
      float widthOffset = PixelHelper.CovertPixelsToUnits(this.widthOffsetInPixels);
      Vector3 position = new Vector3(this.transform.position.x - widthOffset, this.spriteRenderer.bounds.min.y, this.spriteRenderer.bounds.min.z);
      float shadowWidth = this.spriteRenderer.sprite.bounds.size.x + widthOffset;

      GameObject shadowObject = Instantiate(this.fakeShadowPrefab, position, this.transform.rotation, this.transform);
      shadowObject.transform.localScale = new Vector3(shadowWidth, this.transform.localScale.y, this.transform.localScale.z);
    }
  }
}
