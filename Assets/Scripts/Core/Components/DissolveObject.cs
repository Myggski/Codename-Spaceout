using UnityEngine;
using System;
using System.Collections;

[
  RequireComponent(typeof(SpriteRenderer))
]
public class DissolveObject : MonoBehaviour
{
  [SerializeField]
  private Material dissolveMaterial;
  protected SpriteRenderer spriteRenderer;
  private float activeFadeDuration;
  protected float defaultFadeValue;
  protected const string materialParameterName = "_Fade";
  public event Action hasDissolvedAction;

  private void Awake()
  {
    this.SetComponents();
  }

  private void Update()
  {
    this.Dissolve();
  }

  private void SetComponents()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void StartDissolve(Action action)
  {
    Action[] actions = { action };
    this.StartDissolve(actions);
  }

  public void StartDissolve(Action[] actions)
  {
    if (!this.dissolveMaterial.Equals(null))
    {
      this.spriteRenderer.material = this.dissolveMaterial;
      this.defaultFadeValue = this.spriteRenderer.material.GetFloat(DissolveObject.materialParameterName);
      this.activeFadeDuration = this.spriteRenderer.material.GetFloat(DissolveObject.materialParameterName);

      foreach (Action action in actions)
      {
        this.hasDissolvedAction += action;
      }
    }
  }

  private void Dissolve()
  {
    if (this.activeFadeDuration > 0 && !this.dissolveMaterial.Equals(null))
    {
      this.activeFadeDuration -= Time.deltaTime;
      this.spriteRenderer.material.SetFloat(DissolveObject.materialParameterName, activeFadeDuration);

      if (this.activeFadeDuration <= 0f)
      {
        this.hasDissolvedAction.Invoke();
      }
    }
  }
}