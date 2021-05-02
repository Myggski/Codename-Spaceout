using UnityEngine;
using System;
using System.Collections.Generic;

public class Pathfinding : Singleton<Pathfinding>
{

  /// <summary>
  /// Finds the shortest path with help of nodes, and according to the A-star algorithm
  /// </summary>
  /// <param name="request"></param>
  /// <param name="callback"></param>
  public void FindPath(PathRequest request, Action<PathResult> callback)
  {
    NodeGrid nodeGrid = NodeGrid.Instance;
    Vector3[] waypoints = new Vector3[0];
    bool pathFound = false;

    Node startNode = nodeGrid.NodeFromWorldPoint(request.pathStart);
    Node targetNode = nodeGrid.NodeFromWorldPoint(request.pathEnd);

    if (startNode == null || targetNode == null)
    {
      callback(new PathResult(new Vector3[0], false, request.callback));
    }
    else if (startNode.walkable && targetNode.walkable)
    {
      Heap<Node> openSet = new Heap<Node>(nodeGrid.MaxSize);
      HashSet<Node> closedSet = new HashSet<Node>();
      openSet.Add(startNode);

      while (openSet.Count > 0)
      {
        Node activeNode = openSet.RemoveFirst();
        closedSet.Add(activeNode);

        if (activeNode == targetNode)
        {
          pathFound = true;
          break;
        }

        foreach (Node neighbour in nodeGrid.GetNeighbours(activeNode))
        {
          if (!neighbour.walkable || closedSet.Contains(neighbour))
          {
            continue;
          }

          int newCostToNeighbour = activeNode.gCost + GetDistance(activeNode, neighbour);
          if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
          {
            neighbour.gCost = newCostToNeighbour;
            neighbour.hCost = GetDistance(neighbour, targetNode);
            neighbour.parent = activeNode;

            if (!openSet.Contains(neighbour))
            {
              openSet.Add(neighbour);
            }
            else
            {
              openSet.UpdateItem(neighbour);
            }
          }
        }
      }

      if (pathFound)
      {
        waypoints = RetracePath(startNode, targetNode);
        pathFound = waypoints.Length > 0;
      }

      callback(new PathResult(waypoints, pathFound, request.callback));
    }
  }

  /// <summary>
  /// Retracing the path starting from the goal, and back to the start
  /// </summary>
  /// <param name="startNode">Where to start</param>
  /// <param name="targetNode">Where to end</param>
  /// <returns></returns>
  private Vector3[] RetracePath(Node startNode, Node targetNode)
  {
    List<Node> path = new List<Node>();
    Node currentNode = targetNode;

    while (currentNode != startNode)
    {
      path.Add(currentNode);
      currentNode = currentNode.parent;
    }
    Vector3[] waypoints = SimplifyPath(path);
    Array.Reverse(waypoints);

    return waypoints;
  }

  /// <summary>
  // Removes the overflow of nodes, and keeps the ones where the direction changes
  /// </summary>
  /// <param name="path"></param>
  /// <returns></returns>
  private Vector3[] SimplifyPath(List<Node> path)
  {
    List<Vector3> waypoints = new List<Vector3>();
    Vector2 directionOld = Vector2.zero;

    for (int i = 1; i < path.Count; i++)
    {
      Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
      if (directionNew != directionOld)
      {
        waypoints.Add(path[i - 1].WorldPosition);
      }
      directionOld = directionNew;
    }

    return waypoints.ToArray();
  }

  /// <summary>
  /// Get distance between nodes
  /// It's more expensive to walk diagonal, therefore its set to 14
  /// </summary>
  /// <param name="nodeA"></param>
  /// <param name="nodeB"></param>
  /// <returns></returns>
  private int GetDistance(Node nodeA, Node nodeB)
  {
    int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

    if (dstX > dstY)
      return 14 * dstY + 10 * (dstX - dstY);
    return 14 * dstX + 10 * (dstY - dstX);
  }
}
