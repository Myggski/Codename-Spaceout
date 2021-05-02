using UnityEngine;
using System.Collections;

[
    RequireComponent(typeof(SpriteRenderer))
]
public class PlayerBlinkHit : MonoBehaviour
{
  [SerializeField]
  private int numberOfBlinks;
  [SerializeField]
  private float delayBetweenBlinks;
  private SpriteRenderer spriteRenderer;
  private Coroutine blinkCoroutine;
  private bool stopBlink;

  private void Awake()
  {
    this.SetComponents();
  }

  private void OnDisable()
  {
    this.ToggleSpriteRenderer(true);
    this.stopBlink = false;
  }

  private void ToggleSpriteRenderer(bool enabled)
  {
    this.spriteRenderer.enabled = enabled;
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private IEnumerator Blink()
  {
    for (int i = 0; i < this.numberOfBlinks * 2; i++)
    {
      if (this.stopBlink)
      {
        break;
      }

      ToggleSpriteRenderer(!this.spriteRenderer.enabled);
      yield return new WaitForSeconds(this.delayBetweenBlinks);
    }

    this.blinkCoroutine = null;
  }

  public void StartBlink()
  {
    if (this.blinkCoroutine == null)
    {
      this.blinkCoroutine = StartCoroutine(this.Blink());
    }
  }
}
