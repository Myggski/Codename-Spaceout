using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
  public bool walkable;
  private Vector3 worldPosition;
  public int gridX;
  public int gridY;

  /// <summary>
  /// No node equal start position
  /// is just what the previous "checkpoint" was
  /// </summary>
  public Node parent;

  /// <summary>
  /// G is the distance between the current node and the start node.
  /// vertical / horizontal = 10, and diagonal = 14
  /// For example, hard terrain can be 18
  /// </summary>
  public int gCost;

  /// <summary>
  /// H is the heuristic — estimated distance from the current node to the end node.
  /// vertical / horizontal = 10, and diagonal = 14
  /// </summary>
  public int hCost;

  /// <summary>
  /// F is the total cost of the node.
  /// If the hCost are the same between two nodes, compare fCost
  /// </summary>
  /// <value></value>
  public int fCost
  {
    get => gCost + hCost;
  }

  /// <value></value>
  public int HeapIndex
  {
    get;
    set;
  }

  public Vector3 WorldPosition
  {
    get => this.worldPosition;
  }

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="walkable">Is the node / tile walkable</param>
  /// <param name="worldPosition">Position of the node</param>
  /// <param name="gridX"></param>
  /// <param name="gridY"></param>
  public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
  {
    this.walkable = walkable;
    this.worldPosition = worldPosition;
    this.gridX = gridX;
    this.gridY = gridY;
  }

  /// <summary>
  /// Compare cost with other nodes
  /// </summary>
  /// <param name="nodeToCompare">The other node to compare</param>
  /// <returns></returns>
  public int CompareTo(Node nodeToCompare)
  {
    int compare = fCost.CompareTo(nodeToCompare.fCost);
    if (compare == 0)
    {
      compare = hCost.CompareTo(nodeToCompare.hCost);
    }

    return -compare;
  }
}
