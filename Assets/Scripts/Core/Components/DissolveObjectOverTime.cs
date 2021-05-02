using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DissolveObjectOverTime : DissolveObject
{
  [SerializeField]
  [Tooltip("How many seconds before dissolving and being destroyed")]
  private float numberOfSeconds;

  [SerializeField]
  [Tooltip("The gameObject will be destroyed by default, but this only disables the gameObject")]
  private bool disable;

  [SerializeField]
  [Tooltip("Triggers when the gameObject has dissolved")]
  private UnityEvent dissolvedEvent;

  private Coroutine dissolveCoroutine;

  public void StartDissolveOverTime()
  {
    this.dissolveCoroutine = StartCoroutine(this.Dissolve());
  }

  private void OnDisable()
  {
    if (this.dissolveCoroutine != null)
    {
      StopCoroutine(this.dissolveCoroutine);
    }

    this.Reset();
  }

  private void onEnable()
  {
    if (this.dissolveCoroutine != null)
    {
      StopCoroutine(this.dissolveCoroutine);
    }

    this.Reset();
  }

  private void Remove()
  {
    if (this.dissolvedEvent != null)
    {
      this.dissolvedEvent.Invoke();
    }

    if (this.disable)
    {
      this.gameObject.SetActive(false);

    }
    else
    {
      Destroy(this.gameObject);
    }
  }

  private void Reset()
  {
    this.spriteRenderer.material.SetFloat(DissolveObject.materialParameterName, this.defaultFadeValue > 0 ? this.defaultFadeValue : 1f);
  }

  private IEnumerator Dissolve()
  {
    yield return new WaitForSeconds(this.numberOfSeconds);

    this.StartDissolve(this.Remove);
  }
}
