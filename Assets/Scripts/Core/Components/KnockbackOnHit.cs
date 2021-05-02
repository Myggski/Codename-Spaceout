using UnityEngine;
using UnityEngine.Events;
using System.Collections;

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

  [SerializeField]
  private UnityEvent knocked;

  private Rigidbody2D rb2d;

  private Coroutine knockbackCoroutine;

  private void Start()
  {
    this.SetComponents();
  }

  private void SetComponents()
  {
    this.rb2d = GetComponent<Rigidbody2D>();
  }

  private IEnumerator Knockback(Vector2 hitFromPosition)
  {
    yield return new WaitForEndOfFrame();

    Vector2 direction = ((Vector2)this.transform.position - hitFromPosition).normalized;
    this.rb2d.AddForce((direction * this.knockbackForce) * Time.deltaTime, ForceMode2D.Impulse);

    yield return new WaitForSeconds(this.knockbackDuration);

    this.rb2d.velocity = Vector2.zero;

    if (this.knocked != null)
    {
      this.knocked.Invoke();
    }
  }

  public void StartKnockback(Vector2 hitFromPosition)
  {
    if (this.knockbackCoroutine != null)
    {
      StopCoroutine(this.knockbackCoroutine);
      this.rb2d.velocity = Vector2.zero;
    }

    this.knockbackCoroutine = StartCoroutine(this.Knockback(hitFromPosition));
  }
}
