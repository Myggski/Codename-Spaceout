using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator))
]
public class PlayerDasher : MonoBehaviour
{
  [SerializeField]
  [Tooltip("How long the dashing takes to perform")]
  private float dashTimer;
  [SerializeField]
  [Tooltip("How long until the player can trigger dash again")]
  private float dashCooldown;
  [SerializeField]
  [Tooltip("How fast/hard the player dashes")]
  private float dashForce;
  [SerializeField]
  private string dashingAnimationParameterName;
  public UnityEvent startDashing;
  public UnityEvent endDashing;
  private Animator animator;
  private Rigidbody2D rb2d;
  private Vector2 direction;
    private Vector2 previousPosition;
  private Coroutine dashingCoroutine;

  private void Start()
  {
    this.SetComponents();
    this.Setup();
  }

  private void Update()
  {
    this.SetDirection();
  }

  private void SetComponents()
  {
    this.animator = GetComponent<Animator>();
    this.rb2d = GetComponent<Rigidbody2D>();
  }

  private void Setup()
  {
    this.previousPosition = this.transform.position;
  }

  /// <summary>
  /// Calculates the current movement direction, to know here to dash
  /// </summary>
  private void SetDirection()
  {
    this.direction = ((Vector2)this.transform.position - this.previousPosition).normalized;
    this.previousPosition = this.transform.position;
  }

  public void StartDashing()
  {
    if (this.dashingCoroutine == null && this.direction != Vector2.zero)
    {
      if (this.dashingAnimationParameterName != string.Empty)
      {
        this.animator.SetBool(this.dashingAnimationParameterName, true);
      }

      StartCoroutine(this.WaitAFrame());

      this.dashingCoroutine = StartCoroutine(this.Dash());
    }
  }

  /// <summary>
  /// Helper method to wait a frame
  /// TODO: Move this to a helper class, because of other components have similar method
  /// </summary>
  /// <returns></returns>
  private IEnumerator WaitAFrame()
  {
    yield return new WaitForFixedUpdate();

    if (this.startDashing != null)
    {
      this.startDashing.Invoke();
    }
  }

  /// <summary>
  /// Set dash cooldown
  /// </summary>
  /// <returns></returns>
  private IEnumerator ActivateDash()
  {
    yield return new WaitForSeconds(this.dashCooldown);

    this.dashingCoroutine = null;
  }

  /// <summary>
  /// Dashing, with the help of rigitbody, also sets the animation
  /// </summary>
  /// <returns></returns>
  private IEnumerator Dash()
  {
    float timer = this.dashTimer;

    while (timer > 0)
    {
      Vector3 movingPosition = transform.position + (Vector3)(this.direction * this.dashForce * Time.fixedDeltaTime);
      this.rb2d.MovePosition(movingPosition);
      timer -= Time.fixedDeltaTime;

      yield return new WaitForFixedUpdate();
    }

    if (this.dashingAnimationParameterName != string.Empty)
    {
      this.animator.SetBool(this.dashingAnimationParameterName, false);
    }

    if (this.endDashing != null)
    {
      this.endDashing.Invoke();
    }

    StartCoroutine(this.ActivateDash());
  }
}
