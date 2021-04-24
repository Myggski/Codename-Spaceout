using UnityEngine;
using System.Collections;

/// <summary>
/// Adds a white blinking effect when activated
/// For example when an enemy is hit, it will blink solid white a few times
/// </summary>
public class BlinkOnHit : MonoBehaviour
{
  [SerializeField]
  [Tooltip("Material that turns the sprite into a solid color when blink is set to true")]
  private Material blinkMaterial;

  [SerializeField]
  [Tooltip("Number of times the sprite will turn into the solid color")]
  private float numberOfBlinks;

  [SerializeField]
  [Tooltip("Number of seconds of how long the solid color should display")]
  private float blinkTimer;

  [SerializeField]
  [Tooltip("Number of seconds between blinks")]
  private float delayBetweenBlinks;

  private SpriteRenderer spriteRenderer;
  private Material defaultMaterial;

  private bool isBlinking;

  private void Start()
  {
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
    this.defaultMaterial = this.spriteRenderer.material;
  }

  private IEnumerator Blink()
  {
    this.isBlinking = !this.isBlinking;
    float currentNumberOfBlinks = this.numberOfBlinks;
    this.spriteRenderer.material = this.blinkMaterial;

    while (currentNumberOfBlinks > 0)
    {
      this.spriteRenderer.material.SetInt("_Blink", 1);

      yield return new WaitForSeconds(this.blinkTimer);

      this.spriteRenderer.material.SetInt("_Blink", 0);

      yield return new WaitForSeconds(this.delayBetweenBlinks);

      currentNumberOfBlinks--;
    }

    this.spriteRenderer.material = this.defaultMaterial;
    this.isBlinking = !this.isBlinking;
  }

  public void StartBlink()
  {
    if (!this.isBlinking)
    {
      StartCoroutine(this.Blink());
    }
  }
}
