using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D))
]
public class KnockbackOnHit : MonoBehaviour
{
  [SerializeField]
  [Tooltip("How hard the gameObject is being pushed")]
  private float knockbackForce;

  [SerializeField]
  [Tooltip("Number of seconds until the knockback stops")]
  private float knockbackDuration;

  private new Rigidbody2D rigidbody2D;

  private Coroutine knockbackCoroutine;

  private void Start()
  {
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private IEnumerator Knockback(Vector2 hitFromPosition)
  {
    Vector2 direction = ((Vector2)this.transform.position - hitFromPosition).normalized;
    this.rigidbody2D.AddForce((direction * this.knockbackForce) * Time.deltaTime, ForceMode2D.Impulse);

    yield return new WaitForSeconds(this.knockbackDuration);

    this.rigidbody2D.velocity = Vector2.zero;
  }

  public void StartKnockback(Vector2 hitFromPosition)
  {
    if (this.knockbackCoroutine != null)
    {
      StopCoroutine(this.knockbackCoroutine);
      this.rigidbody2D.velocity = Vector2.zero;
    }

    this.knockbackCoroutine = StartCoroutine(this.Knockback(hitFromPosition));
  }
}
