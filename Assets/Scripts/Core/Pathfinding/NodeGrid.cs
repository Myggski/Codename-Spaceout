using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGrid : Singleton<NodeGrid>
{
  public Tilemap obstacleTilemap;
  public float nodeRadius;

  public int MaxSize => obstacleTilemap.cellBounds.size.x * obstacleTilemap.cellBounds.size.y;

  private Node[,] nodeGrid;
  private float nodeDiameter;
  private int gridSizeX;
  private int gridSizeY;
  private int maxSize;

  public Node[,] Grid => this.nodeGrid;

  private void Awake()
  {
    this.Setup();
  }

  /// <summary>
  /// Sets the grid-size and diameter of every node
  /// </summary>
  private void Setup()
  {
    this.gridSizeX = this.obstacleTilemap.size.x;
    this.gridSizeY = this.obstacleTilemap.size.y;
    this.nodeDiameter = this.nodeRadius * 2;
    this.nodeGrid = new Node[this.gridSizeX, gridSizeY];

    CreateGrid();
  }

  /// <summary>
  /// If the node is not in the obstacle-grid, then the node is walkable
  /// </summary>
  /// <param name="position"></param>
  /// <returns></returns>
  private bool isWalkable(Vector3Int position)
  {
    return !obstacleTilemap.HasTile(position);
  }

  private void CreateGrid()
  {
    Vector3Int worldBottomLeft = obstacleTilemap.cellBounds.min;

    for (int x = 0; x < gridSizeX; x++)
    {
      for (int y = 0; y < gridSizeY; y++)
      {
        Vector3 gridPosition = worldBottomLeft + Vector3.right * (x + nodeRadius) + Vector3.up * (y + nodeRadius);
        Vector3Int worldPosition = Vector3Int.FloorToInt(gridPosition);

        this.nodeGrid[x, y] = new Node(isWalkable(worldPosition), gridPosition, x, y);
      }
    }
  }

  /// <summary>
  /// Converting world position to grid position, and returning the grid
  /// </summary>
  /// <param name="worldPosition">Coordinates in units</param>
  /// <returns></returns>
  public Node NodeFromWorldPoint(Vector3 worldPosition)
  {
    if (this.nodeGrid == null || this.nodeGrid.Length == 0)
    {
      return null;
    }

    Vector3Int worldBottomLeft = obstacleTilemap.cellBounds.min;
    Vector3Int position = Vector3Int.FloorToInt(worldPosition) - worldBottomLeft;

    if (position.x < 0 || position.x > this.gridSizeX - 1 || position.y < 0 || position.y > this.gridSizeY - 1)
    {
      return null;
    }

    return this.nodeGrid[position.x, position.y];
  }

  /// <summary>
  /// Getting all the nodes around the current node
  /// </summary>
  /// <param name="node">The current node that is being checked out</param>
  /// <returns></returns>
  public List<Node> GetNeighbours(Node node)
  {
    List<Node> neighbours = new List<Node>();

    for (int x = -1; x <= 1; x++)
    {
      for (int y = -1; y <= 1; y++)
      {
        if (x == 0 && y == 0)
        {
          continue;
        }

        int checkX = node.gridX + x;
        int checkY = node.gridY + y;

        if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
        {
          neighbours.Add(this.nodeGrid[checkX, checkY]);
        }
      }
    }

    return neighbours;
  }

  /// <summary>
  /// Drawing the walkable path and the obstacles
  /// Walkable = white
  /// Obstacles = red
  /// </summary>
  private void OnDrawGizmos()
  {
    if (NodeGrid.Instance != null && NodeGrid.Instance.Grid != null && NodeGrid.Instance.Grid.Length != 0)
    {
      foreach (Node node in this.Grid)
      {
        float radius = this.nodeRadius;
        Gizmos.color = (node.walkable) ? Color.white : Color.red;
        Gizmos.DrawCube(new Vector3(node.WorldPosition.x, node.WorldPosition.y, 0), Vector3.one * ((radius * 2) - .1f));
      }
    }
  }
}
