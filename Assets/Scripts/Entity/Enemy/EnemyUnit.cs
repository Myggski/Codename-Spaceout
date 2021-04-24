using UnityEngine;
using System.Collections;
public class EnemyUnit : EnemyMover
{
  public Transform target;
  Vector3[] path;
  int targetIndex;

  void Update()
  {
    /*if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      PathRequestManager.Instance.RequestPath(new PathRequest(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), OnPathFound));
    }*/
  }

  public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
  {
    if (pathSuccessful)
    {
      path = newPath;
      StopCoroutine("FollowPath");
      StartCoroutine("FollowPath");
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

      this.Run(currentWaypoint);

      yield return null;
    }
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