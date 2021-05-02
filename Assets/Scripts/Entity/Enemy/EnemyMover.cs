using UnityEngine;

/// <summary>
/// Moves to target position
/// </summary>
public class EnemyMover : Mover
{
  private Animator animator;

  private void Update()
  {
    if (!this.rb2d.isKinematic)
    {
      this.rb2d.velocity = Vector2.zero;
    }
  }

  protected override void SetMoreComponents()
  {
    this.animator = GetComponent<Animator>();
  }

  protected void Run(Vector3 targetPosition)
  {
    if (this.animator != null && !this.animator.GetBool("Running"))
    {
      this.animator.SetBool("Running", true);
      this.rb2d.isKinematic = false;
    }

    MoveTowards(targetPosition);
  }

  protected void Idle()
  {
    if (this.animator.GetBool("Running"))
    {
      this.animator.SetBool("Running", false);
      this.rb2d.velocity = Vector2.zero;
      this.rb2d.isKinematic = true;
    }
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