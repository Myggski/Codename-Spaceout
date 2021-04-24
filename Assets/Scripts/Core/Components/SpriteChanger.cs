using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer))
]
public class SpriteChanger : MonoBehaviour
{
  [SerializeField]
  private Sprite sprite;
  private Sprite originalSprite;
  private SpriteRenderer spriteRenderer;

  private Coroutine resetSpriteCoroutine;

  private bool hasChanged = false;

  private void Start()
  {
    this.SetComponents();
    this.Setup();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Setup()
  {
    this.originalSprite = this.spriteRenderer.sprite;
  }

  public void ChangeSprite()
  {
    if (!this.hasChanged)
    {
      this.ToggleSprite();
    }

    if (this.resetSpriteCoroutine != null)
    {
      StopCoroutine(this.resetSpriteCoroutine);
    }

    this.resetSpriteCoroutine = StartCoroutine(this.ResetSprite());
  }

  private void ToggleSprite()
  {
    this.hasChanged = true;
    this.spriteRenderer.sprite = this.sprite;
  }

  private IEnumerator ResetSprite()
  {
    yield return new WaitForSeconds(0.1f);
    this.hasChanged = false;
    this.spriteRenderer.sprite = this.originalSprite;
  }
}
