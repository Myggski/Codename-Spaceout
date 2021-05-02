using UnityEngine;
using System.Collections;
using System;

public class PlayerMover : Mover
{
  [SerializeField]
  private string runningAnimationParameterName;
  private Animator animator;
  private bool isMovingUp;
  private bool isMovingDown;
  private bool isMovingLeft;
  private bool isMovingRight;
  private Vector2 previousPosition;
  private Coroutine resetMovingRightCorutine;
  private Coroutine resetMovingLeftCorutine;
  private Coroutine resetMovingUpCorutine;
  private Coroutine resetMovingDownCorutine;

  private void FixedUpdate()
  {
    this.MovePlayer(this.isMovingUp, this.isMovingDown, this.isMovingLeft, this.isMovingRight);
  }

  protected override void SetMoreComponents()
  {
    this.animator = GetComponent<Animator>();
  }

  /// <summary>
  /// Move player depending on input
  /// Up and down at the same time take out each other
  /// Left or right at the same time take out each other
  /// You either walk, or not. 
  /// Can become a problem to joysticks because they're returning a float value.
  /// </summary>
  /// <param name="up">Is moving up</param>
  /// <param name="down">Is moving down</param>
  /// <param name="right">Is moving right</param>
  /// <param name="left">Is moving left</param>
  public void MovePlayer(bool up, bool down, bool left, bool right)
  {
    int vertical = (up ? 1 : 0) - (down ? 1 : 0);
    int horizontal = (right ? 1 : 0) - (left ? 1 : 0);

    if (vertical != 0 || horizontal != 0)
    {
      this.Move(new Vector3(horizontal, vertical).normalized);

      if (this.IsStandingStill())
      {
        this.IdleAnimation();
      }
      else
      {
        this.RunAnimation();
      }

      this.previousPosition = this.rb2d.position;
    }
    else
    {
      this.IdleAnimation();
    }
  }

  public void MoveUp()
  {
    this.SetMovement(ref this.resetMovingUpCorutine, ref this.isMovingUp, delegate { this.isMovingUp = false; });
  }

  public void MoveDown()
  {
    this.SetMovement(ref this.resetMovingDownCorutine, ref this.isMovingDown, delegate { this.isMovingDown = false; });
  }

  public void MoveLeft()
  {
    this.SetMovement(ref this.resetMovingLeftCorutine, ref this.isMovingLeft, delegate { this.isMovingLeft = false; });
  }

  public void MoveRight()
  {
    this.SetMovement(ref this.resetMovingRightCorutine, ref this.isMovingRight, delegate { this.isMovingRight = false; });
  }

  private void SetMovement(ref Coroutine coroutine, ref bool movement, Action resetMovementCallback)
  {
    movement = true;

    if (coroutine != null)
    {
      StopCoroutine(coroutine);
    }

    this.ResetMovement(ref coroutine, resetMovementCallback);
  }

  private void ResetMovement(ref Coroutine resetMovementCorotine, Action resetMovement)
  {
    resetMovementCorotine = this.StartCoroutine(this.WaitForTwoFrames(resetMovement));
  }

  private IEnumerator WaitForTwoFrames(Action resetMovementCallback)
  {
    float numberOfFrames = 2;

    while (numberOfFrames > 0)
    {
      numberOfFrames--;
      yield return null;
    }

    resetMovementCallback.Invoke();
  }

  private void IdleAnimation()
  {
    if (!this.runningAnimationParameterName.Equals(null) && this.animator.GetBool(this.runningAnimationParameterName))
    {
      this.animator.SetBool(this.runningAnimationParameterName, false);
    }
  }

  private void RunAnimation()
  {
    if (!this.runningAnimationParameterName.Equals(null) && !this.animator.GetBool(this.runningAnimationParameterName))
    {
      this.animator.SetBool(this.runningAnimationParameterName, true);
    }
  }

  private bool IsStandingStill()
  {
    return (this.rb2d.position - this.previousPosition).normalized.Equals(Vector2.zero);
  }
}