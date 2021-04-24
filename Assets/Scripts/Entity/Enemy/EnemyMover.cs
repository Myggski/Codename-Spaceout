using UnityEngine;

/// <summary>
/// Moves to target position
/// </summary>
public class EnemyMover : Mover
{
  [SerializeField]
  private AnimationClip idleAnimation;
  [SerializeField]
  private AnimationClip runningAnimation;

  private Animator animator;

  protected override void SetMoreComponents()
  {
    this.animator = GetComponent<Animator>();
  }

  protected void Run(Vector3 targetPosition)
  {
    if (this.runningAnimation != null && this.animator != null && !this.animator.GetCurrentAnimatorStateInfo(0).IsName(this.runningAnimation.name))
    {
      this.animator.Play(this.runningAnimation.name);
    }

    MoveTowards(targetPosition);
  }

  protected void Idle()
  {
    this.animator.Play(this.idleAnimation.name);
  }

  private void OnDrawGizmos()
  {
    if (NodeGrid.Instance != null)
    {
      Node node = NodeGrid.Instance.NodeFromWorldPoint(transform.position);
      if (node != null)
      {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(node.WorldPosition, new Vector3(0.8f, 0.8f, 0));
      }
    }
  }
}