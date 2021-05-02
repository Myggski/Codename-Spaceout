using UnityEngine;
using System.Collections;
public class EnemyUnit : EnemyMover
{
  private GameObject target;
  private Vector3[] path;
  private Vector3 prevoiusPosition;
  private int targetIndex;
  private float pathFindingDelay;
  private Coroutine pathfindingCoutine;
  private bool disabledWalk;

  private int numberOfTimesSamePosition;

  private void Update()
  {
    if (pathFindingDelay > 1.25f)
    {
      this.FindPath();
      this.pathFindingDelay = 0f;
    }
    else
    {
      this.pathFindingDelay += Time.deltaTime;
    }
  }

  private void FindPath()
  {
    if (this.target == null)
    {
      this.FindTarget();
    }

    if (this.target != null && !this.disabledWalk)
    {
      PathRequestManager.Instance.RequestPath(new PathRequest(this.transform.position, this.target.transform.position, this.OnPathFound));
    }
    else
    {
      this.Idle();
    }
  }

  private void FindTarget()
  {
    target = GameObject.FindGameObjectWithTag("Player");
  }

  public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
  {
    if (pathSuccessful)
    {
      path = newPath;

      if (pathfindingCoutine != null)
      {
        StopCoroutine(this.pathfindingCoutine);
      }

      this.pathfindingCoutine = StartCoroutine(this.FollowPath());
    }
  }

  IEnumerator FollowPath()
  {
    Vector3 currentWaypoint = path[0];
    targetIndex = 0;

    while (true)
    {
      if (transform.position == currentWaypoint)
      {
        targetIndex++;

        if (targetIndex >= path.Length)
        {
          this.Idle();
          yield break;
        }

        currentWaypoint = path[targetIndex];
      }

      if (!target.activeSelf)
      {
        this.Idle();
        yield break;
      }

      this.Run(currentWaypoint);
      this.prevoiusPosition = this.transform.position;

      yield return null;
    }
  }

  public void StopFollow()
  {
    StopCoroutine(this.pathfindingCoutine);
    this.disabledWalk = true;
  }

  private void OnDrawGizmos()
  {
    if (path != null && path.Length > 0)
    {
      for (int i = targetIndex; i < path.Length; i++)
      {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(path[i], Vector3.one / 8);

        if (i == targetIndex)
        {
          Gizmos.DrawLine(transform.position, path[i]);
        }
        else
        {
          Gizmos.DrawLine(path[i], path[i]);
        }
      }
    }
  }
}